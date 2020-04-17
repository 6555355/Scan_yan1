/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;

namespace BYHXPrinterManager
{
    //This struct is revise value, aimed at head color align.
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SCalibrationHorizonSetting
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_HEAD_NUM)]
        public sbyte[] XLeftArray;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_HEAD_NUM)]
        public sbyte[] XRightArray;
        public int nBidirRevise;
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SCalibrationHorizonArray : ICloneable
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_RESLIST_NUM * CoreConst.MAX_SPEED_NUM * CoreConst.SIZEOF_CalibrationHorizonSetting)]
        public byte[] Data;

        public SCalibrationHorizonArray(byte[] data)
        {
            Data = new byte[CoreConst.MAX_RESLIST_NUM * CoreConst.MAX_SPEED_NUM * CoreConst.SIZEOF_CalibrationHorizonSetting];
            if (data == null)
                return;
            if (data.Length == Data.Length)
            {
                data.CopyTo(Data, 0);
            }
            else
            {
                Debug.Assert(false);
            }
        }

        public SCalibrationHorizonSetting this[int index]
        {
            get
            {
                if (Data == null || index * CoreConst.SIZEOF_CalibrationHorizonSetting >= Data.Length)
                {
                    //Debug.Assert(false);
                    return new SCalibrationHorizonSetting();
                }

                int length = Marshal.SizeOf(typeof(SCalibrationHorizonSetting));
                IntPtr ptr = Marshal.AllocHGlobal(length);
                Marshal.Copy(Data, index * length, ptr, length);
                SCalibrationHorizonSetting sSetting = (SCalibrationHorizonSetting)Marshal.PtrToStructure(ptr, typeof(SCalibrationHorizonSetting));
                Marshal.FreeHGlobal(ptr);

                return sSetting;
            }
            set
            {
                if (Data == null)
                    Data = new byte[CoreConst.MAX_RESLIST_NUM * CoreConst.MAX_SPEED_NUM * CoreConst.SIZEOF_CalibrationHorizonSetting];
                Debug.Assert(index * CoreConst.SIZEOF_CalibrationHorizonSetting < Data.Length);
                int length = Marshal.SizeOf(typeof(SCalibrationHorizonSetting));
                IntPtr ptr = Marshal.AllocHGlobal(length);
                Marshal.StructureToPtr(value, ptr, false);
                Marshal.Copy(ptr, Data, index * length, length);
                Marshal.FreeHGlobal(ptr);
            }
        }

        public int Length
        {
            get
            {
                return CoreConst.MAX_RESLIST_NUM * CoreConst.MAX_SPEED_NUM;
            }
        }

        #region Implementation of ICloneable

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            SCalibrationHorizonArray ret = new SCalibrationHorizonArray();
            ret.Data = (byte[])this.Data.Clone();
            return ret;
        }

        #endregion
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SCalibrationSetting : ICloneable
    {
        public int nStepPerHead; //????????????????????????????????????????????
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_PASS_NUM)]
        public int[] nPassStepArray;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_HEAD_NUM*2)]
        public short[] nVerticalArray;

        public int nLeftAngle;
        public int nRightAngle;

        public SCalibrationHorizonArray sCalibrationHorizonArray;

        #region Implementation of ICloneable

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            SCalibrationSetting ret = new SCalibrationSetting();
            ret.nStepPerHead = this.nStepPerHead;
            ret.nPassStepArray = (int[])this.nPassStepArray.Clone();
            ret.nVerticalArray = (short[])this.nVerticalArray.Clone();
            ret.nLeftAngle = this.nLeftAngle;
            ret.nRightAngle = this.nRightAngle;
            ret.sCalibrationHorizonArray = (SCalibrationHorizonArray)sCalibrationHorizonArray.Clone();
            return ret;
        }

        #endregion
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SAdvanceCalibrationSetting
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool bIgnoreAngle;
        [MarshalAs(UnmanagedType.I1)]
        public bool bIgnoreYoffset;
        [MarshalAs(UnmanagedType.I1)]
        public bool bIgnoreXOffset;
    }

    [Serializable]
    public struct SCalibrationHorizonArrayUI
    {
        public List<SCalibrationHorizonSettingUI> HorizonSettings;

        public SCalibrationHorizonArrayUI(object o) : this()
        {
            HorizonSettings = new List<SCalibrationHorizonSettingUI>();
            for (int j = 0; j < CoreConst.MAX_RESLIST_NUM; j++)
            {
                for (int i = 0; i < CoreConst.MAX_SPEED_NUM; i++)
                {
                    HorizonSettings.Add(new SCalibrationHorizonSettingUI(null){SpeedIndex = i,ResIndex = j});
                }
            }
        }

        public SCalibrationHorizonSettingUI this[int index]
        {
            get
            {
                if (HorizonSettings ==null)
                    HorizonSettings = new List<SCalibrationHorizonSettingUI>();
                if (index >= HorizonSettings.Count)
                {
                    HorizonSettings.Add(new SCalibrationHorizonSettingUI(null));
                }
                return HorizonSettings[index];
            }
            set
            {
                HorizonSettings[index] = value;
            }
        }

        public int Length
        {
            get
            {
                return CoreConst.MAX_RESLIST_NUM*CoreConst.MAX_SPEED_NUM;
            }
        }
    }

    [Serializable]
    public struct SCalibrationHorizonSettingUI
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_HORIZONARRAY_LEN)]
        public sbyte[] XLeftArray;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_HORIZONARRAY_LEN)]
        public sbyte[] XRightArray;

        public int ResIndex;
        public int SpeedIndex;
        public SCalibrationHorizonSettingUI(object oo)
        {
            XLeftArray = new sbyte[CoreConst.MAX_HORIZONARRAY_LEN];
            XRightArray = new sbyte[CoreConst.MAX_HORIZONARRAY_LEN];
            ResIndex = 0;
            SpeedIndex = 0;
        }
    }

    [Serializable]
    public struct SCalibrationGroupUI
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_RESLIST_NUM * CoreConst.MAX_SPEED_NUM * 32)]
        public short[] GCValue;

        public SCalibrationGroupUI(object o)
        {
            GCValue = new short[CoreConst.MAX_RESLIST_NUM * CoreConst.MAX_SPEED_NUM * 32];
        }
    }

}
