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
    public partial class DOCANManualCleanSetting : UserControl
    {
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        private SPrinterSetting m_PrinterSetting;
        private DocanTextileParam _docanTextileParam;
        public DOCANManualCleanSetting()
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

        }
        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {

        }
        public void OnPrinterSettingChange(SPrinterSetting ss, SPrinterProperty sp)
        {
            m_PrinterSetting = ss;
            if (EpsonLCD.GetDocanTextileCleanParam(ref _docanTextileParam))
            {
                numericUpDown_xStartPos.Value = _docanTextileParam.xStartPos;
                numericUpDown_zCLeanPos.Value = _docanTextileParam.zCLeanPos;
                numericUpDown_PressInkTime.Value = _docanTextileParam.PressInkTime;
                numericUpDown_ZSensorErrRange.Value = _docanTextileParam.ZSensorErrRange;
                numericUpDown_ZSensorCurVal.Value = _docanTextileParam.ZSensorCurVal;
            }
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            _docanTextileParam.xStartPos = Convert.ToInt32(this.numericUpDown_xStartPos.Value);
            _docanTextileParam.zCLeanPos = Convert.ToInt32(this.numericUpDown_zCLeanPos.Value);
            _docanTextileParam.PressInkTime = Convert.ToUInt16(this.numericUpDown_PressInkTime.Value);
            _docanTextileParam.ZSensorErrRange = Convert.ToUInt16(this.numericUpDown_ZSensorErrRange.Value);
            _docanTextileParam.ZSensorCurVal = Convert.ToUInt16(this.numericUpDown_ZSensorCurVal.Value);
        }

        private void button_set_Click(object sender, EventArgs e)
        {
            OnGetPrinterSetting(ref m_PrinterSetting);
            if (EpsonLCD.SetDocanTextileCleanParam(_docanTextileParam) == false)
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

        private void button_trigger_Click(object sender, EventArgs e)
        {
            if (EpsonLCD.DOCANTEXTILE_SENSOR_INIT() == false)
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.TriggerFail);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string info = ResString.GetEnumDisplayName(typeof(UISuccess), UISuccess.TriggerSuccess);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
