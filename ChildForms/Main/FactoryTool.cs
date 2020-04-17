using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BYHXPrinterManager.Main
{
    public partial class FactoryTool : ByhxBaseChildForm
    {
        public FactoryTool()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        #region 参数
        /// <summary>
        /// 是否双Y
        /// </summary>
        private bool isDoubleYAxis = false;
        /// <summary>
        /// 是否双Y同步
        /// </summary>
        private bool isDoubleYSynchro = false;
        /// <summary>
        /// 是否自动刷新
        /// </summary>
        private bool isAutoRefresh = false;
        /// <summary>
        /// 当前X位置
        /// </summary>
        private int currpos_X;
        /// <summary>
        /// 当前Y位置
        /// </summary>
        private int currpos_Y;
        /// <summary>
        /// 当前Y2位置
        /// </summary>
        private int currpos_Y2;
        /// <summary>
        /// 当前Z位置
        /// </summary>
        private int currpos_Z;
        #endregion


        /// <summary>
        /// 界面加载
        /// </summary>
        private void FactoryTool_Load(object sender, EventArgs e)
        {
            ddSpeed_X.SelectedIndex = 0;
            ddSpeed_Y.SelectedIndex = 0;
            ddSpeed_Y2.SelectedIndex = 0;
            ddSpeed_Z.SelectedIndex = 0;
            isDoubleYAxis = PubFunc.IsDoubleYAxis();
            groupBox_Y2.Visible = isDoubleYAxis;
            cbDoubleYSynchro.Visible = isDoubleYAxis;
            this.Width = isDoubleYAxis ? 58+groupBox_X.Width*4 : 58+groupBox_X.Width*3;

            Thread statethread = new Thread(new ParameterizedThreadStart(GetState));
            statethread.IsBackground = false;
            statethread.Start(false);
        }
        /// <summary>
        /// 获取/刷新界面（循环线程）
        /// </summary>
        /// <param name="isActive">是否时按钮点击触发的</param>
        public void GetState(object isActive)
        {
            while (!this.IsDisposed)
            {
                if (isAutoRefresh)
                {
                    RefreshView(isActive);
                }
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// 获取/刷新界面
        /// </summary>
        /// <param name="isActive">是否时按钮点击触发的</param>
        void RefreshView(object isActive)
        {
            try
            {
                if ((bool)isActive) btnRefresh.Enabled = false;
                const int port = 1;
                const byte PRINTER_PIPECMDSIZE = 26;
                byte[] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
                //First Send Begin Updater
                m_pData[0] = 2;
                m_pData[1] = 0x2c; //Move cmd
                CoreInterface.SetPipeCmdPackage(m_pData, m_pData[0], port);

                Thread.Sleep(1000);
                BYHXPrinterManager.MotionDebugInfo info;
                string labChangeText = ResString.GetResString("FactoryTool_labChange");
                string groupBox_XText = ResString.GetResString("FactoryTool_PossionX");
                string groupBox_YText = ResString.GetResString("FactoryTool_PossionY");
                string groupBox_Y2Text = ResString.GetResString("FactoryTool_PossionY2");
                string groupBox_ZText = ResString.GetResString("FactoryTool_PossionZ");
                if (GetXAndYPos(out info))
                {
                    
                    if (!isDoubleYAxis)
                    {
                        
                        groupBox_Y.Text = string.Format(groupBox_YText, info.yPos);
                        if ((bool)isActive)
                        {
                            labChange_Y.Text = string.Format(labChangeText, info.yPos - currpos_Y);
                            currpos_Y = info.yPos;
                        }
                    }
                    groupBox_X.Text = string.Format(groupBox_XText, info.xPos);
                    groupBox_Z.Text = string.Format(groupBox_ZText, info.nDebugInt1);
                    if ((bool)isActive)
                    {
                        labChange_X.Text = string.Format(labChangeText, info.xPos - currpos_X);
                        labChange_Z.Text = string.Format(labChangeText, info.nDebugInt1 - currpos_Z);
                        currpos_X = info.xPos;
                        currpos_Z = info.nDebugInt1;
                    }
                }
                if (isDoubleYAxis)
                {
                    MotionDebugInfoFromARM motionDebugInfo = new MotionDebugInfoFromARM();
                    if (GetYHomePosition(ref motionDebugInfo))
                    {
                        groupBox_Y.Text = string.Format(groupBox_YText, motionDebugInfo.yPosition);
                        groupBox_Y2.Text = string.Format(groupBox_Y2Text, motionDebugInfo.y1Position);
                        if ((bool)isActive)
                        {
                            labChange_Y.Text = string.Format(labChangeText, motionDebugInfo.yPosition - currpos_Y);
                            labChange_Y2.Text = string.Format(labChangeText, motionDebugInfo.y1Position - currpos_Y2);
                            currpos_Y = motionDebugInfo.yPosition;
                            currpos_Y2 = motionDebugInfo.y1Position;
                        }
                    }
                }

                byte[] buf = new byte[8 + 32];
                uint lenght = (uint)buf.Length;
                CoreInterface.GetEpsonEP0Cmd(0x82, buf, ref lenght, 0, 8);
                CoreInterface.GetEpsonEP0Cmd(0x82, buf, ref lenght, 0, 9);

                int[] pwmList = new int[6];
                int[] GratingList = new int[6];
                byte[] Buffer = new byte[64];
                uint len = (uint)Buffer.Length;
                if (CoreInterface.GetEpsonEP0Cmd(0x94, Buffer, ref len, 0, 1) != 0)
                {
                    for(int i=0;i<6;i++)
                    {
                        pwmList[i] = BitConverter.ToInt32(Buffer, 2 + i * 4);

                        GratingList[i] = BitConverter.ToInt32(Buffer, 2 + (7 + i) * 4);
                    }

                    labelPWMValue1.Text = pwmList[0].ToString();
                    labelPWMValue2.Text = pwmList[1].ToString();
                    labelPWMValue3.Text = pwmList[2].ToString();
                    labelPWMValue5.Text = pwmList[4].ToString();

                    labelGValue1.Text = GratingList[0].ToString();
                    labelGValue2.Text = GratingList[1].ToString();
                    labelGValue3.Text = GratingList[2].ToString();
                    labelGValue5.Text = GratingList[4].ToString();
                    
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if ((bool)isActive) btnRefresh.Enabled = true;
            }
        }
        /// <summary>
        /// 从DSP获取X和Y的位置
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private bool GetXAndYPos(out  BYHXPrinterManager.MotionDebugInfo info)
        {
            byte[] buf = new byte[64];
            info = new BYHXPrinterManager.MotionDebugInfo();
            if (CoreInterface.GetDebugInfo(buf, 64) != 0)
            {
                int xposIndex = 4;
                info.xPos = (buf[xposIndex + 3] << 24) + (buf[xposIndex + 2] << 16) + (buf[xposIndex + 1] << 8) + buf[xposIndex + 0];
                xposIndex += 4;
                info.yPos = (buf[xposIndex + 3] << 24) + (buf[xposIndex + 2] << 16) + (buf[xposIndex + 1] << 8) + buf[xposIndex + 0];
                xposIndex += 4;
                info.nDebugInt1 = (buf[xposIndex + 3] << 24) + (buf[xposIndex + 2] << 16) + (buf[xposIndex + 1] << 8) + buf[xposIndex + 0];
                xposIndex += 4;
                info.nDebugInt2 = (buf[xposIndex + 3] << 24) + (buf[xposIndex + 2] << 16) + (buf[xposIndex + 1] << 8) + buf[xposIndex + 0];
                xposIndex += 4;
                info.nDebugInt3 = (buf[xposIndex + 3] << 24) + (buf[xposIndex + 2] << 16) + (buf[xposIndex + 1] << 8) + buf[xposIndex + 0];
                xposIndex += 4;
                info.nDebugInt4 = (buf[xposIndex + 3] << 24) + (buf[xposIndex + 2] << 16) + (buf[xposIndex + 1] << 8) + buf[xposIndex + 0];
                return true;
            }
            return false;
        }
        /// <summary>
        /// 从ARM获取Y和Y1的位置
        /// </summary>
        /// <param name="position"></param>
        private bool GetYHomePosition(ref MotionDebugInfoFromARM position)
        {
            byte[] val = new byte[64];
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x43, val, ref bufsize, 0, 0);
            if (ret == 0)
            {
                MessageBox.Show(ResString.GetResString("GetYhomepositionfialed"));
                return false;
            }
            else
            {
                int xposIndex = 2;
                position.xPosition = (val[xposIndex + 3] << 24) + (val[xposIndex + 2] << 16) + (val[xposIndex + 1] << 8) + val[xposIndex + 0];
                xposIndex += 4;
                position.yPosition = (val[xposIndex + 3] << 24) + (val[xposIndex + 2] << 16) + (val[xposIndex + 1] << 8) + val[xposIndex + 0];
                xposIndex += 4;
                position.zPosition = (val[xposIndex + 3] << 24) + (val[xposIndex + 2] << 16) + (val[xposIndex + 1] << 8) + val[xposIndex + 0];
                xposIndex += 4;
                position.fPosition = (val[xposIndex + 3] << 24) + (val[xposIndex + 2] << 16) + (val[xposIndex + 1] << 8) + val[xposIndex + 0];
                xposIndex += 4;
                position.y1Position = (val[xposIndex + 3] << 24) + (val[xposIndex + 2] << 16) + (val[xposIndex + 1] << 8) + val[xposIndex + 0];
                return true;
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Thread refreshThread = new Thread(new ParameterizedThreadStart(RefreshView));
                refreshThread.IsBackground = true;
                refreshThread.Start(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnRefresh.Enabled = true;
            }
        }


        /// <summary>
        /// 启动/关闭 双轴同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDoubleYSynchro_CheckedChanged(object sender, EventArgs e)
        {
            isDoubleYSynchro = cbDoubleYSynchro.Checked;
            groupBox_Y2.Enabled = !isDoubleYSynchro;
        }
        /// <summary>
        /// 启动/关闭 自动刷新
        /// </summary>
        private void cbAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            isAutoRefresh = cbAutoRefresh.Checked;
            labChange_X.Visible = !cbAutoRefresh.Checked;
            labChange_Y.Visible = !cbAutoRefresh.Checked;
            labChange_Y2.Visible = !cbAutoRefresh.Checked;
            labChange_Z.Visible = !cbAutoRefresh.Checked;
        }
        /// <summary>
        /// 定长移动X
        /// </summary>
        private void cbFixedLength_X_CheckedChanged(object sender, EventArgs e)
        {
            nLengh_X.Enabled = cbFixedLength_X.Checked;
        }
        /// <summary>
        /// 定长移动Y
        /// </summary>
        private void cbFixedLength_Y_CheckedChanged(object sender, EventArgs e)
        {
            nLengh_Y.Enabled = cbFixedLength_Y.Checked;
        }
        /// <summary>
        /// 定长移动Y2
        /// </summary>
        private void cbFixedLength_Y2_CheckedChanged(object sender, EventArgs e)
        {
            nLengh_Y2.Enabled = cbFixedLength_Y2.Checked;
        }
        /// <summary>
        /// 定长移动Z
        /// </summary>
        private void cbFixedLength_Z_CheckedChanged(object sender, EventArgs e)
        {
            nLengh_Z.Enabled = cbFixedLength_Z.Checked;
        }

        #region 移动

        /// <summary>
        /// 点击停止
        /// </summary>
        private void btnStop_Click(object sender, EventArgs e)
        {
            StopMove();
        }
        /// <summary>
        /// 停止移动
        /// </summary>
        void StopMove()
        {
            CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove, 0);
        }

        /// <summary>
        /// 普通移动命令（双Y会同步移动）
        /// </summary>
        /// <param name="dir">方向</param>
        /// <param name="speed">速度</param>
        /// <param name="len">移动长度（0为一直移动）</param>
        private void MoveNormal(int dir, int speed, int len)
        {
            const int port = 1;
            const byte PRINTER_PIPECMDSIZE = 26;
            byte[] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
            m_pData[0] = 6 + 2;
            m_pData[1] = 0x31; //Move cmd

            m_pData[2] = (byte)dir; //方向
            m_pData[3] = (byte)speed; //速度
            m_pData[4] = (byte)(len & 0xff);
            m_pData[5] = (byte)((len >> 8) & 0xff);
            m_pData[6] = (byte)((len >> 16) & 0xff);
            m_pData[7] = (byte)((len >> 24) & 0xff);

            CoreInterface.SetPipeCmdPackage(m_pData, m_pData[0], port);
        }
        /// <summary>
        /// 单Y移动
        /// </summary>
        /// <param name="isY1">是否是Y1</param>
        /// <param name="isForward">是否是正向移动</param>
        /// <param name="speed">速度</param>
        /// <param name="len">移动长度（0为一直移动）</param>
        private void MoveSingleY(bool isY1, bool isForward, int speed, int len)
        {
            const int port = 1;
            const byte PRINTER_PIPECMDSIZE = 26;
            byte[] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];

            m_pData[0] = 10 + 2;
            m_pData[1] = 0x65; //Move cmd

            m_pData[2] = (byte)(isY1 ? 0x2 : 0x10);  //Move cmd 0x2:Y1 0x10:Y2
            m_pData[3] = (byte)(isForward ? 0x1 : 0x0); //方向 1正0反
            m_pData[4] = (byte)(speed & 0xff);
            m_pData[5] = (byte)((speed >> 8) & 0xff);
            m_pData[6] = (byte)((speed >> 16) & 0xff);
            m_pData[7] = (byte)((speed >> 24) & 0xff);
            m_pData[8] = (byte)(len & 0xff); //Move cmd
            m_pData[9] = (byte)((len >> 8) & 0xff); //Move cmd
            m_pData[10] = (byte)((len >> 16) & 0xff); //Move cmd
            m_pData[11] = (byte)((len >> 24) & 0xff); //Move cmd

            CoreInterface.SetPipeCmdPackage(m_pData, m_pData[0], port);
        }
        #region X移动
        private void btnForward_X_Click(object sender, EventArgs e)
        {
            if (cbFixedLength_X.Checked)
            {
                int dir = (int)MoveDirectionEnum.Left;
                int speed = int.Parse(ddSpeed_X.Text);
                int len = (int)nLengh_X.Value;
                MoveNormal(dir, speed, len);
            }
        }

        private void btnForward_X_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_X.Checked)
                return;
            int dir = (int)MoveDirectionEnum.Left;
            int speed = int.Parse(ddSpeed_X.Text);
            MoveNormal(dir, speed, 0);
        }

        private void btnForward_X_MouseUp(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_X.Checked)
                return;
            StopMove();
        }


        private void btnBack_X_Click(object sender, EventArgs e)
        {
            if (cbFixedLength_X.Checked)
            {
                int dir = (int)MoveDirectionEnum.Right;
                int speed = int.Parse(ddSpeed_X.Text);
                int len = (int)nLengh_X.Value;
                MoveNormal(dir, speed, len);
            }
        }

        private void btnBack_X_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_X.Checked)
                return;
            int dir = (int)MoveDirectionEnum.Right;
            int speed = int.Parse(ddSpeed_X.Text);
            MoveNormal(dir, speed, 0);
        }

        private void btnBack_X_MouseUp(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_X.Checked)
                return;
            StopMove();
        }
        #endregion
        #region Y移动

        private void btnForward_Y_Click(object sender, EventArgs e)
        {
            if (cbFixedLength_Y.Checked)
            {
                int dir = (int)MoveDirectionEnum.Up;
                int speed = int.Parse(ddSpeed_Y.Text);
                int len = (int)nLengh_Y.Value;
                if (isDoubleYSynchro)
                {
                    MoveNormal(dir, speed, len);
                }
                else
                {
                    MoveSingleY(true, true, speed, len);
                }
            }
        }

        private void btnForward_Y_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_Y.Checked)
                return;
            int dir = (int)MoveDirectionEnum.Up;
            int speed = int.Parse(ddSpeed_Y.Text);
            if (isDoubleYSynchro)
            {
                MoveNormal(dir, speed, 0);
            }
            else
            {
                MoveSingleY(true, true, speed, 0);
            }
        }

        private void btnForward_Y_MouseUp(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_Y.Checked)
                return;
            StopMove();
        }

        private void btnBack_Y_Click(object sender, EventArgs e)
        {
            if (cbFixedLength_Y.Checked)
            {
                int dir = (int)MoveDirectionEnum.Down;
                int speed = int.Parse(ddSpeed_Y.Text);
                int len = (int)nLengh_Y.Value;
                if (isDoubleYSynchro)
                {
                    MoveNormal(dir, speed, len);
                }
                else
                {
                    MoveSingleY(true, false, speed, len);
                }
            }
        }

        private void btnBack_Y_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_Y.Checked)
                return;
            int dir = (int)MoveDirectionEnum.Down;
            int speed = int.Parse(ddSpeed_Z.Text);
            if (isDoubleYSynchro)
            {
                MoveNormal(dir, speed, 0);
            }
            else
            {
                MoveSingleY(true, false, speed, 0);
            }
        }

        private void btnBack_Y_MouseUp(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_Y.Checked)
                return;
            StopMove();
        }

        #endregion
        #region Y2移动
        private void btnForward_Y2_Click(object sender, EventArgs e)
        {
            if (cbFixedLength_Y2.Checked)
            {
                int speed = int.Parse(ddSpeed_Y2.Text);
                int len = (int)nLengh_Y2.Value;
                MoveSingleY(false, true, speed, len);
            }
        }

        private void btnForward_Y2_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_Y2.Checked)
                return;
            int speed = int.Parse(ddSpeed_Y2.Text);
            MoveSingleY(false, true, speed, 0);
        }

        private void btnForward_Y2_MouseUp(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_Y2.Checked)
                return;
            StopMove();
        }

        private void btnBack_Y2_Click(object sender, EventArgs e)
        {
            if (cbFixedLength_Y2.Checked)
            {
                int speed = int.Parse(ddSpeed_Y2.Text);
                int len = (int)nLengh_Y2.Value;
                MoveSingleY(false, false, speed, len);
            }
        }

        private void btnBack_Y2_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_Y2.Checked)
                return;
            int speed = int.Parse(ddSpeed_Y2.Text);
            MoveSingleY(false, false, speed, 0);
        }

        private void btnBack_Y2_MouseUp(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_Y2.Checked)
                return;
            StopMove();
        }

        #endregion
        #region Z移动

        private void btnForward_Z_Click(object sender, EventArgs e)
        {
            if (cbFixedLength_Z.Checked)
            {
                int dir = (int)MoveDirectionEnum.Up_Z;
                int speed = int.Parse(ddSpeed_Z.Text);
                int len = (int)nLengh_Z.Value;
                MoveNormal(dir, speed, len);
            }
        }

        private void btnForward_Z_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_Z.Checked)
                return;
            int dir = (int)MoveDirectionEnum.Up_Z;
            int speed = int.Parse(ddSpeed_Z.Text);
            MoveNormal(dir, speed, 0);
        }

        private void btnForward_Z_MouseUp(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_Z.Checked)
                return;
            StopMove();
        }

        private void btnBack_Z_Click(object sender, EventArgs e)
        {
            if (cbFixedLength_Z.Checked)
            {
                int dir = (int)MoveDirectionEnum.Down_Z;
                int speed = int.Parse(ddSpeed_Z.Text);
                int len = (int)nLengh_Z.Value;
                MoveNormal(dir, speed, len);
            }
        }

        private void btnBack_Z_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_Z.Checked)
                return;
            int dir = (int)MoveDirectionEnum.Down_Z;
            int speed = int.Parse(ddSpeed_Z.Text);
            MoveNormal(dir, speed, 0);
        }

        private void btnBack_Z_MouseUp(object sender, MouseEventArgs e)
        {
            if (cbFixedLength_Z.Checked)
                return;
            StopMove();
        }

        #endregion

        #endregion

        #region Ep6 更新状态
        public void IOStatusRefresh(byte[] buf)
        {
            byte stateLength = buf[7];
            uint stateData = BitConverter.ToUInt32(buf, 8);
            if (stateLength == 14)
            {
                labIO_XO.BackColor = ((stateData & 0x1) == 0) ? Color.LightCoral : Color.LightGreen;
                labIO_YO.BackColor = ((stateData >> 1 & 0x1) == 0) ? Color.LightCoral : Color.LightGreen;
                labIO_Y2O.BackColor = ((stateData >> 9 & 0x1) == 0) ? Color.LightCoral : Color.LightGreen;
                labIO_ZO.BackColor = ((stateData >> 2 & 0x1) == 0) ? Color.LightCoral : Color.LightGreen;
                labIO_XE.BackColor = ((stateData >> 3 & 0x1) == 0) ? Color.LightCoral : Color.LightGreen;
                labIO_YE.BackColor = ((stateData >> 4 & 0x1) == 0) ? Color.LightCoral : Color.LightGreen;
                labIO_Y2E.BackColor = ((stateData >> 4 & 0x1) == 0) ? Color.LightCoral : Color.LightGreen;
                labIO_ZE.BackColor = ((stateData >> 5 & 0x1) == 0) ? Color.LightCoral : Color.LightGreen;
                labIO1.BackColor = ((stateData >> 6 & 0x1) == 0) ? Color.LightCoral : Color.LightGreen;
                labIO2.BackColor = ((stateData >> 7 & 0x1) != 0) ? Color.LightCoral : Color.LightGreen;
                labIO3.BackColor = ((stateData >> 8 & 0x1) == 0) ? Color.LightCoral : Color.LightGreen;
                labIO4.BackColor = ((stateData >> 9 & 0x1) == 0) ? Color.LightCoral : Color.LightGreen;
                labIO5.BackColor = ((stateData >> 11 & 0x1) != 0) ? Color.LightCoral : Color.LightGreen;
            }
            else
            {
                labIO6.BackColor = ((stateData & 0x1) != 0) ? Color.LightCoral : Color.LightGreen;
                labIO7.BackColor = ((stateData >> 1 & 0x1) != 0) ? Color.LightCoral : Color.LightGreen;
                labIO8.BackColor = ((stateData >> 2 & 0x1) != 0) ? Color.LightCoral : Color.LightGreen;
                labIO9.BackColor = ((stateData >> 3 & 0x1) != 0) ? Color.LightCoral : Color.LightGreen;
                labIO10.BackColor = ((stateData >> 4 & 0x1) != 0) ? Color.LightCoral : Color.LightGreen;
            }
        }

        public void UsePrinterStatusUpdateButtonStates(JetStatusEnum status)
        {
            if (status == JetStatusEnum.Moving)
            {
                if (cbFixedLength_X.Checked) groupBox_X.Enabled = false;
                if (cbFixedLength_Y.Checked) groupBox_Y.Enabled = false;
                if (cbFixedLength_Y2.Checked) groupBox_Y2.Enabled = false;
                if (cbFixedLength_Z.Checked) groupBox_Z.Enabled = false;
            }
            else
            {
                if (cbFixedLength_X.Checked) groupBox_X.Enabled = true;
                if (cbFixedLength_Y.Checked) groupBox_Y.Enabled = true;
                if (cbFixedLength_Y2.Checked) groupBox_Y2.Enabled = true;
                if (cbFixedLength_Z.Checked) groupBox_Z.Enabled = true;
            }
        }
        #endregion
    }
}
