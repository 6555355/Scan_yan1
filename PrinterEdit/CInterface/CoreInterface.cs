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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

using System.Reflection;
using System.Drawing.Imaging;
using System.Data;
using System.Windows.Forms;


namespace BYHXPrinterManager
{
	/// <summary>
	/// Summary description for CoreInterface.
	/// </summary>
	/// 
	public struct CoreConst
	{
		public const string c_KernelDllName	= "JobPrint.dll";
		public const int MAX_PATH = 260;
		public const int MAX_ELECTRIC_NUM = 32; 

		public const int MAX_NAME				= 32;
		public const int MAX_NOZZLE_NUM			= 512;
		public const int MAX_HEAD_NUM			= 24;
		public const int MAX_GROUPY_NUM			= 4;
		public const int MAX_GROUPX_NUM			= 2;
		public const int MAX_COLOR_NUM			= 8;
#if LIYUUSB
		public const int MAX_X_PASS_NUM			= 4;
		public const int MAX_Y_PASS_NUM         = 8;
#else
		public const int MAX_X_PASS_NUM			= 12;
		public const int MAX_Y_PASS_NUM         = 9;
#endif
		public const int MAX_PASS_NUM           = MAX_X_PASS_NUM*MAX_Y_PASS_NUM;

		public const int MAX_RESLIST_NUM		= 4;
		public const int MAX_SPEED_NUM			= 3;
		public const int MAX_BIDIRECTION_NUM	= 6;
		public const int MAX_PASSWORD_LEN       = 16;

		public const int MAX_USBINFO_STRINGLEN        = 256;
		public const int  MAX_PREVIEW_WIDTH			=256;		
		public const int MAX_PREVIEW_BUFFER_SIZE		=MAX_PREVIEW_WIDTH*MAX_PREVIEW_WIDTH*MAX_COLOR_NUM;
		public const int MAX_BOARD_NUM				= 32;

		public const int SIZEOF_CalibrationHorizonSetting = 49;//256;
		
		///// <summary> will delete laterly
		public const int BOARD_DATE_LEN       = 12;
	}
	public class CoreInterface
	{
		//Sysem 
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int SystemInit();
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int SystemClose();
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int SetMessageWindow(IntPtr hWnd, uint nMsg);//????

		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int SendJetCommand(int nCmd, int nValue);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int MoveCmd(int nCmd, int nValue);


		//2Updater 
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern  int BeginMilling(string sFilename);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern  int BeginUpdating(byte[] sBuffer, int nBufferSize);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern  int AbortUpdating();
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern  int SetPipeCmdPackage(byte []info, int infosize,int port);

		
		//3Print 
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int Printer_Abort();
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int Printer_Pause();
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int Printer_Resume();
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int Printer_PauseOrResume();
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int Printer_IsOpen();
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int Printer_Open();
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern void Printer_Close(int hHandle);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int Printer_Send(int hHandle, byte[] sBuffer, int nBufferSize);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int Printer_GetFileInfo(string sFilename, ref SPrtFileInfo sInfo,int bGenPrev);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int Printer_GetJobInfo(ref SPrtFileInfo sInfo);
		[DllImport(CoreConst.c_KernelDllName,CharSet=CharSet.Auto)]
		public static extern int Printer_GetFilePreview(string sFilename,IntPtr pPreviewData, ref SPrtFileInfo sInfo);


		//Get Parameter
		//PrintCommand
		//1 Calibration
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetStepReviseValue(float fRevise, int Pass , ref SCalibrationSetting sSetting,int bOnePass);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern  int SendCalibrateCmd(CalibrationCmdEnum cmd, int nValue, ref SPrinterSetting sSetting);


		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetSPrinterProperty(ref SPrinterProperty sProperty);


		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetPrinterSetting(ref SPrinterSetting sSetting);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int SetPrinterSetting(ref SPrinterSetting sSetting);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int SavePrinterSetting();
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int SetPrinterProperty(ref SPrinterProperty sProperty);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern  int GetSeviceSetting(ref SSeviceSetting sSetting);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern  int SetSeviceSetting(ref SSeviceSetting sSetting);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern  int GetBoardEnum(ref int nBoardNum, ushort []nBoardIdArray);
		[DllImport(CoreConst.c_KernelDllName,CharSet=CharSet.Auto)]
		public static extern int GetBoardInfo(int BoardId, ref SBoardInfo sBoardInfo);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern  int SetPassword(string sPwd, int nPwdLen, ushort PortId);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern  int GetPassword(byte[] sPwd, ref int nPwdLen, ushort PortId);


		//RealTime Info
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern JetStatusEnum GetBoardStatus();
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetBoardError();
		//Version String(最好有信息）		
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetBoardVersion(ref int nVersion,ref int nMin);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetRealTimeInfo(ref  SRealTimeCurrentInfo info);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int SetRealTimeInfo(ref  SRealTimeCurrentInfo info);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetEncoderInfo(ref int  encoder);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetMotionPos(ref int  Pos);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int MoveZ(int type,float fZSpace,float fPaperThick);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetFWFactoryData(ref  SFWFactoryData info);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int SetFWFactoryData(ref  SFWFactoryData info);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetSupportList(ref  SSupportList info);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetHeadMap(byte[]pElectricMap,int length);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetHWHeadBoardInfo(ref SWriteHeadBoardInfo info);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetPasswdInfo(ref int nLimitTime, ref int nDuration);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetDebugInfo(byte[]pElectricMap,int length);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int GetUVStatus(ref int status);
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern int SetUVStatus(int status);

	}
	public class SerialFunction
	{
		unsafe public static string CovertAnsiToString(byte [] array)
		{
			string info = "";
			fixed (byte * bytePtr = array)
			{
				IntPtr sPtr = new IntPtr(bytePtr);
				info = Marshal.PtrToStringAnsi(sPtr);
			}
			return info;

		}
		public static string VersionToString(int Version)
		{
			int  ProductMajorPart = (Version&0xff00)>>8;
			int  ProductMinorPart = (Version&0xff);

			return ProductMajorPart.ToString("X")
				+"."
				+ ProductMinorPart.ToString("X");
		}
		public static Bitmap CreateImageWithImageInfo(SPrtImageInfo imageInfo)
		{
			if(imageInfo.nImageData != 0)
			{
				SPrtImagePreview	previewData	= (SPrtImagePreview)Marshal.PtrToStructure((IntPtr)imageInfo.nImageData,typeof(SPrtImagePreview));

				return CreateImageWithPreview(previewData);
			}
			else
			{
				return null;
			}
		}

		public static Bitmap CreateImageWithPreview(SPrtImagePreview previewData)
		{
			SPrinterProperty sProperty = new SPrinterProperty();
			CoreInterface.GetSPrinterProperty(ref sProperty); 
			byte[]	defaultChannelOrder	= sProperty.eColorOrder;
			
			if(previewData.nImageType != 0)
			{
				
				Bitmap	image	= null;
				try
				{
					MemoryStream mem = new MemoryStream(previewData.nImageData,0,previewData.nImageDataSize);

					image = new Bitmap(mem);
					
					mem = null;
				}
				catch
				{
					if(image!=null)
					{
						image.Dispose();
						image = null;
					}
					return image;
				}

				return image;
			}
			else if(defaultChannelOrder.Length >= previewData.nImageColorNum && previewData.nImageColorNum > 0)
			{
				byte[]	channels	= new byte[previewData.nImageColorNum];

				for(int i = 0; i < previewData.nImageColorNum; i ++)
				{
					channels[i]	= defaultChannelOrder[i];
				}

				Bitmap	image	= null;

				CMYKToImage(previewData.nImageData,previewData.nImageWidth,previewData.nImageHeight, channels,out image);

				return image;
			}
			else
			{
				//Debug.Assert(false,"The color channel is out of range!");

				return null;
			}
		}
		private unsafe static bool CMYKToImage(byte[] inputData,int w,int h,byte[] colorMode,out Bitmap image)
		{		
			image = null;				
			try
			{
				image = new Bitmap(w,h,PixelFormat.Format24bppRgb);
				BitmapData data = image.LockBits(new Rectangle(0,0,w,h),ImageLockMode.ReadWrite,PixelFormat.Format24bppRgb);
				byte * buf = (byte*)data.Scan0;
			
				byte c,m,y,k,lc,lm,r,g,b;
				c=m=y=k=0;
				int offsetC,offsetM,offsetY,offsetK,offsetLc,offsetLm;
				offsetC=offsetM=offsetY=offsetK=offsetLc=offsetLm=0;
				int colorNumber = colorMode.Length;
				for(int i=0;i<colorNumber;i++)
				{
					switch( (ColorEnum)colorMode[i] )
					{
						case ColorEnum.Cyan:
							offsetC = i;
							break;
						case ColorEnum.Magenta:
							offsetM = i;
							break;
						case ColorEnum.Yellow:
							offsetY = i;
							break;
						case ColorEnum.Black:
							offsetK = i;
							break;
						case ColorEnum.LightCyan:
							offsetLc = i;
							break;
						case ColorEnum.LightMagenta:
							offsetLm = i;
							break;
						default:
							System.Diagnostics.Debug.Assert(true,"Unkown color channel!!!");
							break;							
					}
				}

				for(int j=0;j<h;j++)
				{
					for(int i=0;i<w;i++)
					{
						r=g=b=0;
						switch(colorNumber)
						{
							case 3:
								c  = inputData[ 3*(i+j*w) + offsetC ];
								m  = inputData[ 3*(i+j*w) + offsetM ];
								y  = inputData[ 3*(i+j*w) + offsetY ];
								k  = 0;
								break;
							case 6:
								c  = inputData[ 6*(i+j*w) + offsetC  ];
								m  = inputData[ 6*(i+j*w) + offsetM  ];
								y  = inputData[ 6*(i+j*w) + offsetY  ];
								k  = inputData[ 6*(i+j*w) + offsetK  ];
								lc = inputData[ 6*(i+j*w) + offsetLc ];
								lm = inputData[ 6*(i+j*w) + offsetLm ];
								
								c = (byte)Math.Min(byte.MaxValue, c+lc*0.3);
								m = (byte)Math.Min(byte.MaxValue, m+lm*0.3);
								
								break;
							case 4:
								c = inputData[ 4*(i+j*w) + offsetC ];
								m = inputData[ 4*(i+j*w) + offsetM ];
								y = inputData[ 4*(i+j*w) + offsetY ];
								k = inputData[ 4*(i+j*w) + offsetK ];
								break;
							default:
								System.Diagnostics.Debug.Assert(true,"Unkown color mode!!!");
								break;
						}
						r = (byte)(byte.MaxValue - Math.Min( c + k, byte.MaxValue ));
						g = (byte)(byte.MaxValue - Math.Min( m + k, byte.MaxValue ));
						b = (byte)(byte.MaxValue - Math.Min( y + k, byte.MaxValue ));
						buf[3*i     + data.Stride*j] = b;
						buf[3*i + 1 + data.Stride*j] = g;
						buf[3*i + 2 + data.Stride*j] = r;												

					}
				}				
				image.UnlockBits(data);
				return true;
			}
			catch
			{
				if( image != null)
				{
					image.Dispose();
					image = null;
				}
				return false;
			}
		}


	}

	/// <summary>
	/// //////////////////////////////////////////////////////////////////////////////////////////
	/// 
	/// </summary>
	//?????????????????????????????????????????????????????????????????????????????????????????????
	//Below class need discuss Whether support One Config File Format 
	public class ObjectContainer
	{
		public object Object;
		public Array ParentArray;
		public FieldInfo Info;
		public int Index;
		public Type ObjType;
	}
	public class PubFunc
	{
		public static string SystemConvertToXml(object o,Type type)
		{
			XmlSerializer serializer = new XmlSerializer(type);
			StringWriter writer = new StringWriter();
			serializer.Serialize(writer,o);
			string str = writer.ToString();
			writer.Close();
			//return str;
			XmlDocument tmpXmlDoc = new XmlDocument();
			tmpXmlDoc.LoadXml(str);
			XmlElement NewNode = (XmlElement)tmpXmlDoc.LastChild;
			NewNode.RemoveAllAttributes();
			
			string inner = NewNode.OuterXml;
			//tmpXmlDoc.Save("c:\\1.xml");
		
			return inner;
		}
		public static object SystemConvertFromXml(string xmlString,Type type)
		{	
			XmlSerializer serializer = new XmlSerializer(type);
			StringReader reader = new StringReader(xmlString);
			object o = serializer.Deserialize(reader);
			reader.Close();	
			return o;
		}
		public static bool IsFactoryUser()
		{
			string curFile = Application.StartupPath + Path.DirectorySeparatorChar + "Factory.usr";
			if(!File.Exists(curFile))
				return false; 
			return true;
		}
		public static XmlElement GetFirstChildByName( XmlElement root,string name)
		{
			XmlNode currNode = root.FirstChild;
			while(currNode != null)
			{
				if(currNode.Name == name)
					return (XmlElement)currNode;
				currNode = currNode.NextSibling;
			}
			return null;
		}
		static public void AddNode(string name, object value, TreeNodeCollection nodes, FieldInfo info, Array parentArray, int index)
		{
			TreeNode node = null;
			if (value == null || nodes == null)
			{
				node = new TreeNode(name + ": Null");
				
				if (info != null)
				{
					ObjectContainer contain = new ObjectContainer();
					contain.ParentArray = parentArray;
					contain.Info = info;
					contain.Index = index;
					contain.ObjType = info.FieldType;
					node.Tag = contain;
				}
				
				nodes.Add(node);
				return;
			}
			Type type = value.GetType();

			//node = new TreeNode(name + "(" + type.Name + ")");
			node = new TreeNode(name);

			if (type.IsPrimitive || type == System.String.Empty.GetType() || type.IsEnum)
			{
				node.Text += ": " + value.ToString();
			}
			else if(type.IsArray)
			{
				Array array = value as Array;
				node.Text += "[" + array.Length +  "]";

				int i = 0;
				foreach (object subobj in array)
				{
					AddNode(name + "[" + i.ToString() + "]", subobj, node.Nodes, null, array, i);
					i++;
				}
			}
			else// if (type.IsClass)
			{
				FieldInfo[] infos = type.GetFields();

				foreach (FieldInfo subinfo in infos)
				{
					object sub = type.InvokeMember(subinfo.Name,
						BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
						BindingFlags.GetField | BindingFlags.GetProperty,
						null, value, new object [] {});

					if (sub != null)
						AddNode(subinfo.Name, sub, node.Nodes, subinfo, null, -1);
					else
						AddNode(subinfo.Name , sub, node.Nodes, subinfo, null, -1);
					//AddNode(subinfo.Name + "(" + subinfo.FieldType.Name + ")", sub, node.Nodes, subinfo, null, -1);
				}
			}

			if (node != null)
			{
				ObjectContainer contain = new ObjectContainer();
				contain.ParentArray = parentArray;
				contain.Info = info;
				contain.Object = value;
				contain.Index = index;
				contain.ObjType = type;

				node.Tag = contain;
				nodes.Add(node);
			}
		}

	}

}
