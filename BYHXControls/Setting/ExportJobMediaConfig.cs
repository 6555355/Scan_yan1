using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PrinterStubC.CInterface;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Setting
{
    public partial class ExportJobMediaConfig : Form
    {
        private JobMediaModes m_LayerSettings;
        private JobMediaModes newLayerSettings;
        private System.Windows.Forms.SaveFileDialog saveFileDialog = new SaveFileDialog();
        string fileName = "";

        public ExportJobMediaConfig(JobMediaModes obj)
        {
            m_LayerSettings = obj;
            InitializeComponent();

            fileName = @"C:\JobMode_" + DateTime.Now.ToString("yyMMddHHmmss") + ".xml";

            textBox1.Text = fileName;

            cbxPrintMode.DataSource = m_LayerSettings.Items;
            cbxPrintMode.DisplayMember = "Name";
            cbxPrintMode.ValueMember = "Name";

            for (int i = 0; i < cbxPrintMode.Items.Count; i++)
            {
                cbxPrintMode.SetItemChecked(i, true);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = "Job Files (*.xml)|*.xml";
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = saveFileDialog.FileName;
                fileName = saveFileDialog.FileName;
            }

        }

        private void btnCannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                newLayerSettings = new JobMediaModes();
                for (int i = 0; i < cbxPrintMode.Items.Count; i++)
                {
                    if (cbxPrintMode.GetItemChecked(i))
                    {
                        newLayerSettings.Items.Add((JobMediaMode)cbxPrintMode.Items[i]);
                    }
                }

                var doc = new SelfcheckXmlDocument();
                string xml = string.Empty;
                xml += PubFunc.SystemConvertToXml(newLayerSettings, typeof(JobMediaModes));

                doc.InnerXml = xml;
                doc.Save(fileName);

                MessageBox.Show("Export print mode successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export JobConfig Error:" + ex.Message);
            }
        }
    }
}
