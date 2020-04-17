using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using PrinterStubC.CInterface;
using PrinterStubC.Common;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Setting
{
    public partial class JobConfigModes : UserControl
    {
        private string m_FileName = "";
        private string m_FilePath = "";

        public JobModes m_LayerSettings;
        public JobModes Modes
        {
            get { return m_LayerSettings; }
        }

        private List<string> m_UsedConfigList = new List<string>();
        public List<string> UsedConfigList
        {
            set { m_UsedConfigList = value; }
        }

        private List<string> m_ChangeConfigList = new List<string>();
        public List<string> ChangeConfigList
        {
            get { return m_ChangeConfigList; }
        }

        public JobConfigModes()
        {
            InitializeComponent();

            m_LayerSettings = PubFunc.LoadJobModesFromFile();
            Bind();
        }
        public void OnPrinterSettingChange(SPrinterSetting ss)
        {

        }

        public void OnGetPrinterSetting()
        {
            PubFunc.SaveJobModesFromFile(m_LayerSettings);
        }
        private void Bind()
        {


            m_listBoxLayerSettings.DataSource = null;
            m_listBoxLayerSettings.DataSource = m_LayerSettings.Items;
            m_listBoxLayerSettings.DisplayMember = "Name";
            m_listBoxLayerSettings.ValueMember = "Name";
            JobMode mode = (JobMode)m_listBoxLayerSettings.SelectedItem;
            if (mode != null)
            {
                m_PropertyGridStep.SelectedObject = mode.Item;
            }
        }
        private void AddMode_Click(object sender, EventArgs e)
        {
            List<string> ExistModes = new List<string>();
            foreach (JobMode Mode in m_LayerSettings.Items)
            {
                ExistModes.Add(Mode.Name);
            }
            NameEdit Form = new NameEdit(ExistModes);
            Form.Text = ResString.GetResString("Add_Mode");
            if (DialogResult.OK == Form.ShowDialog())
            {
                JobMode Mode = new JobMode();
                Mode.Name = Form.Input;
                m_LayerSettings.Items.Add(Mode);

                Bind();

                m_listBoxLayerSettings.SelectedIndex = m_listBoxLayerSettings.Items.Count - 1;

                

            }
        }

        private void RemoveMode_Click(object sender, EventArgs e)
        {
            if (m_listBoxLayerSettings.SelectedIndex < 0)
                return;

            try
            {
                JobMode Mode = (JobMode)m_listBoxLayerSettings.SelectedItem;
                if (Mode != null)
                {
                    if (DialogResult.Cancel == MessageBox.Show(string.Format(ResString.GetResString("Confirm_DeleteMode"), Mode.Name), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                    {
                        return;
                    }

                    foreach (JobMode item in m_LayerSettings.Items)
                    {
                        if (item.Name == Mode.Name)
                        {
                            m_LayerSettings.Items.Remove(item);
                            break;
                        }
                    }

                    Bind();

                    m_listBoxLayerSettings.SelectedIndex = m_listBoxLayerSettings.Items.Count - 1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save the config:" + ex.Message);
            }
        }

        private void ImportMode_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "Job Files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                if (File.Exists(fileName))
                {
                    var doc = new SelfcheckXmlDocument();
                    doc.Load(fileName);
                    JobModes importModes = (JobModes)PubFunc.SystemConvertFromXml(doc.InnerXml, typeof(JobModes));

                    for (int i = 0; i < importModes.Items.Count; i++)
                    {
                        foreach (JobMode item in m_LayerSettings.Items)
                        {
                            if (importModes.Items[i].Name.Trim().ToLower() == item.Name.Trim().ToLower())
                            {
                                m_LayerSettings.Items.Remove(item);
                                break;
                            }
                        }

                        m_LayerSettings.Items.Add(importModes.Items[i]);
                    }

                    Bind();
                }
            }
        }

        private void ExportMode_Click(object sender, EventArgs e)
        {
            ExportJobConfig exportJobConfig = new ExportJobConfig(m_LayerSettings);
            exportJobConfig.ShowDialog();
        }

        private void CopyAs_Click(object sender, EventArgs e)
        {
            if (m_listBoxLayerSettings.SelectedIndex < 0)
                return;

            try
            {
                JobMode Mode = (JobMode)m_listBoxLayerSettings.SelectedItem;
                if (Mode != null)
                {
                    List<string> ExistModes = new List<string>();
                    foreach (JobMode item in m_LayerSettings.Items)
                    {
                        ExistModes.Add(item.Name);
                    }

                    NameEdit Form = new NameEdit(ExistModes);
                    Form.Text = ResString.GetResString("Add_Mode");
                    if (DialogResult.OK == Form.ShowDialog())
                    {
                        JobMode newMode = new JobMode();
                        newMode.Name = Form.Input;
                        newMode.Item = Mode.Item.Clone();
                        newMode.LayerNum = Mode.LayerNum;
                        newMode.LayerColorArray = Mode.LayerColorArray;
                        newMode.SpotColor1Mask = Mode.SpotColor1Mask;
                        m_LayerSettings.Items.Add(newMode);

                        Bind();

                        m_listBoxLayerSettings.SelectedIndex = m_listBoxLayerSettings.Items.Count - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save the config:" + ex.Message);
            }
        }

        private void UpMode_Click(object sender, EventArgs e)
        {
            if (m_listBoxLayerSettings.SelectedItems.Count > 0)
            {
                int curIdx = m_listBoxLayerSettings.SelectedIndex;

                if (curIdx > 0)
                {
                    JobMode Mode = (JobMode)m_listBoxLayerSettings.SelectedItem;

                    m_LayerSettings.Items.Remove(Mode);
                    curIdx--;
                    m_LayerSettings.Items.Insert(curIdx, Mode);

                    Bind();

                    m_listBoxLayerSettings.SelectedIndex = curIdx;
                }
            }
        }

        private void DownMode_Click(object sender, EventArgs e)
        {
            if (m_listBoxLayerSettings.SelectedItems.Count > 0)
            {
                int curIdx = m_listBoxLayerSettings.SelectedIndex;

                if (curIdx < m_listBoxLayerSettings.Items.Count - 1)
                {
                    JobMode Mode = (JobMode)m_listBoxLayerSettings.SelectedItem;

                    m_LayerSettings.Items.Remove(Mode);
                    curIdx++;
                    m_LayerSettings.Items.Insert(curIdx, Mode);

                    Bind();

                    m_listBoxLayerSettings.SelectedIndex = curIdx;
                }
            }
        }

        private void m_listBoxLayerSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_PropertyGridStep.SelectedObject = null;

            JobMode mode = (JobMode)m_listBoxLayerSettings.SelectedItem;
            if (mode != null)
            {
                m_PropertyGridStep.SelectedObject = mode.Item;
            }
        }
    }
}
