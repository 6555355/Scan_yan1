using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MultimediaLayout.Helper
{
    /// <summary>
    /// WPF控件本地化帮助类,方便在Winform程序中调用WPF窗口时的本地化。
    /// Author:LJP 
    /// Create Date:2015/01/22
    /// Update Date:
    /// </summary>
    public class LocalizationHelper
    {
        private static Dictionary<string, ResourceDictionary> langs = new Dictionary<string, ResourceDictionary>();
        private static ResourceDictionary curLang = null;
        private static Control appWindow = null;

       

        public static void Initialize()
        {
            LoadDefault();
        }

        /// <summary>
        /// 加载默认支持的语言
        /// </summary>
        private static void LoadDefault()
        {
            string EN_US = "/MultimediaLayout;component/Resources/Langs/en-US.xaml";
            string ZH_CHS = "/MultimediaLayout;component/Resources/Langs/zh-CHS.xaml";
            string ZH_CHT = "/MultimediaLayout;component/Resources/Langs/zh-CHT.xaml";
            langs["en-US"] = new ResourceDictionary() { Source = new Uri(EN_US, UriKind.RelativeOrAbsolute) };
            langs["zh-CHS"] = new ResourceDictionary() { Source = new Uri(ZH_CHS, UriKind.RelativeOrAbsolute) };
            langs["zh-CHT"] = new ResourceDictionary() { Source = new Uri(ZH_CHT, UriKind.RelativeOrAbsolute) };
        }

        public static void SetCurrentCultrues(Control target)
        {
            //target.Resources.MergedDictionaries.Remove(curLang);
            target.Resources.MergedDictionaries.Add(curLang);
        }
     
        public static void SetCultures(Control target,string cultureName,bool isAppLevel=true)
        {
            if (isAppLevel)
            {
                appWindow = target;
            }
            Initialize();
            if (!langs.ContainsKey(cultureName))
                return;
            var newLang = langs[cultureName];
            target.Resources.MergedDictionaries.Remove(curLang);
            target.Resources.MergedDictionaries.Add(newLang);
            curLang = newLang;
        }

        /// <summary>
        /// 支持的语言
        /// </summary>
        public static List<string> SupportLangs
        {
            get
            {
                return new List<string>(langs.Keys);
            }
        }

        public static string GetStringResource(string key)
        {
            if (appWindow == null) return null;
           return  appWindow.FindResource(key) as string;
        }
       
    }
}
