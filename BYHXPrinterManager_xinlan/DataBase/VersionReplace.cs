using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager
{
    public partial class VersionReplace : Form
    {
        public string versionLocation = "";
        public string versionInfo = "";
        public string replaceReason = "";

        public VersionReplace()
        {
            InitializeComponent();
        }

        private void linkVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                linkVersion.LinkVisited = true;
                System.Diagnostics.Process.Start("iexplore.exe", versionLocation);

            }
            catch (Exception ex)
            {
                MessageBox.Show("["+versionLocation+"]" + "\n" + ex.Message);
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VersionReplace_Load(object sender, EventArgs e)
        {
            string info = string.Format(ResString.GetResString("VersionCheck_HB_Version"), versionInfo);
            if (replaceReason.Trim() != "")
            {
                info += "\n" + string.Format(ResString.GetResString("VersionCheck_HB_Reason"), replaceReason);
            }
            info += "\n" + ResString.GetResString("VersionCheck_Support");
            lblInfo.Text = info;

            if (versionLocation.Trim() != "")
            {
                linkVersion.Text = ResString.GetResString("VersionCheck_Download");
            }
            else
            {
                linkVersion.Visible = false;
            }

        }
    }
}
