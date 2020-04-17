using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BYHXPrinterManager;

namespace PrinterStubC.Common
{
    class SingletonBase
    {
    }
    public class GlobalSetting
    {
        private static GlobalSetting _mInstance = null;

        private GlobalSetting()
        { }

        public static GlobalSetting Instance
        {
            get { return _mInstance ?? (_mInstance = new GlobalSetting()); }
        }

        public void Init(string vendorProduct, bool bSupportUv, UILengthUnit unit)
        {
            _mVendorProduct = vendorProduct;
            _bSupportUv = bSupportUv;
            _unit = unit;
        }

        

        private string _mVendorProduct = string.Empty;
        public string VendorProduct
        {
            get
            {
                return _mVendorProduct;
            }
        }

        private bool _bSupportUv = false;
        public bool BSupportUv
        {
            get { return _bSupportUv; }
            set { _bSupportUv = value; }
        }
        private UILengthUnit _unit = UILengthUnit.Inch;
        public UILengthUnit Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
    }
}
