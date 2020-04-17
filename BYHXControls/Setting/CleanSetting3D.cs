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
    public partial class CleanSetting3D : BYHXUserControl
    {
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        public CleanSetting3D()
        {
            InitializeComponent();

            numAutoClean.Minimum = 0;
            numAutoClean.Maximum = uint.MaxValue;
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            numPurgeX.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numPurgeX.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth*sp.fPulsePerInchX));
            numWipeX.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numWipeX.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth * sp.fPulsePerInchX));
            numericSandX.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numericSandX.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth * sp.fPulsePerInchX));
            numericCleanX.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numericCleanX.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth * sp.fPulsePerInchX));

            numPurgeY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numPurgeY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperHeight * sp.fPulsePerInchY));
            numWipeY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numWipeY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperHeight * sp.fPulsePerInchY));
            numericSandY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numericSandY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperHeight * sp.fPulsePerInchY));
            numericCleanY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numericCleanY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperHeight * sp.fPulsePerInchY));
        }

        public void OnPrinterSettingChange(AllParam allParam)
        {
            numAutoClean.Value = allParam.s3DPrint.rate;
            numPurgeX.Value = allParam.s3DPrint.purge.Xpos;
            numPurgeY.Value = allParam.s3DPrint.purge.Ypos;
            numPurgeDelayTime.Value = allParam.s3DPrint.purge.DelaySeconds;
            numWipeX.Value = allParam.s3DPrint.wipe.Xpos;
            numWipeY.Value = allParam.s3DPrint.wipe.Ypos;
            numWipeDelayTime.Value = allParam.s3DPrint.wipe.DelaySeconds;


            numericSandX.Value = allParam.s3DPrint.sand.Xpos;
            numericSandY.Value = allParam.s3DPrint.sand.Ypos;

            numericCleanX.Value = allParam.s3DPrint.clean.Xpos;
            numericCleanY.Value = allParam.s3DPrint.clean.Ypos;
            comboBoxCleanXSpeed.SelectedIndex = allParam.s3DPrint.clean.Xspeed;
            comboBoxCleanYSpeed.SelectedIndex = allParam.s3DPrint.clean.Yspeed;

        }

        public void OnGetPrinterSetting(AllParam allParam)
        {
            allParam.s3DPrint.rate = (uint)numAutoClean.Value;

            allParam.s3DPrint.purge.Xpos = (uint)numPurgeX.Value;
            allParam.s3DPrint.purge.Ypos = (uint)numPurgeY.Value;
            allParam.s3DPrint.purge.DelaySeconds = (uint)numPurgeDelayTime.Value;

            allParam.s3DPrint.wipe.Xpos = (uint)numWipeX.Value;
            allParam.s3DPrint.wipe.Ypos = (uint)numWipeY.Value;
            allParam.s3DPrint.wipe.DelaySeconds = (uint)numWipeDelayTime.Value;

            allParam.s3DPrint.sand.Xpos = (uint)numericSandX.Value;
            allParam.s3DPrint.sand.Ypos = (uint)numericSandY.Value;

            allParam.s3DPrint.clean.Xpos = (uint)numericCleanX.Value;
            allParam.s3DPrint.clean.Ypos = (uint)numericCleanY.Value;
            allParam.s3DPrint.clean.Xspeed = (ushort)comboBoxCleanXSpeed.SelectedIndex;
            allParam.s3DPrint.clean.Yspeed = (ushort)comboBoxCleanYSpeed.SelectedIndex;
        }

        public void OnPreferenceChange(UIPreference up)
        {
        }

    }
}
