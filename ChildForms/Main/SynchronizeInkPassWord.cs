using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BYHXPrinterManager.Main
{
    public partial class SynchronizeInkPassWord : Form
    {
        public int flag = 0;
        public SynchronizeInkPassWord()
        {
            InitializeComponent();
        }
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            string passWordStr = textBoxPassWord.Text.Trim();
            if (passWordStr != string.Empty)
            {
                int passWord;
                if (int.TryParse(passWordStr, out passWord))
                {
                    SBoardInfo sBoardInfo = new SBoardInfo();
                    if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
                    {
                        string filePath =InkAreaStaticsHelper.GetInkStaticsFileName((uint) (passWord / sBoardInfo.m_nBoardManufatureID));
                        if (passWord == sBoardInfo.m_nBoardSerialNum * sBoardInfo.m_nBoardManufatureID)
                        {
                            InkAreaStaticsHelper.ClearInkQuantity();
                            InkAreaStaticsHelper.ClearPrintArea();
                            //新建文件
                            if (InkAreaStaticsHelper.SynchronizeInkAndArea())
                            {
                                MessageBox.Show(ResString.GetResString("SynchronizeInkAndAreaSuccess"),
                                    ResString.GetProductName());
                                flag = 1;
                                Close();
                            }
                            else
                            {
                                labelTips.Text = "Tips:Synchronization error!";
                            }
                        }
                        else if (File.Exists(filePath))
                        {
                            //同步文件
                            FileStream fs = new FileStream(filePath, FileMode.Open);
                            BinaryReader br = new BinaryReader(fs);
                            byte[] ink = new byte[Marshal.SizeOf(typeof(InkOfMonths))];
                            byte[] area = new byte[Marshal.SizeOf(typeof(AreaOfMonths))];
                            ink = br.ReadBytes(ink.Length);
                            area = br.ReadBytes(area.Length);
                            if (InkAreaStaticsHelper.SetInkQuantity(ink) && InkAreaStaticsHelper.SetPrintArea(area))
                            {
                                MessageBox.Show(ResString.GetResString("SynchronizeInkAndAreaSuccess"),
                                    ResString.GetProductName());
                                flag = 1;
                                Close();
                            }
                            else
                            {
                                labelTips.Text = "Tips:Synchronization error!";
                            }
                            fs.Close();
                        }
                        else
                        {
                            labelTips.Text = "Tips:Synchronization error!";
                        }
                    }
                    else
                    {
                        labelTips.Text = "Tips:Get BoardInfo error!";
                    }
                }
                else
                {
                    labelTips.Text = "Tips:Input error!";
                }
            }
        }
    }
}
