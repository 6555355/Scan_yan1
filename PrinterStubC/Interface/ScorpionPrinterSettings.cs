using System.Collections.Generic;
using BYHXPrinterManager;

namespace PrinterStubC.Interface
{
    public struct ScorpionPrinterSettings
    {
        public int CurSettingKey;
        public List<MediaSetting> MediaSettings;
        public SDoubleSidePrint DoubleSidePrint;
    }

    public struct VacuumHalogenSections
    {
        // VacuumSection
        //public bool VacuumSection0Enable;
        public bool VacuumSection1Enable;
        public bool VacuumSection2Enable;
        public bool VacuumSection3Enable;
        public float VacuumSection0Begin;
        public float VacuumSection0End;
        public float VacuumSection1Begin;
        public float VacuumSection1End;
        public float VacuumSection2Begin;
        public float VacuumSection2End;
        public float VacuumSection3Begin;
        public float VacuumSection3End;
        // HalogenSection
        //public bool HalogenSection0Enable;
        public bool HalogenSection1Enable;
        public bool HalogenSection2Enable;
        public float HalogenSection0Begin;
        public float HalogenSection0End;
        public float HalogenSection1Begin;
        public float HalogenSection1End;
        public float HalogenSection2Begin;
        public float HalogenSection2End;

        /// <summary>
        /// Service Station Pos 单位是英寸
        /// </summary>
        public float ServiceStationPos;  // 

        /// <summary>
        /// 单位是脉冲数
        /// </summary>
        public int XOriginPos;  // 

        /// <summary>
        /// ‘A’：打印时自动打开;‘M’：一直开着
        /// </summary>
        public char VacuumMode;

        /// <summary>
        /// Vacuum电机功率,0-6000 有效值，单位0.01HZ（例：4000表示40HZ）
        /// </summary>
        public ushort VacuumPower;

        public bool VacuumSection1On;
        public bool VacuumSection2On;
        public bool VacuumSection3On;
        public bool HalogenSection0On;
        public bool HalogenSection1On;
        public bool HalogenSection2On;
    }

    public struct HeaterSetting
    {
        // Halogen
        public bool HalogenHeatOff;
        public float HalogenCurTemp;
        public float HalogenTargetTemp;
        // Carriage 
        public bool CarriageHeatOff;
        public float CarriageCurTemp;
        public float CarriageTargetTemp;
        // Dry 
        public bool DryHeatOff;
        public float DryCurTemp;
        public float DryTargetTemp;
        // Printing
        public bool PrintingHeatOff;
        public float PrintingCurTemp;
        public float PrintingTargetTemp;
    }

    public struct FlushWipingSetting
    {
        public ushort FirstSolutionPump;
        public ushort FirstSolutionAir;
        public ushort CappingPlateUp;
        public ushort FlushTimes;
        public ushort Time;
        public ushort Delay; 
        public ushort WipingTimes;
        public ushort SecondSolutionPump;
        public ushort SecondSolutionAir;
    }

    public struct MediaSetting
    {
        public string Name;
        public SPrinterSetting PrinterSetting;
        public VacuumHalogenSections VacuumHalogenSetting;
        public HeaterSetting HeatSetting;
        public FlushWipingSetting FlushWiping;
    }
    public enum ErrorFormat : byte
    {
        old = 0x00,
        New = 0x01
    }
}
