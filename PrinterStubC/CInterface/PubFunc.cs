using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;
using PrinterStubC.CInterface;
using PrinterStubC.Common;
using PrinterStubC.Properties;
using PrinterStubC.Utility;
using System.Security.Cryptography;

namespace BYHXPrinterManager
{
    public class PubFunc
    {
        /// <summary>
        /// 判断当前配置是否为AWB模式
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
	    public static bool IsAwBMode(LayoutSetting param)
	    {
            int layercount = param.layerNum;
            bool haveColor = false;
            bool haveColor2 = false;
            uint layenable = param.nLayer;
            bool[] isLayersEnable = new bool[layercount];
            
            for (int i = 0; i < layercount; i++)//层数
            {
                if ((layenable>>i & 0x1)==0) continue;
                int sublayercount = param.layerSetting[i].subLayerNum;//子层数
                ushort datasource = param.layerSetting[i].nlayersource;
                //if (datasource == 0) haveColor = true;
                //else if (datasource == 1 || datasource == 2) haveColor2 = true;
                for (int j = 0; j < sublayercount; j++)
                {
                    int data = ((datasource >> j * 2) & 0x3);

                    if (data == 0) haveColor = true;
                    else if (data == 1 || data == 2) haveColor2 = true;
                }
            }
            return haveColor && haveColor2;
            //    int layercount = param.PrinterSetting.sBaseSetting.nWhiteInkLayer == 0 ? 1 : (int)param.PrinterSetting.sBaseSetting.nWhiteInkLayer;
            //bool haveColor = false;
            //bool haveColor2 = false;
            //for (int i = 0; i < layercount; i++)
            //{
            //    uint layercolor = (param.PrinterSetting.sBaseSetting.nLayerColorArray >> (2 * i)) & 0x03;
            //    switch ((EnumLayerType)layercolor)
            //    {
            //        case EnumLayerType.Color:
            //            haveColor = true;
            //            break;
            //        case EnumLayerType.White:
            //            //tb.Rows.Add(new Row(new Cell[] { new Cell(layer + " " + (i + 1) + ":"), new Cell(White) }));
            //            break;
            //        case EnumLayerType.Varnish:
            //            //tb.Rows.Add(new Row(new Cell[] { new Cell(layer + " " + (i + 1) + ":"), new Cell(Varnish) }));
            //            break;
            //        case EnumLayerType.Color2:
            //            haveColor2 = true;
            //            break;
            //    }
            //}
            //return haveColor && haveColor2;
        }
        /// <summary>
        /// 获取托管dll或exe编译时间
        /// </summary>
        /// <returns>编译时间</returns>
        public static DateTime GetCompileTime()
        {
            string strFile = Assembly.GetEntryAssembly().Location;
            using (var br = new BinaryReader(new FileStream(strFile, FileMode.Open, FileAccess.Read)))
            {
                br.BaseStream.Seek(0x80, SeekOrigin.Begin);
                byte[] bs = br.ReadBytes(4);
                if (bs[0] == 'P' && bs[1] == 'E' && bs[2] == 0 && bs[3] == 0)
                {
                    br.ReadBytes(4);
                    int sec = br.ReadInt32();
                    return DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc).AddSeconds(sec).ToLocalTime();
                }
            }
            return new DateTime(1970, 1, 1);
        }

        /// <summary>
        /// 从厂商配置文件获取默认界面显示模式
        /// </summary>
        /// <returns></returns>
        public static UIViewMode GetDefaultViewMode()
        {
            string splashEnable = string.Empty;
            string NamePath = Application.StartupPath;
            NamePath += "\\setup\\Vender.xml";
            if (File.Exists(NamePath))
            {
                SelfcheckXmlDocument xmldoc = new SelfcheckXmlDocument();
                xmldoc.Load(NamePath);
                XmlElement node = xmldoc.DocumentElement;

                XmlNodeList list = node.GetElementsByTagName("UIViewMode");
                if (list != null && list.Count >= 1)
                {
                    splashEnable = list[0].InnerXml.Trim().ToLower();
                }
            }
            UIViewMode mode = UIViewMode.TopDown;
            if (UIViewMode.TryParse(splashEnable, true, out mode))
                return mode;
            return mode;
        }

        /// <summary>
        /// 初始化墨水曲线数据包信息数据
        /// </summary>
        public static bool GetCurInkType(ref byte SelectedInkType)
        {
            #region 数据解析格式
            /*
             *数据解析格式，按照如下伪代码
             *  typedef struct INK_NAME_INFO_tag{
	                INT8U num;//墨水类型个数
	                INT8U rev[3];//4字节对齐
	                INT8U version[12];//数据包版本
	                INT8U name1[8];//墨水类型1名称
	                INT8U name2[8];//墨水类型2名称
	                …              
                    INT8U namen[8];//墨水类型n名称
                }
             * 
             *  req = 0x68;
                index = 0x02
                收发，数据长度1，当前选中组号
                index = 0x03
                发，数据长度，包信息

             */

            #endregion
            byte[] data = new byte[3];
            uint dataSize = (uint)data.Length;
            int ret1 = CoreInterface.GetEpsonEP0Cmd(0x68, data, ref dataSize, 0, 0x02);
            if (ret1 != 0)
            {
                if (dataSize == 3)
                {
                    SelectedInkType = data[2];
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 切换打印平台校准参数
        /// </summary>
        /// <param name="m_allParma"></param>
        /// <param name="oldPlatform"></param>
        /// <param name="selectedPlatform"></param>
        /// <param name="IsCali">是否为校准</param>
        /// <returns></returns>
        public static int SwitchPrintPlatform(AllParam m_allParma, byte oldPlatform, byte selectedPlatform)
        {
            //if (oldPlatform != selectedPlatform)
            //    m_allParma.SaveToXml(InkTypeSettingFileNameMake(oldPlatform), false); //先保存原来的配置信息
            //string fullFileName = InkTypeSettingFileNameMake(selectedPlatform);
            //if (File.Exists(fullFileName))
            //{
            //    AllParam temp = new AllParam();
            //    temp.LoadFromXml(fullFileName, false);
            //    m_allParma.PrinterSetting.sCalibrationSetting = temp.PrinterSetting.sCalibrationSetting;
            //    m_allParma.NewCalibrationHorizonArray = temp.NewCalibrationHorizonArray;
            //}
            m_allParma.PrinterSetting.sBaseSetting.fYOrigin = selectedPlatform == CoreConst.AXIS_X ? m_allParma.ExtendedSettings.fYOrigin1 : m_allParma.ExtendedSettings.fYOrigin2;
            return 1;
        }
        /// <summary>
        /// 保存当前打印平台校准参数
        /// </summary>
        /// <param name="allParma"></param>
        /// <param name="curPlatform"></param>
        /// <returns></returns>
        public static int SavePlatformCariParas(AllParam allParma, byte curPlatform)
        {
            allParma.SaveToXml(InkTypeSettingFileNameMake(curPlatform), false); //先保存原来的配置信息
            return 1;
        }
        /// <summary>
        /// 切换墨水类型
        /// </summary>
        /// <param name="m_allParma"></param>
        /// <param name="oldInkType"></param>
        /// <param name="selectedInkType"></param>
        /// <returns></returns>
        public static int SwitchInkType(AllParam m_allParma,byte oldInkType, byte selectedInkType)
        {
            if (oldInkType != selectedInkType)
                m_allParma.SaveToXml(InkTypeSettingFileNameMake(oldInkType), false);//先保存原来的配置信息
            byte[] data = new byte[] { selectedInkType };
            uint dataSize = (uint)data.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x68, data, ref dataSize, 0, 0x02);
            if (ret == 0)
            {
                //MessageBox.Show(string.Format("SetEpsonEP0Cmd Error!cmd {0},index {1}", 0x68, 0x02));
                return -1;
            }
            int baseData = Convert.ToInt32("B8220000", 16);
            byte[] buffer = BitConverter.GetBytes((baseData | selectedInkType));
            if (CoreInterface.UpdatePrinterSetting((int) PrinterSettingCmd.HeadChannelSwitch, buffer, 4, 0, 0) != 0)
            {
                string fullFileName = InkTypeSettingFileNameMake(selectedInkType);
                if (File.Exists(fullFileName))
                {
                    AllParam temp = new AllParam();
                    temp.LoadFromXml(fullFileName, false);
                    m_allParma.PrinterSetting.sCalibrationSetting = temp.PrinterSetting.sCalibrationSetting;
                    m_allParma.NewCalibrationHorizonArray = temp.NewCalibrationHorizonArray;
                }
                return 1;
            }
            return -2;
        }

        /// <summary>
        /// 根据当前选择的墨水类型生成包含完整路径的配置文件名
        /// </summary>
        /// <returns>返回包含完整路径的配置文件名</returns>
        private static string InkTypeSettingFileNameMake(byte selectedInkType)
        {
            return System.IO.Path.Combine(Application.StartupPath, string.Format("setting_group{0}.xml", selectedInkType));
        }

        /// <summary>
        /// 获取有效的ColorDeep,成功返回实际值，失败返回-1.
        /// </summary>
        /// <returns></returns>
        public static int GetColorDeep()
        {
            int colorDeep = -1;
            byte[] val = new byte[64];
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x81, val, ref bufsize, 0, 0x4000);
            if (ret != 0)
            {                   
                colorDeep = val[0];
            }

            return colorDeep;
        }


        /// <summary>
        /// 获取根据打印机属性变换后的运动方向
        /// </summary>
        /// <param name="dir">界面按钮指示方向</param>
        /// <param name="property">打印机属性</param>
        /// <param name="preference">使用运动方向相关的个性设置，默认不使用</param>
        /// <returns></returns>
        public static MoveDirectionEnum GetRealMoveDir(MoveDirectionEnum dir,SPrinterProperty property,UIPreference preference=null)
        {
            #region 个性设置启用“左右运动反向”
            if (null != preference && preference.ReverseHoriMoveDirection)
            {
                if (dir == MoveDirectionEnum.Left)
                {
                    dir = MoveDirectionEnum.Right;
                }
                else if (dir == MoveDirectionEnum.Right)
                {
                    dir = MoveDirectionEnum.Left;
                }
            }
            if (null != preference && preference.ReverseVertMoveDirection)
            { 
                if (dir == MoveDirectionEnum.Up)
                {
                    dir = MoveDirectionEnum.Down;
                }
                else if (dir == MoveDirectionEnum.Down)
                {
                    dir = MoveDirectionEnum.Up;
                }
            }
            if (null != preference && preference.ReverseZMoveDirection)
            {
                if (dir == MoveDirectionEnum.Down_Z)
                {
                    dir = MoveDirectionEnum.Up_Z;
                }
                else if (dir == MoveDirectionEnum.Up_Z)
                {
                    dir = MoveDirectionEnum.Down_Z;
                }
            }
            #endregion
            MoveDirectionEnum ret = dir;
            if (property.nMediaType == 2 && dir == MoveDirectionEnum.Up && null != preference && !preference.ReverseVertMoveDirection)
                ret = MoveDirectionEnum.Down;
            else if (property.nMediaType == 2 && dir == MoveDirectionEnum.Down && null != preference && !preference.ReverseVertMoveDirection)
                ret = MoveDirectionEnum.Up;
#if DOUBLE_SIDE_PRINT_HAPOND// hapond 双面喷临时要求调转前后左右移动按钮方向
            switch (dir)
            {
                case MoveDirectionEnum.Left:
                    ret = MoveDirectionEnum.Right;
                    break;
                case MoveDirectionEnum.Right:
                    ret = MoveDirectionEnum.Left;
                    break;
                case MoveDirectionEnum.Up:
                    ret = MoveDirectionEnum.Down;
                    break;
                case MoveDirectionEnum.Down:
                    ret = MoveDirectionEnum.Up;
                    break;
            }
#endif
            // 彩神平台二的运动映射
            if (SPrinterProperty.IsFloraT50OrT180()
                && null != preference && preference.ScanningAxis == CoreConst.AXIS_4)
            {
                if (ret == MoveDirectionEnum.Left)
                    ret = MoveDirectionEnum.Up_4;
                if (ret == MoveDirectionEnum.Right)
                    ret = MoveDirectionEnum.Down_4;
            }
            // 卓展平台二的运动映射
            if (PubFunc.IsZhuoZhan()
          && null != preference && preference.ScanningAxis == CoreConst.AXIS_4)
            {
                if (ret == MoveDirectionEnum.Up)
                    ret = MoveDirectionEnum.Down_4;
                if (ret == MoveDirectionEnum.Down)
                    ret = MoveDirectionEnum.Up_4;
            }
            //碧宏T恤机xy轴翻转
            if (UIFunctionOnOff.SwapXwithY)
            {
                if (ret == MoveDirectionEnum.Left)
                    return MoveDirectionEnum.Up;
                if (ret == MoveDirectionEnum.Right)
                    return MoveDirectionEnum.Down;
                if (ret == MoveDirectionEnum.Up)
                    return MoveDirectionEnum.Left;
                if (ret == MoveDirectionEnum.Down)
                    return MoveDirectionEnum.Right;
            }
            return ret;
        }
        public static string GetVenderSettingPath()
        {
            string path = string.Empty;
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
			{
			    path = Path.Combine(Application.StartupPath, vid.ToString("X4"));
                path = Path.Combine(path, pid.ToString("X4"));
                path = Path.Combine(path, "user.bin");
            }
            return path;
        }
        /// <summary>
        /// 通知主板Pm已经打开
        /// </summary>
        public static void RegisteredToMainboard()
        {
            byte[] buf = new byte[16];
            uint bufsize = (uint) buf.Length;
            CoreInterface.GetEpsonEP0Cmd(0x20, buf, ref bufsize, 1, 0x27);
        }

        /// <summary>
        /// 通知主板pm关闭
        /// </summary>
        public static void UnregisteredToMainboard()
        {
            byte[] buf = new byte[16];
            uint bufsize = (uint)buf.Length;
            CoreInterface.GetEpsonEP0Cmd(0x20, buf, ref bufsize, 0, 0x27);
        }

        /// <summary>
        /// 获取指定头板支持的喷头数
        /// </summary>
        /// <param name="headBoardType">头板类型</param>
        /// <returns>喷头数</returns>
	    public static int GetHeadNumPerHeadborad(HEAD_BOARD_TYPE headBoardType)
	    {
	        int headnum = 0;
	        switch (headBoardType)
	        {
	            case HEAD_BOARD_TYPE.XAAR128_12HEAD:
	            case HEAD_BOARD_TYPE.KM256_12HEAD:
	                headnum = 12;
	                break;
	            case HEAD_BOARD_TYPE.KM512_8HEAD_8VOL:
	            case HEAD_BOARD_TYPE.KM512_8HEAD_16VOL:
	            case HEAD_BOARD_TYPE.KM256_8HEAD:
	            case HEAD_BOARD_TYPE.XAAR382_8HEAD:
	            case HEAD_BOARD_TYPE.NEW512_8HEAD:
	            case HEAD_BOARD_TYPE.KM1024_8HEAD:
	            case HEAD_BOARD_TYPE.KM512_8H_GRAY:
	            case HEAD_BOARD_TYPE.KM512_8H_WATER:
	            case HEAD_BOARD_TYPE.KM1024_8H_GRAY:
	            case HEAD_BOARD_TYPE.SPECTRA_BYHX_V5_8:
	            case HEAD_BOARD_TYPE.SPECTRA_POLARIS_8:
	            case HEAD_BOARD_TYPE._512OVER1024_8HEAD:
	            case HEAD_BOARD_TYPE.SG1024_8HEAD:
	            case HEAD_BOARD_TYPE.KM1024I_8H_GRAY:
	            case HEAD_BOARD_TYPE.SG1024_8H_GRAY_1BIT:
                case HEAD_BOARD_TYPE.SG1024_8H_GRAY_2BIT:
                case HEAD_BOARD_TYPE.SG1024_8H_BY100:
	            case HEAD_BOARD_TYPE.KM512I_8H_GRAY_WATER:
	            case HEAD_BOARD_TYPE.XAAR501_8H:
                case HEAD_BOARD_TYPE.GMA9905300_8H:
                case HEAD_BOARD_TYPE.GMA3305300_8H:
                case HEAD_BOARD_TYPE.EPSON_S1600_8H:
	                headnum = 8;
	                break;
	            case HEAD_BOARD_TYPE.KM512_6HEAD:
	            case HEAD_BOARD_TYPE.SPECTRA_POLARIS_6:
	                headnum = 6;
	                break;
	            case HEAD_BOARD_TYPE.KM512_16HEAD:
	            case HEAD_BOARD_TYPE.KM256_16HEAD:
	            case HEAD_BOARD_TYPE.XAAR128_16HEAD:
	            case HEAD_BOARD_TYPE.KM512_16HEAD_V2:
	            case HEAD_BOARD_TYPE.KM512_16H_WATER:
	            case HEAD_BOARD_TYPE.KM1024_16HEAD:
	            case HEAD_BOARD_TYPE.XAAR382_16HEAD:
	            case HEAD_BOARD_TYPE.POLARIS_16HEAD:
	            case HEAD_BOARD_TYPE.KM1024I_16H_GRAY:
	                headnum = 16;
	                break;
	            case HEAD_BOARD_TYPE.SP_EMERALD_04H:
	            case HEAD_BOARD_TYPE.SP_POLARIS_04H:
	            case HEAD_BOARD_TYPE.SPECTRA_POLARIS_4:
	            case HEAD_BOARD_TYPE.EPSON_GEN5_4HEAD:
	            case HEAD_BOARD_TYPE.KYOCERA_4HEAD:
	            case HEAD_BOARD_TYPE.KM512I_4H_GRAY_WATER:
	            case HEAD_BOARD_TYPE.SG1024_4H:
	            case HEAD_BOARD_TYPE.SG1024_4H_GRAY:
	            case HEAD_BOARD_TYPE.KM1024_4H:
                case HEAD_BOARD_TYPE.KM512_BANNER:
                case HEAD_BOARD_TYPE.SPECTRA_BYHX_V4:
                case HEAD_BOARD_TYPE.SPECTRA_BYHX_V5:
                case HEAD_BOARD_TYPE.EPSON_S2840_4H:
                    headnum = 4;
                    break;
                case HEAD_BOARD_TYPE.SP_KM1024_02H:
                case HEAD_BOARD_TYPE.SP_KM1024i_02H:
                case HEAD_BOARD_TYPE.SP_XAAR1001_02H:
                case HEAD_BOARD_TYPE.KM1800i_2H:
                    headnum = 2;
                    break;
                case HEAD_BOARD_TYPE.SP_XAAR1001_1H:
                case HEAD_BOARD_TYPE.SP_KM512_1H:
                case HEAD_BOARD_TYPE.SP_KM1024_1H:
                case HEAD_BOARD_TYPE.SP_KM1024i_1H:
                    headnum = 1;
                    break;
                case HEAD_BOARD_TYPE.SPECTRA:
                case HEAD_BOARD_TYPE.SPECTRA_256_GZ:
                default:
                    headnum = 8;
                    break;
            }
            return headnum;
        }

        /// <summary>
        /// 当speed是0-7时表示运动速度的索引。若大于7就表示HZ单位，慢速模式
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static bool ParseSeedString(string txt,out int speed,MoveDirectionEnum dir)
        {
            try
            {
                speed = int.Parse(txt);
                if (speed > 0 && speed <= 7)
                {
                    speed -= 1;
                    // x轴档位0-6与其他轴档位速度相反，此处取反
                    if (dir == MoveDirectionEnum.Left || dir == MoveDirectionEnum.Right)
                    {
                        speed = 6 - speed;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                speed = 0;
                return false;
            }
            return true;
        }
        public static string GetFullPreviewPath(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            const string m_PreviewFolder = "Preview";
            string path =Path.Combine(Application.StartupPath,m_PreviewFolder);
            return Path.Combine(path, name);
        }
        public static float GetHeadPlNum(PrinterHeadEnum head)
        {
            switch (head)
            {
                case PrinterHeadEnum.Xaar_126:
                    return 0;
                case PrinterHeadEnum.Xaar_XJ128_40W:
                    return 0;
                case PrinterHeadEnum.Xaar_XJ128_80W:
                    return 0;
                case PrinterHeadEnum.Xaar_500:
                    return 0;
                case PrinterHeadEnum.Spectra_S_128:
                    return 0;
                case PrinterHeadEnum.Spectra_NOVA_256:
                    return 0;
                case PrinterHeadEnum.Spectra_GALAXY_256:
                    return 0;
                case PrinterHeadEnum.Konica_KM512M_14pl:
                case PrinterHeadEnum.Konica_KM128M_14pl:
                case PrinterHeadEnum.Konica_KM256M_14pl:
                case PrinterHeadEnum.Konica_KM1024M_14pl:
                case PrinterHeadEnum.Konica_KM512MAX_14pl:
                    return 14;
                case PrinterHeadEnum.Konica_KM512L_42pl:
                case PrinterHeadEnum.Konica_KM256L_42pl:
                case PrinterHeadEnum.Konica_KM128L_42pl:
                case PrinterHeadEnum.Konica_KM1024L_42pl:
                    return 42;
                case PrinterHeadEnum.Xaar_Electron_35W:
                    return 0;
                case PrinterHeadEnum.Xaar_Electron_70W:
                    return 0;
                case PrinterHeadEnum.Xaar_1001_GS6:
                    return 6;
                //case PrinterHeadEnum.Xaar_1001_GS12:
                //    return 12;
                case PrinterHeadEnum.Spectra_Polaris_15pl:
                    return 15;
                case PrinterHeadEnum.Konica_KM512LNX_35pl:
                case PrinterHeadEnum.Spectra_Polaris_35pl:
                case PrinterHeadEnum.Xaar_Proton382_35pl:
                    return 35;
                case PrinterHeadEnum.EGen5:
                    return 0;
                case PrinterHeadEnum.Spectra_Polaris_80pl:
                case PrinterHeadEnum.Spectra_SG1024LA_80pl:
                    return 80;
                case PrinterHeadEnum.Xaar_Proton382_60pl:
                    return 60;
                case PrinterHeadEnum.Konica_KM512LAX_30pl:
                    return 30;
                case PrinterHeadEnum.Spectra_Emerald_10pl:
                    return 10;
                case PrinterHeadEnum.Spectra_Emerald_30pl:
                    return 30;
                case PrinterHeadEnum.Konica_KM512i_MHB_12pl:
                case PrinterHeadEnum.Spectra_SG1024SA_12pl:
                    return 12;
                case PrinterHeadEnum.Konica_KM1024i_LHE_30pl:
                case PrinterHeadEnum.Konica_KM512i_LHB_30pl:
                    return 30;
                case PrinterHeadEnum.Spectra_PolarisColor4_15pl:
                    return 15;
                case PrinterHeadEnum.Spectra_PolarisColor4_35pl:
                    return 35;
                case PrinterHeadEnum.Spectra_PolarisColor4_80pl:
                    return 80;
                case PrinterHeadEnum.Xaar_Proton382_15pl:
                    return 15;
                case PrinterHeadEnum.Konica_KM512i_MAB_C_15pl:
                    return 15;
                case PrinterHeadEnum.Konica_KM1024i_MHE_13pl:
                case PrinterHeadEnum.Konica_KM1024i_MAE_13pl:
                    return 13;
                case PrinterHeadEnum.Konica_KM1024i_SHE_6pl:
                case PrinterHeadEnum.Konica_KM1024S_6pl:
                case PrinterHeadEnum.Konica_KM3688_6pl:
                case PrinterHeadEnum.Konica_KM512i_SH_6pl:
                case PrinterHeadEnum.Konica_KM512i_SAB_6pl:
                    return 6;
                case PrinterHeadEnum.Spectra_SG1024MA_25pl:
                    return 30;
                case PrinterHeadEnum.Konica_KM1800i_3P5pl:
                    return 3.5f;
                case PrinterHeadEnum.Spectra_SG1024XSA_7pl:
                    return 7;
                case PrinterHeadEnum.Kyocera_KJ4B_0300_5pl_1h2c:
                case PrinterHeadEnum.Kyocera_KJ4A_0300_5pl_1h2c:
                case PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl:
                case PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl:
                    return 5;
                case PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl:
                    return 3;
                case PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl:
                    return 6;
				case PrinterHeadEnum.Kyocera_KJ4A_1200_1p5pl:
                case PrinterHeadEnum.Kyocera_KJ4B_1200_1p5pl:
                    return 1.5f;
                case PrinterHeadEnum.Konica_KM512_SH_4pl:
                    return 4f;
                case PrinterHeadEnum.Konica_KM1024A_6_26pl:
                    return 6f;
                case PrinterHeadEnum.Epson_5113:
                case PrinterHeadEnum.EPSON_S1600_RC_UV:
                case PrinterHeadEnum.EPSON_I3200:
                    return 5f;
                case PrinterHeadEnum.Ricoh_Gen6:
                    return 10f;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 获取单个喷头上喷孔排数
        /// </summary>
        /// <param name="headtype">喷头类型</param>
        /// <returns>单个喷头上喷孔排数</returns>
        public static int GetRealHeadNumPerHead(PrinterHeadEnum headtype)
        {
            switch (headtype)
            {
                case PrinterHeadEnum.Konica_KM1024M_14pl:
                case PrinterHeadEnum.Konica_KM1024L_42pl:
                case PrinterHeadEnum.Konica_KM1024S_6pl:
                case PrinterHeadEnum.Konica_KM3688_6pl:
                case PrinterHeadEnum.Xaar_1001_GS6:
                    //case PrinterHeadEnum.Xaar_1001_GS12:
                    return 2;
                case PrinterHeadEnum.Spectra_Polaris_15pl:
                case PrinterHeadEnum.Spectra_Polaris_80pl:
                case PrinterHeadEnum.Spectra_Polaris_35pl:
                case PrinterHeadEnum.Spectra_PolarisColor4_15pl:
                case PrinterHeadEnum.Spectra_PolarisColor4_80pl:
                case PrinterHeadEnum.Spectra_PolarisColor4_35pl:
                case PrinterHeadEnum.Konica_KM1024i_MHE_13pl:
                case PrinterHeadEnum.Konica_KM1024i_LHE_30pl:
                case PrinterHeadEnum.Konica_KM1024i_MAE_13pl:
                case PrinterHeadEnum.Konica_KM1024i_SHE_6pl:
                    return 4;
                case PrinterHeadEnum.Konica_KM1800i_3P5pl:
                    return 6;
                case PrinterHeadEnum.Spectra_SG1024MA_25pl:
                case PrinterHeadEnum.Spectra_SG1024SA_12pl:
                    return 8;
                case PrinterHeadEnum.Konica_KM512LAX_30pl:
                case PrinterHeadEnum.Konica_KM512LNX_35pl:
                case PrinterHeadEnum.Konica_KM512L_42pl:
                case PrinterHeadEnum.Konica_KM512MAX_14pl:
                case PrinterHeadEnum.Konica_KM512M_14pl:
                case  PrinterHeadEnum.Konica_KM512i_MHB_12pl:
                case PrinterHeadEnum.Konica_KM512i_LHB_30pl:
                case PrinterHeadEnum.Konica_KM512i_MAB_C_15pl:
                case PrinterHeadEnum.Konica_KM512i_SH_6pl:
                case PrinterHeadEnum.Konica_KM512i_SAB_6pl:
                case  PrinterHeadEnum.Konica_KM512i_LNB_30pl:
                    return 2;
                case PrinterHeadEnum.Kyocera_KJ4A_0300_5pl_1h2c:
                case PrinterHeadEnum.Kyocera_KJ4A_1200_1p5pl:
                case PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl:
                case PrinterHeadEnum.Kyocera_KJ4A_RH06:
                case PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl:
                case PrinterHeadEnum.Kyocera_KJ4B_0300_5pl_1h2c:
                case PrinterHeadEnum.Kyocera_KJ4B_1200_1p5pl:
                case PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl:
                case PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl:
                    return 16;
                default:
                    return 1;
            }
        }


        public static bool GetEnableSplash()
        {
            string splashEnable = string.Empty;
            string NamePath = Application.StartupPath;
            NamePath += "\\setup\\Vender.xml";
            if (File.Exists(NamePath))
            {
                SelfcheckXmlDocument xmldoc = new SelfcheckXmlDocument();
                xmldoc.Load(NamePath);
                XmlElement node = xmldoc.DocumentElement;

                XmlNodeList list = node.GetElementsByTagName("Splash");
                if (list != null && list.Count >= 1)
                {
                    splashEnable = list[0].InnerXml.Trim().ToLower();
                }
            }
            string imagePath = Path.Combine(Application.StartupPath, "setup\\splash.png");
            if (splashEnable == "true" && File.Exists(imagePath))
                return true;
            return false;
        }

        /// <summary>
        /// true:高级模式;false:精简模式
        /// </summary>
        public static bool SupportKingColorSimpleMode
        {
            get
            {
#if KINGCOLOR_SIMPLE_MODE
                return true;
#else
                return false;
#endif
            }
        }
        
        private static bool _bKingColorAdvancedMode = !SupportKingColorSimpleMode;

        /// <summary>
        /// true:高级模式;false:精简模式
        /// </summary>
        public static bool IsKingColorAdvancedMode
        {
            get { return _bKingColorAdvancedMode; }
            set { _bKingColorAdvancedMode = value; }
        }

        public static bool SupportDoubleSidePrint
        {
            get
            {
#if DOUBLE_SIDE_PRINT
                return true;
#else
                return UIFunctionOnOff.SupportDoublePrintCalibration;
#endif
            }
        }
        public static double ConvAngleToRadian(double a)
        {
            return a * Math.PI / (double)180;
        }
        public static string SystemConvertToXml(object o,Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer,o);
            string str = writer.ToString();
            writer.Close();
            //return str;
            SelfcheckXmlDocument tmpXmlDoc = new SelfcheckXmlDocument();
            tmpXmlDoc.LoadXml(str);
            XmlElement NewNode = (XmlElement)tmpXmlDoc.LastChild;
            NewNode.RemoveAllAttributes();
			
            string inner = NewNode.OuterXml;
            //tmpXmlDoc.Save("c:\\1.xml");
		
            return inner;
        }
        public static object SystemConvertFromXml(string xmlString,Type type)
        {	
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader reader = new StringReader(xmlString);
            object o = serializer.Deserialize(reader);
            reader.Close();	
            return o;
        }

        /// <summary>
        /// update user permission by factory.usr
        /// </summary>
        /// <returns></returns>
        public static int GetUserPermission()
        {
            try
            {
                string curFile = Application.StartupPath + Path.DirectorySeparatorChar + "Factory.usr";
                if (!File.Exists(curFile))
                {
                    //if (Settings.Default.DebugModeInvalidDate > DateTime.Now)
                    //{
                    //    return (int)Settings.Default.UserPermission;
                    //}
                    return (int)UserPermission.Operator;
                }
                FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                const int BUF_LEN = 4;
                int ret = 0;
                if (fs.Length >= BUF_LEN)
                {
                    byte[] buf = new byte[BUF_LEN];
                    fs.Read(buf, 0, BUF_LEN);
                    for (int i = 0; i < BUF_LEN; i++)
                    {
                        ret <<= 8;
                        ret += buf[i];
                    }
                    switch (ret)
                    {
                        case (int)UserPermission.Operator:
                        case (int)UserPermission.FactoryUser:
                        case (int)UserPermission.SupperUser:
                            {
                                break;
                            }
                        default:
                            {
                                ret = (int)UserPermission.FactoryUser;
                                break;
                            }
                    }
                }
                else
                {
                    ret = (int)UserPermission.FactoryUser;
                }
                fs.Close();
                Settings.Default.UserPermission = (UserPermission)ret;
                //Settings.Default.DebugModeInvalidDate = DateTime.Now.AddDays(3);
                //Settings.Default.Save();
                //File.Delete(curFile);
            }
            catch (Exception ex)
            {
                LogWriter.SaveOptionLog("GetUserPermission:" + ex.Message);
                Settings.Default.UserPermission = UserPermission.Operator;
                //Settings.Default.DebugModeInvalidDate = DateTime.Now.AddDays(3);
                //Settings.Default.Save();
            }
            return (int)Settings.Default.UserPermission;
        }
        /// <summary>
        /// 是否存在Factory.usr文件
        /// </summary>
        /// <returns></returns>
        public static bool IsFactoryUser()
        {
#if LIYUUSB
            FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            const int BUF_LEN = 4;
            byte[] buf = new byte[BUF_LEN];
            fs.Read(buf, 0, BUF_LEN);
            for (int i = 0; i < BUF_LEN; i++)
            {
                if (buf[i] != i)
                    return false;
            }
            return true;
#else
			return GetUserPermission() >= (int) UserPermission.FactoryUser;
#endif
        }
        public static XmlElement GetFirstChildByName( XmlElement root,string name)
        {
            XmlNode currNode = root.FirstChild;
            while(currNode != null)
            {
                if(currNode.Name == name)
                    return (XmlElement)currNode;
                currNode = currNode.NextSibling;
            }
            return null;
        }

        public static XmlElement GetSecondChildByName(XmlElement root, string name)
        {
            XmlNode currNode = root.FirstChild;
            int times = 0;
            while (currNode != null)
            {
                if (currNode.Name == name)
                {
                    times++;
                    if (times == 2)
                    {
                        return (XmlElement)currNode;
                    }
                }
                currNode = currNode.NextSibling;
            }
            return null;
        } 

        static public void AddNode(string name, object value, TreeNodeCollection nodes, FieldInfo info, Array parentArray, int index)
        {
            TreeNode node = null;
            if (value == null || nodes == null)
            {
                if (nodes == null)
                {
                    return;
                }
                node = new TreeNode(name + ": Null");
				
                if (info != null)
                {
                    ObjectContainer contain = new ObjectContainer();
                    contain.ParentArray = parentArray;
                    contain.Info = info;
                    contain.Index = index;
                    contain.ObjType = info.FieldType;
                    node.Tag = contain;
                }
				
                nodes.Add(node);
                return;
            }
            Type type = value.GetType();

            //node = new TreeNode(name + "(" + type.Name + ")");
            node = new TreeNode(name);

            if (type.IsPrimitive || type == System.String.Empty.GetType() || type.IsEnum)
            {
                node.Text += ": " + value.ToString();
            }
            else if(type.IsArray)
            {
                Array array = value as Array;
                node.Text += "[" + array.Length +  "]";

                int i = 0;
                foreach (object subobj in array)
                {
                    AddNode(name + "[" + i.ToString() + "]", subobj, node.Nodes, null, array, i);
                    i++;
                }
            }
            else// if (type.IsClass)
            {
                FieldInfo[] infos = type.GetFields();

                foreach (FieldInfo subinfo in infos)
                {
                    object sub = type.InvokeMember(subinfo.Name,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.GetField | BindingFlags.GetProperty,
                        null, value, new object [] {});

                    if (sub != null)
                        AddNode(subinfo.Name, sub, node.Nodes, subinfo, null, -1);
                    else
                        AddNode(subinfo.Name , sub, node.Nodes, subinfo, null, -1);
                    //AddNode(subinfo.Name + "(" + subinfo.FieldType.Name + ")", sub, node.Nodes, subinfo, null, -1);
                }
            }

            if (node != null)
            {
                ObjectContainer contain = new ObjectContainer();
                contain.ParentArray = parentArray;
                contain.Info = info;
                contain.Object = value;
                contain.Index = index;
                contain.ObjType = type;

                node.Tag = contain;
                nodes.Add(node);
            }
        }

        static public bool IconReload(ImageList il,string skinName)
        {
            try
            {
                string curPath = string.Empty;
                if (string.IsNullOrEmpty(skinName))
                {
                    curPath = Path.Combine(Application.StartupPath, "Icon");
                }
                else
                {
                    curPath = Path.Combine(
                        Path.Combine(
                            Path.Combine(Application.StartupPath,CoreConst.SkinForlderName),skinName), "Icon");
                }
                string dllFile = Path.Combine(Application.StartupPath,"IconLoadDy.dll");
                if(Directory.Exists(curPath)&& File.Exists(dllFile))
                {
                    Assembly assem = Assembly.LoadFrom(dllFile);
                    Type [] layoutType = assem.GetTypes();
                    // Get the type we want to use from the assembly
                    foreach(Type t in 	layoutType)
                    {
                        if( t.Name!= "IconLoadLib"  ) continue;
                        // Get the method we want to call from the type
                        MethodInfo CheckPath = t.GetMethod("CheckPath");
                        if (CheckPath == null) continue;
                        MethodInfo RenewImageList = t.GetMethod("RenewImageList");
                        if (RenewImageList == null) 	continue;

                        Object[] args = new Object[1];
                        args[0] = curPath;

                        if( !(bool)CheckPath.Invoke(null, args)) continue;
                        Object[] args1 = new Object[2];
                        args1[0] = il;
                        args1[1] = curPath;
                        if( !(bool)RenewImageList.Invoke(null, args1))
                            continue;
                        else
                            return true;
                    }
                }
            }
            catch(Exception)
            {
            }
            return false;
        }
		
        public static bool IsInDesignMode() 
        {
            bool returnFlag = false; 
#if DEBUG 
			if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) 
			{   
				returnFlag = true;
			} 
			else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV")) 
			{   
				returnFlag = true;
			} 
#endif
            return returnFlag; 
        } 
        public static bool IsCustomSpeedDisp(PrinterHeadEnum eHead)
        {
#if !LIYUUSB
            ushort pid,vid ;
            pid = vid = 0;
            CoreInterface.GetProductID(ref vid, ref pid);
            if(((vid == (ushort)VenderID.ALLWIN || vid == (ushort)VenderID.ALLWIN_FLAT_UV) && eHead == PrinterHeadEnum.Konica_KM512L_42pl)
               || SPrinterProperty.IsEpson(eHead)
                )
                return true;
            else
                return false;
#else
		    return false;
#endif
        }

        public static bool IsVender92()
        {
            try
            {
                ushort pid, vid;
                pid = vid = 0;
                CoreInterface.GetProductID(ref vid, ref pid);
                bool ret = false;
                //if(vid==0x92)

                ret = true;
                return ret;
            }		
            catch
            {
                return false;
            }
        }

        public static bool IsMicolorOrAllwin()
        {
            try
            {
                ushort pid, vid;
                pid = vid = 0;
                CoreInterface.GetProductID(ref vid, ref pid);
                bool ret = false;
#if !LIYUUSB
                if (vid == (ushort)VenderID.MICOLOR
                   || vid == (ushort)VenderID.MICOLOR_FLAT_UV
                   || vid == (ushort)VenderID.ALLWIN
                   || vid == (ushort)VenderID.ALLWIN_FLAT_UV)
                    ret = true;
#endif
				return ret;
			}		
			catch
			{
				return false;
			}
		}
        /// <summary>
        /// 是否为ColorJet roll 印花打印机
        /// </summary>
        /// <returns></returns>
        public static bool IsCOLORJET_ROLL_TEXTILE()
        {
            try
            {
                ushort pid, vid;
                pid = vid = 0;
                CoreInterface.GetProductID(ref vid, ref pid);
                bool ret = false;
#if !LIYUUSB
                if (vid == (ushort)VenderID.COLORJET_ROLL_TEXTILE)
                    ret = true;
#endif
                return ret;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 是否为ColorJet 印花打印机
        /// </summary>
        /// <returns></returns>
        public static bool IsColorJet_Belt_Textile()
        {
            try
            {
                ushort pid, vid;
                pid = vid = 0;
                CoreInterface.GetProductID(ref vid, ref pid);
                bool ret = false;
#if !LIYUUSB
                if (vid == (ushort)VenderID.COLORJET_BELT_TEXTILE
                    || vid == (ushort)VenderID.COLORJET_ROLL_TEXTILE
                    )
                    ret = true;
#endif
                return ret;
            }
            catch
            {
                return false;
            }
        }/// <summary>
        /// 是否为DocanTextile
        /// </summary>
        /// <returns></returns>
        public static bool IsDocan_Belt_Textile()
        {
            try
            {
                ushort pid, vid;
                pid = vid = 0;
                CoreInterface.GetProductID(ref vid, ref pid);
                bool ret = false;
#if !LIYUUSB
                if (vid == (ushort)VenderID.DOCAN_BELT_TEXTILE)
                    ret = true;
#endif
                return ret;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 是否为DOCAN_FLAT_UV
        /// </summary>
        /// <returns></returns>
        public static bool IsDOCAN_FLAT_UV()
        {
            try
            {
                ushort pid, vid;
                pid = vid = 0;
                CoreInterface.GetProductID(ref vid, ref pid);
                bool ret = false;
#if !LIYUUSB
                if (vid == (ushort)VenderID.DOCAN_FLAT_UV)
                    ret = true;
#endif
                return ret;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 是否为DocanTextile
        /// </summary>
        /// <returns></returns>
        public static bool IsDocan_Belt()
        {
            try
            {
                ushort pid, vid;
                pid = vid = 0;
                CoreInterface.GetProductID(ref vid, ref pid);
                bool ret = false;
#if !LIYUUSB
                if ( vid == (ushort)VenderID.DOCAN_BELT)
                    ret = true;
#endif
                return ret;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 惠丽彩
        /// </summary>
        /// <returns></returns>
        public static bool IsHuiLiCai()
        {
            try
            {
                ushort pid, vid;
                pid = vid = 0;
                CoreInterface.GetProductID(ref vid, ref pid);
                bool ret = false;
#if !LIYUUSB
                if (vid == (ushort)VenderID.HuiLiCai)
                    ret = true;
#endif
                return ret;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 普奇
        /// </summary>
        /// <returns></returns>
        public static bool IsPuQi()
        {
            try
            {
                ushort pid, vid;
                pid = vid = 0;
                CoreInterface.GetProductID(ref vid, ref pid);
                bool ret = false;
#if !LIYUUSB
                if ((vid == (ushort)VenderID.PuQi) || (vid == (ushort)VenderID.PuQi_ROLL_TEXTILE))
                    ret = true;
#endif
                return ret;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 上海博昊070F0300
        /// </summary>
        /// <returns></returns>
        public static bool IsBoHao()
        {
            try
            {
                ushort pid, vid;
                pid = vid = 0;
                int ret = CoreInterface.GetProductID(ref vid, ref pid);
                if (ret != 0)
                {
                    if (vid == 0x070F)
                        return true;
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 是否为奥威，为了界面布局
        /// </summary>
        /// <returns></returns>
        public static bool IsALLWIN_ROLL_TEXTILE()
	    {
            try
            {
                ushort pid, vid;
                pid = vid = 0;
                CoreInterface.GetProductID(ref vid, ref pid);
                bool ret = false;
#if !LIYUUSB
                if (vid == (ushort)VenderID.ALLWIN_ROLL_TEXTILE)
                    ret = true;
#endif
                return ret;
            }
            catch
            {
                return false;
            }
        }

        public static Image CreateThumbnailImage(Image miniature, Size imageSize, Color penColor)
        {
            try
            {
                Bitmap bmp = null;
                int imgWidth = imageSize.Width;
                int imgHeight = imageSize.Height;
                try
                {
                    bmp = (Bitmap)miniature;
                }
                catch
                {
                    bmp = new Bitmap(imgWidth, imgHeight); //If we cant load the image, create a blank one with ThumbSize
                }

                //imgWidth = (bmp == null || (bmp != null && bmp.Width > imgWidth)) ? imgWidth : bmp.Width;
                //imgHeight = (bmp == null || (bmp != null && bmp.Height > imgHeight)) ? imgHeight : bmp.Height;

                Bitmap retBmp = new Bitmap(imgWidth, imgHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Graphics grp = Graphics.FromImage(retBmp);


                int tnWidth = imgWidth, tnHeight = imgHeight;

                if (bmp != null && bmp.Width > bmp.Height)
                    tnHeight = Convert.ToInt32(((float)bmp.Height / (float)bmp.Width) * tnWidth);
                else if (bmp != null && bmp.Width < bmp.Height)
                    tnWidth = Convert.ToInt32(((float)bmp.Width / (float)bmp.Height) * tnHeight);

                int iLeft = (imgWidth / 2) - (tnWidth / 2);
                int iTop = (imgHeight / 2) - (tnHeight / 2);

                grp.FillRectangle(new SolidBrush(Color.White), 0, 0, retBmp.Width, retBmp.Height);

                if (bmp != null)
                    grp.DrawImage(bmp, iLeft, iTop, tnWidth, tnHeight);

                Pen pn = new Pen(penColor, 1); //Color.Wheat
                grp.DrawRectangle(pn, 0, 0, retBmp.Width - 1, retBmp.Height - 1);
                return retBmp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// 计算实际作业可用宽度
        /// </summary>
        /// <param name="jobwidth">作业宽度</param>
        /// <returns>实际作业可用宽度(单位为英寸)</returns>
        public static  float CalcRealJobWidth(float jobwidth,AllParam allparam)
        {
            float ret = 0;
//			AllParam allparam = this.m_iPrinterChange.GetAllParam();
            float orginX = allparam.PrinterSetting.sFrequencySetting.fXOrigin;
            float colorbarspace = allparam.PrinterSetting.sBaseSetting.sStripeSetting.fStripeOffset;
            float colorbarwidth = allparam.PrinterSetting.sBaseSetting.sStripeSetting.fStripeWidth;
            InkStrPosEnum place = allparam.PrinterSetting.sBaseSetting.sStripeSetting.eStripePosition;
            float pw = allparam.PrinterSetting.sBaseSetting.fPaperWidth + allparam.PrinterSetting.sBaseSetting.fLeftMargin;
            switch (place)
            {
                case InkStrPosEnum.Both:
                    ret = (colorbarwidth+colorbarspace)*2 + jobwidth;
                    if(orginX + ret>pw)
                        ret = pw-orginX;
                    break;
                case InkStrPosEnum.Left:
                case InkStrPosEnum.Right:
                    ret = (colorbarwidth+colorbarspace) + jobwidth;
                    if(orginX + ret>pw)
                        ret = pw-orginX;
                    break;
                case InkStrPosEnum.None:
                    ret =jobwidth;
                    if( orginX+ret>pw)
                        ret = pw-orginX;
                    break;
            }
            return ret;
        }


        /// <summary>
        /// The set numric max and min.
        /// </summary>
        /// <param name="topcon">
        /// The topcon.
        /// </param>
        public static void SetNumricMaxAndMin(Control topcon,bool isFloat)
        {
            foreach (Control col in topcon.Controls)
            {
                if (!(col is NumericUpDown) && col.Controls.Count > 0)
                {
                    SetNumricMaxAndMin(col,isFloat);
                }
                else if (col is NumericUpDown)
                {
                    (col as NumericUpDown).Maximum = decimal.MaxValue;
                    (col as NumericUpDown).Minimum = decimal.MinValue;
                    if(isFloat)
                    {
                        (col as NumericUpDown).DecimalPlaces = 3;
                    }
                }
            }
        }

        /// <summary>
        /// 结构体转byte数组
        /// </summary>
        /// <param name="struvtObj">
        /// 要转换的结构体
        /// </param>
        /// <returns>
        /// 转换后的数组
        /// </returns>
        public static byte[] StructToBytes(object struvtObj)
        {
            // 得到结构体大小
            int size = Marshal.SizeOf(struvtObj);

            // 创建byte数组
            byte[] bytes = new byte[size];

            // 分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(struvtObj, structPtr, true);

            // 从内存空间拷到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);

            // 释放内存空间
            Marshal.FreeHGlobal(structPtr);

            // 返回byte数组
            return bytes;
        }

		
        /// <summary>
        /// The bytes to struct.
        /// </summary>
        /// <param name="bytes">
        /// </param>
        /// <param name="type">
        /// </param>
        /// <returns>
        /// The bytes to struct.
        /// </returns>
        public static object BytesToStruct(byte[] bytes, Type type)
        {
            // 得到结构体大小
            int size = Marshal.SizeOf(type);

            // byte数组长度小于结构体的大小
            if (size > bytes.Length)
            {
                // 返回空
                return null;
            }

            object obj = null;

            // 分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);

            // 将byte数组拷到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);

            // 将内存空间转换为目标结构体
            obj = Marshal.PtrToStructure(structPtr, type);

            // 释放内存空间
            Marshal.FreeHGlobal(structPtr);

            // 返回byte数组
            return obj;
        }

        public static int GetLanguage()
        {
            uint len = 1;
            byte[] lang = new byte[len];
            int lcid = 0x0004;
			
            if(PreInterface.GetEpsonEP0Cmd(0x7F,lang,ref len,13,0) != 0)
            {
                //0: Sim chinese, 1: trad chinese, 3: english, 4: tailand 
                switch(lang[0])
                {
                    case 0:
                        lcid = 0x0004;// zh-CHS 0x0004 中文（简体）
                        break;
                    case 1:
                        lcid = 0x7C04; // zh-CHT 0x7C04 中文（繁体） 
                        break;				
                    case 2:
                        lcid = 0x0409;// en-US 0x0409 英语 - 美国 
                        break;				
                    case 3:
                        lcid = 0x001E;// th 0x001E 泰语 
                        break;	
                    default:
                        lcid = -1;
                        break;
                }
            }
            else
            {
                lcid = -1;
            }
            return lcid;
        }
        public static string GetDoublePrintFileName(string sourcefile,bool isPos)
        {
            if(isPos)
            {
                if (!string.IsNullOrEmpty(sourcefile))
                    return Path.Combine(Path.GetDirectoryName(sourcefile),
                        Path.GetFileNameWithoutExtension(sourcefile) + CoreConst.DoublePrintPrtPreName + Path.GetExtension(sourcefile));
            }
            else if (!string.IsNullOrEmpty(sourcefile))
                return Path.Combine(Path.GetDirectoryName(sourcefile),
                    Path.GetFileNameWithoutExtension(sourcefile) + CoreConst.DoublePrintPrtPosName + Path.GetExtension(sourcefile));
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] GetAllDataFromFile(string path)
        {
            int len = 2042 +16;
            byte[] ret = new byte[len];
            byte[] source = new byte[len*2];
            try
            {
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                stream.Seek(6, SeekOrigin.Begin);
                byte[] db = new byte[2040];
                int read = stream.Read(db, 0, db.Length);
                if (read!= db.Length)
                {
                    return null;
                }
                Array.Copy(db, 0, source, 4 + 32, db.Length);
                //
                byte[] da = new byte[2040];
                read = stream.Read(da, 0, da.Length);
                if (read != da.Length)
                {
                    return null;
                }
                Array.Copy(da, 0, source, 4 + da.Length +32, da.Length);
                //
                byte[] id = new byte[4];
                read = stream.Read(id, 0, id.Length);
                if (read != id.Length)
                {
                    return null;
                }
                byte[] sampleClock = new byte[4];
                read = stream.Read(sampleClock, 0, sampleClock.Length);
                if (read != sampleClock.Length)
                {
                    return null;
                }
                Array.Copy(sampleClock, 0, source, 32, sampleClock.Length);
                //
                int j = 0;
                for (int i = 0; i < source.Length; i += 2)
                {
                    ret[j++] = AsciiToHex(source, i);
                }
                //waveformid 
                Array.Copy(id, 0, ret, 0, id.Length);

            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }


        public const int Kyocera_WaveformLength = 224;
        public const int KJ4A_RH06_WaveformLength = 105;
        /// <summary>
        /// 数据包格式：Head（从0开始，1个字节）+color（1个字节）+SaveID（1个字节） + hash（16个字节） + reserve(IN8U，2个字节) + data(INT8U,224个字节) 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] GetAllDataFromFileKyocera(string path)
        {
            int len = Kyocera_WaveformLength + 18;
            byte[] ret = new byte[len];
            try
            {
#if false // 按二进制文件读取
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                stream.Seek(6, SeekOrigin.Begin);
                byte[] db = new byte[Kyocera_WaveformLength];
                int read = stream.Read(db, 0, db.Length);
                if (read != db.Length)
                {
                    return null;
                }
                Array.Copy(db, 0, ret, 18, db.Length);
                //
                byte[] id = new byte[4];
                read = stream.Read(id, 0, id.Length);
                if (read != id.Length)
                {
                    return null;
                }
                //waveformid 
                Array.Copy(id, 0, ret, 0, id.Length);
#else 
                // 按文本文件读取,每行一个数字
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] db = new byte[Kyocera_WaveformLength];
                TextReader txtReader = new StreamReader(stream);
                int read = 0;
                for (int i = 0; i < db.Length; i++)
                {
                    string line = txtReader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    byte val = 0;
                    byte.TryParse(line, out val);
                    db[i] = val;
                    read++;
                }

                if (read != db.Length)
                {
                    return null;
                }
                Array.Copy(db, 0, ret, 18, db.Length);
                // hashcode
#if true
                int hash = db.GetHashCode();
                byte[] id = BitConverter.GetBytes(hash);
#else
                //
                byte[] id = new byte[4] { (byte)'B', (byte)'Y', (byte)'H', (byte)'X' };
#endif
                Array.Copy(id, 0, ret, 0, id.Length);
#endif
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public static byte[] GetAllDataFromFileKyocera_Des(string path, ref string info)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.ASCII))
            {
                string temp = sr.ReadLine();
                if (temp != null)
                {
                    if (temp.IndexOf("BYHXEncrypted", StringComparison.Ordinal) == 0)
                    {
                        string vidpid = PubFunc.GetVendorId();
                        if (vidpid == string.Empty)
                        {
                            return null;
                        }
                        string key = vidpid;
                        string iv = "BYHX" + vidpid.Substring(0, 4);
                        string content = temp.Substring(15, temp.Length - 15 - 4);
                        byte[] buffer = DecryptBytes(Convert.FromBase64String(content), key, iv);
                        string contenttemp = Convert.ToBase64String(buffer);
                        string contentvidpid = contenttemp.Substring(0, 8);
                        string bbb = contenttemp.Substring(8);
                        byte[] realbuffer = Convert.FromBase64String(bbb);
                        if (realbuffer.Length != Kyocera_WaveformLength)
                        {
                            return null;
                        }
                        else
                        {
                            if (contentvidpid == vidpid)
                            {
                                int len = Kyocera_WaveformLength + 18;
                                byte[] ret = new byte[len];
                                byte[] db = realbuffer;

                                Array.Copy(db, 0, ret, 18, db.Length);
                                int hash = db.GetHashCode();
                                byte[] id = BitConverter.GetBytes(hash);
                                Array.Copy(id, 0, ret, 0, id.Length);

                                return ret;

                            }
                            else
                            {
                                info = "Waveform file vendor does not match current vendor id,Download not supported!";
                                return null;
                            }
                        }
                    }
                    else
                    {
                        return GetAllDataFromFileKyocera(path);
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        
        public static byte[] GetAllDataFromFileKyocera_KJ4A_RH06(string path)
        {
            int len = KJ4A_RH06_WaveformLength + 18;
            byte[] ret = new byte[len];
            try
            {
#if false // 按二进制文件读取
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                stream.Seek(6, SeekOrigin.Begin);
                byte[] db = new byte[224];
                int read = stream.Read(db, 0, db.Length);
                if (read != db.Length)
                {
                    return null;
                }
                Array.Copy(db, 0, ret, 18, db.Length);
                //
                byte[] id = new byte[4];
                read = stream.Read(id, 0, id.Length);
                if (read != id.Length)
                {
                    return null;
                }
                //waveformid 
                Array.Copy(id, 0, ret, 0, id.Length);
#else
                // 按文本文件读取,每行一个数字
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] db = new byte[KJ4A_RH06_WaveformLength];
                TextReader txtReader = new StreamReader(stream);
                int read = 0;
                for (int i = 0; i < db.Length; i++)
                {
                    string line = txtReader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    byte val = 0;
                    byte.TryParse(line, out val);
                    db[i] = val;
                    read++;
                }

                if (read != db.Length)
                {
                    return null;
                }
                Array.Copy(db, 0, ret, 18, db.Length);
                // hashcode
#if true
                int hash = db.GetHashCode();
                byte[] id = BitConverter.GetBytes(hash);
#else
                //
                byte[] id = new byte[4] { (byte)'B', (byte)'Y', (byte)'H', (byte)'X' };
#endif
                Array.Copy(id, 0, ret, 0, id.Length);
#endif
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public static byte[] GetAllDataFromFileKyocera_KJ4A_RH06_Des(string path,ref string info)
        {
            using(StreamReader sr=new StreamReader(path,Encoding.ASCII))
            {
                string temp = sr.ReadLine();
                if (temp != null)
                {
                    
                    if (temp.IndexOf("BYHXEncrypted", StringComparison.Ordinal) == 0)
                    {
                        string vidpid = PubFunc.GetVendorId();
                        if (vidpid == string.Empty)
                        {
                            return null;
                        }
                        string key = vidpid;
                        string iv = "BYHX" + vidpid.Substring(0, 4);
                        string content = temp.Substring(15, temp.Length - 15 - 4);
                        byte[] buffer = DecryptBytes(Convert.FromBase64String(content), key, iv);
                        string contenttemp = Convert.ToBase64String(buffer);
                        string contentvidpid = contenttemp.Substring(0, 8);
                        string bbb = contenttemp.Substring(8);
                        byte[] realbuffer = Convert.FromBase64String(bbb);
                        if (realbuffer.Length != KJ4A_RH06_WaveformLength)
                        {
                            return null;
                        }
                        else
                        {
                            if (contentvidpid == vidpid)
                            {
                                int len = KJ4A_RH06_WaveformLength + 18;
                                byte[] ret = new byte[len];
                                byte[] db = realbuffer;

                                Array.Copy(db, 0, ret, 18, db.Length);
                                int hash = db.GetHashCode();
                                byte[] id = BitConverter.GetBytes(hash);
                                Array.Copy(id, 0, ret, 0, id.Length);

                                return ret;

                            }
                            else
                            {
                                info = "Waveform file vendor does not match current vendor id,Download not supported!";
                                return null;
                            }
                        }
                    }
                    else
                    {
                        return GetAllDataFromFileKyocera_KJ4A_RH06(path);
                    }
                }
                else
                {
                    return null;
                }
            }
        }



	    public static byte[] GetDelaySettingKyocera_KJ4A_RH06(string path)
	    {
            try
            {
                // 按文本文件读取,每行一个数字
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] db = new byte[128];
                TextReader txtReader = new StreamReader(stream);
                int read = 0;
                for (int i = 0; i < db.Length; i++)
                {
                    string line = txtReader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    byte val = 0;
                    byte.TryParse(line, out val);
                    db[i] = val;
                    read++;
                }

                if (read != db.Length)
                {
                    return null;
                }
                return db;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public static byte[] GetWaveFormDataFromFile(string path)
        {
            byte[] ret = new byte[2046];
            try
            {
                // 读取元数据
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                stream.Seek(6, SeekOrigin.Begin);
                byte[] db = new byte[2040];
                stream.Read(db, 0, db.Length);
                byte[] da = new byte[2040];
                stream.Read(da, 0, da.Length);
                byte[] wfid = new byte[4];
                stream.Read(wfid, 0, wfid.Length);
                byte[] sclock = new byte[4];
                stream.Read(sclock, 0, sclock.Length);
                stream.Close();
                // 由ASIIC码转hex
                int j = 0;
                Buffer.BlockCopy(wfid, 0, ret, 0, wfid.Length); // waveform id 不需要转码
                j += wfid.Length;

                for (int i = 0; i < sclock.Length; i += 2)
                {
                    ret[j++] = AsciiToHex(sclock, i);
                }
                for (int i = 0; i < db.Length; i += 2)
                {
                    ret[j++] = AsciiToHex(db, i);
                }
                for (int i = 0; i < da.Length; i += 2)
                {
                    ret[j++] = AsciiToHex(da, i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }

        private static byte AsciiToHex(byte[] data, int startindex)
        {
            byte ret = 0;
            int high = data[startindex];
            int low = data[startindex + 1];
            high = high >= 0x30 && high <= 0x39 ? high - 0x30 : (high >= 0x41 && high <= 0x46 ? high - 0x41 + 0xa : (high >= 0x61 && high <= 0x66 ? high - 0x61 + 0xa : 0));
            high = high << 4;
            low = low >= 0x30 && low <= 0x39 ? low - 0x30 : (low >= 0x41 && low <= 0x46 ? low - 0x41 + 0xa : (low >= 0x61 && low <= 0x66 ? low - 0x61 + 0xa : 0));
            ret = (byte)(high + low);
            return ret;
        }


        public static int GetHeadBoardMaxWaveFormNum(HEAD_BOARD_TYPE headBoardType)
        {
            int maxnum = 0;
            switch (headBoardType)
            {
                case HEAD_BOARD_TYPE.SP_XAAR1001_02H:
                    maxnum = 8;
                    break;
                case HEAD_BOARD_TYPE.XAAR501_8H:
                case HEAD_BOARD_TYPE.GMA9905300_8H:
                case HEAD_BOARD_TYPE.GMA3305300_8H:
                    maxnum = 8;
                    break;
            }
            return maxnum;
        }

        public static string GetHashcodeString(byte[] hash)
        {
            string hashcode = string.Empty;
            for (int i = 0; i < hash.Length; i++)
            {
                hashcode += hash[i].ToString("x").PadLeft(2, '0');
            }
            return hashcode;
        }

        /// <summary>
        /// 判断DoubleYAxis.bin文件是否存在，如果支持Y双轴，必须有此文件
        /// </summary>
        /// <returns></returns>
        public static bool IsDoubleYAxis()
        {
            bool bDoubleYAxis = false;
            string curFile = Application.StartupPath + Path.DirectorySeparatorChar + "DoubleYAxis.bin";
            if (File.Exists(curFile))
                return true;
            return UIFunctionOnOff.SupportDoubleYAxis;
        }

        /// <summary>
        /// 是否支持电机设置
        /// </summary>
        /// <returns></returns>
        public static bool IsMororSettingAdmin()
        {
            return UIFunctionOnOff.SupportMotorSettingAdmin;
        }

        //喷嘴关闭chg
        public static bool IsCustomCloseNozzle()
        {
            //CustomCloseNozzle
            ushort pid, vid;
            pid = vid = 0;
            CoreInterface.GetProductID(ref vid, ref pid);

            if (vid == (ushort)VenderID.PuQi
               || vid == (ushort)VenderID.PuQi_ROLL_TEXTILE)
                return true;
            return UIFunctionOnOff.SupportCustomCloseNozzle;
        }

        //打印质量调试
        public static bool SupportDebugQuality()
        {
            return UIFunctionOnOff.SupportDebugQuality;
        }

        /// <summary>
        /// 判读是否3d打印厂商
        /// 判断Machine3D.bin文件是否存在，如果存在则支持3D打印机
        /// </summary>
        /// <returns></returns>
        public static bool Is3DPrintMachine()
        {
            string curFile = Application.StartupPath + Path.DirectorySeparatorChar + "Machine3D.bin";
            if (File.Exists(curFile))
                return true; // 兼容老的配置文件

            //如果配置文件不存就按厂商id 判断
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if ((vid >= 0x600 && vid <= 0x700) || (vid >= 0x8600 && vid <= 0x8700))
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 3D打印参数从JobInfo中发送
        /// </summary>
        /// <returns></returns>
        public static bool Is3DPrint_JobInfo()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)0xEF && pid == (ushort)0x0100)
                    return true;
                return false;
            }
            return false;
        }

        public static bool IsJobConfigModesXml()
	    {
            string m_FilePath;
            if (GlobalSetting.Instance.VendorProduct != null && GlobalSetting.Instance.VendorProduct.Length >= 8)
            {
                m_FilePath = GlobalSetting.Instance.VendorProduct.Substring(0, 4) + "/" + GlobalSetting.Instance.VendorProduct.Substring(4, 4) + "/" + JobConfigFileName;
            }
            else
            {
                m_FilePath = JobConfigFileName;
            }
            if (!File.Exists(m_FilePath))
                return false;
            var printmodelist = PubFunc.LoadJobModesFromFile();
            if (printmodelist.Items.Count == 0)
                return false;
            return true;
        }
        /// <summary>
        /// 从文件加载作业打印模式
        /// </summary>
        /// <returns></returns>
        public static JobModes LoadJobModesFromFile()
	    {
            string m_FileName = "";
            string m_FilePath = "";
            string oldSelectConfigName = "";
            bool bExistConfigName = false;

            if (GlobalSetting.Instance.VendorProduct != null && GlobalSetting.Instance.VendorProduct.Length >= 8)
            {
                m_FilePath = GlobalSetting.Instance.VendorProduct.Substring(0, 4) + "/" + GlobalSetting.Instance.VendorProduct.Substring(4, 4) + "/";
            }
            else
            {
                m_FilePath = "";
            }

            m_FileName = m_FilePath + PubFunc.JobConfigFileName;
            JobModes jobConfigList = new JobModes();
            try
            {
                if (File.Exists(m_FileName))
                {
                    var doc = new SelfcheckXmlDocument();
                    doc.Load(m_FileName);
                    jobConfigList = (JobModes) PubFunc.SystemConvertFromXml(doc.InnerXml, typeof (JobModes));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ResString.GetResString("LoadJobConfigError")+ ex.Message);
            }
            return jobConfigList;
	    }
        /// <summary>
        /// 保存作业打印模式到文件
        /// </summary>
        /// <returns></returns>
        public static void SaveJobModesFromFile(JobModes jobConfigList)
        {
           
            string m_FileName = "";
            string m_FilePath = "";
            string oldSelectConfigName = "";
            bool bExistConfigName = false;

            if (GlobalSetting.Instance.VendorProduct != null && GlobalSetting.Instance.VendorProduct.Length >= 8)
            {
                m_FilePath = GlobalSetting.Instance.VendorProduct.Substring(0, 4) + "/" + GlobalSetting.Instance.VendorProduct.Substring(4, 4) + "/";
            }
            else
            {
                m_FilePath = "";
            }

            m_FileName = m_FilePath + PubFunc.JobConfigFileName;
            try
            {
                if (jobConfigList.Items.Count == 0)
                {
                    if (File.Exists(m_FileName))
                        File.Delete(m_FileName);
                }
                else
                {
                    var doc = new SelfcheckXmlDocument();
                    string xml = string.Empty;
                    if (jobConfigList == null || jobConfigList.Items.Count < 0) return;
                    xml += PubFunc.SystemConvertToXml(jobConfigList, typeof(JobModes));
                    doc.InnerXml = xml;
                    doc.Save(m_FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ResString.GetResString("SaveJobConfigError") + ex.Message);
            }
        }

        public const string JobConfigFileName = "JobConfigModes.xml";
        public const string MediaConfigFileName = "JobMediaModes.xml";
        public static bool IsMediaConfigModesXml()
        {
            string m_FilePath;
            if (GlobalSetting.Instance.VendorProduct != null && GlobalSetting.Instance.VendorProduct.Length >= 8)
            {
                m_FilePath = GlobalSetting.Instance.VendorProduct.Substring(0, 4) + "/" + GlobalSetting.Instance.VendorProduct.Substring(4, 4) + "/" + MediaConfigFileName;
            }
            else
            {
                m_FilePath = MediaConfigFileName;
            }
            if (!File.Exists(m_FilePath))
                return false;
            return true;
        }
        /// <summary>
        /// 从文件加载介质模式参数
        /// </summary>
        /// <returns></returns>
        public static JobMediaModes LoadMediaModesFromFile()
	    {
            string m_FileName = "";
            string m_FilePath = "";
            string oldSelectConfigName = "";
            bool bExistConfigName = false;

            if (GlobalSetting.Instance.VendorProduct != null && GlobalSetting.Instance.VendorProduct.Length >= 8)
            {
                m_FilePath = GlobalSetting.Instance.VendorProduct.Substring(0, 4) + "/" + GlobalSetting.Instance.VendorProduct.Substring(4, 4) + "/";
            }
            else
            {
                m_FilePath = "";
            }

            m_FileName = m_FilePath + PubFunc.MediaConfigFileName;
            JobMediaModes mediaConfigList = new JobMediaModes();

            try
            {
                if (File.Exists(m_FileName))
                {
                    var doc = new SelfcheckXmlDocument();
                    doc.Load(m_FileName);
                    mediaConfigList = (JobMediaModes) PubFunc.SystemConvertFromXml(doc.InnerXml, typeof (JobMediaModes));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ResString.GetResString("LoadJobConfigError") + ex.Message);
            }
            return mediaConfigList;
	    }

        /// <summary>
        /// 保存介质模式参数到文件
        /// </summary>
        /// <returns></returns>
        public static void SaveMediaModesFromFile(JobMediaModes mediaConfigList)
        {
            if (mediaConfigList.Items.Count == 0)
                return;
            string m_FileName = "";
            string m_FilePath = "";
            string oldSelectConfigName = "";
            bool bExistConfigName = false;

            if (GlobalSetting.Instance.VendorProduct != null && GlobalSetting.Instance.VendorProduct.Length >= 8)
            {
                m_FilePath = GlobalSetting.Instance.VendorProduct.Substring(0, 4) + "/" + GlobalSetting.Instance.VendorProduct.Substring(4, 4) + "/";
            }
            else
            {
                m_FilePath = "";
            }

            m_FileName = m_FilePath + PubFunc.MediaConfigFileName;
            try
            {
                var doc = new SelfcheckXmlDocument();
                string xml = string.Empty;
                if (mediaConfigList == null || mediaConfigList.Items.Count < 0) 
                    return;
                xml += PubFunc.SystemConvertToXml(mediaConfigList, typeof(JobMediaModes));
                doc.InnerXml = xml;
                doc.Save(m_FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ResString.GetResString("SaveJobMediaModesError") + ex.Message);
            }
        }

        /// <summary>
        /// 是否为峰华卓立
        /// </summary>
        /// <returns></returns>
        public static bool IsFhzl3D()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FHZL_3D_PRINT)
                    return true;
                else
                    return false;
            }
            else
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
#else
		    return false;
#endif
        }

        /// <summary>
        /// 是否为中心回转3D
        /// </summary>
        /// <returns></returns>
        public static bool IsZXHZ3D()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.ZXHZ_3D_PRINT)
                    return true;
                else
                    return false;
            }
            else
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
#else
		    return false;
#endif
        }



        /// <summary>
        /// 是否为KinColor
        /// </summary>
        /// <returns></returns>
        public static bool IsKINCOLOR_FLAT_UV()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.KINCOLOR_FLAT_UV)
                    return true;
                else
                    return false;
            }
            else
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
#else
		    return false;
#endif
        }

        /// <summary>
        /// 是否为乐彩打印机
        /// </summary>
        /// <returns></returns>
        public static bool IsUserExtensionFormNeed()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.LECAI_FLAT_UV)
                    return true;
                else
                    return false;
            }
            else
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
#else
		    return false;
#endif
        }


        public static bool IsGzSurrportManualClean(SPrinterProperty sp)
        {
            try
            {
                bool isGama = sp.ePrinterHead == PrinterHeadEnum.Fujifilm_GMA3305300_5pl
                              || sp.ePrinterHead == PrinterHeadEnum.Fujifilm_GMA9905300_5pl;
                ushort pid, vid;
                pid = vid = 0;
                CoreInterface.GetProductID(ref vid, ref pid);
                bool ret = false;
#if !LIYUUSB
                if (vid == (ushort)VenderID.GONGZENG_BELT_TEXTILE
                    || vid == (ushort)VenderID.GONGZENG_ROLL_UV 
                    || vid == (ushort)VenderID.GONGZENG_FLAT_UV
                    || (vid == (ushort)VenderID.GONGZENG && isGama))
                    ret = true;
#endif
                return ret;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 隆源3D打印机
        /// </summary>
        /// <returns></returns>
        public static bool IsLongYuan()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.LongYuan)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// 是否为Hapond
        /// </summary>
        /// <returns></returns>
        public static bool IsHapond()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.HAPOND || vid == (ushort)VenderID.HAPOND_FLAT_UV
                    || vid == 0x8f // 0x8f兼容老版本
                    )
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
#else
		    return false;
#endif
        }
        /// <summary>
        /// 是否为卓展或者卓展BELT_UV
        /// </summary>
        /// <returns></returns>
	    public static bool IsZhuoZhan()
	    {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.ZHUOZHAN || vid == (ushort)VenderID.ZHUOZHAN_BELT_UV)
                    return true;
                return false;
            }
	        return false;
        }
        /// <summary>
        /// 中山变革UV120J
        /// </summary>
        /// <returns></returns>
        public static bool IsZhongShanBianGeUV120J()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.INWEAR_ROLL_UV && pid == 0x0120)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否为ColorJet 0x8
        /// </summary>
        /// <returns></returns>
        public static bool IsColorJet_0x8()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.COLORJET)
                    return true;
                return false;
            }
            return false;
        }

        public static bool SupportLengthProgress()
        {
            bool isSupport = false;

            //FLORA(彩神)	0x4B	0XCB			0X34B				0X74B
        //FLORA = 0x4b,
        //FLORA_FLAT_UV = 0xcb,
        //FLORA_BELT_TEXTILE = 0x34b, //T180
        //FLORA_FLAT_TEXTILE = 0x74b, //T50

            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA || vid == (ushort)VenderID.FLORA_FLAT_UV || vid == (ushort)VenderID.FLORA_BELT_TEXTILE || vid == (ushort)VenderID.FLORA_FLAT_TEXTILE)
                    return true;
                return false;
            }

            return isSupport;
        }

        /// <summary>
        /// 是否支持彩神机械手
        /// </summary>
        public static bool SupportFLORA_MachineHand()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA_FLAT_UV)
                    return true;
                return false;
            }

            return false;

        }


        /// <summary>
        /// Excute Enum MBId(added by ljp 2014-5-23)
        /// </summary>
        /// <param name="isCheckFactoryData">indicates whether or not to check factory data,the default value is false</param>
        /// <returns>MBId list</returns>
        public static List<int> ExcuteEnumMBId(bool isCheckFactoryData = false)
        {
            List<int> mbidList = new List<int>();

            int arraynum = 64;
            int[] usbIdArray = new int[arraynum];
            int usbNum = 0;
            if (CoreInterface.EnumUsb(ref usbNum, usbIdArray, arraynum) != 1)
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.EnumUSBFail);
                MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                for (int i = 0; i < usbNum; i++)
                {
                    mbidList.Add(usbIdArray[i]);
                }

                if (isCheckFactoryData)
                {
                    PubFunc.CheckFactortyData(usbIdArray, usbNum);
                }
            }

            return mbidList;
        }
        /// <summary>
        /// 检查双主板工厂参数设定是否一致
        /// </summary>
        public static void CheckFactortyData(int[] usbIdArray, int usbNum)
        {
            if (usbNum < 2)
            {
                return;
            }
            SFWFactoryData mainFactoryData = new SFWFactoryData();
            SFWFactoryData extraFactoryData = new SFWFactoryData();
            StringBuilder difference = new StringBuilder();

            if (CoreInterface.GetFWFactoryData(ref mainFactoryData, usbIdArray[0]) == 0)
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                MessageBox.Show(string.Format("{0}mbid:{1}", info, usbIdArray[0]), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (CoreInterface.GetFWFactoryData(ref extraFactoryData, usbIdArray[1]) == 0)
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                MessageBox.Show(string.Format("{0}mbid:{1}", info, usbIdArray[1]), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var fields = typeof(SFWFactoryData).GetFields();
            string uiInfo = ResString.GetEnumDisplayName(typeof(UIInfo), UIInfo.FactoryDataValue);
            uiInfo = "[" + uiInfo + "]" + " mbid_{1}：{2}；mbid_{3}：{4}\n";
            foreach (var field in fields)
            {
                var fieldValue1 = field.GetValue(mainFactoryData);
                var fieldValue2 = field.GetValue(extraFactoryData);
                if (fieldValue1.GetType().IsValueType && !fieldValue1.Equals(fieldValue2))
                {
                    difference.Append(string.Format(uiInfo,
                            field.Name, usbIdArray[0], fieldValue1, usbIdArray[1], fieldValue2));
                }

            }
            var checkResult = CheckArrayData(mainFactoryData.eColorOrder, extraFactoryData.eColorOrder);
            if (checkResult.Item1)
            {
                difference.Append(string.Format(uiInfo,
                          "eColorOrder", usbIdArray[0], checkResult.Item2, usbIdArray[1], checkResult.Item3));
            }

            checkResult = CheckArrayData(mainFactoryData.m_nReserve, extraFactoryData.m_nReserve);
            if (checkResult.Item1)
            {
                difference.Append(string.Format(uiInfo,
                           "m_nReserve", usbIdArray[0], checkResult.Item2, usbIdArray[1], checkResult.Item3));
            }

            if (difference.Length > 0)
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.FWFactoryDataDiffer);
                difference.Insert(0, info + "\n");
                MessageBox.Show(difference.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private static Tuple<bool, string, string> CheckArrayData(byte[] mainData, byte[] extraData)
        {
            string valueString1 = string.Empty;
            string valueString2 = string.Empty;
            bool isValueDiffer = false;
            for (int i = 0; i < mainData.Length; i++)
            {
                if (!isValueDiffer && mainData[i] != extraData[i])
                {
                    isValueDiffer = true;
                }
                if (i == 0)
                {
                    valueString1 += mainData[i].ToString();
                    valueString2 += extraData[i].ToString();
                }
                else
                {
                    valueString1 += "," + mainData[i].ToString();
                    valueString2 += "," + extraData[i].ToString();
                }
            }
            return new Tuple<bool, string, string>(isValueDiffer, valueString1, valueString2);
        }
        #region 卷长剩余长度监测
        private static int? _curY = null; // 当前y轴坐标位置
        /// <summary>
        /// 每次就绪时按y轴坐标更新检测的纸长
        /// </summary>
        public static void RefreshRollLength()
        {
            if (!CoreInterface.AllParams.ExtendedSettings.bEnableDetectRollLength)
                return;
            int x = 0, y = 0, z = 0;
            if (CoreInterface.QueryCurrentPosN(ref x, ref y, ref z) > 0)
            {
                if (!_curY.HasValue)
                {
                    _curY = y;//上电第一次获取位置,只初始化位置,不更新纸长
                }
                else
                {
                    // 根据本次位置和上次位置更新纸长
                    CoreInterface.AllParams.ExtendedSettings.fCalculateRollLength -= (y - _curY.Value) / CoreInterface.AllParams.PrinterProperty.fPulsePerInchY;
                    if (CoreInterface.AllParams.ExtendedSettings.fCalculateRollLength < 0)
                        CoreInterface.AllParams.ExtendedSettings.fCalculateRollLength = 0;
                    _curY = y;
                }
            }

        }


	    public static void ResetCurY()
	    {
	        _curY = null;
	    }
        #endregion

        public static Dictionary<String, Color> SetHeadColorList()
        {
            Dictionary<String, Color> info = new Dictionary<String, Color>();

            info.Add("C", Color.FromArgb(0, 255, 255));
            info.Add("M", Color.FromArgb(255, 0, 255));
            info.Add("Y", Color.FromArgb(255, 255, 0));
            info.Add("K", Color.FromArgb(0, 0, 0));

            info.Add("Lc", Color.FromArgb(224, 255, 255));
            info.Add("Lm", Color.FromArgb(255, 224, 255));
            info.Add("Ly", Color.FromArgb(255, 255, 224));
            info.Add("Lk", Color.FromArgb(224, 224, 224));

            info.Add("R", Color.FromArgb(255, 0, 0));
            info.Add("B", Color.FromArgb(0, 0, 255));
            info.Add("G", Color.FromArgb(0, 255, 0));
            info.Add("Or", Color.FromArgb(255, 165, 0));

            info.Add("W", Color.FromArgb(255, 255, 255));
            info.Add("W1", Color.FromArgb(255, 255, 255));
            info.Add("W2", Color.FromArgb(255, 255, 255));
            info.Add("W3", Color.FromArgb(255, 255, 255));
            info.Add("W4", Color.FromArgb(255, 255, 255));
            info.Add("W5", Color.FromArgb(255, 255, 255));
            info.Add("W6", Color.FromArgb(255, 255, 255));
            info.Add("W7", Color.FromArgb(255, 255, 255));
            info.Add("W8", Color.FromArgb(255, 255, 255));

            info.Add("V", Color.FromArgb(240, 255, 255));
            info.Add("V1", Color.FromArgb(240, 255, 255));
            info.Add("V2", Color.FromArgb(240, 255, 255));
            info.Add("V3", Color.FromArgb(240, 255, 255));
            info.Add("V4", Color.FromArgb(240, 255, 255));
            info.Add("V5", Color.FromArgb(240, 255, 255));
            info.Add("V6", Color.FromArgb(240, 255, 255));
            info.Add("V7", Color.FromArgb(240, 255, 255));
            info.Add("V8", Color.FromArgb(240, 255, 255));

            info.Add("S1", Color.FromArgb(255, 255, 255));
            info.Add("S2", Color.FromArgb(255, 255, 255));
            info.Add("S3", Color.FromArgb(255, 255, 255));
            info.Add("S4", Color.FromArgb(255, 255, 255));

            info.Add("N", Color.FromArgb(255, 255, 255));

            return info;
        }


        public static LayoutSettingClassList GetLayoutSettingClassList()
        {
            LayoutSettingClassList m_LayoutSettingList;
            try
            {
                m_LayoutSettingList = new LayoutSettingClassList();

                if (File.Exists(CoreConst.LayoutFileName))
                {
                    var doc = new XmlDocument();
                    doc.Load(CoreConst.LayoutFileName);
                    m_LayoutSettingList = (LayoutSettingClassList)PubFunc.SystemConvertFromXml(doc.InnerXml, typeof(LayoutSettingClassList));
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

            return m_LayoutSettingList;
        }

        /// <summary>
        /// 获取8位的厂商id,Vid+0x0000 此方法用于京瓷波形加密；
        /// </summary>
        /// <returns></returns>
        private static string GetVendorId()
        {
            ushort vid = 0;
            ushort pid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                ushort key = 0;
                foreach (KeyValuePair<ushort, List<ushort>> keyValuePair in VidMatrix)
                {
                    if (keyValuePair.Value.Contains(vid))
                    {
                        key = keyValuePair.Key;
                        break;
                    }
                }
                if (key == 0)
                {
                    key = (ushort)(vid & 0x807f);
                }
                return key.ToString("X4") + 0.ToString("X4");
            }
            return string.Empty;
        }

        public static int GetVID()
        {
            ushort pid = 0;
            ushort vid = 0;
            CoreInterface.GetProductID(ref vid, ref pid);
            return vid;
        }

        private static readonly Dictionary<ushort, List<ushort>> VidMatrix = GetVidMatrix();

        /// <summary>
        /// 获取所有厂商ID矩阵：Dictionary中key：默认厂商ID，value：特殊机型厂商ID
        /// </summary>
        /// <returns></returns>
        private static Dictionary<ushort, List<ushort>> GetVidMatrix()
        {
            Dictionary<ushort, List<ushort>> vidMatrix = new Dictionary<ushort, List<ushort>>();
            List<ushort> keys = new List<ushort>
            {
                0x01,
                0x02,
                0x03,
                0x04,
                0x05,
                0x06,
                0x07,
                0x08,
                0x09,
                0x0A,
                0x0B,
                0x0C,
                0x0D,
                0x0E,
                0x0F,
                0x10,
                0x11,
                0x12,
                0x13,
                0x14,
                0x15,
                0x16,
                0x17,
                0x18,
                0x19,
                0x1A,
                0x1B,
                0x1C,
                0x1D,
                0x1E,
                0x1F,
                0x20,
                0x21,
                0x22,
                0x23,
                0x24,
                0x25,
                0x26,
                0x27,
                0x28,
                0x29,
                0x3A,
                0x3B,
                0x3C,
                0x3D,
                0x3E,
                0x3F,
                0x40,
                0x41,
                0x42,
                0x43,
                0x44,
                0x45,
                0x46,
                0X47,
                0x48,
                0X49,
                0X4A,
                0x4B,
                0x4C,
                0x4D,
                0x4E,
                0x4F,
                0X50,
                0X51,
                0x52,
                0x53,
                0x54,
                0X55,
                0X56,
                0X57,
                0X58,
                0x59,
                0X5A,
                0X5B,
                0X5C,
                0X5D,
                0X5E,
                0X5F,
                0X60,
                0x61,
                0X62,
                0X63,
                0x64,
                0x65,
                0X66,
                0x67,
                0x68,
                0x69,
                0x6A,
                0x6B,
                0x6C,
                0x6D,
                0x6E,
                0x6F,
                0x70,
                0x71,
                0x72,
                0x73,
                0x74,
                0x75,
                0x76,
                0x77,
                0x78,
                0x79,
                0x7A,
                0x7B,
                0x7C,
                0x7D,
                0x7E,
                0x7F,
                0xFF,
                0x8000,
                0x8001,
                0x8002,
                0x8003,
                0x8004,
                0x8005,
                0x8006,
                0x8007,
                0x8008,
                0x8009,
                0x800A,
                0x800B,
                0x800C,
                0x800D,
                0x800E,
                0x800F,
                0x8010,
                0x8011,
                0x8012,
                0x8013,
                0x8014,
                0x8070,
                0x8071,
                0x8072
            };
            List<ushort> additions = new List<ushort>
            {
                0x0000,
                0x0080,
                0x0100,
                0x0180,
                0x0200,
                0x0300,
                0x0400,
                0x0500,
                0x0600,
                0x0700,
                0x0800,
                0x0900,
                0x0A00,
                0x0B00
            };
            foreach (ushort key in keys)
            {
                List<ushort> value = new List<ushort>();
                foreach (ushort addition in additions)
                {
                    //特殊值
                    if (key == 0x02 && addition == 0X80)
                    {
                        value.Add(0X93);
                    }
                    else if (key == 0x3A && addition == 0X80)
                    {
                        value.Add(0xAA);
                    }
                    else if (key == 0x03 && addition == 0X180)
                    {
                        value.Add(0X9E);
                    }
                    else if (key == 0X05 && addition == 0X180)
                    {
                        value.Add(0x95);
                    }
                    else if (key == 0x3A && addition == 0X180)
                    {
                        value.Add(0x1AA);
                    }
                    else if (key == 0x8072 && addition == 0X300)
                    {
                        value.Add(0X0307);
                    }
                    //普通值
                    else
                    {
                        value.Add((ushort)(key + addition));
                    }
                }
                vidMatrix.Add(key, value);
            }
            return vidMatrix;
        }

        
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] EncryptBytes(byte[] bytes, string key, string iv)
        {
            DESCryptoServiceProvider sa = new DESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(key),
                IV = Encoding.UTF8.GetBytes(iv)
            };
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            return ms.ToArray();
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] DecryptBytes(byte[] bytes, string key, string iv)
        {
            DESCryptoServiceProvider sa = new DESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(key),
                IV = Encoding.UTF8.GetBytes(iv)
            };
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, sa.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            return ms.ToArray();
        }

        public static void EncryptSave(string filename, byte[] bytes)
        {
            try
            {
                string content = "BYHXEncrypted";
                Random r = new Random();
                int num1 = r.Next(10, 99);
                content += num1;
                string vendorid = PubFunc.GetVendorId();
                string key = vendorid;
                string iv = "BYHX" + vendorid.Substring(0, 4);
                List<byte> temp = new List<byte>();
                temp.AddRange(Convert.FromBase64String(vendorid));
                temp.AddRange(bytes);
                byte[] encrypt = EncryptBytes(temp.ToArray(), key, iv);
                content += Convert.ToBase64String(encrypt);
                int num2 = r.Next(1000, 9999);
                content += num2;
                using (StreamWriter sw = new StreamWriter(filename, false))
                {
                    sw.WriteLine(content);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        /// <summary>
        /// 判断文件是否正在被使用
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFileInUse(string fileName,FileAccess requestAccess)
        {
            bool inUse = true;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, requestAccess, FileShare.None);
                inUse = false;
            }
            catch
            {
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return inUse; //true表示正在使用,false没有使用  
        }

        public static bool IsSupportJobTileHeight()
        {
            bool ret = false;
            ret = SPrinterProperty.IsFloraT50OrT180();
            return ret;
        }

        public static bool IsSupportHistoryRecord()
        {
            bool ret = false;
            ret = SPrinterProperty.IsFloraT50OrT180();
            return ret;
        }

        public static bool IsSupportYSpeed()
        {
            bool ret = false;
            ret = SPrinterProperty.IsFloraT50OrT180();
            return ret;
        }

        public static bool IsSupportKeepwet()
        {
            bool ret = false;
            ret = SPrinterProperty.IsLiCai5113();
            return ret;
        }

        /// <summary>
        /// 是否支持UV偏移距离设置
        /// </summary>
        /// <returns></returns>
        public static bool IsSupportUVOffsetDistance()
        {
            return true;
        }
    }


    public class NewLayoutFun
    {
        public static int GetHeadNum()
        {
            int ret = 0;
            int rowNum = CoreInterface.GetRowNum();
            for (int i = 0; i < rowNum; i++)
            {
                ret += CoreInterface.GetHeadNumPerRow(i);
            }

            return ret;                
        }

        public static int GetLineNum()
        {
            int ret = 0;
            int rowNum = CoreInterface.GetRowNum();
            for (int i = 0; i < rowNum; i++)
            {
                ret += CoreInterface.GetLineNumPerRow(i);
            }

            return ret;
        }

        public static string GetColorName(int colorIdx)
        {
            string name = "";

            name = Enum.GetName(typeof(LayoutColorNameEnum), colorIdx);

            return name;
        }

        public static int GetColorID(string colorName)
        {
            int colorID = 0;

            LayoutColorNameEnum colorEnum = (LayoutColorNameEnum)Enum.Parse(typeof(LayoutColorNameEnum), colorName, true);
            colorID = (int)colorEnum;

            return colorID;
        }

        public static bool GetLayoutSetting(int index, ref LayoutSetting curLayoutSetting)
        {
            bool ret = false;
            curLayoutSetting = new LayoutSetting();

            try
            {
                LayoutSettingClassList m_LayoutSettingList = new LayoutSettingClassList();

                if (File.Exists(CoreConst.LayoutFileName))
                {
                    var doc = new XmlDocument();
                    doc.Load(CoreConst.LayoutFileName);
                    m_LayoutSettingList = (LayoutSettingClassList)PubFunc.SystemConvertFromXml(doc.InnerXml, typeof(LayoutSettingClassList));

                    if (m_LayoutSettingList.Items.Count > 0)
                    {
                        curLayoutSetting = m_LayoutSettingList.Items[index].Layout;
                        ret = true;
                    }
                }
            }
            catch
            {
            }

            return ret;
        }

        public static bool IsSupportGray
        {
            get
            {
                return true;
            }
        }
    }



    //新布局数据结构
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum LayoutColorNameEnum : int
    {
        Y = 1,
        M,
        C,
        K,
        Lk,
        Lc,
        Lm,
        Ly,
        O,
        G,
        R,
        B,
        W1 = 29,
        W2,
        W3,
        W4,
        W5,
        W6,
        W7,
        W8,
        V1 = 37,
        V2,
        V3,
        V4,
        V5,
        V6,
        V7,
        V8,
        P1 = 45,
        P2,
        P3,
        P4,
        P5,
        P6,
        P7,
        P8
    }

    public enum LayerType
    {
        LayerType_Base = 0,
        LayerType_TwoYcontinue,
        LayerType_ThreeYcontinue,
        LayerType_NoYcontinue = 0x10,
        LayerType_Yinterleave = 0x20,
    };

    [StructLayout(LayoutKind.Sequential,Pack=1)]
    [Serializable]
    public struct RealTimeDataOneHead
    {
        public int iHeadID;
		public int iRow;									//the row that the head located
		public int iHeatChannelCount;
		public int iFullVoltageChannelCount;
        public int iHalfVoltageChannelCount;
		public int iTemperatureChannelCount;
        public int iPulseWidthChannelCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_CHANNEL_NUM)]
		public float[] cTemperatureCur2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_CHANNEL_NUM)]
		public float[] cTemperatureSet;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_CHANNEL_NUM)]
		public float[] cTemperatureCur;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_CHANNEL_NUM)]
		public float[] cPulseWidth;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_CHANNEL_NUM)]
		public float[] cFullVoltage;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_CHANNEL_NUM)]
        public float[] cHalfVoltage;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_CHANNEL_NUM)]
		public float[] cFullVoltageBase;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_CHANNEL_NUM)]
        public float[] cHalfVoltageBase;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_CHANNEL_NUM)]
		public float[] cFullVoltageCurrent;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_CHANNEL_NUM)]
        public float[] cHalfVoltageCurrent;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] sName;

        public RealTimeDataOneHead(bool bInit)
        {
            iHeadID = 0;
            iRow = 0;
            iHeatChannelCount = 1;
            iFullVoltageChannelCount = 1;
            iHalfVoltageChannelCount = 1;
            iTemperatureChannelCount = 1;
            iPulseWidthChannelCount = 1;
            cTemperatureCur2 = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cTemperatureSet = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cTemperatureCur = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cPulseWidth = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cFullVoltage = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cHalfVoltage = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cFullVoltageBase = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cHalfVoltageBase = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cFullVoltageCurrent = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cHalfVoltageCurrent = new float[CoreConst.MAX_VOL_TEMP_NUM];
            sName = new byte[16];
        }
    }

    //和C++ 间传参的数据结构
    public struct LayerSetting
    {
        public byte curYinterleaveNum;//当前的Y拼插数（不改）
        public byte YContinueHead;//
        public byte curLayerType;
        public float layerYOffset;//当前行的偏移
        public ushort subLayerNum;//子行数
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public uint[] printColor;//16位当前行包含什么颜色
        public ushort nlayersource;//按位存数据源，每层两位
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] ndatasource;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] reverse;
        public uint nEnableLine;

        public LayerSetting(ushort subNum)
        {
            curYinterleaveNum = 2;
            YContinueHead = 1;
            curLayerType = 0;
            layerYOffset = 0.00f;
            subLayerNum = 1;
            printColor = new uint[8];
            nlayersource = 0;
            nEnableLine = 0x101;
            reverse = new byte[2];
            ndatasource = new byte[8];
        }

        public LayerSetting Clone()
        {
            LayerSetting ret = (LayerSetting)MemberwiseClone();
            return ret;
        }

    }

    public struct LayoutSetting
    {
        public byte layerNum;//层数
        public byte baseLayerIndex;//最小层
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public LayerSetting[] layerSetting;
        public uint nLayer;//用每个位表示一个行是否使用

        public LayoutSetting(int num)
        {
            layerNum = (byte)num;
            baseLayerIndex = 0;
            layerSetting = new LayerSetting[8];
            for (int i = 0; i < layerSetting.Length; i++)
            {
                layerSetting[i] = new LayerSetting(1);
            }
            nLayer = 1;
        }

        public LayoutSetting Clone()
        {
            LayoutSetting ret = (LayoutSetting)MemberwiseClone();
            ret.layerSetting = (LayerSetting[])this.layerSetting.Clone();
            return ret;
        }
    };


    public class LayoutSettingClass
    {
        private string _name;
        private bool _specialLayout;
        private int _specialYSpace;
        private LayoutSetting _layout;

        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        public bool SpecialLayout
        {
            set { _specialLayout = value; }
            get { return _specialLayout; }
        }

        public int SpecialYSpace
        {
            set { _specialYSpace = value; }
            get { return _specialYSpace; }
        }

        public LayoutSetting Layout
        {
            set { _layout = value; }
            get { return _layout; }
        }

        public LayoutSettingClass()
        {
            _layout = new LayoutSetting(1);
        }
    }

    public class LayoutSettingClassList
    {
        public List<LayoutSettingClass> Items;

        public LayoutSettingClassList()
        {
            Items = new List<LayoutSettingClass>();
        }
    };

    public enum DataSourceType : byte
    {
        Normal = 0,
        Data1 = 1,
        Data2 = 2,
        Data3 = 3,
        Data4 = 4,
        Grey = 5
    };


}