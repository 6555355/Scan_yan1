using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class NameEdit : Form
    {
        private List<string> m_CheckList;

        private string m_Input;
        public string Input
        {
            get { return m_Input; }
        }
        public NameEdit(List<string> CheckList)
        {
            InitializeComponent();
            m_CheckList = CheckList;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (!m_CheckList.Contains(m_textBoxInput.Text))
            {
                m_Input = m_textBoxInput.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("This Mode does exist!");
                return;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
