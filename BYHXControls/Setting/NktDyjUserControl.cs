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
    public partial class NktDyjUserControl : UserControl
    {
        private SPrinterProperty m_SPrinterProperty;
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;

        public NktDyjUserControl()
        {
            InitializeComponent();
            numBeltSpeed.Minimum =
                numFeedSpeed.Minimum =
                    numStepSpeed.Minimum = 0;
            numBeltSpeed.Maximum =
              numFeedSpeed.Maximum =
                  numStepSpeed.Maximum = int.MaxValue;
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_SPrinterProperty = sp;
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
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numDetectorOffset);

            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numDetectorOffset, this.m_ToolTip);
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            RefreshUiParameter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NKTParam nktParam = new NKTParam();
            nktParam.Flag = new char[] {'N', 'K', 'T', '\0'};
            nktParam.BeltSpeed = (uint) numBeltSpeed.Value;
            nktParam.FeedSpeed = (uint) numFeedSpeed.Value;
            nktParam.StepSpeed = (uint) numStepSpeed.Value;
            float offset =
                    (UIPreference.ToInchLength(m_CurrentUnit, (float) numDetectorOffset.Value)*
                     m_SPrinterProperty.fPulsePerInchY);
          nktParam.DetectorOffset = (uint)offset;
            if (!EpsonLCD.SetNktParam(nktParam))
            {
                MessageBox.Show("设置参数失败.");
            }
        }

        private void RefreshUiParameter()
        {
            NKTParam nktParam = new NKTParam();
            if (EpsonLCD.GetNktParam(ref nktParam))
            {
                numBeltSpeed.Value = nktParam.BeltSpeed;
                numFeedSpeed.Value = nktParam.FeedSpeed;
                numStepSpeed.Value = nktParam.StepSpeed;
                if (m_SPrinterProperty.fPulsePerInchY > 0)
                {
                    float offset = nktParam.DetectorOffset/m_SPrinterProperty.fPulsePerInchY;
                    numDetectorOffset.Value = (decimal) UIPreference.ToDisplayLength(m_CurrentUnit, offset);
                }
                else
                {
                    MessageBox.Show(string.Format("fPulsePerInchY==0,nktParam.DetectorOffset={0}.",
                        nktParam.DetectorOffset));

                }
            }
            else
            {
                MessageBox.Show("获取参数失败.");
            }

        }

        private void NktDyjUserControl_Load(object sender, EventArgs e)
        {
            //RefreshUiParameter();
        }
    }
}
