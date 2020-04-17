using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrinterStubC.VenderSetting
{
   public interface IVenderSetting<T> where  T :struct
   {
       bool SaveVenderSettings(T   settings);
       T? LoadVenderSettings();
   }
}
