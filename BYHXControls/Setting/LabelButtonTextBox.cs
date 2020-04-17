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
    public partial class LabelButtonTextBox : UserControl
    {
        public int HeadNo { set; get; }
        public string LabelHead
        {
            get { return label_Head.Text; }
            set { label_Head.Text = value; }
        }

        public string FileName
        {
            get { return textBox_FileName.Text; }
            set { textBox_FileName.Text = value; }
        }

        public LabelButtonTextBox()
        {
            InitializeComponent();
        }

        private void button_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = @"waveform file|*.wav"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_FileName.Text = openFileDialog.FileName;
            }
        }
    }
}
