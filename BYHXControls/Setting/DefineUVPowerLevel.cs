using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class DefineUVPowerLevel : UserControl
    {
        //UvPowerLevelMap uvPowerLeverMap;
        readonly uint flag;
        public DefineUVPowerLevel()
        {
            InitializeComponent();
            InitializeUvLightType();
            flag = GenFlag();
            groupBoxOnPrint.Visible = groupBoxOnReady.Visible = false;
        }

        private void InitializeUvLightType()
        {
            Type enumtype = typeof(UvLightType);
            UvLightType[] valse = (UvLightType[])Enum.GetValues(enumtype);
            for (int i = 0; i < valse.Length; i++)
            {
                string item = ResString.GetEnumDisplayName(enumtype, valse[i]);
                this.cmb_UvLightType.Items.Add(item);
            }
        }

        public void OnPrinterSettingChange(AllParam allParam)
        {
            this.cmb_UvLightType.SelectedIndex = allParam.PrinterSetting.UVSetting.eUvLightType >= cmb_UvLightType.Items.Count ? 0 : allParam.PrinterSetting.UVSetting.eUvLightType;
            var uvPowerLeverMap = allParam.UvPowerLevelMap;

            numericOnReadyOff.Value = uvPowerLeverMap.LeftL1PowerOnReady;
            numericOnReadyLow.Value = uvPowerLeverMap.LeftL2PowerOnReady;
            numericOnReadyMid.Value = uvPowerLeverMap.LeftL3PowerOnReady;
            numericOnReadyHigh.Value = uvPowerLeverMap.LeftL4PowerOnReady;
            numericOnPrintOff.Value = uvPowerLeverMap.LeftL1PowerOnPrint;
            numericOnPrintLow.Value = uvPowerLeverMap.LeftL2PowerOnPrint;
            numericOnPrintMid.Value = uvPowerLeverMap.LeftL3PowerOnPrint;
            numericOnPrintHigh.Value = uvPowerLeverMap.LeftL4PowerOnPrint;

        }

        private uint GenFlag()
        {
            int charL = (int)'L';
            int charM = (int)'M';
            int charA = (int)'A';
            int charP = (int)'P';
            UInt32 flag = (UInt32)(charL | (charM << 8) | (charA << 16) | (charP << 24));
            return flag;
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss,ref UvPowerLevelMap uvPowerLeverMap)
        {
            if (cmb_UvLightType.SelectedIndex == -1)
            {
                ss.UVSetting.eUvLightType = 0;
                uvPowerLeverMap.LightType = UvLightType.UvLightType1;
            }
            else
            {
                ss.UVSetting.eUvLightType = (byte)this.cmb_UvLightType.SelectedIndex;
                uvPowerLeverMap.LightType = (UvLightType)this.cmb_UvLightType.SelectedIndex;
            }
            uvPowerLeverMap.Flag = flag;
            uvPowerLeverMap.RightL1PowerOnReady = uvPowerLeverMap.LeftL1PowerOnReady = (byte)numericOnReadyOff.Value;
            uvPowerLeverMap.RightL2PowerOnReady = uvPowerLeverMap.LeftL2PowerOnReady = (byte)numericOnReadyLow.Value;
            uvPowerLeverMap.RightL3PowerOnReady = uvPowerLeverMap.LeftL3PowerOnReady = (byte)numericOnReadyMid.Value;
            uvPowerLeverMap.RightL4PowerOnReady = uvPowerLeverMap.LeftL4PowerOnReady = (byte)numericOnReadyHigh.Value;
            uvPowerLeverMap.RightL1PowerOnPrint = uvPowerLeverMap.LeftL1PowerOnPrint = (byte)numericOnPrintOff.Value;
            uvPowerLeverMap.RightL2PowerOnPrint = uvPowerLeverMap.LeftL2PowerOnPrint = (byte)numericOnPrintLow.Value;
            uvPowerLeverMap.RightL3PowerOnPrint = uvPowerLeverMap.LeftL3PowerOnPrint = (byte)numericOnPrintMid.Value;
            uvPowerLeverMap.RightL4PowerOnPrint = uvPowerLeverMap.LeftL4PowerOnPrint = (byte)numericOnPrintHigh.Value;
        }

        private void cmb_UvLightType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_UvLightType.SelectedIndex < 0) return;
            if (cmb_UvLightType.SelectedIndex == (int)UvLightType.UvLightType3)
            {
                groupBoxOnPrint.Visible = groupBoxOnReady.Visible = true;
            }
            else 
            {
                groupBoxOnPrint.Visible = groupBoxOnReady.Visible = false;
            }
          
        }
    }
}
