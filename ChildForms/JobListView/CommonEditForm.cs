using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.JobListView
{
    public partial class CommonEditForm : Form
    {
        private List<string> m_CheckList;
        
        private string m_Input;
        public string Input
        {
            get { return m_Input; }
        }

        public CommonEditForm(List<string> CheckList)
        {
            InitializeComponent();

            m_CheckList = CheckList;
        }

        private void m_buttonOK_Click(object sender, EventArgs e)
        {
            if (m_textBoxInput.Text.Trim() == "")
            {
                return;
            }
            if (m_CheckList.Contains(m_textBoxInput.Text))
            {
                MessageBox.Show("This Mode does exist!");
                return;
            }
            else
            {
                m_Input = m_textBoxInput.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
