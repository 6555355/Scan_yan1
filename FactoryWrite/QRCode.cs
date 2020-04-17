using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BYHXPrinterManager;
using BYHXPrinterManager.Setting;
using System.Runtime.InteropServices;

namespace FactoryWrite
{
    public partial class QRCode : Form
    {
        private bool GetQRCodeFlag;
        private int TextLength = 35;
        HEAD_BOARD_TYPE headType;
        private SPrinterProperty property;
        private int HeadNumPerBoard = 0;
        private int HeadBoardIndex = 0;
        public QRCode()
        {
            InitializeComponent();
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            property = sp;
            //int headNum = 0;
            headType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);

            if (CoreInterface.IsS_system())
            {
                USER_SET_INFORMATION info = new USER_SET_INFORMATION();
                CoreInterface.GetUserSetInfo(ref info);
                comboBox_HeadBoardIndex.Items.Clear();
                for (int i = 0; i < info.HeadBoardNum; i++)
                {
                    comboBox_HeadBoardIndex.Items.Add((i + 1).ToString("D"));
                }
                if (comboBox_HeadBoardIndex.Items.Count > 0)
                {
                    comboBox_HeadBoardIndex.SelectedIndex = 0;
                }
            }
            else
            {
                //headNum = HeadNumPerBoard;
                comboBox_HeadBoardIndex.Items.Clear();
                comboBox_HeadBoardIndex.Items.Add("1");
                comboBox_HeadBoardIndex.SelectedIndex = 0;
            }

            switch (headType)
            {
                case HEAD_BOARD_TYPE.EPSON_S2840_4H:
                case HEAD_BOARD_TYPE.EPSON_I3200_4H_8DRV:
                    {
                        HeadNumPerBoard = 4;
                    }
                    break;
                case HEAD_BOARD_TYPE.EPSON_S1600_8H:
                    {
                        HeadNumPerBoard = 8;
                    }
                    break;
            }
            for (int i = 0; i < HeadNumPerBoard; i++)
            {
                LabelTextBox textBox1 = new LabelTextBox
                {
                    String = "喷头" + i.ToString(),
                    Text = "",
                    MsgStringVisible = true,
                    MsgString = 2,
                    TextLength = TextLength,
                }; 
                LabelTextBox textBox2 = new LabelTextBox
                {
                    String = "喷头" + i.ToString(),
                    Text = "",
                    MsgStringVisible = false,
                    MsgString = 2,
                    TextLength = TextLength,
                };
                flowLayoutPanel1.Controls.Add(textBox1);
                flowLayoutPanel2.Controls.Add(textBox2);
            }
        }

        private void button_Get_Click(object sender, EventArgs e)
        {
            if (comboBox_HeadBoardIndex.SelectedIndex < 0)
            {
                MessageBox.Show(ResString.GetResString("inputerror"));
                return;
            }
            GetQRCodeFlag = false;
            for (int i = 0; i < HeadNumPerBoard; i++)
            {
                LabelTextBox textBox1 = flowLayoutPanel1.Controls[i] as LabelTextBox;
                LabelTextBox textBox2 = flowLayoutPanel2.Controls[i] as LabelTextBox;
                if (textBox1 != null && textBox2 != null)
                {
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                    textBox1.MsgString = 2;
                    textBox1.TextBoxBackColor = SystemColors.Window;
                    textBox2.TextBoxBackColor = SystemColors.Window;
                }
                else
                {
                    //MessageBox.Show(@"Control Is NULL!");
                    return;
                }
            }
            byte[] val = new byte[2];
            val[0] = 0xC5;
            val[1] = 0xD4;
            uint bufsize = (uint)val.Length;
            ushort value = (ushort)comboBox_HeadBoardIndex.SelectedIndex;
            if (CoreInterface.SetEpsonEP0Cmd(0x80, val, ref bufsize, value, 0) == 0)
            {
                MessageBox.Show(ResString.GetResString("SendCmdError"));
            }
            else
            {
                panel2.Enabled = false;
            }
            Thread thread = new Thread(GetQRCode_TimeOut) { IsBackground = true };
            thread.Start();
        }

        private void GetQRCode_TimeOut()
        {
            for (int i = 0; i < 50; i++)
            {
                if (!GetQRCodeFlag)
                {
                    Thread.Sleep(200);
                }
                else
                {
                    break;
                }
            }
            this.Invoke(new Action<bool>(o => panel2.Enabled = o), true);
            if (!GetQRCodeFlag)
            {
                MessageBox.Show(ResString.GetResString("GetQRCodeFailed"));
            }
        }

        public void SetQrCode(byte[] data)
        {
            List<QRCodeStruct> qrCodeStructList=new List<QRCodeStruct>();
            byte headNum = data[1];
            for (int i = 0; i < headNum; i++)
            {
                byte headIndex = data[2 + (TextLength + 1) * i];
                byte[] qRbytes = new byte[TextLength];
                Array.Copy(data, 2 + (TextLength + 1) * i + 1, qRbytes, 0, TextLength);
                QRCodeStruct qrCodeStruct = new QRCodeStruct
                {
                    HeadIndex = headIndex,
                    QRbytes = new List<byte>(qRbytes)
                };
                qrCodeStructList.Add(qrCodeStruct);
            }
            foreach (QRCodeStruct qrCodeStruct in qrCodeStructList)
            {
                LabelTextBox textBox = flowLayoutPanel1.Controls[qrCodeStruct.HeadIndex] as LabelTextBox;
                if (textBox != null)
                {
                    textBox.Text = Encoding.ASCII.GetString(qrCodeStruct.QRbytes.ToArray());
                    textBox.TextBoxBackColor = SystemColors.Window;
                    textBox.MsgString = 2;
                }
            }
            panel2.Enabled = true;
            GetQRCodeFlag = true;
        }

        public void SetQrCodeFromHead(byte[] data)
        {
            List<QRCodeStruct_Head> qrCodeStructList = new List<QRCodeStruct_Head>();
            byte headNum = data[1];
            for (int i = 0; i < headNum; i++)
            {
                byte headIndex = data[2 + (TextLength + 2) * i];
                byte flag = data[2 + (TextLength + 2) * i + 1];
                byte[] qRbytes = new byte[TextLength];
                Array.Copy(data, 2 + (TextLength + 2) * i + 2, qRbytes, 0, TextLength);
                QRCodeStruct_Head qrCodeStruct = new QRCodeStruct_Head
                {
                    HeadIndex = headIndex,
                    rdFlag = flag,
                    QRbytes = new List<byte>(qRbytes)
                };
                qrCodeStructList.Add(qrCodeStruct);
            }
            foreach (QRCodeStruct_Head qrCodeStruct in qrCodeStructList)
            {
                if (qrCodeStruct.rdFlag == 1)
                {
                    LabelTextBox textBox = flowLayoutPanel1.Controls[qrCodeStruct.HeadIndex] as LabelTextBox;
                    LabelTextBox textBox1 = flowLayoutPanel2.Controls[qrCodeStruct.HeadIndex] as LabelTextBox;
                    if (textBox != null)
                    {
                        textBox.Text = Encoding.ASCII.GetString(qrCodeStruct.QRbytes.ToArray());
                        textBox.TextBoxBackColor = SystemColors.Window;
                        textBox.MsgString = 2;
                    }
                    if (textBox1 != null)
                    {
                        textBox1.Text = Encoding.ASCII.GetString(qrCodeStruct.QRbytes.ToArray());
                        textBox1.TextBoxBackColor = SystemColors.Window;
                        textBox1.MsgString = 2;
                    }
                }
            }
            panel2.Enabled = true;
            GetQRCodeFlag = true;
        }

        private void button_Set_Click(object sender, EventArgs e)
        {
            if (comboBox_HeadBoardIndex.SelectedIndex < 0)
            {
                MessageBox.Show(ResString.GetResString("inputerror"));
                return;
            }
            HeadBoardIndex = comboBox_HeadBoardIndex.SelectedIndex;
            if (flowLayoutPanel1.Controls.Count == flowLayoutPanel2.Controls.Count)
            {
                bool error = false;
                List<string> qrCodeList = new List<string>();
                int count = flowLayoutPanel1.Controls.Count;
                for (int i = 0; i < count; i++)
                {
                    LabelTextBox textBox1 = flowLayoutPanel1.Controls[i] as LabelTextBox;
                    LabelTextBox textBox2 = flowLayoutPanel2.Controls[i] as LabelTextBox;
                    if (textBox1 != null && textBox2 != null)
                    {
                        textBox1.MsgString = 2;
                        qrCodeList.Add(textBox1.Text);
                        if (textBox1.Text == string.Empty && textBox2.Text == string.Empty)
                        {
                            textBox1.TextBoxBackColor = SystemColors.Window;
                            textBox2.TextBoxBackColor = SystemColors.Window;
                        }
                        else
                        {
                            if (textBox1.Text.Length != textBox1.TextLength ||
                                textBox2.Text.Length != textBox2.TextLength ||
                                textBox1.Text != textBox2.Text)
                            {
                                textBox1.TextBoxBackColor = Color.Red;
                                textBox2.TextBoxBackColor = Color.Red;
                                error = true;
                            }
                            else
                            {
                                textBox1.TextBoxBackColor = SystemColors.Window;
                                textBox2.TextBoxBackColor = SystemColors.Window;
                            }
                        }
                    }
                    else
                    {
                        //MessageBox.Show(@"Control Is NULL!");
                        return;
                    }
                }
                if (error)
                {
                    MessageBox.Show(ResString.GetResString("inputerror"));
                    return;
                }
                if (!qrCodeList.TrueForAll(qa => qa == string.Empty))
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(SetQrCodeThread)) { IsBackground = true };
                    thread.Start(qrCodeList);
                    panel2.Enabled = false;
                }
            }
            //else
            //{
            //    MessageBox.Show(@"Control's Count Error!");
            //}
        }

        private bool feedBack = false;
        private bool feedBackData = false;
        public void FeedBack(byte[] data)
        {
            feedBack = true;
            feedBackData = data[1] == 0;
        } 

        private void SetQrCodeThread(Object obj)
        {
            List<string> qrCodeList = obj as List<string>;
            if (qrCodeList != null)
            {
                for (int i = 0; i < qrCodeList.Count; i++)
                {
                    if (qrCodeList[i] != string.Empty)
                    {
                        feedBack = false;
                        feedBackData = false;
                        List<byte> buffer = new List<byte>();
                        byte headIndex = (byte)i;
                        buffer.Add(0xC5);
                        buffer.Add(0xD3);
                        buffer.Add(headIndex);
                        buffer.AddRange(Encoding.ASCII.GetBytes(qrCodeList[i]));
                        if (buffer.Count < (TextLength + 3))
                        {
                            buffer.AddRange(new byte[(TextLength + 3) - buffer.Count]);
                        }
                        ushort value = (ushort)HeadBoardIndex;
                        uint bufsize = (uint)buffer.Count;
                        if (CoreInterface.SetEpsonEP0Cmd(0x80, buffer.ToArray(), ref bufsize, value, 0) == 0)
                        {
                            MessageBox.Show(ResString.GetResString("SendCmdError"));
                        }
                        for (int j = 0; j < 50; j++)
                        {
                            if (feedBack)
                                break;
                            Thread.Sleep(100);
                        }
                        //while (true)
                        //{
                        //    if (feedBack)
                        //        break;
                        //}
                        this.Invoke(new Action<bool, int>((x, y) =>
                        {
                            LabelTextBox textBox = flowLayoutPanel1.Controls[y] as LabelTextBox;
                            if (textBox != null)
                                textBox.MsgString = x ? 1 : 0;
                        }), feedBackData, i);
                    }
                    
                }
            }
            this.Invoke(new Action<bool>(o => panel2.Enabled = o), true);
        }

        private void buttonGetHeadQR_Click(object sender, EventArgs e)
        {
            if (comboBox_HeadBoardIndex.SelectedIndex < 0)
            {
                MessageBox.Show(ResString.GetResString("inputerror"));
                return;
            }
            GetQRCodeFlag = false;
            for (int i = 0; i < HeadNumPerBoard; i++)
            {
                LabelTextBox textBox1 = flowLayoutPanel1.Controls[i] as LabelTextBox;
                LabelTextBox textBox2 = flowLayoutPanel2.Controls[i] as LabelTextBox;
                if (textBox1 != null && textBox2 != null)
                {
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                    textBox1.MsgString = 2;
                    textBox2.MsgString = 2;
                    textBox1.TextBoxBackColor = SystemColors.Window;
                    textBox2.TextBoxBackColor = SystemColors.Window;
                }
                else
                {
                    //MessageBox.Show(@"Control Is NULL!");
                    return;
                }
            }
            byte[] val = new byte[2];
            val[0] = 0xC5;
            val[1] = 0xE6;
            uint bufsize = (uint)val.Length;
            ushort value = (ushort)comboBox_HeadBoardIndex.SelectedIndex;
            if (CoreInterface.SetEpsonEP0Cmd(0x80, val, ref bufsize, value, 0) == 0)
            {
                MessageBox.Show(ResString.GetResString("SendCmdError"));
            }
            else
            {
                panel2.Enabled = false;
            }
            Thread thread = new Thread(GetQRCode_TimeOut) { IsBackground = true };
            thread.Start();
        }
    }

    //typedef struct HeadSn_tag
    //{
    //    uint8_t	headIndex;		//!< 喷头序号，也就是第几个喷头
    //    uint8_t	QR[MAX_LABEL_QR_LEN];	//!< 输入的QR码，该码贴在喷头上面，	需	要手动输入,由数字、特殊符号、英文字母组成，每个输入字符的值范围为0x20~0x7E
    //} HeadQR_t
    //typedef struct HeadInf_tag
    //{
    //    uint8_t	type;			//!< 当前数据类型，0xD4
    //    uint8_t	headNum;		//!< 有多少个喷头	
    //    HeadQR_t  headQR[headNum];
    //} HeadInf_t;
    struct QRCodeStruct
    {
        public byte HeadIndex;
        public List<byte> QRbytes;
    }

    struct QRCodeStruct_Head
    {
        public byte HeadIndex;
        public byte rdFlag;
        public List<byte> QRbytes;
    }


}
