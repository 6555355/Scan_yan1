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
    public partial class HLCCleanSetting : UserControl
    {
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        private SPrinterSetting m_PrinterSetting;
        private HlcCleanParam _hlcCleanParam;
        public HLCCleanSetting()
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
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numXPressInkPos);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numYCleanPos);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numZCleanPos);


            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numXPressInkPos, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numYCleanPos, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numZCleanPos, this.toolTip1);
        }

        private SPrinterProperty _sp;

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            _sp = sp;
            numXPressInkPos.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));
            numZCleanPos.Maximum = int.MaxValue;
        }

        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            m_PrinterSetting = ss;
            if (EpsonLCD.GetHlcCleanParam(ref _hlcCleanParam))
            {
                UIPreference.SetValueAndClampWithMinMax(numXPressInkPos, m_CurrentUnit, _hlcCleanParam.XPressInkPos / _sp.fPulsePerInchX);
                UIPreference.SetValueAndClampWithMinMax(numYCleanPos, m_CurrentUnit, _hlcCleanParam.YCleanPos / _sp.fPulsePerInchY);
                UIPreference.SetValueAndClampWithMinMax(numZCleanPos, UILengthUnit.Inch, _hlcCleanParam.ZCleanPos);
                if (_sp.fPulsePerInchZ > 0)
                {
                    UIPreference.SetValueAndClampWithMinMax(numZCleanPos, m_CurrentUnit, _hlcCleanParam.ZCleanPos / _sp.fPulsePerInchZ);
                }
                numYCleanSpeed.Value = _hlcCleanParam.YCleanSpeed < numYCleanSpeed.Maximum ? _hlcCleanParam.YCleanSpeed : 0;
                numPressInkTime.Value = _hlcCleanParam.PressInkTime < numPressInkTime.Maximum ? _hlcCleanParam.PressInkTime : 0;
                numRecoveryInkTime.Value = _hlcCleanParam.RecoveryInkTime < numRecoveryInkTime.Maximum ? _hlcCleanParam.RecoveryInkTime : 0;
            }
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            _hlcCleanParam.XPressInkPos = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numXPressInkPos.Value) * _sp.fPulsePerInchX);
            _hlcCleanParam.YCleanPos = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numYCleanPos.Value) * _sp.fPulsePerInchY);
            _hlcCleanParam.ZCleanPos = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numZCleanPos.Value) * _sp.fPulsePerInchZ);
            _hlcCleanParam.YCleanSpeed = (int)numYCleanSpeed.Value;
            _hlcCleanParam.PressInkTime = (int)numPressInkTime.Value;
            _hlcCleanParam.RecoveryInkTime = (int)numRecoveryInkTime.Value;
        }

        private void button_set_Click(object sender, EventArgs e)
        {
            OnGetPrinterSetting(ref m_PrinterSetting);
            if (EpsonLCD.SetHlcCleanParam(_hlcCleanParam) == false)
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
