using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class PrintHeadOrderTypeForm : Form
    {
        private int headBoardNum = 1;
        private List<HeadBoardPortSetting> headBoardPorts;
        private ArrayList _supportHeadList;
        public PrintHeadOrderTypeForm(int hbNum, ArrayList supportHeadList)
        {
            InitializeComponent();

            headBoardNum = hbNum;
            _supportHeadList = supportHeadList;
            headBoardPorts = new List<HeadBoardPortSetting>();
            HEAD_BOARD_TYPE headBoard = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);
            tabControl1.SuspendLayout();
            tabControl1.TabPages.Clear();
            for (int i = 0; i < headBoardNum; i++)
            {
                TabPage tabPage = new TabPage(string.Format("头板{0}", i + 1));
                HeadBoardPortSetting headBoardPort = new HeadBoardPortSetting(_supportHeadList, headBoard);
                headBoardPort.Dock = DockStyle.Fill;
                tabPage.Controls.Add(headBoardPort);
                tabControl1.TabPages.Add(tabPage);
                headBoardPorts.Add(headBoardPort);
            }
            tabControl1.ResumeLayout(true);
        }

        public void OnSettingChanged( )
        {
            
        }

        public void GetSetting( ref PrintHeadOrder headOrder)
        {
            int offset = 0;
            for (int i = 0; i < headBoardPorts.Count; i++)
            {
                byte[] buf = headBoardPorts[i].GetSetting();
                Buffer.BlockCopy(buf, 0, headOrder.Order, offset, buf.Length);
                offset += buf.Length;
            }
        }

        private void PrintHeadOrderTypeForm_Load(object sender, EventArgs e)
        {
            PrintHeadOrder headOrder = new PrintHeadOrder();
            if (EpsonLCD.GetPrintHeadOrder(ref headOrder))
            {
                for (int i = 0; i < headBoardPorts.Count; i++)
                {
                    headBoardPorts[i].OnSettingChanged(headOrder.Order,i);
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            PrintHeadOrder order = new PrintHeadOrder(null);
            GetSetting(ref order);
            EpsonLCD.SetPrintHeadOrder(order);
        }

    }
}
