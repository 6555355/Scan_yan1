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
    public partial class PQCleanSetting : UserControl
    {
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        private SPrinterSetting m_PrinterSetting;
        private PQCleanParam _pqCleanParam;
        public PQCleanSetting()
        {
            InitializeComponent();
        }
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
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numXPressInkPos);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numXStartPos);
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numYDistance);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numCleanAdjust);
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numericUpDownCleanZPos);
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numericUpDownWetZPos);


            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.numXPressInkPos, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numXStartPos, this.toolTip1);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.numYDistance, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numCleanAdjust, this.toolTip1);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownCleanZPos, this.toolTip1);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownWetZPos, this.toolTip1);
        }

        private SPrinterProperty _sp;

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            _sp = sp;
            numCleanAdjust.Minimum =
            numYDistance.Minimum =
            numXStartPos.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numXStartPos.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));
            numYDistance.Maximum = numCleanAdjust.Maximum = int.MaxValue;

            //色序初始化
            panelColorMask.SuspendLayout();
            panelColorMask.Controls.Clear();
            for (int n = 0; n < sp.nColorNum; n++)
            {
                CheckBox colorBox = new CheckBox();
                colorBox.Text = ((ColorEnum_Short) sp.eColorOrder[n]).ToString();
                colorBox.TextAlign = ContentAlignment.MiddleLeft;
                colorBox.AutoSize = true;
                panelColorMask.Controls.Add(colorBox);
            }
            panelColorMask.ResumeLayout(false);
        }

        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            m_PrinterSetting = ss;
            if (EpsonLCD.GetPQCleanParam(ref _pqCleanParam))
            {
                UIPreference.SetValueAndClampWithMinMax(numXStartPos, m_CurrentUnit,
                    _pqCleanParam.XStartPos / _sp.fPulsePerInchX);
                //UIPreference.SetValueAndClampWithMinMax(numYDistance, m_CurrentUnit,
                //    _pqCleanParam.YDistance / _sp.fPulsePerInchY);
                UIPreference.SetValueAndClampWithMinMax(numCleanAdjust, m_CurrentUnit,
                    _pqCleanParam.XAdjust / _sp.fPulsePerInchX);

                numYDistance.Value = _pqCleanParam.YDistance < numYDistance.Maximum ? _pqCleanParam.YDistance : 0;
                numPressInkTime.Value = _pqCleanParam.PressInkTime < numPressInkTime.Maximum ? _pqCleanParam.PressInkTime : 0;
                numCleanNum.Value = _pqCleanParam.CleanNum;

                numericUpDownScrapeNum.Value = _pqCleanParam.ScrapTimes;
                numericUpDownCleanZPos.Value = _pqCleanParam.CleanZPos;
                numericUpDownWetZPos.Value = _pqCleanParam.WetZPos;
                //UIPreference.SetValueAndClampWithMinMax(numericUpDownCleanZPos, m_CurrentUnit,
                //    _pqCleanParam.CleanZPos / _sp.fPulsePerInchZ);
                //UIPreference.SetValueAndClampWithMinMax(numericUpDownWetZPos, m_CurrentUnit,
                //    _pqCleanParam.WetZPos / _sp.fPulsePerInchZ);
                checkBoxAutoCAP.Checked = _pqCleanParam.bAutoCap == 1 ? true : false;
                numericUpDownCapWaitTime.Value = _pqCleanParam.AutoCapWaitTime;
            }
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            _pqCleanParam.XStartPos = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numXStartPos.Value) * _sp.fPulsePerInchX);
            //_pqCleanParam.YDistance = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numYDistance.Value) * _sp.fPulsePerInchX);
            _pqCleanParam.YDistance = Convert.ToInt32(this.numYDistance.Value);
            _pqCleanParam.XAdjust = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numCleanAdjust.Value) * _sp.fPulsePerInchX);
            _pqCleanParam.CleanNum = (byte)(this.numCleanNum.Value);
            _pqCleanParam.PressInkTime = (int)numPressInkTime.Value;
            _pqCleanParam.ScrapTimes = (byte)(this.numericUpDownScrapeNum.Value);
            //_pqCleanParam.CleanZPos = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numericUpDownCleanZPos.Value) * _sp.fPulsePerInchZ);
            _pqCleanParam.CleanZPos = (int)this.numericUpDownCleanZPos.Value;
            _pqCleanParam.bAutoCap = (byte)(checkBoxAutoCAP.Checked ? 1 : 0);
            //_pqCleanParam.WetZPos = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numericUpDownWetZPos.Value) * _sp.fPulsePerInchZ);
            _pqCleanParam.WetZPos = (int)this.numericUpDownWetZPos.Value;
            _pqCleanParam.AutoCapWaitTime = (int)this.numericUpDownCapWaitTime.Value;

        }

        private void button_set_Click(object sender, EventArgs e)
        {
            OnGetPrinterSetting(ref m_PrinterSetting);
            if (EpsonLCD.SetPQCleanParam(_pqCleanParam) == false)
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.SetCleanParamFail);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string info = ResString.GetEnumDisplayName(typeof(UISuccess), UISuccess.SetCleanParamSuccess);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void m_ButtonStartClean_Click(object sender, EventArgs e)
        {
            byte color = 0;//(byte) (m_ComboBoxColorIndex.SelectedIndex + 1);
            for (int i = 0; i < panelColorMask.Controls.Count; i++)
            {
                if (panelColorMask.Controls[i] is CheckBox)
                {
                    if (((CheckBox)panelColorMask.Controls[i]).Checked)
                    {
                        color |= (byte)(1 << i);
                    }
                }
            }
            if (checkBoxAll.Checked)
                color = 0xff;

            EpsonLCD.SetManualCleanCmd_PQ(color);
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            panelColorMask.Enabled = !checkBoxAll.Checked;
        }
    }
}
