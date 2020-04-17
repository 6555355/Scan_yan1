using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class LabelTextBox : UserControl
    {
        private int textLength = 0;
        public LabelTextBox()
        {
            InitializeComponent();
        }
        [Browsable(true)]
        public string Text
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }
        [Browsable(true)]
        public int TextLength
        {
            get
            {
                return textLength;
            }
            set
            {
                textLength = value;
            }
        }
        [Browsable(true)]
        public string String
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text = value;
            }
        }

        [Browsable(true)]
        public bool MsgStringVisible
        {
            get
            {
                return label_Msg.Visible;
            }
            set
            {
                label_Msg.Visible = value;
            }
        }

        [Browsable(true)]
        public int MsgString
        {
            set
            {
                switch (value)
                {
                    case 0:
                        label_Msg.Text = @"✘";
                        label_Msg.ForeColor = Color.Red;
                        break;
                    case 1:
                        label_Msg.Text = @"✔";
                        label_Msg.ForeColor = Color.Green;
                        break;
                    case 2:
                        label_Msg.Text = @"";
                        label_Msg.ForeColor = Color.Black;
                        break;
                }
            }
        }

        public Color TextBoxBackColor
        {
            set { textBox.BackColor = value; }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 0x20 || e.KeyChar > 0x7E) && e.KeyChar != 8) || (textBox.Text.Length >= textLength && e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            textBox.BackColor = textBox.TextLength != textLength ? Color.Red : SystemColors.Window;
        }
    }
}
