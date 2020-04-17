using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BYHXPrinterManager;
using Write382;
using WriteBoardConfig;

namespace Write501
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.ShowInTaskbar = true;
            //必须先初始化BYHXSoftLock.m_DongleKeyAlarm
            BYHXSoftLock.m_DongleKeyAlarm = new DongleKeyAlarm();
            BYHXSoftLock.m_DongleKeyAlarm.EncryptDogExpired += new EventHandler(m_DongleKeyAlarm_EncryptDogExpired);
            BYHXSoftLock.m_DongleKeyAlarm.EncryptDogLast100H += new EventHandler(m_DongleKeyAlarm_EncryptDogLast100H);
            BYHXSoftLock.m_DongleKeyAlarm.LoadAndCheckDongleKeyFinished += new EventHandler(m_DongleKeyAlarm_LoadAndCheckDongleKeyFinished);
        }
        private void m_DongleKeyAlarm_EncryptDogExpired(object sender, EventArgs e)
        {

        }

        private void m_DongleKeyAlarm_EncryptDogLast100H(object sender, EventArgs e)
        {
        }

        private void m_DongleKeyAlarm_LoadAndCheckDongleKeyFinished(object sender, EventArgs e)
        {

        }


        private int _case = 0;
        /// <summary>
        /// 1=382;2:501;
        /// </summary>
        public int SelcetedCase { get { return _case; } }
        private void buttonXaar382_Click(object sender, EventArgs e)
        {
            _case = 1;
            this.Close();
        }

        private void buttonXaar501_Click(object sender, EventArgs e)
        {
            _case = 2;
            this.Close();
        }

        private void buttonKYOCERA_Click(object sender, EventArgs e)
        {
            _case = 3;
            this.Close();
        }

        public bool Start()
        {
            if (!BYHXSoftLock.m_DongleKeyAlarm.Start(this.Handle, false))
            {
                return false;
            }
            bool bEnableInktest = false;
            int funcMask = 0;
            BYHX_SL_RetValue ret = BYHXSoftLock.GetFunctionWords(ref funcMask);
            if (ret == BYHX_SL_RetValue.SUCSESS)
            {
                bEnableInktest = (funcMask & (1 << 31)) != 0;
            }
            if (!bEnableInktest)
                MessageBox.Show("加密狗权限不够!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return bEnableInktest;
        }

    }
}
