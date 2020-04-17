using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class SelectInkTypeForm : Form
    {
        private byte inkNum = 1;
        private string inkDataTagVersion = "Unknowed";
        private List<string> inkNames = new List<string>();
        private byte selectedInkType;
        private AllParam m_allParma;
        /// <summary>
        /// 支持的墨水类型个数
        /// </summary>
        public byte InkNum
        {
            get { return inkNum; }
            set { inkNum = value; }
        }

        /// <summary>
        /// 墨水曲线数据包版本
        /// </summary>
        public string InkDataTagVersion
        {
            get { return inkDataTagVersion; }
            set { inkDataTagVersion = value; }
        }

        /// <summary>
        /// 供选择的墨水类型名称列表
        /// </summary>
        public List<string> InkNames
        {
            get { return inkNames; }
            set { inkNames = value; }
        }

        /// <summary>
        /// 选择的墨水类型，有效值从1开始
        /// </summary>
        public byte SelectedInkType
        {
            get { return selectedInkType; }
            set { selectedInkType = value; }
        }

        public SelectInkTypeForm(AllParam allParam)
        {
            InitializeComponent();
            m_allParma = allParam;
            InitializeData();
        }
      
        /// <summary>
        /// 初始化墨水曲线数据包信息数据
        /// </summary>
        private void InitializeData()
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
            byte[] buffer=new byte[64];
            uint bufferSize = (uint)buffer.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x68,buffer,ref bufferSize,0,0x03);
            if (ret == 0)
            {
                MessageBox.Show(string.Format("GetEpsonEP0Cmd Error!cmd {0},index {1}", 0x68, 0x03));
            }
            else
            {
                byte[] bufferData = buffer.Skip(2).ToArray();
                if (bufferData.Length >= 16)
                {
                    InkNum = bufferData[0];
                    InkDataTagVersion = Encoding.ASCII.GetString(bufferData.Skip(4).Take(12).ToArray());
                }
                if (bufferData.Length>= (16 + 8 * InkNum))
                {
                    var inkNamesData = bufferData.Skip(16).ToArray();
                    for (int i = 0; i < InkNum; i++)
                    {
                        InkNames.Add(Encoding.ASCII.GetString(inkNamesData, 8 * i, 8));
                    }
                }
            }
            ;
            if (!PubFunc.GetCurInkType(ref selectedInkType))
            {
                MessageBox.Show(string.Format("GetEpsonEP0Cmd Error!cmd {0},index {1}", 0x68, 0x02));
            }          
            UpdateUI();
        }

        /// <summary>
        /// 更新界面控件数据
        /// </summary>
        private void UpdateUI()
        {
            labelInkDataTagVersion.Text = inkDataTagVersion;
            comboBoxInkTypes.Items.Clear();
            for (int i = 0; i < InkNames.Count; i++)
            {
                comboBoxInkTypes.Items.Add(InkNames[i]);
            }
            comboBoxInkTypes.SelectedIndex = SelectedInkType - 1;
        }

       
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (comboBoxInkTypes.SelectedIndex < 0)
            {
                return;
            }
            var newSelectedInkType= (byte)(comboBoxInkTypes.SelectedIndex + 1);
            if(newSelectedInkType==SelectedInkType)
            {
                return;
            }
            int ret = PubFunc.SwitchInkType(m_allParma, SelectedInkType, newSelectedInkType);
            SelectedInkType = newSelectedInkType;//应用新的墨水类型
            if (ret ==1) 
            {          
                MessageBox.Show("应用成功！");
            }
            else if (ret == -1)
            {
                MessageBox.Show(string.Format("SetEpsonEP0Cmd Error!cmd {0},index {1}", 0x68, 0x02));
                return;
            }
            else if (ret == -2)
            {
                MessageBox.Show(string.Format("UpdatePrinterSetting Error!cmd {0}", 0x04));
            }
            
        }       
    }
}
