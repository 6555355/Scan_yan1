using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class HeadBoardPortSetting : UserControl
    {
        List<ComboBox> portComboBoxs = new List<ComboBox>();
        private ArrayList m_SupportHeadList = new ArrayList(); //File Vender.dll
        private int headNumPerHb = 8;
        public HeadBoardPortSetting(ArrayList supportHeadList,HEAD_BOARD_TYPE headBoardType)
        {
            InitializeComponent();

            //switch (headBoardType)
            //{
            //    //此处应该根据头板类型动态生成布局
            //}
            headNumPerHb = PubFunc.GetHeadNumPerHeadborad(headBoardType);

            m_SupportHeadList = supportHeadList;
            portComboBoxs = new List<ComboBox>()
            {
                comboBoxHead8,comboBoxHead4,
                comboBoxHead7,comboBoxHead3,
                comboBoxHead6,comboBoxHead2,
                comboBoxHead5,comboBoxHead1,
            };

            for (int i = 0; i < portComboBoxs.Count; i++)
            {
                portComboBoxs[i].Items.Clear();
                for (int j = 0; j < m_SupportHeadList.Count; j++)
                {
                    string cmode = ((VenderDisp)m_SupportHeadList[j]).DisplayName;
                    portComboBoxs[i].Items.Add(cmode);
                }
            }
        }

        public byte[] GetSetting()
        {
            byte[] buf = new byte[8];
            for (int i = 0; i < portComboBoxs.Count; i++)
            {
                if (portComboBoxs[i].SelectedIndex != -1)
                {
                    buf[i] = (byte) ((VenderDisp)m_SupportHeadList[portComboBoxs[i].SelectedIndex]).VenderID;
                }
            }
            return buf;
        }

        public void OnSettingChanged(byte[] order,int hbIndex)
        {
            for (int i = 0; i < portComboBoxs.Count; i++)
            {
                for (int j = 0; j < m_SupportHeadList.Count; j++)
                {
                    if ((byte)((VenderDisp)m_SupportHeadList[j]).VenderID == order[hbIndex*headNumPerHb+i])
                    {
                        portComboBoxs[i].SelectedIndex = j;
                    }
                }
            }
        }

    }
}
