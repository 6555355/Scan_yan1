using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using PrinterStubC.Utility;

namespace BYHXPrinterManager
{
    public enum BYHX_SL_RetValue : byte
    {
        SUCSESS = 0,//成功
        EXPIRED = 1,//过期
        NOFOUNDDOG = 2,//未发现狗
        ILLEGALDOG = 3,//非法狗(其他错误)
        WILLEXPIREDWORNING_100 = 4,//剩余时间不足100小时
    }

	public class BYHXSoftLock
	{
		/* global variables definition */
		private const int IO_PACKAGE_HEADER_SIZE = 2;
		private const int IO_PACKAGE_BUFF_SIZE = 0xf8;
		private const short PASSWORDFID = 0x01;
		private const short FEATUREWORDFID = 0x02;
		public static int FIRST_S4_INDEX = -1;
		public const int JOBHEADERSIZE = 64;
		public const int PASSWORDLEN = 16;
		private const string S4_EXE_FILE_ID = "b001";
		public static string S4_FolderName = "000d";
		private static byte[] UserPin = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38 };
		public static DongleKeyAlarm m_DongleKeyAlarm;

        /// <summary>
        /// 获取加密狗的系统时钟时间
        /// </summary>
        /// <returns></returns>
	    public static DateTime GetDateTime()
	    {
            // 格林尼治时间和北京时间相差8小时
            DateTime retdt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified).AddHours(8);
            try
            {
                IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
                IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
                stDataPkgIn.tag = (byte)SubFunction_ID.GetDogTimer;
                stDataPkgIn.len = IO_PACKAGE_BUFF_SIZE;
                stDataPkgIn.buff = new byte[IO_PACKAGE_BUFF_SIZE];

                stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
                stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

                BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut);
                if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
                {
                    //MessageBox.Show("GetCateInfo failed!");
                    LogWriter.SaveOptionLog("获取加密锁时钟失败!!使用系统时间替代");
                    return DateTime.Now;// 获取失败则按系统时间
                }
                //加密狗返回的数据字节顺序和pc端是反向的,此处倒序处理
                byte[] buf = new byte[stDataPkgOut.len];
                for (int i = 0; i < buf.Length; i++)
                {
                    buf[i] = stDataPkgOut.buff[buf.Length - 1 - i];
                }
                retdt=  retdt.AddSeconds(BitConverter.ToUInt32(buf,0));
                return retdt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LogWriter.SaveOptionLog("获取加密锁时钟失败!!使用系统时间替代:" + ex.Message);
                return DateTime.Now;
            }
	    }

		/// <summary>
		/// 检查是否过期
		/// </summary>
		/// <returns>false=已过期;true=未过期</returns>
		public static BYHX_SL_RetValue CheckValidDate(ref int leftT)
		{
			IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
			IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
			stDataPkgIn.tag = (byte)SubFunction_ID.ValidDateCheck;
			stDataPkgIn.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgIn.buff = new byte[IO_PACKAGE_BUFF_SIZE];

			stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];
			BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut);
			if (ret == BYHX_SL_RetValue.SUCSESS)
			{
				if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
				{
					if(stDataPkgOut.tag == 0x86)//#define ErrorEnum_IllegalPWD 0x86
						return BYHX_SL_RetValue.EXPIRED;
					else
						return BYHX_SL_RetValue.ILLEGALDOG;
				}
				else
				{
					byte[] toint =new byte[ stDataPkgOut.len];
					Buffer.BlockCopy(stDataPkgOut.buff, 0, toint, 0, toint.Length);
					leftT = BitConverter.ToInt32(toint, 0);
				}
				if (leftT <= 0)
				{
					return BYHX_SL_RetValue.EXPIRED;
				}
				else if (leftT < 100 * 60)
				{
					return BYHX_SL_RetValue.WILLEXPIREDWORNING_100;
				}
				else
				{
					return BYHX_SL_RetValue.SUCSESS;
				}
			}
			return ret;
		}
		/// <summary>
		/// 打印时检查是否过期,并处理打印数据
		/// </summary>
		/// <param name="jobheader">加密后的值</param>
		/// <returns>true=调用成功/false=调用失败</returns>
		public static BYHX_SL_RetValue CheckValidDateWithData(byte[] jobheader, ref byte[] retValue)
		{
			byte[] inputbuff = new byte[jobheader.Length];
			Buffer.BlockCopy(jobheader, 0, inputbuff, 0, jobheader.Length);

			IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
			IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
			stDataPkgIn.tag = (byte)SubFunction_ID.ValidDateAndDataCheck;
			stDataPkgIn.len = (byte)inputbuff.Length;
			stDataPkgIn.buff = inputbuff;

			stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

			BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut);
#if DEBUG
			if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
			{
				MessageBox.Show("CheckValidDateWithData Run failed!");
			}
#endif
			Buffer.BlockCopy(stDataPkgOut.buff, 0, retValue, 0, stDataPkgOut.len);
			return ret;
		}

		/// <summary>
		/// 取得当前的加密数
		/// </summary>
		/// <returns>当前的加密数</returns>
		//        public static int GetRandomNum()
		//        {
		//            IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
		//            IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
		//            stDataPkgIn.tag = (byte)SubFunction_ID.GetRandomNum;
		//            stDataPkgIn.len = IO_PACKAGE_BUFF_SIZE;
		//            stDataPkgIn.buff = new byte[IO_PACKAGE_BUFF_SIZE];

		//            stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE; ;
		//            stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

		//            byte[] bb = new byte[Marshal.SizeOf(typeof(Int32))];
		//            FILE_OP_BLOCK fcb = new FILE_OP_BLOCK(EncryptNumFID, 0, (byte)Marshal.SizeOf(typeof(Int32)), bb);

		//            bb = fcb.ToByteArray();
		//            Buffer.BlockCopy(bb, 0, stDataPkgIn.buff, 0, bb.Length);

		//            BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut, false);
		//#if DEBUG
		//            if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
		//            {
		//                MessageBox.Show("read EncryptNum failed!");
		//            }
		//#endif
		//            byte[] toint = DecryptNumFromOutput(stDataPkgOut);
		//            return BitConverter.ToInt32(toint, 0);
		//        }

		/// <summary>
		/// GetDongleInfoNoTiem：DogId 4, vender 2,Reserve 4
		/// </summary>
		/// <returns>DogId 4, vender 2,Reserve 4</returns>
		public static BYHX_SL_RetValue GetDongleInfoNoTiem(ref byte[] infos)
		{
			IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
			IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
			stDataPkgIn.tag = (byte)SubFunction_ID.GetDongleInfoNoTime;
			stDataPkgIn.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgIn.buff = new byte[IO_PACKAGE_BUFF_SIZE]; ;

			stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

			BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut);

			if (ret == BYHX_SL_RetValue.SUCSESS)
			{
				if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
				{
#if DEBUG
					MessageBox.Show("GetDongleInfoNoTiem failed!");
#endif
					if(stDataPkgOut.tag == 0x86)//#define ErrorEnum_IllegalPWD 0x86
						return BYHX_SL_RetValue.EXPIRED;
					else
						return BYHX_SL_RetValue.ILLEGALDOG;
				}

				//byte[] dt = new byte[Marshal.SizeOf(typeof(RTC_TIME_T))];
				//Buffer.BlockCopy(stDataPkgOut.buff, 0, dt, 0, dt.Length);
				//RTC_TIME_T rett = new RTC_TIME_T(dt);
				infos = new byte[stDataPkgOut.len];
				Buffer.BlockCopy(stDataPkgOut.buff, 0, infos, 0, infos.Length);
			}
			return ret;
		}

		/// <summary>
		/// Fun_CMD_GetDongleInfo：DogId 4,  Left time 4 ,Dead time 4,vender 2,lang 1
		/// </summary>
		/// <returns>DogId 4,  Left time 4 ,Dead time 4,vender 2,lang 1</returns>
		public static BYHX_SL_RetValue GetDongleInfo(ref byte[] infos,int index)
		{
			IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
			IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
			stDataPkgIn.tag = (byte)SubFunction_ID.GetDongleInfo;
			stDataPkgIn.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgIn.buff = new byte[IO_PACKAGE_BUFF_SIZE]; ;

			stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

			BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut,index);

			if (ret == BYHX_SL_RetValue.SUCSESS)
			{
				if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
				{
#if DEBUG
					MessageBox.Show("Get ValidDate failed!");
#endif
					if(stDataPkgOut.tag == 0x86)//#define ErrorEnum_IllegalPWD 0x86
						return BYHX_SL_RetValue.EXPIRED;
					else
						return BYHX_SL_RetValue.ILLEGALDOG;
				}

				//byte[] dt = new byte[Marshal.SizeOf(typeof(RTC_TIME_T))];
				//Buffer.BlockCopy(stDataPkgOut.buff, 0, dt, 0, dt.Length);
				//RTC_TIME_T rett = new RTC_TIME_T(dt);
				infos = new byte[stDataPkgOut.len];
				Buffer.BlockCopy(stDataPkgOut.buff, 0, infos, 0, infos.Length);
			}
			return ret;
		}
		public static BYHX_SL_RetValue GetDongleInfo(ref byte[] infos)
		{
			return GetDongleInfo(ref infos,FIRST_S4_INDEX);
		}

		/// <summary>
		/// 取得当前的软件狗密码
		/// </summary>
		/// <returns>当前的软件狗密码</returns>
		public static BYHX_SL_RetValue GetPassWord(ref string strkey)
		{
			IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
			IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
			stDataPkgIn.tag = (byte)SubFunction_ID.GetPassWord;
			stDataPkgIn.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgIn.buff = new byte[IO_PACKAGE_BUFF_SIZE];

			stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

			BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut);
#if DEBUG
			if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
			{
				MessageBox.Show("Get ValidDate failed!");
			}
#endif
			byte[] dt = new byte[PASSWORDLEN];
			Buffer.BlockCopy(stDataPkgOut.buff, 0, dt, 0, dt.Length);
			strkey = Encoding.ASCII.GetString(dt);
			//for (int i = 0; i < dt.Length; i++)
			//{
			//    strkey += dt[i].ToString("X").PadLeft(2, '0');
			//}
			return ret;
		}

		/// <summary>
		/// 设置新的有效期
		/// </summary>
		/// <param name="mValidDate">新的有效期</param>
		/// <returns>true=设置成功;false=设置失败</returns>
		public static BYHX_SL_RetValue SetPassWord(string keyword)
		{
			byte[] keys = Encoding.ASCII.GetBytes(keyword);//DonglekeyWordToBuffer(keyword);

			IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
			IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
			stDataPkgIn.tag = (byte)SubFunction_ID.SetPassWord;
			stDataPkgIn.len = (byte)keys.Length;
			stDataPkgIn.buff = keys;

			stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

			BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut);
#if DEBUG
			if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
			{
				MessageBox.Show("Set ValidDate failed!");
			}
#endif
			return ret;
		}

		public static BYHX_SL_RetValue SetProductInfo(byte[]info, int size)
		{
			IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
			IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
			stDataPkgIn.tag = (byte)SubFunction_ID.SetProductInfo;
			stDataPkgIn.len = (byte)size;
			stDataPkgIn.buff = info;

			stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

			BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut);
#if DEBUG
			if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
			{
				MessageBox.Show("Set ValidDate failed!");
			}
#endif
			return ret;
		}

		/// <summary>
		/// 设置新的加密数
		/// </summary>
		/// <param name="EncryptNum">新的加密数</param>
		/// <returns>true=设置成功;false=设置失败</returns>
		public static BYHX_SL_RetValue GetFeatureWords(ref byte[] fw)
		{
			IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
			IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
			stDataPkgIn.tag = (byte)SubFunction_ID.GetFeatureWords;
			stDataPkgIn.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgIn.buff = new byte[IO_PACKAGE_BUFF_SIZE];

			stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

			BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut);
#if DEBUG
			if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
			{
				MessageBox.Show("write EncryptNum failed!");
			}
#endif
			fw = new byte[16];
			Buffer.BlockCopy(stDataPkgOut.buff, 0, fw, 0, fw.Length);
			return ret;
		}

        /// <summary>
        /// 获取加密狗内的特殊功能权限
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static BYHX_SL_RetValue GetFunctionWords(ref int func)
        {
            //BIT31 = 1  表示支持波形调试
            byte[] FeaByte = new byte[16];
            BYHX_SL_RetValue ret = GetFeatureWords(ref FeaByte);
            {
                func = 0;
                func = (func << 8) + FeaByte[8];
                func = (func << 8) + FeaByte[9];
                func = (func << 8) + FeaByte[10];
                func = (func << 8) + FeaByte[11];
            }
            return ret;
        }

		//        /// <summary>
		//        /// 用TDES算法解密数据
		//        /// </summary>
		//        /// <param name="input">密文</param>
		//        /// <param name="output">明文</param>
		//        /// <returns>成功与否</returns>
		//        public static BYHX_SL_RetValue Decrypt_TDES(byte[] input, ref byte[] output)
		//        {
		//            IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
		//            IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
		//            stDataPkgIn.tag = (byte)SubFunction_ID.Decrypt_TDES;
		//            stDataPkgIn.len = IO_PACKAGE_BUFF_SIZE;
		//            stDataPkgIn.buff = new byte[IO_PACKAGE_BUFF_SIZE];

		//            stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
		//            stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

		//            Buffer.BlockCopy(input, 0, stDataPkgIn.buff, 0, input.Length);
		//            BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut);
		//#if DEBUG
		//            if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
		//            {
		//                MessageBox.Show("CheckValidDateWithData Run failed!");
		//            }
		//#endif
		//            Buffer.BlockCopy(stDataPkgOut.buff, 0, output, 0, stDataPkgOut.len);
		//            return ret;
		//        }
		//        /// <summary>
		//        /// 用TDES算法加密数据
		//        /// </summary>
		//        /// <param name="input">明文</param>
		//        /// <param name="output">密文</param>
		//        /// <returns>成功与否</returns>
		//        public static BYHX_SL_RetValue Encrypt_TDES(byte[] input, ref byte[] output)
		//        {
		//            IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
		//            IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
		//            stDataPkgIn.tag = (byte)SubFunction_ID.Encrypt_TDES;
		//            stDataPkgIn.len = (byte)input.Length;
		//            stDataPkgIn.buff = input;

		//            stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
		//            stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

		//            BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut);
		//#if DEBUG
		//            if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
		//            {
		//                MessageBox.Show("CheckValidDateWithData Run failed!");
		//            }
		//#endif
		//            Buffer.BlockCopy(stDataPkgOut.buff, 0, output, 0, output.Length);
		//            return ret;
		//        }

		/// <summary>
		/// 运行锁内可执行程序
		/// </summary>
		/// <param name="inbuf">输入</param>
		/// <param name="outbuf">输出</param>
		/// <returns>true=调用成功;false=调用失败</returns>
		private static BYHX_SL_RetValue ExecuteExeById(IO_PACKAGE inbuf, ref IO_PACKAGE outbuf,int index)
		{
			SENSE4_CONTEXT stS4Ctx = new SENSE4_CONTEXT();
			uint dwResult = 0;
			uint dwBytesReturned = 250;
			dwResult = (uint)Common.OpenS4ByIndex(index, ref stS4Ctx);
			if (dwResult != (uint)BYHX_SL_RetValue.SUCSESS)
			{
				return (BYHX_SL_RetValue)dwResult;
			}

			dwResult = S4_API.S4ChangeDir(ref stS4Ctx, S4_FolderName);//"\\");
			if (dwResult != S4_API.S4_SUCCESS)
			{
#if DEBUG
				string msg = string.Format("Change directory failed! <error code: {0}>\n", dwResult);
				MessageBox.Show(msg);
#endif
				Common.ResetAndCloseS4(stS4Ctx);
				return BYHX_SL_RetValue.ILLEGALDOG;
			}

			// Call S4VerifyPin(...) to verify User PIN so as to get the privilege to execute the program in EliteIV.
			dwResult = S4_API.S4VerifyPin(ref stS4Ctx, UserPin, (uint)UserPin.Length, S4_API.S4_USER_PIN);
			if (dwResult != S4_API.S4_SUCCESS)
			{
#if DEBUG
				string msg = string.Format("Verify Pin failed! <error code: {0}>\n", dwResult);
				MessageBox.Show(msg);
#endif
				Common.ResetAndCloseS4(stS4Ctx);
				return BYHX_SL_RetValue.ILLEGALDOG;
			}

			byte[] input = inbuf.ToByteArray();
			byte[] output = outbuf.ToByteArray();
			dwBytesReturned = outbuf.len;
			dwResult = S4_API.S4Execute(ref stS4Ctx, S4_EXE_FILE_ID, input,
				IO_PACKAGE_HEADER_SIZE + (uint)inbuf.len,
				output, (uint)outbuf.len, ref dwBytesReturned);
			if (dwResult != S4_API.S4_SUCCESS)
			{
#if DEBUG
				MessageBox.Show("Call S4Execute failed!\n");
#endif
				return BYHX_SL_RetValue.ILLEGALDOG;
			}
			else
			{
				outbuf.tag = output[0];
				outbuf.len = output[1];
				Buffer.BlockCopy(output, IO_PACKAGE_HEADER_SIZE, outbuf.buff, 0, IO_PACKAGE_BUFF_SIZE);
				//MessageBox.Show("Success!\n");
			}
			/* for better security,use the following instead of using S4close() directly */
			Common.ResetAndCloseS4(stS4Ctx);
			return BYHX_SL_RetValue.SUCSESS;
		}

		private static BYHX_SL_RetValue ExecuteExeById(IO_PACKAGE inbuf, ref IO_PACKAGE outbuf)
		{
			return ExecuteExeById(inbuf,ref outbuf,FIRST_S4_INDEX);
		}


		/// <summary>
		/// 检查锁是否合法
		/// </summary>
		/// <param name="inbuf">输入</param>
		/// <param name="outbuf">输出</param>
		/// <returns>true=调用成功;false=调用失败</returns>
		public static void CheckDongle()
		{
			try
			{
				// 枚举插入的所有狗
				bool bfirst = true;
				int index = 0;//-1;
				int leftt = 0;
				SENSE4_CONTEXT[] jobInfoList = Common.EnumAllS4();
				if(jobInfoList != null && jobInfoList.Length > 0)
				{
					// 遍历识别主锁
					for (int i = 0; i < jobInfoList.Length; i++)
					{
						byte ret1 = BYHXSoftLock.RecognizeMasterKey(i, UserPin);
						if (ret1 == 1)
						{
							if(bfirst)
							{
								index = i;
								bfirst = false;
							}
							int lefttM = 0;
							FIRST_S4_INDEX = i;
							if(CheckValidDate(ref lefttM)== BYHX_SL_RetValue.SUCSESS && lefttM> leftt)
							{
								leftt = lefttM;
								index = i;
								//								break;
							}
						}
					}
					FIRST_S4_INDEX = index;
				}
			}
			catch
			{
				return;
			}
		}



		public static void CheckDongle_WrBd()
		{
			try
			{
				// 枚举插入的所有狗
				bool bfirst = true;
				int index =  -1;
				int leftt = 0;
				SENSE4_CONTEXT[] jobInfoList = Common.EnumAllS4();
				if(jobInfoList != null & jobInfoList.Length > 0)
				{
					// 遍历识别主锁
					for (int i = 0; i < jobInfoList.Length; i++)
					{
						byte ret1 = BYHXSoftLock.RecognizeMasterKey(i, UserPin);
						if (ret1 == 1)
						{	
								byte[] infos = new byte[19];
								BYHX_SL_RetValue ret = BYHXSoftLock.GetDongleInfo(ref infos,i);
								if (ret != BYHX_SL_RetValue.SUCSESS)
								{
									continue;
								}
								else
								{
									if (0xff != (infos[16]))
									{
										continue;
									}
								}

								if(bfirst)
								{
									index = i;
									bfirst = false;
								}
								int lefttM = 0;
								FIRST_S4_INDEX = i;
								if(CheckValidDate(ref lefttM)== BYHX_SL_RetValue.SUCSESS && lefttM> leftt)
								{
									leftt = lefttM;
									index =  i;
									//								break;
							}
						}
					}
					FIRST_S4_INDEX = index;
				}
			}
			catch
			{
				return;
			}
		}


        private static byte[] DecryptNumFromOutput(IO_PACKAGE stDataPkgOut)
        {
            byte[] toint = new byte[stDataPkgOut.len];
            Buffer.BlockCopy(stDataPkgOut.buff, 0, toint, 0, toint.Length);
            for (int i = 0; i < toint.Length; i++)
            {
                toint[i] ^= (byte)(DateTime.Now.Year - 1900);
            }
            return toint;
        }

        private static byte[] DecryptLeftTimeFromOutput(IO_PACKAGE stDataPkgOut)
        {
            byte[] toint = new byte[stDataPkgOut.len - 1];
            Buffer.BlockCopy(stDataPkgOut.buff, 1, toint, 0, toint.Length);
            for (int i = 0; i < toint.Length; i++)
            {
                toint[i] ^= (byte)stDataPkgOut.buff[0];
            }
            return toint;
        }

        private static byte[] DonglekeyWordToBuffer(string keyword)
        {
            if (keyword.Length != PASSWORDLEN * 2)
                keyword = keyword.PadLeft(PASSWORDLEN * 2, '0');
            byte[] keys = new byte[PASSWORDLEN];
            byte[] Lastkeys = new byte[PASSWORDLEN];
            SENSE4_CONTEXT stS4Ctx = new SENSE4_CONTEXT();
            uint dwResult = 0;
            uint pdwBytesReturned = 0;
            if (Common.OpenS4ByIndex(FIRST_S4_INDEX, ref stS4Ctx) != BYHX_SL_RetValue.SUCSESS)
                return Lastkeys;

            //S4_GET_SERIAL_NUMBER
            //			byte[] SerNumkey = new byte[8];
            //			dwResult = S4_API.S4Control(ref stS4Ctx,S4_API.S4_GET_SERIAL_NUMBER,null, 0, SerNumkey, (uint)SerNumkey.Length,ref pdwBytesReturned);
            if (dwResult == S4_API.S4_SUCCESS)
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    keys[i] = byte.Parse(keyword.Substring(i * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                }
                //				byte[] ceter = new byte[4];
                //				ceter = BitConverter.GetBytes(BitConverter.ToInt32(keys,0) ^ BitConverter.ToInt32(SerNumkey,0));
                //				Buffer.BlockCopy(ceter, 0, Lastkeys, 0, ceter.Length);
                //				ceter = BitConverter.GetBytes(BitConverter.ToInt32(keys,4) ^ BitConverter.ToInt32(SerNumkey,4));
                //				Buffer.BlockCopy(ceter, 0, Lastkeys, 4, ceter.Length);
            }

            return keys;
        }

		/// <summary>
		/// 设置新的有效期
		/// </summary>
		/// <param name="mValidDate">新的有效期</param>
		/// <returns>true=设置成功;false=设置失败</returns>
		public static BYHX_SL_RetValue SetFeatureWords(string keyword)
		{
			byte[] keys = Encoding.ASCII.GetBytes(keyword);//DonglekeyWordToBuffer(keyword);

			IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
			IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
			stDataPkgIn.tag = (byte)SubFunction_ID.SetFeatureWords;
			stDataPkgIn.len = (byte)keys.Length;
			stDataPkgIn.buff = keys;

			stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
			stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

			BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut);
#if DEBUG
			if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
			{
				MessageBox.Show("Set ValidDate failed!");
			}
#endif
			return ret;
		}


		/// <summary>
		/// 识别母狗 0:母狗 1：子狗 2：非法狗
		/// </summary>
		/// <param name="index"></param>
		/// <param name="UserPin"></param>
		/// <returns>0:母狗 1：子狗 2：非法狗</returns>
		public static byte RecognizeMasterKey(int index,byte[] UserPin)
		{
			try
			{
				IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
				IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
				stDataPkgIn.tag = (byte)SubFunction_ID.IsMasterDog;
				stDataPkgIn.len = IO_PACKAGE_BUFF_SIZE;
				stDataPkgIn.buff = new byte[IO_PACKAGE_BUFF_SIZE];

				stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
				stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

				BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut,index);
				if(ret != BYHX_SL_RetValue.SUCSESS)
					return 2;
				if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
				{
					return 2;
				}
				//byte[] dt = new byte[Marshal.SizeOf(typeof(RTC_TIME_T))];
				//Buffer.BlockCopy(stDataPkgOut.buff, 0, dt, 0, dt.Length);
				//RTC_TIME_T rett = new RTC_TIME_T(dt);
				byte[] infos = new byte[stDataPkgOut.len];
				Buffer.BlockCopy(stDataPkgOut.buff, 0, infos, 0, infos.Length);
				if(infos.Length >0)
				{
					if(infos[0] == 1)
						return 0;
					else
						return 1;
				}
				else
					return 2;
			}
			catch (Exception ex)
			{
#if DEBUG
				MessageBox.Show(ex.Message);
#endif
				return 2;
			}
		}

        public static bool OnDeviceChange(IntPtr nEventType, IntPtr dwData)
        {
            // SenseLock EL USB version GUID
            DEV_BROADCAST_DEVICEINTERFACE pDbt;
            pDbt = (DEV_BROADCAST_DEVICEINTERFACE)Marshal.PtrToStructure(dwData, typeof(DEV_BROADCAST_DEVICEINTERFACE));
            switch ((uint)nEventType)
            {
                case Common.DBT_DEVICEARRIVAL:
                case Common.DBT_DEVICEREMOVECOMPLETE:
                    // Check GUID to see whether it's a SenseLock EL device. Notice:the GUIDs of HID devices are 
                    // of the same,so we have to make further check on PID and VID
                    if (pDbt.dbcc_classguid == Common.GUID_CLASS_ITOKEN2)
                    {
                        //MessageBox.Show("SenseLock EL removed!");
                        // TODO: Add your handler code here
                        if (!m_DongleKeyAlarm.LoadAndCheckDongleKey())
                            Application.Exit();
                    }
                    break;
                default:
                    break;
            }
            return true;
        }

		public static bool OnDeviceChange_WrBd(IntPtr nEventType, IntPtr dwData)
		{
			// SenseLock EL USB version GUID
			DEV_BROADCAST_DEVICEINTERFACE pDbt;
			pDbt = (DEV_BROADCAST_DEVICEINTERFACE)Marshal.PtrToStructure(dwData, typeof(DEV_BROADCAST_DEVICEINTERFACE));
			switch ((uint)nEventType)
			{
				case Common.DBT_DEVICEARRIVAL:
				case Common.DBT_DEVICEREMOVECOMPLETE:
					// Check GUID to see whether it's a SenseLock EL device. Notice:the GUIDs of HID devices are 
					// of the same,so we have to make further check on PID and VID
					if (pDbt.dbcc_classguid == Common.GUID_CLASS_ITOKEN2)
					{
						//MessageBox.Show("SenseLock EL removed!");
						// TODO: Add your handler code here
						if (!m_DongleKeyAlarm.LoadAndCheckDongleKey_WrBd())
							Application.Exit();
					}
					break;
				default:
					break;
			}
			return true;
		}
        /// <summary>
        /// 获取加密狗中的支持的喷头类型
        /// </summary>
        /// <param name="infos"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetCateInfo(out byte[] infos, int index)
        {
            infos = null;
            try
            {
                IO_PACKAGE stDataPkgIn = new IO_PACKAGE();
                IO_PACKAGE stDataPkgOut = new IO_PACKAGE();
                stDataPkgIn.tag = (byte)SubFunction_ID.GetProductCat;
                stDataPkgIn.len = IO_PACKAGE_BUFF_SIZE;
                stDataPkgIn.buff = new byte[IO_PACKAGE_BUFF_SIZE];

                stDataPkgOut.len = IO_PACKAGE_BUFF_SIZE;
                stDataPkgOut.buff = new byte[IO_PACKAGE_BUFF_SIZE];

                BYHX_SL_RetValue ret = ExecuteExeById(stDataPkgIn, ref stDataPkgOut, index);
                if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
                {
                    //MessageBox.Show("GetCateInfo failed!");
                    return false;
                }

                infos = new byte[stDataPkgOut.len];
                Buffer.BlockCopy(stDataPkgOut.buff, 0, infos, 0, infos.Length);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }

	public class DongleKeyAlarm
	{
		public event EventHandler EncryptDogExpired;
		public event EventHandler EncryptDogLast100H;
		public event EventHandler LoadAndCheckDongleKeyFinished;

		private  bool m_bOutdated = false;
		private  System.Windows.Forms.Timer tCheckDog = new System.Windows.Forms.Timer();
		private  System.Windows.Forms.Timer tCloseTimer = new System.Windows.Forms.Timer();
		private  bool bMatchVender = true;
//		private IPrinterChange m_IPrinterChange;
		public DongleKeyAlarm()
		{
//			m_IPrinterChange = ipc;
		}

		public bool IsILLEGALDOG
		{
		    get
		    {
#if ADD_HARDKEY
		        return m_bOutdated || !bMatchVender;
#else
		        return false;
#endif
		    }
		}

		public bool Start(IntPtr handle,bool bWrBd)
		{
			DEV_BROADCAST_DEVICEINTERFACE dbf = new DEV_BROADCAST_DEVICEINTERFACE();
			dbf.dbcc_size = Marshal.SizeOf(dbf);
			dbf.dbcc_devicetype = Common.DBT_DEVTYP_DEVICEINTERFACE;
			dbf.dbcc_reserved = 0;
			IntPtr hDevNotify1 = Common.RegisterDeviceNotification(handle, dbf, Common.DEVICE_NOTIFY_WINDOW_HANDLE | Common.DEVICE_NOTIFY_ALL_INTERFACE_CLASSES);
			int lt = 0;
			if(bWrBd)
				BYHXSoftLock.CheckDongle_WrBd();
			else
				BYHXSoftLock.CheckDongle();
#if ADD_HARDKEY
			BYHX_SL_RetValue ret = BYHXSoftLock.CheckValidDate(ref lt);

			switch (ret)
			{
				case BYHX_SL_RetValue.EXPIRED:
					m_bOutdated = true;
					MessageBox.Show(ResString.GetResString("EncryptDog_Expired"), ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
				case BYHX_SL_RetValue.ILLEGALDOG:
					MessageBox.Show(ResString.GetResString("EncryptDog_Illegal"), ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				case BYHX_SL_RetValue.NOFOUNDDOG:
					MessageBox.Show(ResString.GetResString("EncryptDog_NoFound"), ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				case BYHX_SL_RetValue.WILLEXPIREDWORNING_100:
					MessageBox.Show(ResString.GetResString("EncryptDog_Warning"), ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					tCheckDog.Interval = (lt - 1) * 60 * 1000 + 1;
					tCheckDog.Tick += new EventHandler(tCheckDog_Tick);
					tCheckDog.Start();
					break;
				default:
					break;
			}
			if (lt > 60 * 100 && lt - 100 * 60 <= int.MaxValue / 1000 / 60)
			{
				tCloseTimer.Interval = (lt - 100 * 60) * 60 * 1000;
				tCloseTimer.Tick += new EventHandler(tCloseTimer_Tick);
				tCloseTimer.Start();
			}
#endif
            return true;
		}
		public void ConvertAToB(byte[] infos)
		{
#if LIYUUSB
            if (infos[16] == 7)
                infos[16] = (byte)VenderID.LOTUS;
#endif
		}
		public void FirstReadyShakeHand()
        {
#if ADD_HARDKEY
            ushort Vid, Pid;
			Vid = Pid = 0;

			bool result = true;
			byte[] infos = new byte[19];
			if (CoreInterface.GetProductID(ref Vid, ref  Pid) == 0)
			{
				result = false;
			}
			else
			{
				BYHX_SL_RetValue ret = BYHXSoftLock.GetDongleInfo(ref infos,BYHXSoftLock.FIRST_S4_INDEX);
				if (ret != BYHX_SL_RetValue.SUCSESS)
				{
					result = false;
				}
				else
				{
					ConvertAToB(infos);
				    ushort dogVid = BitConverter.ToUInt16(infos, 16);
					//if (（Vid&0x7F） != (infos[16] & 0x7f))//模糊检查
					//if (Vid != (infos[16] & 0x7f)) //BUG
                    if (Vid != dogVid) //严格检查
					{
						result = false;
#if LIYUUSB
                        if (infos[16] == 7 && Vid == (ushort)VenderID.LOTUS)
                        {
                            result = true;
                        }
#endif
                        if ((Vid & 0x807F) == (dogVid & 0x807f))
						{
                            //if ((Vid & 0x7F) == (ushort)VenderID.GONGZENG)
                                result = true;
						}
					}
				}
			}
			if (result != bMatchVender)
			{
				bMatchVender = result;
//				m_IPrinterChange.OnPrinterStatusChanged(JetStatusEnum.PowerOff);
			}
			if (result == false)
			{
				MessageBox.Show(ResString.GetResString("EncryptDog_VidNotMatch"), ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

#if false
            //This is for Dog Check Board
            byte[] info = new byte[64];
            info[0] = (byte)(Vid & 0xff);
            info[1] = (byte)((Vid >> 8) & 0xff);
            info[2] = (byte)(Pid & 0xff);
            info[3] = (byte)((Pid >> 8) & 0xff);
            ret = BYHXSoftLock.SetProductInfo(info,32);
            if (ret != BYHX_SL_RetValue.SUCSESS)
            {
#if DEBUG
                //if (stDataPkgOut.tag != S4_API.S4_SUCCESS)
                {
                    MessageBox.Show("SetProductInfo Run failed!");
                }
#endif
            }
#endif
			//infos[16] = (byte)(Vid & 0xff);
			CoreInterface.SetDspPwmInfo(infos, infos.Length);
#endif
        }

		private void tCheckDog_Tick(object sender, EventArgs e)
		{
			m_bOutdated = true;
			if(this.EncryptDogExpired!=null)
				this.EncryptDogExpired(sender,e);
		}
		private void tCloseTimer_Tick(object sender, EventArgs e)
		{
			tCheckDog.Interval = 3600 * 100 * 1000;//100 hour
			tCheckDog.Tick += new EventHandler(tCheckDog_Tick);
			tCheckDog.Start();
			if(this.EncryptDogLast100H!=null)
				this.EncryptDogLast100H(sender,e);
		}

		public bool LoadAndCheckDongleKey()
		{
			BYHXSoftLock.CheckDongle();
#if ADD_HARDKEY
			int lt = 0;
			BYHX_SL_RetValue ret = BYHXSoftLock.CheckValidDate(ref lt);
			m_bOutdated = false;
			switch (ret)
			{
				case BYHX_SL_RetValue.EXPIRED:
					m_bOutdated = true;
					break;
				case BYHX_SL_RetValue.ILLEGALDOG:
				case BYHX_SL_RetValue.NOFOUNDDOG:
					return false;
				case BYHX_SL_RetValue.WILLEXPIREDWORNING_100:
					tCheckDog.Interval = (lt - 1) * 60 * 1000 + 1;
					tCheckDog.Tick += new EventHandler(tCheckDog_Tick);
					tCheckDog.Start();
					break;
				default:
					break;
			}

			if (lt > 100 * 60 && lt - 100 * 60 <= int.MaxValue / 1000 / 60)
			{
				tCloseTimer.Interval = (lt - 100 * 60) * 60 * 1000;
				tCloseTimer.Tick += new EventHandler(tCloseTimer_Tick);
				tCloseTimer.Start();
			}
			if(this.LoadAndCheckDongleKeyFinished!=null)
				this.LoadAndCheckDongleKeyFinished(null,new EventArgs());
#endif
			return true;
		}
		public bool LoadAndCheckDongleKey_WrBd()
		{
			BYHXSoftLock.CheckDongle_WrBd();
			int lt = 0;
			BYHX_SL_RetValue ret = BYHXSoftLock.CheckValidDate(ref lt);
			m_bOutdated = false;
			switch (ret)
			{
				case BYHX_SL_RetValue.EXPIRED:
					m_bOutdated = true;
					break;
				case BYHX_SL_RetValue.ILLEGALDOG:
				case BYHX_SL_RetValue.NOFOUNDDOG:
					return false;
				case BYHX_SL_RetValue.WILLEXPIREDWORNING_100:
					tCheckDog.Interval = (lt - 1) * 60 * 1000 + 1;
					tCheckDog.Tick += new EventHandler(tCheckDog_Tick);
					tCheckDog.Start();
					break;
				default:
					break;
			}

			if (lt > 100 * 60 && lt - 100 * 60 <= int.MaxValue / 1000 / 60)
			{
				tCloseTimer.Interval = (lt - 100 * 60) * 60 * 1000;
				tCloseTimer.Tick += new EventHandler(tCloseTimer_Tick);
				tCloseTimer.Start();
			}
			if(this.LoadAndCheckDongleKeyFinished!=null)
				this.LoadAndCheckDongleKeyFinished(null,new EventArgs());
			return true;
		}
	}

}