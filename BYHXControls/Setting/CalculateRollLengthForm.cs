using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class CalculateRollLengthForm : Form
    {
        public CalculateRollLengthForm()
        {
            InitializeComponent();
        }

        public void OnExtendedSettingsChange(PeripheralExtendedSettings ss)
        {
            UIPreference.SetValueAndClampWithMinMax(numDiameterCore, UILengthUnit.Millimeter, ss.fDiameterCore);
            UIPreference.SetValueAndClampWithMinMax(numDiameterRoll, UILengthUnit.Millimeter, ss.fDiameterRoll);
            UIPreference.SetValueAndClampWithMinMax(numMediaThickness, UILengthUnit.Millimeter, ss.fMediaThickness);
        }

        public void OnGetExtendedSettingsChange(ref PeripheralExtendedSettings ss)
        {
            ss.fDiameterCore = UIPreference.ToInchLength(UILengthUnit.Millimeter, (float)numDiameterCore.Value);
            ss.fDiameterRoll = UIPreference.ToInchLength(UILengthUnit.Millimeter, (float)numDiameterRoll.Value);
            ss.fMediaThickness = UIPreference.ToInchLength(UILengthUnit.Millimeter, (float)numMediaThickness.Value);
            ss.fCalculateRollLength = UIPreference.ToInchLength(UILengthUnit.Meter, (float)numCalculateRollLength.Value);
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            CalculateRollLength();
        }

        private void CalculateRollLengthForm_Load(object sender, EventArgs e)
        {
            CalculateRollLength();
        }

        private void CalculateRollLength()
        {
            float fDiameterCore = UIPreference.ToInchLength(UILengthUnit.Millimeter, (float)numDiameterCore.Value); //筒径
            float fDiameterRoll = UIPreference.ToInchLength(UILengthUnit.Millimeter, (float)numDiameterRoll.Value);//卷径(
            float fMediaThickness = UIPreference.ToInchLength(UILengthUnit.Millimeter, (float)numMediaThickness.Value);//介质厚度
            if (fMediaThickness > 0)
            {
                float fCalculateRollLength = (float)((Math.PI * (Math.Pow(fDiameterRoll, 2) - Math.Pow(fDiameterCore, 2)) / 4) / fMediaThickness);
                UIPreference.SetValueAndClampWithMinMax(numCalculateRollLength, UILengthUnit.Meter, fCalculateRollLength);
            }
        }

        private void numDiameterCore_ValueChanged(object sender, EventArgs e)
        {
            numDiameterRoll.Minimum = numDiameterCore.Value;
        }
    }
}
