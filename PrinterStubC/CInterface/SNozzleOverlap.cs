using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace BYHXPrinterManager
{
    [Serializable]
    public struct SNozzleOverlap
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public ushort[] OverLapTotalNozzle;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public ushort[] OverLapUpWasteNozzle;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public ushort[] OverLapDownWasteNozzle;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public ushort[] OverLapUpPercent;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public ushort[] OverLapDownPercent;

        public SNozzleOverlap(object o)
        {
            OverLapTotalNozzle = new ushort[1024];
            OverLapUpWasteNozzle = new ushort[1024];
            OverLapDownWasteNozzle = new ushort[1024];
            OverLapUpPercent = new ushort[1024];
            OverLapDownPercent = new ushort[1024];
        }
    }

    [Serializable]
    public struct SPrintQualityUI
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_HEAD_NUM * 2)]
        public float[] VerticalOffset;

        public SPrintQualityUI(object o)
        {
            VerticalOffset = new float[CoreConst.MAX_HEAD_NUM * 2];
        }
    }
    
}
