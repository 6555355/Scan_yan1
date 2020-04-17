using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using BYHXPrinterManager;
using PrinterStubC.Utility;

namespace PrinterStubC.Common
{
    public static class UIFunctionOnOff
    {
        private static volatile bool bLoaded = false;
        private static volatile XmlElement root = null;

        /// <summary>
        /// 预览图旋转180度,docan为了界面预览和机器实际出图一致
        /// </summary>
        public static bool PreviewRotate180
        {
            get
            {
                if (SPrinterProperty.IsDongChuan_Rich_G6_Flat_UV())
                    return true;
                bool? ret = GetConfigValueFromFile("PreviewRotate180");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

	    /// <summary>
        /// 是否支持全局反向打印
        /// </summary>
        public static bool SupportGlogalAlternatingPrint
        {
            get
            {
                bool? ret = GetConfigValueFromFile("SupportGlogalAlternatingPrint");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        /// <summary>
        /// 是否支持双平台
        /// </summary>
        public static bool SurpportDoublePlatform
        {
            get
            {
                bool? ret = GetConfigValueFromFile("SurpportDoublePlatform");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        /// <summary>
        /// 卷轴机类似平板应用
        /// 作业间距作为y原点[卷轴本来没有y原点概念的]
        /// </summary>
        /// <returns></returns>
        public static bool SurpportJobSpaceAsOriginY
        {
            get
            {
                bool? ret = GetConfigValueFromFile("SurpportJobSpaceAsOriginY");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }
        /// <summary>
        /// 层间暂停由Fw升级包实现
        /// </summary>
        public static bool AutoPausePerPageControlByFw  // 
        {
            get
            {
                bool? ret = GetConfigValueFromFile("AutoPausePerPageControlByFw");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        /// <summary>
        /// 是否交换xy控制方向
        /// </summary>
        public static bool SwapXwithY
        {
            get
            {
                bool? ret = GetConfigValueFromFile("SwapXwithY");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        /// <summary>
        /// 是否支持4个打印方向; 单双向+正反向
        /// </summary>
        public static bool Support4PrintDir
        {
            get
            {
                bool? ret = GetConfigValueFromFile("Support4PrintDir");
                if (ret.HasValue)
                    return ret.Value;
                return true;
            }
        }

        /// <summary>
        /// 是否支持软件控制打印完y是否回原点
        /// </summary>
        public static bool SupportBackToOrgYControlBySw
        {
            get
            {
                bool? ret = GetConfigValueFromFile("SupportBackToOrgYControlBySw");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        public static bool SupportDoubleYAxis
        {
            get
            {
                bool? ret = GetConfigValueFromFile("SupportDoubleYAxis");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        public static bool SupportMotorSettingAdmin
        {
            get
            {
                bool? ret = GetConfigValueFromFile("MotorSettingAdmin");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        public static bool SupportCapping
        {
            get
            {
                bool? ret = GetConfigValueFromFile("SupportCapping");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        public static bool SupportPrintMode
        {
            get
            {
                if (SPrinterProperty.IsDocanPrintMode() || PubFunc.IsPuQi())
                    return true;
                bool? ret = GetConfigValueFromFile("SupportPrintMode");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        public static bool SupportMediaMode
        {
            get
            {
                bool? ret = GetConfigValueFromFile("SupportMediaMode");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        public static bool SupportCloseNozzle
        {
            get
            {
                bool? ret = GetConfigValueFromFile("SupportCloseNozzle");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        /// <summary>
        /// 设置Y方向最大行程（没有Y方向限位传感器）
        /// </summary>
        public static bool SupportSetYMaxLen
        {
            get
            {
                bool? ret = GetConfigValueFromFile("SupportSetYMaxLen");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }
        /// <summary>
        /// 只有工厂调试权限以上才显示实时设置
        /// </summary>
        public static bool OnlyFacUserShowRealTime
        {
            get
            {
                bool? ret = GetConfigValueFromFile("OnlyFacUserShowRealTime");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        /// <summary>
        /// 只有工厂调试权限以上才显示硬件设置菜单
        /// </summary>
        public static bool OnlyFacUserShowHwSimple
        {
            get
            {
                bool? ret = GetConfigValueFromFile("OnlyFacUserShowHwSimple");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }
        public static bool CanSetYSpeed
        {
            get
            {
                bool? ret = GetConfigValueFromFile("CanSetYSpeed");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }
        public static bool SupportDoublePrintCalibration
        {
            get
            {
                //bool? ret = GetConfigValueFromFile("SupportDoubleYAxis");
                bool? ret = GetConfigValueFromFile("DoubleSidePrint");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }


        private static bool? GetConfigValueFromFile(string configName)
        {
            string curFile = GetSettingPath();
            if (!File.Exists(curFile))
                return null; //????should be same with other case
            if (root == null && bLoaded ==false)
            {
                SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
                if (!doc.Load(curFile))
                {
                    return null;
                }
                root = doc.DocumentElement;
                bLoaded = true;
            }
            if (root != null && bLoaded)
            {
                XmlElement elemI = PubFunc.GetFirstChildByName(root, configName);
                if (elemI != null)
                {
                    bool ret;
                    bool.TryParse(elemI.InnerText, out ret);
                    return ret;
                }
            }
            return null;
        }

        private static string GetSettingPath()
        {
            ushort Vid = 0;
            ushort Pid = 0;
            int ret = CoreInterface.GetProductID(ref Vid, ref Pid);
            if (ret == 0)
            {
                string retS = Application.StartupPath + Path.DirectorySeparatorChar + "FuncOO.bin";
                return retS;
            }
            else
            {
                string path1 = Application.StartupPath + Path.DirectorySeparatorChar + Vid.ToString("X4");
                string path2 = path1 + Path.DirectorySeparatorChar + Pid.ToString("X4");
                string retS = path2 + Path.DirectorySeparatorChar + "FuncOO.bin";
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(path1);
                }
                if (!Directory.Exists(path2))
                {
                    Directory.CreateDirectory(path2);
                }
                return retS;
            }
        }

        public static bool SupportCustomCloseNozzle
        {
            get
            {
                bool? ret = GetConfigValueFromFile("CustomCloseNozzle");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }

        public static bool SupportDebugQuality
        {
            get
            {
                bool? ret = GetConfigValueFromFile("DebugQuality");
                if (ret.HasValue)
                    return ret.Value;
                return false;
            }
        }
    }
}
