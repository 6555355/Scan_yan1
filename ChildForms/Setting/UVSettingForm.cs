using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Setting
{
    public partial class UVSettingForm : ByhxBaseChildForm
    {
        private bool bZMeasurSensorSupport = false;
        private UvPowerLevelMap _UvPowerLevelMap;
        public UVSettingForm(UvPowerLevelMap UvPowerLevelMap)
        {
            InitializeComponent();
            uvSetting1.SetUvPowerLevelMap(UvPowerLevelMap);
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            bZMeasurSensorSupport = sp.IsZMeasurSupport && !sp.IsALLWIN_FLAT();
            if (bZMeasurSensorSupport && !this.tabControl1.TabPages.Contains(this.tabPage2))
            {
                this.tabControl1.TabPages.Add(this.tabPage2);
            }
            else if (!bZMeasurSensorSupport && this.tabControl1.TabPages.Contains(this.tabPage2))
            {
                this.tabControl1.TabPages.Remove(this.tabPage2);
            }
            uvSetting1.OnPrinterPropertyChange(sp);
        }

        public void OnPreferenceChange(UIPreference up)
        {
            zAixsSetting1.OnPreferenceChange(up);
            uvSetting1.OnPreferenceChange(up);
        }

        public void OnPrinterSettingChange(AllParam allParam)
        {
            zAixsSetting1.OnPrinterSettingChange(allParam.PrinterSetting);
            uvSetting1.OnPrinterSettingChange(allParam);
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss, ref PeripheralExtendedSettings ex)
        {
            bool bMoveSettingChang = false;
            //zAixsSetting1.OnGetPrinterSetting(ref ss, ref bMoveSettingChang);
            uvSetting1.OnGetPrinterSetting(ref ss, ref ex);
        }

        public void SetPrinterStatusChanged(JetStatusEnum status)
        {
            zAixsSetting1.SetPrinterStatusChanged(status);
        }

        private void m_ButtonToBoard_Click(object sender, System.EventArgs e)
        {
            uvSetting1.ApplyToBoard();
        }

        private void UVSettingForm_Load(object sender, EventArgs e)
        {
            uvSetting1.OnRealTimeChange();
        }
    }
}
