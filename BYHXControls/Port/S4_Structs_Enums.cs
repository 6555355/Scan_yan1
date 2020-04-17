using System.Runtime.InteropServices;
using System.Collections;
using System;

namespace BYHXPrinterManager
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct S4OPENINFO
	{
		public short dwS4OpenInfoSize;//必须为sizeof(S4OPENINFO)。
		public short dwShareMode;
		/*设备连接模式，现在有两种支持的模式：
							S4_EXCLUSIZE_MODE 独占模式
							S4_SHARE_MODE 共享模式*/
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct SENSE4_CONTEXT
	{
        public int dwIndex;		//device index
		public int dwVersion;		//version		
		public IntPtr hLock;			//device handle
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
		public byte[] reserve;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
		public byte[] bAtr;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] bID;
		public uint dwAtrLen;
	}

	public enum SubFunction_ID : byte
	{
		ValidDateCheck = 1,
		ValidDateAndDataCheck = 2,
		GetPassWord = 3,
		SetPassWord = 4,
		GetFeatureWords = 5,
		SetFeatureWords = 6,
		GetDongleInfo = 7,
        GetRandomNum = 8,
        CreatPassWord = 9,
		IsMasterDog =10,
		GetDongleInfoNoTime = 11,
        SetProductInfo = 12,
        GetProductCat = 0x0e,
        GetDogTimer = 0x11
    }

	/* RTC (Real-Time Clock) format		*/
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct RTC_TIME_T
	{
		public byte second;								/* second (0-59)					*/
		public byte minute;								/* minute (0-59)					*/
		public byte hour;								/* hour (0-23)						*/
		public byte day;								/* day of month (1-31)				*/
		public byte week;								/* day of week (0-6, sunday is 0)	*/
		public byte month;								/* month (0-11)						*/
		public ushort year;								/* year (0- 138, 1900 - 2038)		*/

		public RTC_TIME_T(DateTime mValidDate)
		{
			this.second = 0;//(byte)mValidDate.Second;
			this.minute = 0;//(byte)mValidDate.Minute;
			this.hour = 0;//(byte)mValidDate.Hour;
			this.day = (byte)mValidDate.Day;
			this.week = (byte)mValidDate.DayOfWeek;
			this.month = (byte)(mValidDate.Month - 1);
			this.year = (ushort)(mValidDate.Year - 1900);
		}

		public RTC_TIME_T(byte[] mValidDate)
		{
			if (mValidDate.Length >= Marshal.SizeOf(typeof(RTC_TIME_T)))
			{
				this.second = (byte)mValidDate[0];
				this.minute = (byte)mValidDate[1];
				this.hour = (byte)mValidDate[2];
				this.day = (byte)mValidDate[3];
				this.week = (byte)mValidDate[4];
				this.month = (byte)mValidDate[5];
				ushort y = BitConverter.ToUInt16(mValidDate, 6);
				this.year = y;
			}
			else
			{
				this.day = 0;
				this.hour = 0;
				this.minute = 0;
				this.month = 0;
				this.second = 0;
				this.week = 0;
				this.year = 0;
			}
		}

		public DateTime ToDateTime()
		{
			DateTime dt =new DateTime(this.year + 1900, this.month + 1, this.day, this.hour, this.minute, this.second); 
			return dt;
		}

		public byte[] ToByteArray()
		{
			byte[] year = BitConverter.GetBytes(this.year);
			byte[] bb = new byte[8] { this.second, this.minute, this.hour,
										this.day,this.week,this.month,year[0],year[1] };
			return bb;
		}
	}

	/* user-specific data block */
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct FILE_OP_BLOCK
	{
		public short fid;     // objective file ID 
		public short offset;  // operating offset
		public byte len; 	    // buffer length(update)
		// read length(read)	
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xf0)]    //#define MAX_BUFF_SIZE  0xf0      //maximum buffer size 
		public byte[] buff;

		public FILE_OP_BLOCK(short mfid, short moffset, byte mlen, byte[] mbuf)
		{
			this.fid = mfid;
			this.offset = moffset;
			this.len = mlen;
			this.buff = mbuf;
		}
		public byte[] ToByteArray()
		{
			byte[] mCREATE_BLOCK = new byte[Marshal.SizeOf(typeof(FILE_OP_BLOCK))];
			byte[] mfid = BitConverter.GetBytes(this.fid);

			int drcOffset = 0;
			Buffer.BlockCopy(mfid, 0, mCREATE_BLOCK, drcOffset, mfid.Length);
			drcOffset += mfid.Length;

			mfid = BitConverter.GetBytes(this.offset);
			Buffer.BlockCopy(mfid, 0, mCREATE_BLOCK, drcOffset, mfid.Length);
			drcOffset += mfid.Length;

			mfid = BitConverter.GetBytes(this.len);
			Buffer.BlockCopy(mfid, 0, mCREATE_BLOCK, drcOffset, mfid.Length);
			drcOffset += mfid.Length;

			Buffer.BlockCopy(this.buff, 0, mCREATE_BLOCK, drcOffset, this.buff.Length);
			return mCREATE_BLOCK;
		}
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct FILE_CREATE_BLOCK
	{
		public short fid;     // objective file ID 
		public short fsize;   // file size
		public create_file_type ftype;	// file type
		public create_file_flag flag; 	// create flag
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xf0)]    //#define MAX_BUFF_SIZE  0xf0      //maximum buffer size 
		public byte[] buff;

		public FILE_CREATE_BLOCK(short mfid, short mfsize, create_file_type mftype, create_file_flag mflag, byte[] buf)
		{
			this.fid = mfid;
			this.flag = mflag;
			this.fsize = mfsize;
			this.ftype = mftype;
			this.buff = buf;
		}
		public byte[] ToByteArray()
		{
			byte[] mCREATE_BLOCK = new byte[Marshal.SizeOf(typeof(FILE_CREATE_BLOCK))];
			byte[] mfid = BitConverter.GetBytes(this.fid);
			int drcOffset = 0;
			Buffer.BlockCopy(mfid, 0, mCREATE_BLOCK, drcOffset, mfid.Length);
			drcOffset += mfid.Length;

			mfid = BitConverter.GetBytes(this.fsize);
			Buffer.BlockCopy(mfid, 0, mCREATE_BLOCK, drcOffset, mfid.Length);
			drcOffset += mfid.Length;

			mCREATE_BLOCK[drcOffset++] = (byte)this.ftype;
			mCREATE_BLOCK[drcOffset++] = (byte)this.flag;
			Buffer.BlockCopy(this.buff, 0, mCREATE_BLOCK, drcOffset, this.buff.Length);
			return mCREATE_BLOCK;
		}
	}

	//    #define IO_PACKAGE_HEADER_SIZE 2 //IO package header length:tag+len

	///* create file type, quoted from ses_v3.h */
	public enum create_file_type : byte
	{
		SES_FILE_TYPE_EXE = 0x00,			/* executable file			*/
		SES_FILE_TYPE_EXE_DATA = 0x01,			/* data file				*/
		SES_FILE_TYPE_RSA_PUB = 0x02,			/* RSA public key file		*/
		SES_FILE_TYPE_RSA_SEC = 0x03,			/* RSA secret key file		*/
	}
	///* create file flag, quoted from ses_v3.h */
	public enum create_file_flag : byte
	{
		CREATE_OPEN_ALWAYS = 0x00,			/* open if file already exist, create a new file if not existing	*/
		CREATE_FILE_NEW = 0x01,			/* create a new file and open it									*/
		CREATE_OPEN_EXISTING = 0x02,			/* open a file exist												*/
	}


	/* IO package */
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct IO_PACKAGE
	{
		public byte tag;
		public byte len;
		//#define MAX_IO_DATA_SIZE	0xf8 // maximum IO data size
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xf8)]
		public byte[] buff;

		public byte[] ToByteArray()
		{
			byte[] mCREATE_BLOCK = new byte[Marshal.SizeOf(typeof(IO_PACKAGE))];
			mCREATE_BLOCK[0] = this.tag;
			mCREATE_BLOCK[1] = this.len;
			Buffer.BlockCopy(buff, 0, mCREATE_BLOCK, 2, buff.Length);
			return mCREATE_BLOCK;
		}
	}
}