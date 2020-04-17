using System;
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace BYHXPrinterManager
{
	///   <summary>   
	///   Style   的摘要说明。   
	///   </summary>   
    //[Serializable]
    //[TypeConverter(typeof(StyleConverter))]   
	public class Style : MarshalByRefObject   
	{   
		public readonly static string[] PropertyNames = 
			new string[] { 
							 "Color1",   
							 "Color2"   
						 };   
    
		//		private Font FFont   ;   
		private Color FBackColor = SystemColors.Control;//Color.LightBlue;
        private Color FForeColor = SystemColors.Control;//Color.SteelBlue;
		//		private Image FBackgroundImage;   
		public Style()   
		{   
		}   
    
		public Style(Color bc, Color fc)   
		{   
			this.FBackColor = bc;   
			this.FForeColor = fc;   
		}   
    
		/*public   Font   Font   
			  {   
				  get   
				  {   
				  return   FFont   ;   
				  }   
				  set   
				  {   
				  FFont   =   value   ;   
				  }   
			  }*/   
    
		public Color Color1     
		{   
			get     
			{   
				return FBackColor ;   
			}   
			set   
			{   
				FBackColor = value ;   
			}   
		}   
		public Color Color2   
		{   
			get   
			{   
				return FForeColor;   
			}   
			set   
			{   
				FForeColor = value;   
			}   
		}   
    
		/*public   Image   BackgroundImage     
			  {   
				  get   
				  {   
				  return   FBackgroundImage;   
				  }   
				  set   
				  {   
				  FBackgroundImage   =   value;   
				  }   
			  }*/   
    
		public override string ToString()   
		{   
			//   没有实现   
			string ret = string.Empty;
            ret += TypeDescriptor.GetConverter(Color1).ConvertToString(Color1) + "|";
            ret += TypeDescriptor.GetConverter(Color2).ConvertToString(Color2);
			return ret;   
		}   
    
		///   <summary>   
		///   从字符串解析出实例   
		///   </summary>   
		///   <param   name="source"></param>   
		///   <returns></returns>   
		public static Style Parse(string source)   
		{   
			//   没有实现   
			string[] colors = source.Split(new char[]{'|'});
			if(colors.Length != 2)
				return null;   
			else
			{
                Color Color1 = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(colors[0]);
                Color Color2 = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(colors[1]);

                Style rets = new Style(Color1, Color2);
				return rets;
			}
		}   
	}   
    
	public class StyleConverter : System.ComponentModel.TypeConverter   
	{   
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)   
		{   
			if (sourceType == typeof(string))   
				return true;   
			return base.CanConvertFrom(context, sourceType);   
		}   
    
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)   
		{   
			if (destinationType == typeof(string))   
				return true;   
			if (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor))   
				return true;   
			return base.CanConvertTo(context, destinationType);   
		}   
    
		public override object ConvertFrom (ITypeDescriptorContext context,   
			System.Globalization.CultureInfo culture, object value)   
		{   
			if (value is string)   
			{   
				//   从字符串中解析出   Style   实例，请自已实现。   
				return Style.Parse(value as string);   
			}   
			return base.ConvertFrom(context, culture, value);   
		}   
    
		public override object ConvertTo(ITypeDescriptorContext context,     
			System.Globalization.CultureInfo culture, object value, Type destinationType)   
		{   
			if (destinationType == typeof(string))   
			{   
				Style gs = (Style)value;   
				//   Style   的   ToString()   请自行实现。   
				return gs.ToString();   
			}   
			if (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor))   
			{   
				Style gs = (Style)value;   
				return new System.ComponentModel.Design.Serialization.InstanceDescriptor(   
					typeof(Style).GetConstructor(   
					new Type[2]{typeof(Color), typeof(Color)}),     
					new Color[2]{gs.Color1, gs.Color2});   
			} 
			return base.ConvertTo(context, culture, value, destinationType);   
		}   
    
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)   
		{   
			return true;     
		}     
    
    
		public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)   
		{   
			return new Style((Color)propertyValues[Style.PropertyNames[0]],   
				(Color)propertyValues[Style.PropertyNames[1]]);   
		}   
    
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)   
		{   
			return true;   
		}   
    
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context,   
			object value, Attribute[] attributes)   
		{   
			PropertyDescriptorCollection properties;   
			properties = TypeDescriptor.GetProperties(value, attributes);   
    
			return properties.Sort(Style.PropertyNames);   
		}   
	}   
}
