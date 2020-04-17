using System;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class RenameMediaSettingForm : Form
    {
        public RenameMediaSettingForm()
        {
            InitializeComponent();
        }

        public string MediaSettingName
        {
            get { return this.textBox1.Text; }
            set { this.textBox1.Text = value; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(this.textBox1.Text.Trim()))
                this.DialogResult = DialogResult.OK;
            else
            {
                MessageBox.Show("New name cannot be empty.");
            }
        }
    }
}
