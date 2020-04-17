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
    public partial class GmaCleanSetting : UserControl
    {
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        private SPrinterSetting m_PrinterSetting;
        private GmaCleanParam _gmaCleanParam;
        public GmaCleanSetting()
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
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numxStartPos);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numxDistance);
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numScraperPos);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.num_zCLeanPos);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numYCarryCleanPos);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numZCarryCleanPos);

            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numxStartPos, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numxDistance, this.toolTip1);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.numScraperPos, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.num_zCLeanPos, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numYCarryCleanPos, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numZCarryCleanPos, this.toolTip1);
        }

        private SPrinterProperty _sp;

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            _sp = sp;
            numScraperPos.Minimum =
                numxDistance.Minimum =
                    numxStartPos.Minimum =
                        numYCarryCleanPos.Minimum =
                            numZCarryCleanPos.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numxDistance.Maximum =
                    numxStartPos.Maximum =
                    numYCarryCleanPos.Maximum =
                            numZCarryCleanPos.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));
            numScraperPos.Maximum = int.MaxValue;
        }

        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            m_PrinterSetting = ss;
            if (EpsonLCD.GetGmaCleanParam(ref _gmaCleanParam))
            {
                UIPreference.SetValueAndClampWithMinMax(numxStartPos, m_CurrentUnit,
                    _gmaCleanParam.XStartPos/_sp.fPulsePerInchX);
                UIPreference.SetValueAndClampWithMinMax(numxDistance, m_CurrentUnit,
                    _gmaCleanParam.XDistance/_sp.fPulsePerInchX);
                UIPreference.SetValueAndClampWithMinMax(numScraperPos, UILengthUnit.Inch, _gmaCleanParam.ScraperPos);
                if (_sp.fPulsePerInchZ > 0)
                {
                    UIPreference.SetValueAndClampWithMinMax(num_zCLeanPos, m_CurrentUnit,
                        _gmaCleanParam.ZCleanPos/_sp.fPulsePerInchZ);
                    UIPreference.SetValueAndClampWithMinMax(numZCarryCleanPos, m_CurrentUnit,
                        _gmaCleanParam.ZCarryCleanPos/_sp.fPulsePerInchZ);
                }
                numcleanRowNum.Value = _gmaCleanParam.CleanRowNum;
                numcleanTimes.Value = _gmaCleanParam.CleanTime;
                UIPreference.SetValueAndClampWithMinMax(numYCarryCleanPos, m_CurrentUnit,
                    _gmaCleanParam.YCarryCleanPos/_sp.fPulsePerInchY);
            }
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            _gmaCleanParam.XStartPos = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numxStartPos.Value) * _sp.fPulsePerInchX);
            _gmaCleanParam.XDistance = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numxDistance.Value) * _sp.fPulsePerInchX);
            _gmaCleanParam.ScraperPos = Convert.ToInt32(this.numScraperPos.Value);
            _gmaCleanParam.ZCleanPos = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.num_zCLeanPos.Value) * _sp.fPulsePerInchZ);
            _gmaCleanParam.CleanRowNum = (byte) (this.numcleanRowNum.Value);
            _gmaCleanParam.CleanTime = (byte)numcleanTimes.Value;
            _gmaCleanParam.YCarryCleanPos = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numYCarryCleanPos.Value) * _sp.fPulsePerInchY);
            _gmaCleanParam.ZCarryCleanPos = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numZCarryCleanPos.Value) * _sp.fPulsePerInchZ);
        }

        private void button_set_Click(object sender, EventArgs e)
        {
            OnGetPrinterSetting(ref m_PrinterSetting);
            if (EpsonLCD.SetGmaCleanParam(_gmaCleanParam) == false)
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
    }
}
