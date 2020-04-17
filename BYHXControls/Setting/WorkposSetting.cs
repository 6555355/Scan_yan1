using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Setting
{
    public partial class WorkposSetting : BYHXUserControl
    {
        public WorkposSetting()
        {
            InitializeComponent();

        }

        private UILengthUnit m_CurrentUnit = UILengthUnit.Centimeter;

        public void OnPreferenceChange(UIPreference up)
        {
            OnUnitChange(up.Unit);
            m_CurrentUnit = up.Unit;
        }

        private void OnUnitChange(UILengthUnit newUnit)
        {
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numPos1);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numPos2);

            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numPos1, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numPos2, this.m_ToolTip);
        }

        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            UIPreference.SetValueAndClampWithMinMax(this.numPos1, m_CurrentUnit,
                (float)ss.sExtensionSetting.WorkPosList[0] / _printerProperty.fPulsePerInchAxis4);
            UIPreference.SetValueAndClampWithMinMax(this.numPos2, m_CurrentUnit,
                (float)ss.sExtensionSetting.WorkPosList[1] / _printerProperty.fPulsePerInchAxis4);

            cbxEnable1.Checked = (ss.sExtensionSetting.WorkPosEnable & 1) == 1 ? true : false;
            cbxEnable2.Checked = (ss.sExtensionSetting.WorkPosEnable >> 1 & 1) == 1 ? true : false;
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            ss.sExtensionSetting.WorkPosList[0] =
                (int) (UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numPos1.Value)) *
                       _printerProperty.fPulsePerInchAxis4);
            ss.sExtensionSetting.WorkPosList[1] =
                (int) (UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numPos2.Value)) *
                       _printerProperty.fPulsePerInchAxis4);

            int enable = 0;
            if (cbxEnable1.Checked)
                enable = enable | 1;

            if (cbxEnable2.Checked)
                enable = enable | (1 << 1);

            ss.sExtensionSetting.WorkPosEnable = (byte) enable;

            EpsonLCD.SetWorkPosInfo(ss);

            LogWriter.WriteLog(
                new string[]
                {
                    string.Format("[JianRui]WorkPos1:{0},JianRui]WorkPos2:{1}", ss.sExtensionSetting.WorkPosList[0],
                        ss.sExtensionSetting.WorkPosList[1])
                }, true);
        }

        private SPrinterProperty _printerProperty;

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            _printerProperty = sp;
        }
    }
}
