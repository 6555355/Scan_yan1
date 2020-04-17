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
    public partial class UserExtensionSetting : UserControl
    {
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;

        public UserExtensionSetting()
        {
            InitializeComponent();
        }

        private bool isDirty = false;
        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }

        private void ToolTipInit()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BaseSetting));

            UIPreference.NumericUpDownToolTip(resources.GetString("numericUpDownFeedDistance.ToolTip"), this.numericUpDownFeedDistance, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(resources.GetString("numericUpDownZSensorOffset.ToolTip"), this.numericUpDownZSensorOffset, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(resources.GetString("numericUpDownZDefaultPos.ToolTip"), this.numericUpDownZDefaultPos, this.m_ToolTip);
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
        }

        private void OnUnitChange(UILengthUnit newUnit)
        {
            this.isDirty = false;
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            this.isDirty = false;
        }

        /// <summary>
        /// 控件显示更新
        /// </summary>
        /// <param name="ss"></param>
        public void OnPrinterSettingChange(AllParam allParam)
        {
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownFeedDistance, allParam.sPS_lecai.nForwardDis);
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownZSensorOffset, allParam.sPS_lecai.nZSensorOffset);
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownZDefaultPos, allParam.sPS_lecai.nDefaultPos);
            this.isDirty = false;
        }

        /// <summary>
        /// 获取界面中设定的值
        /// </summary>
        /// <param name="ss"></param>
        public void OnGetPrinterSetting(AllParam allParam)
        {
            allParam.sPS_lecai.nForwardDis = Decimal.ToUInt32(this.numericUpDownFeedDistance.Value);
            allParam.sPS_lecai.nZSensorOffset = Decimal.ToUInt32(this.numericUpDownZSensorOffset.Value);
            allParam.sPS_lecai.nDefaultPos = Decimal.ToUInt32(this.numericUpDownZDefaultPos.Value);
            this.isDirty = true;
        }
    }
}
