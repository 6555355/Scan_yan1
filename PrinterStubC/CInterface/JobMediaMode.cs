using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BYHXPrinterManager;

namespace PrinterStubC.CInterface
{
    #region 介质类型参数
    [TypeConverter(typeof(MediaConfig.PropertyConverter))]
    public class MediaConfig
    {
        private class PropertyConverter : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(value, true);
                List<PropertyDescriptor> list = new List<PropertyDescriptor>
                {
                    collection["BaseStep"],
                };
                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        public MediaConfig()
        {

        }
        public MediaConfig Clone()
        {
            MediaConfig ret = (MediaConfig)MemberwiseClone();
            return ret;
        }


        private int m_BaseStep;

        [SrCategory("Print"), SrDisplayName("BaseStep"), ReadOnly(false)]
        public int BaseStep
        {
            get { return m_BaseStep; }
            set
            {
                m_BaseStep = value;
            }
        }
    }
    public class JobMediaMode
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

        public List<MediaConfig> Items = new List<MediaConfig>();

        private MediaConfig m_Item = new MediaConfig();
        public MediaConfig Item
        {
            set { m_Item = value; }
            get { return m_Item; }
        }

        public JobMediaMode()
        {
            m_Item = new MediaConfig();
            m_LayerNum = 1;
            m_LayerColorArray = 0;
            m_SpotColor1Mask = 0;
        }
    };
    public class JobMediaModes
    {
        public List<JobMediaMode> Items;

        public JobMediaModes()
        {
            Items = new List<JobMediaMode>();
        }
    };
    #endregion

}
