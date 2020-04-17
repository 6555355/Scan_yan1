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
    public partial class DoublePrintSetting : BYHXUserControl
    {
        private UILengthUnit _mCurrentUnit = UILengthUnit.Null;
        public DoublePrintSetting()
        {
            InitializeComponent();
        }

        public void OnGetScorpionSettings(ref SDoubleSidePrint settings)
        {
            settings.CrossColor = 0;
            if (crossColorY.Checked)
                settings.CrossColor |= 1;
            if (crossColorM.Checked)
                settings.CrossColor |= 1 << 1;
            if (crossColorC.Checked)
                settings.CrossColor |= 1 << 2;
            if (crossColorK.Checked)
                settings.CrossColor |= 1 << 3;
            if (crossColorLc.Checked)
                settings.CrossColor |= 1 << 4;
            if (crossColorLm.Checked)
                settings.CrossColor |= 1 << 5;
            settings.CrossFlag = this.cmbCrossFlag.SelectedIndex;
            var value = this.updownAddLineNum.Value;
            if (value != null)
                settings.AddLineNum = (int)value;
            var value1 = this.updownCrossHeight.Value;
            if (value1 != null)
                settings.CrossHeight = UIPreference.ToInchLength(_mCurrentUnit, (float)value1);
            value1 = this.updownCrossOffsetX.Value;
            if (value1 != null)
                settings.CrossOffsetX = UIPreference.ToInchLength(_mCurrentUnit, (float)value1);
            value1 = this.updownCrossOffsetY.Value;
            if (value1 != null)
                settings.CrossOffsetY = UIPreference.ToInchLength(_mCurrentUnit, (float)value1);
            value1 = this.updownCrossWidth.Value;
            if (value1 != null)
                settings.CrossWidth = UIPreference.ToInchLength(_mCurrentUnit, (float)value1);
            value1 = this.updownPenWidth.Value;
            if (value1 != null)
                settings.PenWidth = UIPreference.ToInchLength(_mCurrentUnit, (float)value1);
        }

        public void OnScorpionSettingsChanged(SDoubleSidePrint settings)
        {
            this.updownAddLineNum.Value = settings.AddLineNum;
            this.updownPenWidth.Value = new decimal(UIPreference.ToDisplayLength(_mCurrentUnit, settings.PenWidth));
            this.updownCrossWidth.Value = (decimal)UIPreference.ToDisplayLength(_mCurrentUnit, settings.CrossWidth);
            this.updownCrossHeight.Value = (decimal)UIPreference.ToDisplayLength(_mCurrentUnit, settings.CrossHeight);
            this.updownCrossOffsetX.Value = (decimal)UIPreference.ToDisplayLength(_mCurrentUnit, settings.CrossOffsetX);
            this.updownCrossOffsetY.Value = (decimal)UIPreference.ToDisplayLength(_mCurrentUnit, settings.CrossOffsetY);
            this.cmbCrossFlag.SelectedIndex = settings.CrossFlag;
            this.crossColorY.Checked = (settings.CrossColor & (1 << 0)) != 0;
            this.crossColorM.Checked = (settings.CrossColor & (1 << 1)) != 0;
            this.crossColorC.Checked = (settings.CrossColor & (1 << 2)) != 0;
            this.crossColorK.Checked = (settings.CrossColor & (1 << 3)) != 0;
            this.crossColorLc.Checked = (settings.CrossColor & (1 << 4)) != 0;
            this.crossColorLm.Checked = (settings.CrossColor & (1 << 5)) != 0;
        }
        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            //这里应该按照ripcolororder显示,但目前无法得到ripcolororder
            //this.crossColorY.Text = sp.Get_ColorIndex(0);
            //this.crossColorM.Text = sp.Get_ColorIndex(1);
            //this.crossColorC.Text = sp.Get_ColorIndex(2);
            //this.crossColorK.Text = sp.Get_ColorIndex(3);
            //this.crossColorLc.Text = sp.Get_ColorIndex(4);
            //this.crossColorLm.Text = sp.Get_ColorIndex(5);
        }
        public void OnPreferenceChange(UIPreference up)
        {
            OnUnitChange(up.Unit);
            _mCurrentUnit = up.Unit;
        }

        private void OnUnitChange(UILengthUnit newUnit)
        {
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, _mCurrentUnit, updownPenWidth);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, _mCurrentUnit, updownCrossWidth);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, _mCurrentUnit, updownCrossHeight);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, _mCurrentUnit, updownCrossOffsetX);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, _mCurrentUnit, updownCrossOffsetY);
            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            toolTip1.SetToolTip(updownPenWidth, newUnitdis);
            toolTip1.SetToolTip(updownCrossWidth, newUnitdis);
            toolTip1.SetToolTip(updownCrossHeight, newUnitdis);
            toolTip1.SetToolTip(updownCrossOffsetX, newUnitdis);
            toolTip1.SetToolTip(updownCrossOffsetY, newUnitdis);
        }                                                                       

    }
}
