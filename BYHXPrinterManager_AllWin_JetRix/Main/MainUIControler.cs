using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for MainUIControler.
	/// </summary>
	public class MainUIControler
	{
		public MainUIControler()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}

	public class ProcessMessaging
	{
		static ShareMem Data = new ShareMem();

		/// <summary>
		/// ��ȡ�����ڴ�(MyData�ṹ)
		/// </summary>
		/// <returns></returns>
		public static MyData GetShareMem()
		{
			int MemSize = Marshal.SizeOf(typeof(MyData));
			Data = new ShareMem();
			if (Data.Init("MyData", MemSize) != 0)
			{
				return new MyData(-1);
			}

			byte[] temp = new byte[MemSize];

			try
			{
				Data.Read(ref temp, 0, temp.Length);
				MyData stuc = (MyData)PubFunc.BytesToStruct(temp, typeof(MyData));
				return stuc;
			}
			catch (Exception)
			{
				return new MyData(-1);
			}

		}//end fun

		/// <summary>
		/// ���ù����ڴ�(MyData�ṹ)
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool SetShareMem(MyData data)
		{
			int MemSize = Marshal.SizeOf(typeof(MyData));

			if (Data.Init("MyData", MemSize) != 0)//"MyData"�����ڴ�����,����������Ҳ����
			{
				return false;
			}

			try
			{
				byte[] b = PubFunc.StructToBytes(data);
				Data.Write(b, 0, b.Length);
			}
			catch (Exception)
			{
				return false;
			}			

			return true;
		}//end fun
	}//end class

	public class ShareMem
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr CreateFileMapping(int hFile, IntPtr lpAttributes, uint flProtect, 

			uint dwMaxSizeHi, uint dwMaxSizeLow, string lpName);

		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs

																			  (UnmanagedType.Bool)] bool bInheritHandle, string lpName);

		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint 

			dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		public static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);

		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		public static extern bool CloseHandle(IntPtr handle);

		[DllImport("kernel32", EntryPoint = "GetLastError")]
		public static extern int GetLastError();

		const int ERROR_ALREADY_EXISTS = 183;

		const int FILE_MAP_COPY = 0x0001;
		const int FILE_MAP_WRITE = 0x0002;
		const int FILE_MAP_READ = 0x0004;
		const int FILE_MAP_ALL_ACCESS = 0x0002 | 0x0004;

		const int PAGE_READONLY = 0x02;
		const int PAGE_READWRITE = 0x04;
		const int PAGE_WRITECOPY = 0x08;
		const int PAGE_EXECUTE = 0x10;
		const int PAGE_EXECUTE_READ = 0x20;
		const int PAGE_EXECUTE_READWRITE = 0x40;

		const int SEC_COMMIT = 0x8000000;
		const int SEC_IMAGE = 0x1000000;
		const int SEC_NOCACHE = 0x10000000;
		const int SEC_RESERVE = 0x4000000;

		const int INVALID_HANDLE_VALUE = -1;

		IntPtr m_hSharedMemoryFile = IntPtr.Zero;
		IntPtr m_pwData = IntPtr.Zero;
		bool m_bAlreadyExist = false;
		bool m_bInit = false;
		long m_MemSize = 0;

		public ShareMem()
		{
		}

		~ShareMem()
		{
			Close();
		}

		/// <summary>
		/// ��ʼ�������ڴ�
		/// </summary>
		/// <param name="strName">�����ڴ�����</param>
		/// <param name="lngSize">�����ڴ��С</param>
		/// <returns></returns>
		public int Init(string strName, long lngSize)
		{
			if (lngSize <= 0 || lngSize > 0x00800000) lngSize = 0x00800000;
			m_MemSize = lngSize;
			if (strName.Length > 0)
			{
				//�����ڴ湲����(INVALID_HANDLE_VALUE)
				m_hSharedMemoryFile = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, (uint)

					PAGE_READWRITE, 0, (uint)lngSize, strName);
				if (m_hSharedMemoryFile == IntPtr.Zero)
				{
					m_bAlreadyExist = false;
					m_bInit = false;
					return 2; //����������ʧ��
				}
				else
				{
					if (GetLastError() == ERROR_ALREADY_EXISTS)  //�Ѿ�����
					{
						m_bAlreadyExist = true;
					}
					else                                         //�´���
					{
						m_bAlreadyExist = false;
					}
				}
				//---------------------------------------
				//�����ڴ�ӳ��
				m_pwData = MapViewOfFile(m_hSharedMemoryFile, FILE_MAP_WRITE, 0, 0, (uint)lngSize);
				if (m_pwData == IntPtr.Zero)
				{
					m_bInit = false;
					CloseHandle(m_hSharedMemoryFile);
					return 3; //�����ڴ�ӳ��ʧ��
				}
				else
				{
					m_bInit = true;
					if (m_bAlreadyExist == false)
					{
						//��ʼ��
						MyData data = new MyData(-1);
						byte[] bd = PubFunc.StructToBytes(data);
						Marshal.Copy(m_pwData, bd, 0, bd.Length);
					}
				}
				//----------------------------------------
			}
			else
			{
				return 1; //��������     
			}

			return 0;     //�����ɹ�
		}//end fun

		/// <summary>
		/// �رչ����ڴ�
		/// </summary>
		public void Close()
		{
			if (m_bInit)
			{
				UnmapViewOfFile(m_pwData);
				CloseHandle(m_hSharedMemoryFile);
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		/// <param name="bytData">����</param>
		/// <param name="lngAddr">��ʼ��ַ</param>
		/// <param name="lngSize">����</param>
		/// <returns></returns>
		public int Read(ref byte[] bytData, int lngAddr, int lngSize)
		{
			if (lngAddr + lngSize > m_MemSize) return 2; //����������
			if (m_bInit)
			{
				Marshal.Copy(m_pwData, bytData, lngAddr, lngSize);
			}
			else
			{
				return 1; //�����ڴ�δ��ʼ��
			}
			return 0;     //���ɹ�
		}

		/// <summary>
		/// д����
		/// </summary>
		/// <param name="bytData">����</param>
		/// <param name="lngAddr">��ʼ��ַ</param>
		/// <param name="lngSize">���ݳ���</param>
		/// <returns></returns>
		public int Write(byte[] bytData, int lngAddr, int lngSize)
		{
			if (lngAddr + lngSize > m_MemSize) return 2; //����������
			if (m_bInit)
			{
				Marshal.Copy(bytData, lngAddr, m_pwData, lngSize);
			}
			else
			{
				return 1; //�����ڴ�δ��ʼ��
			}
			return 0;     //д�ɹ�
		}
	}//end class


	//������Ҫ��չ�ýṹ��
	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
	public struct MyData
	{
//		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
//		public char[] c0;
//		public int l0;
//
//		public int i0;
//		public int i1;
//		public int i2;
//		public int i3;
		public int i4;
		public int i5;

		//placeholder���ò������ò���,��C#�涨������,�������������
		public MyData(int placeholder)
		{
//			c0 = new char[1024];
//			l0 = 0;     
//			i0 = 0;
//			i1 = 0;
//			i2 = 0;
//			i3 = 0;
			i4 = 0;
			i5 = 0;
		}//end fun

		/// <summary>
		/// ���̼�ͨ�ţ�����ID,-2��ʾ���н���
		/// </summary>
		public int ProcessID
		{
			get
			{ return i4; }
			set
			{

				i4 = value;
			}
		}

		/// <summary>
		/// ���̼�ͨ��,��Ϣ��
		/// </summary>
		public int InfoCode
		{
			get
			{ return i5; }
			set
			{ i5 = value; }
		}

//		/// <summary>
//		/// ʾ���ַ�������
//		/// </summary>
//		public string Url
//		{
//			get
//			{
//				if (l0 > 0)
//				{ return new string(c0, 0, l0); }
//				else
//				{
//					return "";
//				}
//			}
//			set
//			{
//				if (value != null)
//				{
//					value.CopyTo(0, c0, 0, value.Length);
//					l0 = value.Length;
//				}
//			}
//		}
//
//		/// <summary>
//		/// ʾ�����β���
//		/// </summary>
//		public Rectangle WindowPosition
//		{
//			get
//			{ return new Rectangle(i0, i1, i2, i3); }
//			set
//			{
//				i0 = value.X;
//				i1 = value.Y;
//				i2 = value.Width;
//				i3 = value.Height;
//			}
//		}
	}//end struct


}
