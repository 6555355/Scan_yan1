using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BYHXPrinterManager.GradientControls;

namespace BYHXPrinterManager.Setting
{
    public partial class Printer3DSetting : BYHXUserControl
    {
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;

        public Printer3DSetting()
        {
            InitializeComponent();

        }

        private bool isDirty = false;
        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }

        /// <summary>
        /// 单位变化
        /// </summary>
        /// <param name="up"></param>
        public void OnPreferenceChange(UIPreference up)
        {
            {
                OnUnitChange(up.Unit);
                m_CurrentUnit = up.Unit;
                this.isDirty = false;
            }
            this.cleanSetting3D1.OnPreferenceChange(up);
        }

        private void OnUnitChange(UILengthUnit newUnit)
        {
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.m_NumericUpDown_3DZDownDis);
            m_NumericUpDown_3DZDownDis.DecimalPlaces = 3;
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.m_NumericUpDown_3DSandDis);
     
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.m_NumericUpDown_ZAdjustment);
            m_NumericUpDown_ZAdjustment.DecimalPlaces = 4;
            m_NumericUpDown_ZAdjustment.Increment = new Decimal(0.0001f);

            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDown_ZAdjustment, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDown_3DZDownDis, this.m_ToolTip);
            this.isDirty = false;
        }


        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            if (PubFunc.IsFhzl3D() || PubFunc.IsZXHZ3D()||PubFunc.Is3DPrintMachine())
            {
                this.m_NumericUpDown_3DZDownDis.Visible = true;
                this.m_NumericUpDown_ZAdjustment.Visible = false;
            }
            else// (PubFunc.IsKINCOLOR_FLAT_UV())
            {
                this.m_NumericUpDown_3DZDownDis.Visible = false;
                this.m_NumericUpDown_ZAdjustment.Visible = true;
                m_NumericUpDown_ZAdjustment.Location = m_NumericUpDown_3DZDownDis.Location;
            }
            this.m_Label_3DSandDis.Visible = this.m_Label_3DFusionTime.Visible =
            this.m_Label_3DSandSpeed.Visible =
            this.m_NumericUpDown_3DSandDis.Visible = this.m_NumericUpDown_3DFusionTime.Visible =
            this.m_NumericUpDown_3DSandSpeed.Visible = false;

            this.cleanSetting3D1.OnPrinterPropertyChange(sp);
        }

        /// <summary>
        /// 控件显示更新
        /// </summary>
        /// <param name="ss"></param>
        public void OnPrinterSettingChange(AllParam allParam)
        {
            //UIPreference.SetValueAndClampWithMinMax(this.m_NumericUpDown_3DZDownDis, m_CurrentUnit, ss.sExtensionSetting.s3DPrint.fZDownDis);
            //UIPreference.SetValueAndClampWithMinMax(this.m_NumericUpDown_3DSandDis, m_CurrentUnit, ss.sExtensionSetting.s3DPrint.fSandDis);
            if (allParam.PrinterProperty.fPulsePerInchZ > 0)
                UIPreference.SetValueAndClampWithMinMax(this.m_NumericUpDown_3DZDownDis, m_CurrentUnit, allParam.s3DPrint.nZDownDis / allParam.PrinterProperty.fPulsePerInchZ);
#if false
            UIPreference.SetValueAndClampWithMinMax(this.m_NumericUpDown_3DSandDis,  ss.sExtensionSetting.s3DPrint.nSandDis);
            UIPreference.SetValueAndClampWithMinMax(this.m_NumericUpDown_3DFusionTime, ss.sExtensionSetting.s3DPrint.nFusionTime);
            UIPreference.SetValueAndClampWithMinMax(this.m_NumericUpDown_3DSandSpeed, ss.sExtensionSetting.s3DPrint.byteSandSpeed);
#endif
            UIPreference.SetValueAndClampWithMinMax(this.m_NumericUpDown_ZAdjustment, m_CurrentUnit, allParam.PrinterSetting.sBaseSetting.fZAdjustmentDistance);

            this.cleanSetting3D1.OnPrinterSettingChange(allParam);
            this.isDirty = false;
        }

        /// <summary>
        /// 获取界面中设定的值
        /// </summary>
        /// <param name="ss"></param>
        public void OnGetPrinterSetting(AllParam allParam)
        {
            allParam.s3DPrint.nZDownDis = (uint)(UIPreference.ToInchLength(m_CurrentUnit, (float)this.m_NumericUpDown_3DZDownDis.Value) * allParam.PrinterProperty.fPulsePerInchZ);
#if false
            allParam.s3DPrint.nSandDis = Decimal.ToUInt32(this.m_NumericUpDown_3DSandDis.Value);
            allParam.s3DPrint.nFusionTime = Decimal.ToUInt32(this.m_NumericUpDown_3DFusionTime.Value);
            allParam.s3DPrint.byteSandSpeed = Decimal.ToByte(this.m_NumericUpDown_3DSandSpeed.Value);
#endif
            allParam.PrinterSetting.sBaseSetting.fZAdjustmentDistance = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.m_NumericUpDown_ZAdjustment.Value));

            this.cleanSetting3D1.OnGetPrinterSetting(allParam);
        }

        public void SetGroupBoxStyle(Grouper ts)
        {
            this.GrouperTitleStyle = ts;
            this.cleanSetting3D1.GrouperTitleStyle = ts;
        }
    }
}
