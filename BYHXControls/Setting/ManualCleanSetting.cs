using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BYHXPrinterManager.Setting
{
    public partial class ManualCleanSetting : UserControl
    {
        private SPrinterSetting m_PrinterSetting;
        private bool bCanCleanFlg = true;
        private bool bColorJet = true;
        private bool bALLWIN = true;
        private ALLWINCleanParam _allwinData;
        private ManualCleanParam _manualCleanParamData;
        public ManualCleanSetting()
        {
            InitializeComponent();
            bColorJet = PubFunc.IsColorJet_Belt_Textile();

            this.m_ButtonStartClean.Enabled = bCanCleanFlg;

            bALLWIN = PubFunc.IsALLWIN_ROLL_TEXTILE();
            m_GroupBoxCleanParamAoKang.Visible = bALLWIN;
            labelCleanTime.Visible = comboBoxLevel.Visible = !bColorJet;
            buttonZCenter.Visible=
            buttonZBottom.Visible = true;  // 20180901 colorjet要求修改
            if (!bColorJet)
            {
                comboBoxLevel.SelectedIndex = 4;
            }
        }

        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        public void OnPreferenceChange(UIPreference up)
        {
            if (m_CurrentUnit != up.Unit)
            {
                OnUnitChange(up.Unit);
                m_CurrentUnit = up.Unit;
            }
        }
        private void OnUnitChange(UILengthUnit newUnit)
        {
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.m_NumericUpDownYEndPos);
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numericXAxisPos);
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numericBladeYMobileDistance);
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numericScrapPlatformZPos);

            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownYEndPos, this.m_ToolTip);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.numericXAxisPos, this.m_ToolTip);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.numericBladeYMobileDistance, this.m_ToolTip);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.numericScrapPlatformZPos, this.m_ToolTip);
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {

        }
        public void OnPrinterSettingChange(SPrinterSetting ss, SPrinterProperty sp)
        {
            m_PrinterSetting = ss; //Printer Setting backup
            if (bALLWIN)
            {
                EpsonLCD.GetALLWINCleanParam(ref _allwinData);
            }
            if (bColorJet)
            {
                if (EpsonLCD.GetManualCleanParam(ref _manualCleanParamData) == false)
                {
                    string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.ManualCleanNotSupport);
                    MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //色序初始化
            panelColorMask.SuspendLayout();
            panelColorMask.Controls.Clear();
            for (int n = 0; n < sp.nColorNum; n++)
            {
                if (bColorJet)
                {
                    RadioButton colorBox = new RadioButton();
                    colorBox.Text = string.Format("CH{0}", n + 1);//((ColorEnum_Short) sp.eColorOrder[n]).ToString();
                    colorBox.TextAlign = ContentAlignment.MiddleLeft;
                    colorBox.AutoSize = true;
                    panelColorMask.Controls.Add(colorBox);
                }
                else
                {
                    CheckBox colorBox = new CheckBox();
                    colorBox.Text = string.Format("CH{0}", n + 1);//((ColorEnum_Short) sp.eColorOrder[n]).ToString();
                    colorBox.TextAlign = ContentAlignment.MiddleLeft;
                    colorBox.AutoSize = true;
                    panelColorMask.Controls.Add(colorBox);
                }
            }
            panelColorMask.ResumeLayout(false);

            if (bALLWIN)
            {
                UIPreference.SetValueAndClampWithMinMax(this.numericXAxisPos, _allwinData.xPos);
                UIPreference.SetValueAndClampWithMinMax(this.numericBladeYMobileDistance, _allwinData.yPos);
                UIPreference.SetValueAndClampWithMinMax(this.numericScrapPlatformZPos, _allwinData.zPos);
                UIPreference.SetValueAndClampWithMinMax(this.numericBladeCleanDuration, _allwinData.CleanPumpTime);
            }
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            if (bALLWIN)
            {
                _allwinData.xPos = Convert.ToUInt32(this.numericXAxisPos.Value);
                _allwinData.yPos = Convert.ToUInt32(this.numericBladeYMobileDistance.Value);
                _allwinData.zPos = Convert.ToUInt32(this.numericScrapPlatformZPos.Value);
                _allwinData.CleanPumpTime = Convert.ToUInt16(this.numericBladeCleanDuration.Value);
            }
        }

        /// <summary>
        /// 设置清洗参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ButtonSet_Click(object sender, EventArgs e)
        {
            this.m_ButtonStartClean.Enabled = bCanCleanFlg = true;
            OnGetPrinterSetting(ref m_PrinterSetting);
            if (EpsonLCD.SetManualCleanParam(_manualCleanParamData) == false) 
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.SetCleanParamFail);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_ButtonStartClean.Enabled = bCanCleanFlg = false;
            }
        }

        /// <summary>
        /// 设置清洗颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ButtonStartClean_Click(object sender, EventArgs e)
        {
            byte color = 0;//(byte) (m_ComboBoxColorIndex.SelectedIndex + 1);
            for (int i = 0; i < panelColorMask.Controls.Count; i++)
            {
                if (bColorJet)
                {
                    if (panelColorMask.Controls[i] is RadioButton)
                    {
                        if (((RadioButton)panelColorMask.Controls[i]).Checked)
                        {
                            color = (byte)(i);
                        }
                    }
                }
                else
                {
                    if (panelColorMask.Controls[i] is CheckBox)
                    {
                        if (((CheckBox)panelColorMask.Controls[i]).Checked)
                        {
                            color |= (byte)(1 << i);
                        }
                    }
                }
            }
            byte? level = (byte) (comboBoxLevel.SelectedIndex + 1);
            if (level < 0)
                level = 0;
            if (bColorJet) level = null;

            if (checkBoxAll.Checked)
                color = 0xff;

            EpsonLCD.SetManualCleanCmd(color, level);
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            panelColorMask.Enabled = !checkBoxAll.Checked;
        }

        private void m_ButtonCancel_Click(object sender, EventArgs e)
        {
            EpsonLCD.StopManualClean();
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            //this.m_ButtonStartClean.Enabled = bCanCleanFlg = true;
            OnGetPrinterSetting(ref m_PrinterSetting);
            if (EpsonLCD.SetALLWINCleanParam(_allwinData) == false)
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.SetCleanParamFail);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.m_ButtonStartClean.Enabled = bCanCleanFlg = false;
            }
        }

        private void buttonZCenter_Click(object sender, EventArgs e)
        {
            EpsonLCD.SetManualCleanParamZCenter();
        }

        private void buttonZBottom_Click(object sender, EventArgs e)
        {
            EpsonLCD.SetManualCleanParamZBottom();
        }
    }
}
