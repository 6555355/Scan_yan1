using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using BYHXPrinterManager;
using PrinterStubC.Common;

namespace PrinterStubC.CInterface
{
    #region 打印模式
    [TypeConverter(typeof(ModeConfig.PropertyConverter))]
    public class ModeConfig
    {
        private class PropertyConverter : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(value, true);
                List<PropertyDescriptor> list = new List<PropertyDescriptor>
                {
                    collection["AutoJumpWhite"],

                    collection["ReversePrint"],
                    collection["PrintMode"],
                    collection["Pass"],
                    collection["FeatherType"],
                    collection["Feather"],
                    //collection["BetweenNozzle"],
                    //collection["IntensityFeather"],
                    collection["Speed"],
                    collection["MultipleInk"],
                    collection["ExquisiteFeather"],
                    collection["FeatherType_Value"],
                    collection["Feather_Custom"],
                };

                SPrinterProperty sp = new SPrinterProperty();
                if (!sp.IsDocan())
                {
                    list.Add(collection["Bidirection"]);
                }

                if (GlobalSetting.Instance.BSupportUv)
                {
                    list.Add(collection["LeftLightLeftPrint"]);
                    list.Add(collection["LeftLightRightPrint"]);
                    list.Add(collection["RightLightLeftPrint"]);
                    list.Add(collection["RightLightRightPrint"]);
                }
                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        public ModeConfig()
        {
            m_Pass = "Global";
            m_Feather = "Global";
            m_FeatherType = "Global";
            m_PrintMode = "Global";
            m_Speed = "Global";
            m_RepeatPrint = "Global";
            m_MultipleInk = "Global";
        }
        public ModeConfig Clone()
        {
            ModeConfig ret = (ModeConfig)MemberwiseClone();
            return ret;
        }
        /// <summary>
        /// UV
        /// </summary>
        private BoolEx m_LeftLightLeftPrint;

        [CategoryAttribute("UV"), SrDisplayName("LeftLightLeftPrint"), DefaultValue(BoolEx.Global), ReadOnly(false)]
        public BoolEx LeftLightLeftPrint
        {
            get { return m_LeftLightLeftPrint; }
            set { m_LeftLightLeftPrint = value; }
        }

        private BoolEx m_LeftLightRightPrint;

        [CategoryAttribute("UV"), DefaultValue(BoolEx.Global), SrDisplayName("LeftLightRightPrint"), ReadOnly(false)]
        public BoolEx LeftLightRightPrint
        {
            get { return m_LeftLightRightPrint; }
            set { m_LeftLightRightPrint = value; }
        }

        private BoolEx m_RightLightLeftPrint;

        [CategoryAttribute("UV"), SrDisplayName("RightLightLeftPrint"), DefaultValue(BoolEx.Global), ReadOnly(false)]
        public BoolEx RightLightLeftPrint
        {
            get { return m_RightLightLeftPrint; }
            set { m_RightLightLeftPrint = value; }
        }

        private BoolEx m_RightLightRightPrint;

        [CategoryAttribute("UV"), DefaultValue(BoolEx.Global), SrDisplayName("RightLightRightPrint"), ReadOnly(false)]
        public BoolEx RightLightRightPrint
        {
            get { return m_RightLightRightPrint; }
            set { m_RightLightRightPrint = value; }
        }
        /// <summary>
        /// Misc
        /// </summary>
        private string m_Name;

        [SrCategory("Misc"), SrDisplayName("Name"), ReadOnly(false)]
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        private BoolEx m_Bidirection;

        [DefaultValue(BoolEx.Global), SrCategory("Misc"), SrDisplayName("Bidirection"), ReadOnly(false)]
        public BoolEx Bidirection
        {
            get
            {
                return m_Bidirection;
            }
            set
            {
                m_Bidirection = value;
            }
        }

        private BoolEx m_AutoJumpWhite;

        [SrCategory("Misc"), SrDisplayName("AutoJumpWhite"), DefaultValue(BoolEx.Global), ReadOnly(false)]
        public BoolEx AutoJumpWhite
        {
            get
            {
                return m_AutoJumpWhite;
            }
            set
            {
                m_AutoJumpWhite = value;
            }
        }
        /// <summary>
        /// Print
        /// </summary>
        private BoolEx m_ReversePrint;

        [SrCategory("Print"), SrDisplayName("ReversePrint"), DefaultValue(BoolEx.Global), ReadOnly(false)]
        public BoolEx ReversePrint
        {
            get { return m_ReversePrint; }
            set { m_ReversePrint = value; }
        }

        private string m_PrintMode;

        [SrCategory("Print"), SrDisplayName("PrintMode"), TypeConverter(typeof(PrintModeListConverter)),
         DefaultValue("Global"), ReadOnly(false)]
        public string PrintMode
        {
            get { return m_PrintMode; }
            set { m_PrintMode = value; }
        }

        private string m_Pass;

        [SrCategory("Print"), TypeConverter(typeof(PassListConverter)), DefaultValue("Global"), ReadOnly(false)]
        //[SrDisplayName("Pass")]
        public string Pass
        {
            get { return m_Pass; }
            set { m_Pass = value; }
        }

        private string m_FeatherType;

        [SrCategory("Print"), SrDisplayName("FeatherType"), TypeConverter(typeof(FeatherTypeListConverter)),
         DefaultValue("Global"), ReadOnly(false)]
        public string FeatherType
        {
            get
            {
                //if (m_FeatherType == "Global")
                //{
                //    PropertyHandle.SetPropertyReadOnly(this, "FeatherType_Value", true);
                //}
                //else
                {
                    foreach (FeatherType type in Enum.GetValues(typeof(FeatherType)))
                    {
                        string cmode = ResString.GetEnumDisplayName(typeof(FeatherType), type);
                        if (cmode == m_FeatherType)
                        {
                            switch (type)
                            {
                                case BYHXPrinterManager.FeatherType.Uv:
                                case BYHXPrinterManager.FeatherType.Advance:
                                case BYHXPrinterManager.FeatherType.Wave:
                                    {
                                        if (!m_exquisiteFeather)
                                        {
                                            PropertyHandle.SetPropertyReadOnly(this, "FeatherType_Value", false);
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        PropertyHandle.SetPropertyReadOnly(this, "FeatherType_Value", true);
                                        m_FeatherType_Value = 0;
                                        break;
                                    }
                            }
                        }
                    }
                }
                return m_FeatherType;
            }
            set
            {
                m_FeatherType = value;
            }
        }

        private string m_Feather;

        [SrCategory("Print"), SrDisplayName("Feather"), TypeConverter(typeof(FeatherListConverter)),
         DefaultValue("Global"), ReadOnly(false)]
        public string Feather
        {
            get
            {
                //if (m_Feather == "Global")
                //{
                //    PropertyHandle.SetPropertyReadOnly(this, "Feather_Custom", true);
                //    m_Feather_Custom = 0;
                //}
                //else
                {
                    foreach (EpsonFeatherType place in Enum.GetValues(typeof(EpsonFeatherType)))
                    {
                        string cmode = ResString.GetEnumDisplayName(typeof(EpsonFeatherType), place);
                        if (cmode == m_Feather)
                        {
                            switch (place)
                            {
                                case EpsonFeatherType.None:
                                    {
                                        PropertyHandle.SetPropertyReadOnly(this, "Feather_Custom", true);
                                        m_Feather_Custom = 0;
                                        break;
                                    }
                                case EpsonFeatherType.Small:
                                    {
                                        PropertyHandle.SetPropertyReadOnly(this, "Feather_Custom", true);
                                        m_Feather_Custom = SPrinterProperty.IsGongZengUv() || SPrinterProperty.IsTILE_PRINT_ID() ? 100 : 33;
                                        break;
                                    }
                                case EpsonFeatherType.Medium:
                                    {
                                        PropertyHandle.SetPropertyReadOnly(this, "Feather_Custom", true);
                                        m_Feather_Custom = SPrinterProperty.IsGongZengUv() || SPrinterProperty.IsTILE_PRINT_ID() ? 200 : 66;
                                        break;
                                    }
                                case EpsonFeatherType.Large:
                                    {
                                        PropertyHandle.SetPropertyReadOnly(this, "Feather_Custom", true);
                                        m_Feather_Custom = SPrinterProperty.IsGongZengUv() || SPrinterProperty.IsTILE_PRINT_ID() ?400:101;//100:101;
                                        break;
                                    }
                                case EpsonFeatherType.Custom:
                                    {
                                        if (!m_exquisiteFeather)
                                        {
                                            PropertyHandle.SetPropertyReadOnly(this, "Feather_Custom", false);
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
                return m_Feather;
            }
            set
            {
                m_Feather = value;
            }
        }

        private bool m_exquisiteFeather;

        [SrCategory("Print"), SrDisplayName("ExquisiteFeather"), ReadOnly(false)]
        //[DefaultValue(BoolEx.Global)]
        public bool ExquisiteFeather
        {
            get
            {
                PropertyHandle.SetPropertyReadOnly(this, "Feather", m_exquisiteFeather);
                PropertyHandle.SetPropertyReadOnly(this, "FeatherType", m_exquisiteFeather);
                PropertyHandle.SetPropertyReadOnly(this, "FeatherType_Value", m_exquisiteFeather);
                PropertyHandle.SetPropertyReadOnly(this, "Feather_Custom", m_exquisiteFeather);
                return m_exquisiteFeather;
            }
            set
            {
                m_exquisiteFeather = value;
            }
        }

        //private BoolEx m_BetweenNozzle;

        //[SrCategory("Print"), SrDisplayName("BetweenNozzleFeather"), DefaultValue(BoolEx.Global), ReadOnly(false)]
        //public BoolEx BetweenNozzle
        //{
        //    get { return m_BetweenNozzle; }
        //    set { m_BetweenNozzle = value; }
        //}

        //private BoolEx m_IntensityFeather;

        //[SrCategory("Print"), SrDisplayName("IntensityFeather"), DefaultValue(BoolEx.Global), ReadOnly(false)]
        //public BoolEx IntensityFeather
        //{
        //    get { return m_IntensityFeather; }
        //    set { m_IntensityFeather = value; }
        //}

        private string m_Speed;

        [SrCategory("Print"), SrDisplayName("Speed"), TypeConverter(typeof(SpeedListConverter)), DefaultValue("Global"),
         ReadOnly(false)]
        public string Speed
        {
            get
            {
                return m_Speed;
            }
            set
            {
                m_Speed = value;
            }
        }

        private BoolEx m_MirrorX;

        [SrCategory("Print"), SrDisplayName("MirrorX"), DefaultValue(BoolEx.Global), ReadOnly(false)]
        public BoolEx MirrorX
        {
            get { return m_MirrorX; }
            set { m_MirrorX = value; }
        }

        private string m_MultipleInk;

        [SrCategory("Print"), SrDisplayName("MultipleInk"), TypeConverter(typeof(MultipleInkListConverter)),
         DefaultValue("Global"), ReadOnly(false)]
        public string MultipleInk
        {
            get { return m_MultipleInk; }
            set { m_MultipleInk = value; }
        }

        private string m_RepeatPrint;

        [SrCategory("Print"), SrDisplayName("RepeatPrint"), TypeConverter(typeof(RepeatPrintListConverter)), DefaultValue("Global"), ReadOnly(false)]
        public string RepeatPrint
        {
            get { return m_RepeatPrint; }
            set { m_RepeatPrint = value; }
        }

        private float m_FeatherType_Value;

        [SrCategory("Print"), SrDisplayName("FeatherPercent"), ReadOnly(false)]
        public float FeatherType_Value
        {
            get { return m_FeatherType_Value; }
            set
            {
                foreach (FeatherType type in Enum.GetValues(typeof(FeatherType)))
                {
                    string cmode = ResString.GetEnumDisplayName(typeof(FeatherType), type);
                    if (cmode == m_FeatherType)
                    {
                        switch (type)
                        {
                            case BYHXPrinterManager.FeatherType.Uv:
                            case BYHXPrinterManager.FeatherType.Advance:
                                {
                                    if (value > 100 || value < -1) return;
                                    m_FeatherType_Value = (float)Math.Floor(value);
                                    break;
                                }
                            case BYHXPrinterManager.FeatherType.Wave:
                                {
                                    if (value > UIPreference.ToDisplayLength(GlobalSetting.Instance.Unit, 4.0f) || value < UIPreference.ToDisplayLength(GlobalSetting.Instance.Unit, 0.39f)) return;
                                    m_FeatherType_Value = (float)decimal.Round((decimal)value, 2);
                                    break;
                                }
                        }
                    }
                }
            }
        }

        private int m_Feather_Custom;

        [SrCategory("Print"), SrDisplayName("Feather_Custom"), ReadOnly(false)]
        public int Feather_Custom
        {
            get { return m_Feather_Custom; }
            set
            {
                if (value > 10800 || value < -1) return;
                m_Feather_Custom = value;
            }
        }
    };
    public class JobMode
    {
        private string m_Name = "";
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        private int m_LayerNum;
        public int LayerNum
        {
            get { return m_LayerNum; }
            set { m_LayerNum = value; }
        }

        private uint m_LayerColorArray;
        public uint LayerColorArray
        {
            get { return m_LayerColorArray; }
            set { m_LayerColorArray = value; }
        }

        private ushort m_SpotColor1Mask;
        public ushort SpotColor1Mask
        {
            get { return m_SpotColor1Mask; }
            set { m_SpotColor1Mask = value; }
        }

        public List<ModeConfig> Items = new List<ModeConfig>();

        private ModeConfig m_Item = new ModeConfig();
        public ModeConfig Item
        {
            set { m_Item = value; }
            get { return m_Item; }
        }

        public JobMode()
        {
            m_Item = new ModeConfig();
            m_LayerNum = 1;
            m_LayerColorArray = 0;
            m_SpotColor1Mask = 0;
        }
    };
    public class JobModes
    {
        public List<JobMode> Items;

        public JobModes()
        {
            Items = new List<JobMode>();
        }
    };
    internal sealed class SrCategoryAttribute : CategoryAttribute
    {
        public SrCategoryAttribute(string category)
            : base(category)
        { }

        protected override string GetLocalizedString(string value)
        {
            string name = ResString.GetResString(value);
            if (string.IsNullOrEmpty(name))
                return value;
            return name;
        }
    }
    public class SrDisplayNameAttribute : DisplayNameAttribute
    {
        public SrDisplayNameAttribute(string category) : base(category) { }

        public override string DisplayName
        {
            get
            {
                string name = ResString.GetResString(DisplayNameValue);
                if (string.IsNullOrEmpty(name))
                    return DisplayNameValue;
                return name;
            }
        }
    }
    public class PassListConverter : TypeConverter
    {
        public override bool
        GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // display drop
        }
        public override bool
        GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; // drop-down vs combo
        }
        public override StandardValuesCollection
        GetStandardValues(ITypeDescriptorContext context)
        {
            // note you can also look at context etc to build list
            //TODO：此处和InitComboBoxPass代码一样
            string sPass = ResString.GetDisplayPass();
            List<string> Passes = new List<string>();
            Passes.Add("Global");
            for (int i = 0; i < CoreConst.MAX_PASS_NUM; i++)
            {
                //int passNum = PassList[i];
                string dispPass = (i + 1).ToString() + " " + sPass;
                Passes.Add(dispPass);
            }

            return new StandardValuesCollection(Passes);
        }

        public override string ToString()
        {
            return "Global";
        }
    }
    public class FeatherListConverter : TypeConverter
    {
        public override bool
        GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // display drop
        }
        public override bool
        GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; // drop-down vs combo
        }
        public override StandardValuesCollection
        GetStandardValues(ITypeDescriptorContext context)
        {
            // note you can also look at context etc to build list
            //TODO：此处和InitComboBoxPass代码一样
            List<string> Items = new List<string>();
            Items.Add("Global");
            foreach (EpsonFeatherType place in Enum.GetValues(typeof(EpsonFeatherType)))
            {
                string cmode = ResString.GetEnumDisplayName(typeof(EpsonFeatherType), place);
                Items.Add(cmode);
            }

            return new StandardValuesCollection(Items);
        }

        public override string ToString()
        {
            return "Global";
        }
    }
    public class FeatherTypeListConverter : TypeConverter
    {
        public override bool
        GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // display drop
        }
        public override bool
        GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; // drop-down vs combo
        }
        public override StandardValuesCollection
        GetStandardValues(ITypeDescriptorContext context)
        {
            // note you can also look at context etc to build list
            //TODO：此处和InitComboBoxPass代码一样
            List<string> Items = new List<string>();
            Items.Add("Global");
            foreach (FeatherType place in Enum.GetValues(typeof(FeatherType)))
            {
                string cmode = ResString.GetEnumDisplayName(typeof(FeatherType), place);
                //string cmode = place.ToString();
                Items.Add(cmode);
            }

            return new StandardValuesCollection(Items);
        }

        public override string ToString()
        {
            return "Global";
        }
    }
    public class SpeedListConverter : TypeConverter
    {
        public override bool
        GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // display drop
        }
        public override bool
        GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; // drop-down vs combo
        }
        public override StandardValuesCollection
        GetStandardValues(ITypeDescriptorContext context)
        {
            // note you can also look at context etc to build list
            //TODO：此处和InitComboBoxPass代码一样
            List<string> Items = new List<string>();
            Items.Add("Global");

            foreach (SpeedEnum mode in Enum.GetValues(typeof(SpeedEnum)))
            {
                if (mode == SpeedEnum.CustomSpeed)
                    continue;
                string cmode = ResString.GetEnumDisplayName(typeof(SpeedEnum), mode);
                //if (BYHXPrinterManager.SystemCall.AppSpeed == 4)
                //{
                //if ((int)mode >= 10) continue;
                //cmode = "VSD_" + ((int)mode + 1).ToString();
                //}
                //else
                //{
                //    if ((int)mode < 10) continue;
                //    cmode = mode.ToString();
                //}
                Items.Add(cmode);
            }

            return new StandardValuesCollection(Items);
        }

        public override string ToString()
        {
            return "Global";
        }
    }
    public class RepeatPrintListConverter : TypeConverter
    {
        public override bool
        GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // display drop
        }
        public override bool
        GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; // drop-down vs combo
        }
        public override StandardValuesCollection
        GetStandardValues(ITypeDescriptorContext context)
        {
            // note you can also look at context etc to build list
            //TODO：此处和InitComboBoxPass代码一样
            string sPass = ResString.GetDisplayPass();
            List<string> RepeatList = new List<string>();
            RepeatList.Add("Global");
            for (int i = 0; i < 10; i++)
            {
                //int passNum = PassList[i];
                string repeat = i.ToString();
                RepeatList.Add(repeat);
            }

            return new StandardValuesCollection(RepeatList);
        }

        public override string ToString()
        {
            return "Global";
        }
    }
    public class MultipleInkListConverter : TypeConverter
    {
        public override bool
        GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // display drop
        }
        public override bool
        GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; // drop-down vs combo
        }
        public override StandardValuesCollection
        GetStandardValues(ITypeDescriptorContext context)
        {
            // note you can also look at context etc to build list
            //TODO：此处和InitComboBoxPass代码一样
            List<string> Items = new List<string>();
            Items.Add("Global");
            for (int i = 0; i < 8; i++)
            {
                Items.Add((i + 1).ToString());
            }
            //foreach (MultipleInkEnum place in Enum.GetValues(typeof(MultipleInkEnum)))
            //{
            //    string cmode = ResString.GetEnumDisplayName(typeof(MultipleInkEnum), place);
            //    Items.Add(cmode);
            //}

            return new StandardValuesCollection(Items);
        }

        public override string ToString()
        {
            return "Global";
        }
    }
    public class PrintModeListConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // display drop
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; // drop-down vs combo
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            // note you can also look at context etc to build list
            //TODO：此处和InitComboBoxPass代码一样
            List<string> Items = new List<string>();
            Items.Add("Global");
            foreach (XResDivMode place in Enum.GetValues(typeof(XResDivMode)))
            {
                if (place < XResDivMode.PrintMode3)
                {
                    string cmode = ResString.GetEnumDisplayName(typeof(XResDivMode), place);
                    Items.Add(cmode);
                }
            }

            return new StandardValuesCollection(Items);
        }
        public override string ToString()
        {
            return "Global";
        }
    }
    public static class PropertyHandle
    {
        #region 反射控制只读、可见属性
        //SetPropertyVisibility(obj,   "名称 ",   true);   
        //obj指的就是你的SelectObject，   “名称”是你SelectObject的一个属性   
        //当然，调用这两个方法后，重新SelectObject一下，就可以了。  
        /// <summary>  
        /// 通过反射控制属性是否只读  
        /// </summary>  
        /// <param name="obj"></param>  
        /// <param name="propertyName"></param>  
        /// <param name="readOnly"></param>  
        public static void SetPropertyReadOnly(object obj, string propertyName, bool readOnly)
        {
            Type type = typeof(ReadOnlyAttribute);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            AttributeCollection attrs = props[propertyName].Attributes;
            FieldInfo fld = type.GetField("isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance);
            fld.SetValue(attrs[type], readOnly);
        }

        /// <summary>  
        /// 通过反射控制属性是否可见  
        /// </summary>  
        /// <param name="obj"></param>  
        /// <param name="propertyName"></param>  
        /// <param name="visible"></param>  
        public static void SetPropertyVisibility(object obj, string propertyName, bool visible)
        {
            Type type = typeof(BrowsableAttribute);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            AttributeCollection attrs = props[propertyName].Attributes;
            FieldInfo fld = type.GetField("browsable", BindingFlags.Instance | BindingFlags.NonPublic);
            fld.SetValue(attrs[type], visible);
        }
        #endregion
    }
    #endregion

}
