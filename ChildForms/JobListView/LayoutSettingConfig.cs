using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace BYHXPrinterManager.JobListView
{
    public partial class LayoutSettingConfig : Form
    {
        private const int ctlCnt = 8;
        private const int OPTIONINDEX = 4; // 操作标志位存放开始位置
        private int colorNum = 8;
        private string[] colorArray = new string[8];// { "Y", "M", "C", "K", "Lc", "Lm", "W", "W" };
        private bool isReloadLayout = false;
        Dictionary<String, Color> HeadColorList = new Dictionary<String, Color>();
        private int oldLayerIdx = -1;
        public LayoutSettingClassList m_LayoutSettingList;
        public LayoutSettingClassList LayoutList
        {
            get { return m_LayoutSettingList; }
        }

        private string strEnable = "Enable";
        private string strSource = "Source";
        private string strDataType = "DataType";
        private string strXMirror = "MirrorX";

        public void GetColorInfo()
        {
            colorNum = CoreInterface.GetLayoutColorNum();
            colorArray = new string[colorNum];
            for (int i = 0; i < colorNum; i++)
            {
                string strColor = GetColorName(CoreInterface.GetLayoutColorID(i));
                if (strColor == null) strColor = "";
                colorArray[i] = strColor;
            }
        }

        string GetColorName(int colorIdx)
        {
            string name = "";

            name = Enum.GetName(typeof(LayoutColorNameEnum), colorIdx);

            return name;
        }

        public LayoutSettingConfig()
        {
            InitializeComponent();

            InitHGroupList();

            strEnable = ResString.GetResString("Enable");
            strSource = ResString.GetResString("Source");
            strDataType = ResString.GetResString("DataType");
            strXMirror = ResString.GetResString("MirrorX");

            if (false)
            {
                cbxSpecialLayout.Visible = true;
                lblLayerSpaceY.Visible = true;
                numLayerSpaceY.Visible = true;
            }
            else
            {
                cbxSpecialLayout.Checked = false;
                cbxSpecialLayout.Visible = false;
                lblLayerSpaceY.Visible = false;
                numLayerSpaceY.Visible = false;
            }

            HeadColorList = PubFunc.SetHeadColorList();

            GetColorInfo();

            if (colorNum > 10)
            {
                this.Width += (colorNum - 10) * 35;
                if (this.Width > 1600) this.Width = 1600;
            }

            m_LayoutSettingList = new LayoutSettingClassList();

            if (File.Exists(CoreConst.LayoutFileName))
            {
                var doc = new XmlDocument();
                doc.Load(CoreConst.LayoutFileName);
                m_LayoutSettingList = (LayoutSettingClassList)PubFunc.SystemConvertFromXml(doc.InnerXml, typeof(LayoutSettingClassList));
            }

            Bind();
            
            if(m_listBoxLayout.Items.Count > 0)
            {
                m_listBoxLayout.SelectedIndex = 0;
            }
        }

        private void Bind()
        {
            isReloadLayout = true;
            try
            {
                m_listBoxLayout.DataSource = null;
                m_listBoxLayout.DataSource = m_LayoutSettingList.Items;
                m_listBoxLayout.DisplayMember = "Name";
                m_listBoxLayout.ValueMember = "Name";

                m_listBoxLayout.SelectedIndex = -1;
            }
            catch { }
            finally
            {
                isReloadLayout = false;
            }
        }

        private void ClearInfo()
        {
            numYinterleaveNum.Value = 0;
            numYContinue.Value = 0;
            numYOffset.Value = 0;
            listSubLayerNum.Text = "0";
            subLayerPanel.Controls.Clear();
            listLayerNum.Text = "0";
            listBaseLayer.Text = "0";
            cbxListLayer.Items.Clear();
        }

        private void m_buttonAddMode_Click(object sender, EventArgs e)
        {
            List<string> ExistLayout = new List<string>();
            foreach (LayoutSettingClass Layout in m_LayoutSettingList.Items)
            {
                ExistLayout.Add(Layout.Name);
            }

            CommonEditForm Form = new CommonEditForm(ExistLayout);
            Form.Text = ResString.GetResString("Add_Layout");
            if (DialogResult.OK == Form.ShowDialog())
            {
                LayoutSettingClass layout = new LayoutSettingClass();
                layout.Name = Form.Input;
                m_LayoutSettingList.Items.Add(layout);

                Bind();

                m_listBoxLayout.SelectedIndex = m_listBoxLayout.Items.Count - 1;
            }
        }

        private void m_buttonRemoveMode_Click(object sender, EventArgs e)
        {
            if (m_listBoxLayout.SelectedIndex < 0)
                return;

            try
            {
                LayoutSettingClass Layout = (LayoutSettingClass)m_listBoxLayout.SelectedItem;

                if (DialogResult.Cancel == MessageBox.Show(string.Format(ResString.GetResString("Confirm_DeleteLayout"), Layout.Name), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                {
                    return;
                }

                if (Layout.Name != "")
                {
                    //检查当前设置是否被使用

                    foreach (LayoutSettingClass item in m_LayoutSettingList.Items)
                    {
                        if (item.Name == Layout.Name)
                        {
                            m_LayoutSettingList.Items.Remove(Layout);
                            break;
                        }
                    }

                    Bind();

                    m_listBoxLayout.SelectedIndex = m_listBoxLayout.Items.Count - 1;

                    if (m_listBoxLayout.Items.Count == 0)
                    {
                        ClearInfo();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to remove the layout:" + ex.Message);
            }
        }

        private void m_buttonCopyAs_Click(object sender, EventArgs e)
        {
            if (m_listBoxLayout.SelectedIndex < 0)
                return;

            try
            {
                int LayoutIdx = m_listBoxLayout.SelectedIndex;

                if (LayoutIdx < 0) return; 

                LayoutSettingClass Layout = m_LayoutSettingList.Items[LayoutIdx];

                if (Layout.Name != "")
                {
                    List<string> ExistLayout = new List<string>();
                    foreach (LayoutSettingClass item in m_LayoutSettingList.Items)
                    {
                        ExistLayout.Add(item.Name);
                    }

                    CommonEditForm Form = new CommonEditForm(ExistLayout);
                    Form.Text = ResString.GetResString("Add_Layout");
                    if (DialogResult.OK == Form.ShowDialog())
                    {
                        LayoutSettingClass newItem = new LayoutSettingClass();
                        newItem.Layout = Layout.Layout.Clone();
                        newItem.SpecialLayout = Layout.SpecialLayout;
                        newItem.SpecialYSpace = Layout.SpecialYSpace;
                        newItem.Name = Form.Input;

                        m_LayoutSettingList.Items.Add(newItem);

                        Bind();

                        m_listBoxLayout.SelectedIndex = m_listBoxLayout.Items.Count - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to copy layout:" + ex.Message);
            }
        }

        private bool SaveLayerInfo(ref LayerSetting curLayerSetting)
        {
            try
            {
                uint[] printColor = new uint[8];
                ushort nlayersource = 0;
                byte[] dataSourceType = new byte[8];
                uint nEnableLine = 0;
                uint nLayer = 0;

                curLayerSetting.curYinterleaveNum = (byte)numYinterleaveNum.Value;
                curLayerSetting.YContinueHead = (byte)numYContinue.Value;
                curLayerSetting.layerYOffset = (float)numYOffset.Value;
                curLayerSetting.subLayerNum = Convert.ToUInt16(listSubLayerNum.Text);

                if (maxHGNum == 1)
                {
                    nEnableLine = 0x101;
                }
                else
                {
                    nEnableLine = ((uint)(listHorGroupNum.SelectedIndex + 1) & 0xFF);
                    HGroupControlGetInfo(ref nEnableLine);
                }

                for (int i = 0; i < subLayerPanel.Controls.Count; i++)
                {
                    Panel panel = (Panel)subLayerPanel.Controls[i];
                    CheckBox enableCtl = (CheckBox)panel.Controls[0];
                    //if (enableCtl.Checked)
                    //{
                    //    nEnableLine |= (uint)(1 << i);
                    //}

                    for (int j = 1; j < panel.Controls.Count - 5; j++)
                    {
                        CheckBox crl = (CheckBox)panel.Controls[j];
                        if (crl.Checked)
                        {
                            printColor[i] |= Convert.ToUInt32(1 << (j - 1));
                        }
                    }

                    ComboBox sourceCtl = (ComboBox)panel.Controls[panel.Controls.Count - 4];
                    //ushort source = Convert.ToUInt16(sourceCtl.Text);
                    ushort source = (ushort)sourceCtl.SelectedIndex;
                    nlayersource |= (ushort)(source << i * 2);

                    ComboBox dataTypeCtl = (ComboBox)panel.Controls[panel.Controls.Count - 2];
                    int dataTypeIdx = dataTypeCtl.SelectedIndex;
                    dataSourceType[i] = (byte)dataTypeIdx;

                    CheckBox cbxMP = (CheckBox)panel.Controls[panel.Controls.Count - 1]; //镜像打印
                    if (cbxMP.Checked)
                        dataSourceType[i] |= 0x80;
                }

                curLayerSetting.nEnableLine = nEnableLine;
                curLayerSetting.printColor = printColor;
                curLayerSetting.nlayersource = nlayersource;
                curLayerSetting.ndatasource = dataSourceType;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            if (m_listBoxLayout.SelectedIndex < 0)
                return;

            try
            {
                //LayoutSetting curLayoutSetting = m_LayoutSettingList.Items[m_listBoxLayout.SelectedIndex].Layout;
                //LayerSetting curLayerSetting = curLayoutSetting.layerSetting[cbxListLayer.SelectedIndex];

                LayoutSetting curLayoutSetting = m_LayoutSettingList.Items[m_listBoxLayout.SelectedIndex].Layout;
                LayerSetting curLayerSetting = new LayerSetting(1);

                uint[] printColor = new uint[8];
                ushort nlayersource = 0;
                byte[] dataSourceType = new byte[8];
                uint nEnableLine = 0;
                uint nLayer = 0;

                if (cbxListLayer.SelectedIndex >= 0)
                {
                    #region noused
                    //curLayerSetting.curYinterleaveNum = (byte)numYinterleaveNum.Value;
                    //curLayerSetting.YContinueHead = (byte)numYContinue.Value;
                    //curLayerSetting.curLayerType = (byte)listLayerType.SelectedIndex;
                    //curLayerSetting.layerYOffset = (float)numYOffset.Value;
                    //curLayerSetting.subLayerNum = Convert.ToUInt16(listSubLayerNum.Text);

                    //for (int i = 0; i < subLayerPanel.Controls.Count; i++)
                    //{
                    //    Panel panel = (Panel)subLayerPanel.Controls[i];
                    //    CheckBox enableCtl = (CheckBox)panel.Controls[0];
                    //    if (enableCtl.Checked)
                    //    {
                    //        nEnableLine |= (uint)(1 << i);
                    //    }

                    //    for (int j = 1; j < panel.Controls.Count - 4; j++)
                    //    {
                    //        CheckBox crl = (CheckBox)panel.Controls[j];
                    //        if (crl.Checked)
                    //        {
                    //            printColor[i] |= Convert.ToUInt32(1 << (j-1));
                    //        }
                    //    }

                    //    ComboBox sourceCtl = (ComboBox)panel.Controls[panel.Controls.Count - 3];
                    //    ushort source = Convert.ToUInt16(sourceCtl.Text);
                    //    nlayersource |= (ushort)(source << i * 2);

                    //    ComboBox dataTypeCtl = (ComboBox)panel.Controls[panel.Controls.Count - 1];
                    //    int dataTypeIdx = dataTypeCtl.SelectedIndex;
                    //    dataSourceType[i] = (byte)dataTypeIdx;
                    //}

                    //curLayerSetting.nEnableLine = nEnableLine;
                    //curLayerSetting.printColor = printColor;
                    //curLayerSetting.nlayersource = nlayersource;
                    //curLayerSetting.ndatasource = dataSourceType;
                    #endregion

                    bool isOK = SaveLayerInfo(ref curLayerSetting);
                    if (isOK)
                    {
                        curLayoutSetting.layerSetting[cbxListLayer.SelectedIndex] = curLayerSetting;
                    }
                }

                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                curLayoutSetting.layerNum = (byte)Convert.ToInt32(listLayerNum.Text);
                curLayoutSetting.baseLayerIndex = (byte)Convert.ToInt32(listBaseLayer.Text);
                for (int i = 0; i < cbxListLayer.Items.Count; i++)
                {
                    if (cbxListLayer.GetItemCheckState(i) == CheckState.Checked)
                    {
                        nLayer |= (uint)(1 << i);
                    }
                }
                curLayoutSetting.nLayer = nLayer;

                CoreInterface.ModifySPrinterModeSetting(ref curLayoutSetting);

                m_LayoutSettingList.Items[m_listBoxLayout.SelectedIndex].Layout = curLayoutSetting;

                m_LayoutSettingList.Items[m_listBoxLayout.SelectedIndex].SpecialLayout = cbxSpecialLayout.Checked;//特殊布局
                m_LayoutSettingList.Items[m_listBoxLayout.SelectedIndex].SpecialYSpace = (int)numLayerSpaceY.Value;

                m_listBoxLayout_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save the layout:" + ex.Message);
            }
        }

        private void LayoutSettingConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                var doc = new XmlDocument();
                string xml = string.Empty;
                xml += PubFunc.SystemConvertToXml(m_LayoutSettingList, typeof(LayoutSettingClassList));

                doc.InnerXml = xml;
                doc.Save(CoreConst.LayoutFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadCheckListData(int layerNum, uint enableLayer)
        {
            isReloadLayout = true;

            cbxListLayer.Items.Clear();
            listBaseLayer.Items.Clear();
            for (int i = 0; i < layerNum; i++)
            {
                cbxListLayer.Items.Add("Layer" + (i+1).ToString());

                if (((enableLayer >> i) & 1) == 1)
                {
                    cbxListLayer.SetItemChecked(i, true);
                }
                else
                {
                    cbxListLayer.SetItemChecked(i, false);
                }

                listBaseLayer.Items.Add(i);
            }

            cbxListLayer.SelectedIndex = -1;
            listBaseLayer.SelectedIndex = -1;

            isReloadLayout = false;
        }

        private void m_listBoxLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isReloadLayout) return;

            oldLayerIdx = -1;

            int LayoutIdx = m_listBoxLayout.SelectedIndex;
            if (LayoutIdx < 0) return;

            LayoutSetting curLayoutSetting = m_LayoutSettingList.Items[LayoutIdx].Layout;

            int layerNum = (int)curLayoutSetting.layerNum;
            uint enableLayer = curLayoutSetting.nLayer;
            listLayerNum.Text = layerNum.ToString();

            LoadCheckListData(layerNum, enableLayer);

            if (cbxListLayer.Items.Count > 0)
            {
                cbxListLayer.SelectedIndex = 0;
            }

            listBaseLayer.Text = curLayoutSetting.baseLayerIndex.ToString();

            cbxSpecialLayout.Checked = m_LayoutSettingList.Items[LayoutIdx].SpecialLayout;
            numLayerSpaceY.Value = m_LayoutSettingList.Items[LayoutIdx].SpecialYSpace;

        }

        private void listLayerNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_listBoxLayout.SelectedIndex < 0) return;

            LayoutSettingClass curLayoutSettingClass = (LayoutSettingClass)m_listBoxLayout.SelectedItem;
            LayoutSetting curLayoutSetting = curLayoutSettingClass.Layout;
            uint enableLayer = curLayoutSetting.nLayer;
            int curIdx = cbxListLayer.SelectedIndex;

            int curlayerNum = Convert.ToInt32(listLayerNum.Text);

            LoadCheckListData(curlayerNum, enableLayer);

            if (cbxListLayer.Items.Count > 0)
            {
                if (curIdx < cbxListLayer.Items.Count)
                {
                    cbxListLayer.SelectedIndex = curIdx;
                }
                else
                {
                    cbxListLayer.SelectedIndex = 0;
                }
            }

            listBaseLayer.Text = curLayoutSetting.baseLayerIndex.ToString();
        }

        private void cbxListLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isReloadLayout) return;

            int LayoutIdx = m_listBoxLayout.SelectedIndex;
            int layerIdx = cbxListLayer.SelectedIndex;

            if (LayoutIdx < 0) return;

            if (oldLayerIdx != -1)
            {
                bool isOK = SaveLayerInfo(ref m_LayoutSettingList.Items[LayoutIdx].Layout.layerSetting[oldLayerIdx]);
            }

            if (layerIdx < 0) return;

            oldLayerIdx = layerIdx;

            LayerSetting curLayerSetting = m_LayoutSettingList.Items[LayoutIdx].Layout.layerSetting[layerIdx];

            int sublayerNum = curLayerSetting.subLayerNum;

            numYinterleaveNum.Value = curLayerSetting.curYinterleaveNum;
            numYContinue.Value = curLayerSetting.YContinueHead;
            numYOffset.Value = (Decimal)curLayerSetting.layerYOffset;
            //numLayerType.Value = curLayerSetting.curLayerType;
            listSubLayerNum.Text = sublayerNum.ToString();
            uint hgValue = (curLayerSetting.nEnableLine & 0xFF);
            if (hgValue <= listHorGroupNum.Items.Count)

                listHorGroupNum.SelectedIndex = (int)hgValue - 1;
            else
                listHorGroupNum.SelectedIndex = 0;
            HGroupControlSetInfo(curLayerSetting.nEnableLine);

            subLayerPanel.Controls.Clear();

            for (int i = 0; i < sublayerNum; i++)
            {
                Panel panel = BuileSubLayerPanel();
                subLayerPanel.Controls.Add(panel);

                //bool enableLine = ((curLayerSetting.nEnableLine >> i) & 1) == 1 ? true : false;
                bool enableLine = true;
                int source = ((curLayerSetting.nlayersource >> (i * 2)) & 0x3);

                int dataType = 0;
                if(curLayerSetting.ndatasource != null && curLayerSetting.ndatasource.Length > i)
                {
                    dataType = (int)curLayerSetting.ndatasource[i];
                }

                SetSubLayerPanel(panel, enableLine, curLayerSetting.printColor[i], source, dataType);
            }

            subLayerPanel.Refresh();

        }

        private void listSubLayerNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sublayerNum = Convert.ToInt32(listSubLayerNum.Text);

            int LayoutIdx = m_listBoxLayout.SelectedIndex;
            int layerIdx = cbxListLayer.SelectedIndex;

            if (LayoutIdx < 0 || layerIdx < 0) return;

            LayerSetting curLayerSetting = m_LayoutSettingList.Items[LayoutIdx].Layout.layerSetting[layerIdx];

            subLayerPanel.Controls.Clear();
            for (int i = 0; i < sublayerNum; i++)
            {
                Panel panel = BuileSubLayerPanel();
                subLayerPanel.Controls.Add(panel);

                if (i >= curLayerSetting.subLayerNum)
                    continue;

                //bool enableLine = ((curLayerSetting.nEnableLine >> i) & 1) == 1 ? true : false;
                bool enableLine = true;
                int source = ((curLayerSetting.nlayersource >> (i * 2)) & 0x3);
                int dataType = 0;
                if (curLayerSetting.ndatasource != null && curLayerSetting.ndatasource.Length > i)
                {
                    dataType = (int)curLayerSetting.ndatasource[i];
                }

                SetSubLayerPanel(panel, enableLine, curLayerSetting.printColor[i], source, dataType);
                
            }

            subLayerPanel.Refresh();
        }

        private Panel BuileSubLayerPanel()
        {
            string colorName = "";
            Panel p = new Panel();
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Height = 50;
            p.Width = 690;

            if (colorNum > 10)
            {
                p.Width += (colorNum - 10) * 35;
            }

            int top = 5;
            int left = 5;
            int cbxHeight = 40;
            int cbxWidth = 30;
            int paddingWidth = 5;

            CheckBox cbxEnable = new CheckBox();
            cbxEnable.Checked = true;
            cbxEnable.Width = 5;// 65;
            cbxEnable.Visible = false; //隐藏不使用
            cbxEnable.Height = cbxHeight;
            cbxEnable.Text = strEnable;
            cbxEnable.Location = new Point(left, top);
            p.Controls.Add(cbxEnable);
            left += cbxEnable.Width; // +paddingWidth;

            for (int i = 0; i < colorNum; i++)
            {
                CheckBox cbx1 = new CheckBox();
                cbx1.Checked = false;
                cbx1.Text = colorArray[i];
                cbx1.Width = cbxWidth;
                cbx1.Height = cbxHeight;
                cbx1.CheckAlign = ContentAlignment.TopCenter;
                cbx1.TextAlign = ContentAlignment.MiddleCenter;
                cbx1.Location = new Point(left, top);

                colorName = colorArray[i];
                if (HeadColorList.ContainsKey(colorName))
                {
                    Color backColor = HeadColorList[colorName];
                    cbx1.BackColor = backColor;
                    if (colorName == "K" || colorName == "R" || colorName == "B" || colorName == "G")
                    {
                        cbx1.ForeColor = Color.White;
                    }
                }

                p.Controls.Add(cbx1);
                left += cbxWidth + paddingWidth;
            }

            left += paddingWidth;

            Label lblSource = new Label();
            lblSource.Text = strSource;
            lblSource.Width = 50;
            lblSource.Location = new Point(left, top + 12);
            p.Controls.Add(lblSource);
            left += lblSource.Width;

            ComboBox listSource = new ComboBox();
            listSource.Items.Add(0);
            listSource.Items.Add(1);
            listSource.Items.Add(2);
            listSource.Items.Add(3);
            //listSource.Items.Add(4);
            //if (NewLayoutFun.IsSupportGray)
            //{
            //    listSource.Items.Add("Grey");
            //}
            listSource.SelectedIndex = 0;
            listSource.Width = 45;
            listSource.Location = new Point(left, top+10);
            p.Controls.Add(listSource);
            left += listSource.Width;

            left += paddingWidth;

            Label lblDataType = new Label();
            lblDataType.Text = strDataType;
            lblDataType.Width = 55;
            lblDataType.Location = new Point(left, top + 12);
            p.Controls.Add(lblDataType);
            left += lblDataType.Width;

            left += paddingWidth;

            ComboBox listDataType = new ComboBox();
            String[] DataTypeNames = Enum.GetNames(typeof(DataSourceType));

            for (int m = 0; m < DataTypeNames.Length; m++)
            {
                if (!NewLayoutFun.IsSupportGray)
                {
                    if (m >= DataTypeNames.Length - 1)
                    {
                        continue;
                    }
                }
                listDataType.Items.Add(DataTypeNames[m]);
            }

            listDataType.SelectedIndex = 0;
            listDataType.Width = 60;
            listDataType.Location = new Point(left, top + 10);
            p.Controls.Add(listDataType);

            left += listDataType.Width;
            left += paddingWidth;

            CheckBox cbxXMirror = new CheckBox();
            cbxXMirror.Checked = false;
            cbxXMirror.Width = 65;
            cbxXMirror.Height = cbxHeight;
            cbxXMirror.Text = strXMirror;
            cbxXMirror.Location = new Point(left, top);
            p.Controls.Add(cbxXMirror);

            return p;
        }

        private void SetSubLayerPanel(Panel panel, bool enableLine, uint printColor,int source,int dataTypeIdx)
        {
            //子行是否可用
            CheckBox enableCtl = (CheckBox)panel.Controls[0];
            if (enableLine)
            {
                enableCtl.Checked = true;
            }
            else
            {
                enableCtl.Checked = false;
            }

            //颜色是否可用
            for (int j = 1; j < panel.Controls.Count - 5; j++)
            {
                CheckBox crl = (CheckBox)panel.Controls[j];
                if (((printColor >> (j-1)) & 1) == 1)
                {
                    crl.Checked = true;
                }
                else
                {
                    crl.Checked = false;
                }
            }

            //数据源
            ComboBox sourceCtl = (ComboBox)panel.Controls[panel.Controls.Count - 4];
            sourceCtl.Text = source.ToString();

            //类型
            ComboBox dataTypeCtl = (ComboBox)panel.Controls[panel.Controls.Count - 2];
            if ((dataTypeIdx & 0x7F) < dataTypeCtl.Items.Count)
            {
                dataTypeCtl.SelectedIndex = dataTypeIdx & 0x7F;
            }

            //镜像
            CheckBox cbxMP = (CheckBox)panel.Controls[panel.Controls.Count - 1]; //镜像打印
            if ((dataTypeIdx & 0x80) != 0)
            {
                cbxMP.Checked = true;
            }

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Filter = "Layout Files (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;

                    if (File.Exists(fileName))
                    {
                        var doc = new XmlDocument();
                        doc.Load(fileName);
                        m_LayoutSettingList = (LayoutSettingClassList)PubFunc.SystemConvertFromXml(doc.InnerXml, typeof(LayoutSettingClassList));

                        Bind();

                        if (m_listBoxLayout.Items.Count > 0)
                        {
                            m_listBoxLayout.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = @"LayoutSetting_" + DateTime.Now.ToString("yyMMddHHmmss") + ".xml";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = fileName;
                saveFileDialog.Filter = "Layout Files (*.xml)|*.xml";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var doc = new XmlDocument();
                    string xml = string.Empty;
                    xml += PubFunc.SystemConvertToXml(m_LayoutSettingList, typeof(LayoutSettingClassList));

                    doc.InnerXml = xml;
                    doc.Save(saveFileDialog.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private List<CheckBox> HBControlList = new List<CheckBox>();
        int maxHGNum = 0;

        private void listHorGroupNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildHGroupControl(listHorGroupNum.SelectedIndex + 1);
        }

        private void InitHGroupList()
        {
            listHorGroupNum.Items.Clear();
            maxHGNum = CoreInterface.GetMaxColumnNum();
            if (maxHGNum < 1) maxHGNum = 1;
            for (int i = 1; i <= maxHGNum; i++)
            {
                listHorGroupNum.Items.Add(i);
            }

            if (listHorGroupNum.Items.Count > 0)
                listHorGroupNum.SelectedIndex = 0;

            HBControlList.Clear();
            HBControlList.Add(cbxHG1);
            HBControlList.Add(cbxHG2);
            HBControlList.Add(cbxHG3);
            HBControlList.Add(cbxHG4);

            if (maxHGNum == 1)
            {
                groupHoriz.Visible = false;
            }
            else
            {
                groupHoriz.Visible = true;
            }
        }

        private void HGroupControlSetInfo(uint value)
        {
            byte temp = BitConverter.GetBytes(value)[1];

            for (int i = 0; i < HBControlList.Count; i++)
            {
                HBControlList[i].Checked = ((temp >> i) & 1) == 1 ? true : false;
            }
        }

        private void HGroupControlGetInfo(ref uint value)
        {
            uint temp = 0;

            for (int i = 0; i < HBControlList.Count; i++)
            {
                if (HBControlList[i].Visible == true)
                {
                    temp = (uint)(HBControlList[i].Checked == true ? (1 << i) : 0) | temp;
                }
            }

            value = value | (temp << 8);
        }

        private void BuildHGroupControl(int num)
        {
            for (int i = 0; i < HBControlList.Count; i++)
            {
                if (i < num)
                {
                    HBControlList[i].Visible = true;
                }
                else
                {
                    HBControlList[i].Visible = false;
                }
            }

        }


    }
}
