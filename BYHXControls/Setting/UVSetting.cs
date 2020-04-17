using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Setting
{
    public partial class UVSetting : UserControl
    {
        private SPrinterProperty m_sPrinterProperty;
        private UvPowerLevelMap _UvPowerLevelMap;
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        private GZUVX2Param GZUVX2Param;
        public float fPulsePerInchX;
        public UVSetting()
        {
            InitializeComponent();
        }

        public void SetUvPowerLevelMap(UvPowerLevelMap UvPowerLevelMap)
        {
            _UvPowerLevelMap = UvPowerLevelMap;
        }
        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_sPrinterProperty = sp;
            fPulsePerInchX = sp.fPulsePerInchX;
            
            //bool isSimpleUv = SPrinterProperty.IsSimpleUV();
            bool bshowReadyUvSet = PubFunc.GetUserPermission() == (int)UserPermission.SupperUser;
            m_checkbox_autoSwitchMode.Visible =
                label6.Visible = cmb_UvLightType.Visible =
            groupBox_Ready.Visible = bshowReadyUvSet;
            Type enumtype = typeof(UvLightType);
            UvLightType[] valse = (UvLightType[])Enum.GetValues(enumtype);
            for (int i = 0; i < valse.Length; i++)
            {
                string item = ResString.GetEnumDisplayName(enumtype, valse[i]);
                this.cmb_UvLightType.Items.Add(item);
            }
            groupBox_UVPower.Visible = SPrinterProperty.IsGongZengUv();

            groupBoxUVOffset.Visible = PubFunc.IsSupportUVOffsetDistance();

            if (groupBox_UVPower.Visible == false)
            {
                groupBoxUVOffset.Location = groupBox_UVPower.Location;
            }
        }

        private int OnGetRealTimeFromUI()
        {
            int status = 0;
            int left = MapUVStatusToJetStatus(m_ComboBoxLeftSet.SelectedIndex, true, false);
            int right = MapUVStatusToJetStatus(m_ComboBoxRightSet.SelectedIndex, false, false);
            if (m_CheckBoxShutterLeft.Checked)
                left |= (int)INTBIT.Bit_2;
            if (m_CheckBoxShutterRight.Checked)
                right |= (int)INTBIT.Bit_6;
            status = (left | right);
            int onelight = 0;
#if false
			if(m_CheckBoxOneLight.Checked)
				onelight |= 1;
			else
				onelight &= ~1;
			if(m_CheckBoxUVHighPower.Checked)
				onelight |= 2;
			else
				onelight &= ~2;
#endif
#if DOUBLE_SIDE_PRINT_HAPOND
            int leftP = MapUVStatusToJetStatus(m_ComboBoxPLeftSet.SelectedIndex, false, true);
            int rightP = MapUVStatusToJetStatus(m_ComboBoxPRightSet.SelectedIndex, true, true);
#else
            int leftP = MapUVStatusToJetStatus(m_ComboBoxPLeftSet.SelectedIndex, true, true);
            int rightP = MapUVStatusToJetStatus(m_ComboBoxPRightSet.SelectedIndex, false, true);
#endif
            onelight = (leftP | rightP);
            // 
            onelight |= (int)INTBIT.Bit_3;
            onelight |= (int)(m_checkbox_autoSwitchMode.Checked ? INTBIT.Bit_2 : 0);
            status |= (onelight << 16);
            return status;
        }
        public void OnPreferenceChange(UIPreference up)
        {
            if (m_CurrentUnit != up.Unit)
            {
                OnUnitChange(up.Unit);
                m_CurrentUnit = up.Unit;
                //				this.isDirty = false;
            }
        }

        private void OnUnitChange(UILengthUnit newUnit)
        {
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numericUpDownLeftDis);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numericUpDownRightDis);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numericUpDownShutterDis);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numPLOpen);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numPLClose);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numPROpen);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numPRClose);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numPLOpen2);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numPLClose2);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numPROpen2);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numPRClose2);

            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownLeftDis, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownRightDis, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownShutterDis, this.m_ToolTip);

            UIPreference.NumericUpDownToolTip(newUnitdis, this.numPLOpen, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numPLClose, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numPROpen, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numPRClose, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numPLOpen2, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numPLClose2, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numPROpen2, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numPRClose2, this.m_ToolTip);
            //			this.isDirty = false;
        }

        private UvLightType m_uvLightType = UvLightType.UvLightType1;
        public void OnPrinterSettingChange(AllParam allParam)
        {
            SPrinterSetting ss = allParam.PrinterSetting;

            m_uvLightType = (UvLightType)ss.UVSetting.eUvLightType;

            this.m_ComboBoxLeftSet.Items.Clear();
            this.m_ComboBoxRightSet.Items.Clear();
            this.m_ComboBoxPLeftSet.Items.Clear();
            this.m_ComboBoxPRightSet.Items.Clear();
            Type enumtype = typeof(UVStatus);
            if (m_uvLightType == UvLightType.UvLightType2 || m_uvLightType == UvLightType.UvLightType3)//(sp.IsAllWinZMeasurSensorSupport())
            {
                enumtype = typeof(UVStatus_ALLWIN);
                UVStatus_ALLWIN[] vals = (UVStatus_ALLWIN[])Enum.GetValues(enumtype);
                for (int i = 0; i < vals.Length; i++)
                {
                    string item = ResString.GetEnumDisplayName(enumtype, vals[i]);
                    this.m_ComboBoxLeftSet.Items.Add(item);
                    this.m_ComboBoxRightSet.Items.Add(item);
                    this.m_ComboBoxPLeftSet.Items.Add(item);
                    this.m_ComboBoxPRightSet.Items.Add(item);
                }
            }
            else
            {
                UVStatus[] vals = (UVStatus[])Enum.GetValues(enumtype);
                for (int i = 0; i < vals.Length; i++)
                {
                    string item = ResString.GetEnumDisplayName(enumtype, vals[i]);
                    this.m_ComboBoxLeftSet.Items.Add(item);
                    this.m_ComboBoxRightSet.Items.Add(item);
                    this.m_ComboBoxPLeftSet.Items.Add(item);
                    this.m_ComboBoxPRightSet.Items.Add(item);
                }
            }

            SUVSetting uvseting = ss.UVSetting;
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownShutterDis, uvseting.fShutterOpenDistance);
#if !DOUBLE_SIDE_PRINT_HAPOND
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownLeftDis, uvseting.fLeftDisFromNozzel);
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownRightDis, uvseting.fRightDisFromNozzel);
            this.checkBox1Leftprinton.Checked = (uvseting.iLeftRightMask & 0x02) != 0;
            this.checkBox1Rightprinton.Checked = (uvseting.iLeftRightMask & 0x01) != 0;
            this.checkBox2Leftprinton.Checked = (uvseting.iLeftRightMask & 0x08) != 0;
            this.checkBox2Rightprinton.Checked = (uvseting.iLeftRightMask & 0x04) != 0;
#else
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownLeftDis, uvseting.fRightDisFromNozzel);
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownRightDis, uvseting.fLeftDisFromNozzel);
            this.checkBox1Leftprinton.Checked = (uvseting.iLeftRightMask & 0x04) != 0;
            this.checkBox1Rightprinton.Checked = (uvseting.iLeftRightMask & 0x08) != 0;
            this.checkBox2Leftprinton.Checked = (uvseting.iLeftRightMask & 0x01) != 0;
            this.checkBox2Rightprinton.Checked = (uvseting.iLeftRightMask & 0x02) != 0;
#endif
            this.cmb_UvLightType.SelectedIndex = ss.UVSetting.eUvLightType;
            if (SPrinterProperty.IsGongZengUv())
            {
                if (EpsonLCD.GetGZUVX2Param(ref GZUVX2Param))
                {
                    numUVX1Power.Value = GZUVX2Param.UVX1Power <= 100 ? GZUVX2Param.UVX1Power : 0;
                    numUVX2Power.Value = GZUVX2Param.UVX2Power <= 100 ? GZUVX2Param.UVX2Power : 0;
                }
            }

            if (PubFunc.IsSupportUVOffsetDistance())
            {
                if (allParam.ExtendedSettings.UVOffsetDis.OffsetDistArray != null && allParam.ExtendedSettings.UVOffsetDis.OffsetDistArray.Length >= 8)
                {
                    UIPreference.SetValueAndClampWithMinMax(this.numPLOpen, allParam.ExtendedSettings.UVOffsetDis.OffsetDistArray[0]);
                    UIPreference.SetValueAndClampWithMinMax(this.numPLClose, allParam.ExtendedSettings.UVOffsetDis.OffsetDistArray[1]);
                    UIPreference.SetValueAndClampWithMinMax(this.numPROpen, allParam.ExtendedSettings.UVOffsetDis.OffsetDistArray[2]);
                    UIPreference.SetValueAndClampWithMinMax(this.numPRClose, allParam.ExtendedSettings.UVOffsetDis.OffsetDistArray[3]);

                    UIPreference.SetValueAndClampWithMinMax(this.numPLOpen2, allParam.ExtendedSettings.UVOffsetDis.OffsetDistArray[4]);
                    UIPreference.SetValueAndClampWithMinMax(this.numPLClose2, allParam.ExtendedSettings.UVOffsetDis.OffsetDistArray[5]);
                    UIPreference.SetValueAndClampWithMinMax(this.numPROpen2, allParam.ExtendedSettings.UVOffsetDis.OffsetDistArray[6]);
                    UIPreference.SetValueAndClampWithMinMax(this.numPRClose2, allParam.ExtendedSettings.UVOffsetDis.OffsetDistArray[7]);
                }
            }
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss ,ref PeripheralExtendedSettings ex)
        {
            ss.UVSetting.fShutterOpenDistance = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numericUpDownShutterDis.Value));
            int mask = 0x00;
#if !DOUBLE_SIDE_PRINT_HAPOND
            ss.UVSetting.fLeftDisFromNozzel = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numericUpDownLeftDis.Value));
            ss.UVSetting.fRightDisFromNozzel = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numericUpDownRightDis.Value));
            mask |= (this.checkBox1Leftprinton.Checked ? 0x2 : 0);
            mask |= (this.checkBox1Rightprinton.Checked ? 0x1 : 0);
            mask |= (this.checkBox2Leftprinton.Checked ? 0x8 : 0);
            mask |= (this.checkBox2Rightprinton.Checked ? 0x4 : 0);
            //			mask |=	(this.m_checkBoxVisableLeft.Checked? 0x1:0);
            //			mask |=	(this.m_checkBoxVisableRight.Checked?0x4:0);
#else
            ss.UVSetting.fLeftDisFromNozzel		=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDownRightDis.Value));
			ss.UVSetting.fRightDisFromNozzel		=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDownLeftDis.Value));
            mask |= (this.checkBox2Leftprinton.Checked ? 0x1 : 0);
            mask |= (this.checkBox2Rightprinton.Checked ? 0x2 : 0);
            mask |= (this.checkBox1Leftprinton.Checked ? 0x4 : 0);
            mask |= (this.checkBox1Rightprinton.Checked ? 0x8 : 0);
            //			mask |=	(this.m_checkBoxVisableLeft.Checked? 0x1:0);
            //			mask |=	(this.m_checkBoxVisableRight.Checked?0x4:0);
#endif
            ss.UVSetting.iLeftRightMask = (uint)mask;
            ss.UVSetting.eUvLightType = (byte)this.cmb_UvLightType.SelectedIndex;

            if (PubFunc.IsSupportUVOffsetDistance())
            {
                ex.UVOffsetDis = GetUVOffsetFromUI();
            }
        }


        public void OnRealTimeChange()
        {
            int status = 0;
            if (CoreInterface.GetUVStatus(ref status) != 0)
            {
                int UV_status = status & 0x33;
#if DOUBLE_SIDE_PRINT_HAPOND
				UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxLeftSet,MapJetStatusToUVStatus((UV_status),false,false));
                UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxRightSet, MapJetStatusToUVStatus((UV_status), true, false));
#else
                UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxLeftSet, MapJetStatusToUVStatus((UV_status), true, false));
                UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxRightSet, MapJetStatusToUVStatus((UV_status), false, false));
#endif
                m_CheckBoxShutterLeft.Checked = ((status & 0x4) != 0);
                m_CheckBoxShutterRight.Checked = ((status & 0x40) != 0);
#if false
				m_NumericUpDownDebugCur.Value = (byte)((status>>8)&0xff);
				m_CheckBoxOneLight.Checked = ((status>>16)&0x1) != 0;
				m_CheckBoxUVHighPower.Checked = ((status>>16)&0x2) != 0;
#endif
                int UV_statusP = (status >> 16) & 0xf0;
                UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxPLeftSet, MapJetStatusToUVStatus((UV_statusP), true, true));
                UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxPRightSet, MapJetStatusToUVStatus((UV_statusP), false, true));
                m_checkbox_autoSwitchMode.Checked = ((status >> 16) & 0x04) != 0;
            }
            else
                MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.LoadSetFromBoardFail));

        }


        public void SetPrinterStatusChanged(JetStatusEnum status)
        {

        }

        public void ApplyToBoard()
        {
            int status = 0;
            status = OnGetRealTimeFromUI();
#if false
			////////////////////////////////
			byte onelight = 1;
			if(m_CheckBoxOneLight.Checked)
			{
				onelight = 1;
			}
			else
			{
				onelight = 0;
			}
			const int port = 1;
			const byte PRINTER_PIPECMDSIZE = 26;
			byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
			//First Send Begin Updater
			m_pData[0] = 2 + 1;
			m_pData[1] = 0x45; //One Light mode
			m_pData[2] = onelight; 

			if(CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port)==0)
			{
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.SaveRealTimeFail));
				return;
			}
#endif
            byte[] uvPowerLevelMap = SerializationUnit.StructToBytes(_UvPowerLevelMap);
            uint length = (uint)uvPowerLevelMap.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x68, uvPowerLevelMap, ref length, 0, 0x01);
            if (ret == 0)
            {
                MessageBox.Show(ResString.GetResString("SetFailed"));
            }
            if (SPrinterProperty.IsGongZengUv())
            {
                GZUVX2Param.UVX1Power = (ushort)numUVX1Power.Value;
                GZUVX2Param.UVX2Power = (ushort)numUVX2Power.Value;
                if (!EpsonLCD.SetGZUVX2Param(GZUVX2Param))
                {
                    MessageBox.Show(ResString.GetResString("SetFailed"));
                }
            }
            if (CoreInterface.SetUVStatus(status) != 0)
            {
                if ((status & 0xf) == 0 || (status & 0xf0) == 0)
                    MessageBox.Show(ResString.GetResString("OpenUV"));
            }
            else
                MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.SaveRealTimeFail));

            if (PubFunc.IsSupportUVOffsetDistance())
            {
                UVOffsetDistanceUI uvOffset = GetUVOffsetFromUI();
                EpsonLCD.SetUVOffsetDistToFw(uvOffset, fPulsePerInchX);
            }
        }

        private UVOffsetDistanceUI GetUVOffsetFromUI()
        {
            UVOffsetDistanceUI uvOffset = new UVOffsetDistanceUI();
            uvOffset.OffsetDistArray = new float[8];

            uvOffset.OffsetDistArray[0] = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numPLOpen.Value));
            uvOffset.OffsetDistArray[1] = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numPLClose.Value));
            uvOffset.OffsetDistArray[2] = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numPROpen.Value));
            uvOffset.OffsetDistArray[3] = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numPRClose.Value));

            uvOffset.OffsetDistArray[4] = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numPLOpen2.Value));
            uvOffset.OffsetDistArray[5] = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numPLClose2.Value));
            uvOffset.OffsetDistArray[6] = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numPROpen2.Value));
            uvOffset.OffsetDistArray[7] = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numPRClose2.Value));

            return uvOffset;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">Bit0:ON/OFF  BIT1:HIGH/LOW  BIT2:SHUTER ON/OFF</param>
        /// <param name="bLeft"></param>
        /// <param name="bPrinting"></param>
        /// <returns></returns>
        private int MapUVStatusToJetStatus(int status, bool bLeft, bool bPrinting)
        {
            int jetstatus = 0;
            switch (m_uvLightType)
            {
                case UvLightType.UvLightType1:
                    {
                        if (status == (int)UVStatus.OFF)
                            jetstatus = 0;
                        else
                        {
                            jetstatus |= 1;
                            if (status == (int)UVStatus.ON100)
                            {
                                jetstatus |= 2;
                            }
                        }
                        break;
                    }
                case UvLightType.UvLightType2:
                case UvLightType.UvLightType3:
                    {
                        jetstatus = status & 0x3;
                        break;
                    }
            }
            if (bPrinting)
            {
                if (bLeft)
                    jetstatus = (jetstatus << 4);
                else
                    jetstatus = (jetstatus << 6);
            }
            else
            {
                if (!bLeft)
                    jetstatus = (jetstatus << 4);
            }
            return jetstatus;
        }
        private int MapJetStatusToUVStatus(int jetstatus, bool bLeft, bool bPrinting)
        {
            int iRet = 0;
            if (bPrinting)
            {
                if (bLeft)
                    jetstatus = (jetstatus >> 4);
                else
                    jetstatus = (jetstatus >> 6);
            }
            else
            {
                if (!bLeft)
                    jetstatus = (jetstatus >> 4);
            }
            switch (m_uvLightType)
            {
                case UvLightType.UvLightType1:
                    {
                        UVStatus status = UVStatus.OFF;
                        if ((jetstatus & 1) == 0)
                        {
                            status = UVStatus.OFF;
                        }
                        else
                        {
                            if ((jetstatus & 2) == 0)
                            {
                                status = UVStatus.ON60;
                            }
                            else
                            {
                                status = UVStatus.ON100;
                            }
                        }
                        iRet = (int)status;
                        break;
                    }
                case UvLightType.UvLightType2:
                case UvLightType.UvLightType3:
                    {
                        iRet = jetstatus & 0x3;
                        break;
                    }
            }
            return iRet;
        }

    }
}
