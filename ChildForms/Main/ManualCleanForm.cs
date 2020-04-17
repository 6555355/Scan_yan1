using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Main
{
    public partial class ManualCleanForm : Form
    {
        private bool bColorJet = false;
        private bool bSsystem = false;
        private bool bDocanTextile = false;
        private bool bGzGmaHead = false;
        private bool bIsHLC = false;
        private bool bIsPQ = false;
        public ManualCleanForm(SPrinterProperty sp)
        {
            InitializeComponent();

            bGzGmaHead = SPrinterProperty.IsGongZeng() &&
    (sp.ePrinterHead == PrinterHeadEnum.Fujifilm_GMA9905300_5pl ||
    sp.ePrinterHead == PrinterHeadEnum.Fujifilm_GMA3305300_5pl);
            bColorJet = PubFunc.IsColorJet_Belt_Textile();
            bSsystem = CoreInterface.IsS_system();
            bDocanTextile = PubFunc.IsDocan_Belt_Textile();
            bIsHLC = PubFunc.IsHuiLiCai();
            bIsPQ = PubFunc.IsPuQi();

        }

        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        public void OnPreferenceChange(UIPreference up)
        {
            if (m_CurrentUnit != up.Unit)
            {
                OnUnitChange(up.Unit);
                m_CurrentUnit = up.Unit;
            }
            this.manualCleanSetting.OnPreferenceChange(up);
            docanManualCleanSetting1.OnPreferenceChange(up);
            if (bColorJet)
            {
                if (bSsystem)
                    colorjetManualCleanSettingS.OnPreferenceChange(up);
                else
                    colorjetManualCleanSettingA1.OnPreferenceChange(up);
            }
            if (bGzGmaHead)
                this.gmaCleanSetting1.OnPreferenceChange(up);
            if (bIsHLC)
                this.hlcCleanSetting1.OnPreferenceChange(up);
            if (bIsPQ)
                this.pqCleanSetting1.OnPreferenceChange(up);

        }
        private void OnUnitChange(UILengthUnit newUnit)
        {
        }


        public void OnPrinterSettingChange(SPrinterSetting ss, SPrinterProperty sp)
        {

        this.manualCleanSetting.OnPrinterSettingChange(ss, sp);
        if (bDocanTextile)
            this.docanManualCleanSetting1.OnPrinterSettingChange(ss, sp);
            if (bColorJet)
            {
                if (bSsystem)
                    colorjetManualCleanSettingS.OnPrinterSettingChange(ss, sp);
                else
                    colorjetManualCleanSettingA1.OnPrinterSettingChange(ss, sp);
            }
            if (bGzGmaHead)
                this.gmaCleanSetting1.OnPrinterSettingChange(ss);

            if (bIsHLC)
                this.hlcCleanSetting1.OnPrinterSettingChange(ss);
            if (bIsPQ)
                this.pqCleanSetting1.OnPrinterSettingChange(ss);

        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            this.manualCleanSetting.OnGetPrinterSetting(ref ss);
            this.docanManualCleanSetting1.OnGetPrinterSetting(ref ss);
            if (bColorJet)
            {
                if (bSsystem)
                    colorjetManualCleanSettingS.OnGetPrinterSetting(ref ss);
                else
                    colorjetManualCleanSettingA1.OnGetPrinterSetting(ref ss);
            }
            if (bGzGmaHead)
                this.gmaCleanSetting1.OnGetPrinterSetting(ref ss);

            if (bIsHLC)
                this.hlcCleanSetting1.OnGetPrinterSetting(ref ss);

            if (bIsPQ)
                this.pqCleanSetting1.OnGetPrinterSetting(ref ss);
        }
        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            colorjetManualCleanSettingS.Visible = bColorJet && bSsystem && !bDocanTextile && !bIsHLC;
            colorjetManualCleanSettingA1.Visible = bColorJet && !bSsystem && !bDocanTextile && !bIsHLC;
            manualCleanSetting.Visible = !bColorJet && !bDocanTextile && !bIsHLC;
            docanManualCleanSetting1.Visible = bDocanTextile;
            gmaCleanSetting1.Visible = bGzGmaHead;
            hlcCleanSetting1.Visible = bIsHLC;
            pqCleanSetting1.Visible = bIsPQ;

            gmaCleanSetting1.Dock =
            colorjetManualCleanSettingS.Dock =
            colorjetManualCleanSettingA1.Dock =
            hlcCleanSetting1.Dock =
            pqCleanSetting1.Dock =
            manualCleanSetting.Dock = DockStyle.Fill;
            
            this.manualCleanSetting.OnPrinterPropertyChange(sp);
            this.docanManualCleanSetting1.OnPrinterPropertyChange(sp);
            if (bColorJet)
            {
                if (bSsystem)
                    colorjetManualCleanSettingS.OnPrinterPropertyChange(sp);
                else
                    colorjetManualCleanSettingA1.OnPrinterPropertyChange(sp);
            }
            if (bGzGmaHead)
                this.gmaCleanSetting1.OnPrinterPropertyChange(sp);

            if (bIsHLC)
                this.hlcCleanSetting1.OnPrinterPropertyChange(sp);

            if (bIsPQ)
                this.pqCleanSetting1.OnPrinterPropertyChange(sp);
        }
        private void m_ButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
