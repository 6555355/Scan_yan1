using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class FloraParamControl : UserControl
    {
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        public FloraParamControl()
        {
            InitializeComponent();
        }
        public void OnPreferenceChange(UIPreference up)
        {
            UILengthUnit newUnit = up.Unit;
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numCappingPosY);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numCleanPosZ);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numCappingPosZ);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numSuckStartPlace);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numSuckEndPlace);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numPreOffset);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numCleanSlotSpace);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numScraperStart1);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numScraperStart2);
            
            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numCappingPosY, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numCleanPosZ, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numCappingPosZ, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numSuckStartPlace, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numSuckEndPlace, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numPreOffset, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numCleanSlotSpace, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numScraperStart1, this.toolTip1);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numScraperStart2, this.toolTip1);


            string speedUnit = string.Format("{0}/{1}", newUnitdis, ResString.GetResString("DisplayTime_Second"));
            UIPreference.NumericUpDownToolTip(speedUnit, this.numPretreatmentSpeed, this.toolTip1);
            UIPreference.NumericUpDownToolTip(speedUnit, this.numRuturnSpeed, this.toolTip1);
            m_CurrentUnit = newUnit;
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            bool isT50 = SPrinterProperty.IsFloraT50();
            panelSuckEnd.Visible =
            panelCappingY.Visible =
            label7.Visible =
            numPreOffset.Visible = isT50;
            panelCleanMotorSpeed.Visible =
            panelCleanSlotCount.Visible = panelCleanSlotSpace.Visible = !isT50;
            numPreOffset.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numPreOffset.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));

            numSuckStartPlace.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numSuckStartPlace.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));
            numSuckEndPlace.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numSuckEndPlace.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));
            numCleanPosZ.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numCleanPosZ.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));
            numCappingPosZ.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numCappingPosZ.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));
            numCappingPosY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numCappingPosY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperHeight));

            numPurgeInkRecoverTime.Minimum = numPurgeInkTime.Minimum = 0;
            numPurgeInkRecoverTime.Maximum = numPurgeInkTime.Maximum = int.MaxValue;
            numCleanSlotCount.Minimum = 0;
            numCleanSlotSpace.Minimum = 0;
            numCleanMotorSpeed.Minimum = 0;

            numScraperStart1.Minimum = numScraperStart2.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numScraperStart1.Maximum = numScraperStart2.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));

        }
        
        public void OnSettingChanged(FloraParamUI param)
        {
            UIPreference.SetValueAndClampWithMinMax(numCappingPosZ, m_CurrentUnit, param.cappingZPlace);
            UIPreference.SetValueAndClampWithMinMax(numCappingPosY, m_CurrentUnit, param.cappingYPlace);
            UIPreference.SetValueAndClampWithMinMax(numCleanPosZ, m_CurrentUnit, param.cleanZPlace);
            UIPreference.SetValueAndClampWithMinMax(numSuckStartPlace, m_CurrentUnit, param.suckStartPlace);
            UIPreference.SetValueAndClampWithMinMax(numSuckEndPlace, m_CurrentUnit, param.suckEndPlace);
            UIPreference.SetValueAndClampWithMinMax(numPreOffset, m_CurrentUnit, param.preOffset);
            UIPreference.SetValueAndClampWithMinMax(numRuturnSpeed, m_CurrentUnit, param.speedBack);
            UIPreference.SetValueAndClampWithMinMax(numPretreatmentSpeed, m_CurrentUnit, param.speed);
            numPurgeInkTime.Value = param.purgeInkTime;
            numPurgeInkRecoverTime.Value = param.purgeInkRecoverTime;
            checkBoxPretreatment.Checked = param.bIsNeedPrepare;
            cbxSprayAfterScraping.Checked = param.bIsNeedCleanFlash;
            numCleanSlotCount.Value = param.cleanSlotNum;
            UIPreference.SetValueAndClampWithMinMax(numCleanSlotSpace, m_CurrentUnit, param.cleanSlotSpace);
            numCleanMotorSpeed.Value = param.cleanMotorSpeed;
            numCappingDelayTime.Value = param.doWetDelay;
            numWaitTime.Value = param.waitTime;
            UIPreference.SetValueAndClampWithMinMax(numScraperStart1, m_CurrentUnit, param.scraperStart1);
            UIPreference.SetValueAndClampWithMinMax(numScraperStart2, m_CurrentUnit, param.scraperStart2);
        }

        public void OnGetSettings(ref FloraParamUI param)
        {
            param.purgeInkRecoverTime = (uint) numPurgeInkRecoverTime.Value;
            param.purgeInkTime = (uint) numPurgeInkTime.Value;
            param.cleanZPlace = UIPreference.ToInchLength(m_CurrentUnit, (float) numCleanPosZ.Value);
            param.cappingYPlace = UIPreference.ToInchLength(m_CurrentUnit, (float) numCappingPosY.Value);
            param.cappingZPlace = UIPreference.ToInchLength(m_CurrentUnit, (float) numCappingPosZ.Value);
            param.suckStartPlace = UIPreference.ToInchLength(m_CurrentUnit, (float) numSuckStartPlace.Value);
            param.suckEndPlace = UIPreference.ToInchLength(m_CurrentUnit, (float) numSuckEndPlace.Value);
            param.bIsNeedPrepare = checkBoxPretreatment.Checked;
            param.bIsNeedCleanFlash = cbxSprayAfterScraping.Checked;
            param.speed = UIPreference.ToInchLength(m_CurrentUnit, (float)  numPretreatmentSpeed.Value);
            param.preOffset = UIPreference.ToInchLength(m_CurrentUnit, (float)numPreOffset.Value);
            param.cleanSlotNum = (byte) numCleanSlotCount.Value;
            param.cleanSlotSpace = UIPreference.ToInchLength(m_CurrentUnit, (float)numCleanSlotSpace.Value);
            param.cleanMotorSpeed = (uint)numCleanMotorSpeed.Value;
            param.doWetDelay=(ushort)numCappingDelayTime.Value;
            param.waitTime = (ushort) numWaitTime.Value;
            param.speedBack = UIPreference.ToInchLength(m_CurrentUnit, (float) numRuturnSpeed.Value);
            param.scraperStart1 = UIPreference.ToInchLength(m_CurrentUnit, (float)numScraperStart1.Value);
            param.scraperStart2 = UIPreference.ToInchLength(m_CurrentUnit, (float)numScraperStart2.Value);
        }

        private void checkBoxPretreatment_CheckedChanged(object sender, EventArgs e)
        {
            numPretreatmentSpeed.Enabled = checkBoxPretreatment.Checked;
        }
    }
}
