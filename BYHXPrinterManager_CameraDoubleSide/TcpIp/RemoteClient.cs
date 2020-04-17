#define NEWFUNC


using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;

// -----------------------------------------------------------------------
// <copyright file="RemoteClient.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------


namespace BYHXPrinterManager.TcpIp
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
	using System.Globalization;
	using BYHXPrinterManager.JobListView;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
	public class RemoteClient:IDisposable
	{
		//		private TcpClient client;
		private TcpClient[] clients = null;
		private NetworkStream[] streams = null;
		private const int BufferSize = 8192;
		private ArrayList buffers = new ArrayList();
		private TcpClient[] m_SendClients = null;
		private NetworkStream[] m_ClientStreams = null;

		private ProtocolHandler handler;
		private string[] ipAddress;
		private int port;

		private BackgroundWorker worker;

		//public event EventHandler TcpClientClosed;
		//public event CustomCommandEventHandler ReceiveNewCommond;
		private bool isDirty = false;
		//private SystemSetting systemSetting;
		private ToleranceBand tBand = new ToleranceBand();
		private ToleranceBand PreBand = new ToleranceBand();
		private TbandMonitor tbandMonitor = new TbandMonitor();
		private JetStatusEnum curStatus = JetStatusEnum.Unknown;
		//		private System.Windows.Forms.Timer mTcpipRespondTimer; 
		private bool bManualTrigger = false;
		private IPrinterChange m_iPrinterChange;
		private int CurStep = 0;
		private int bandCount = 0;
		private TcpListener listener = null;

		public RemoteClient(string[] connectstring, IPrinterChange iPrinterChange)
		{
			m_iPrinterChange = iPrinterChange;
			this.ipAddress = connectstring;
			this.port = port;

            handler = new ProtocolHandler();
			tbandMonitor.Reset();

			//			mTcpipRespondTimer = new System.Windows.Forms.Timer();
			//			mTcpipRespondTimer.Interval = 60000;// 60秒
			//			mTcpipRespondTimer.Tick +=new EventHandler(mTcpipRespondTimer_Tick);
		}

		public BackgroundWorker Worker
		{
			get
			{
				return this.worker;
			}
			set
			{
				this.worker = value;
			}
		}
		public string[] IpAddress
		{
			get
			{
				return this.ipAddress;
			}
			set
			{
				this.ipAddress = value;
			}
		}

		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				this.port = value;
			}
		}

		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
			set
			{
				this.isDirty = value;
			}
		}

		private void OnTcpClientClosed(EventArgs e)
		{
			this.Run(true);
		}


		private bool bConnected = false;
		public bool Connected
		{
			get{return bConnected;}
			set{bConnected = value;}
		}

		public bool Run(bool bShowMsg)
		{
			try
			{
				//			while (true)
			{
				m_SendClients = new TcpClient[ipAddress.Length];
				m_ClientStreams = new NetworkStream[ipAddress.Length];

				buffers = new ArrayList();
				clients = new TcpClient[ipAddress.Length];
				streams = new NetworkStream[ipAddress.Length];
				for(int i = 0; i< this.ipAddress.Length; i++)
				{
					string[] para = this.ipAddress[i].Split(new char[]{'-'});
					IPAddress ip = IPAddress.Parse(para[0]);
					int sendport = int.Parse(para[1]);
					int recivePort = int.Parse(para[2]);
#if false
					string username = para[3];
					string password = para[4];
					m_SendClients[i] = new  System.Net.Sockets.TcpClient();
					m_SendClients[i].Connect(ip,sendport);
					m_ClientStreams[i] = m_SendClients[i].GetStream();
					// Receive the TcpServer.response.

					// Buffer to store the response bytes.
					Byte[] data = new Byte[64];

					// String to store the response ASCII representation.
					String responseData = String.Empty;
					Int32 bytes;
					do
					{
						// Read the first batch of the TcpServer response bytes.
						bytes = m_ClientStreams[i].Read(data, 0, data.Length);
						responseData = System.Text.Encoding.Default.GetString(data, 0, bytes);
						Console.WriteLine("Received: {0}", responseData); 
					}while(!responseData.EndsWith("User: "));


					//Sleep(200);
					string message = username+Environment.NewLine;
					// Translate the passed message into ASCII and store it as a Byte array.
					Byte[] data1 = System.Text.Encoding.Default.GetBytes(message);         

					m_ClientStreams[i].Write(data1, 0, data1.Length);

					Console.WriteLine("Sent: {0}", message);         

					// Receive the TcpServer.response.

					// Buffer to store the response bytes.
					data = new Byte[64];

					// String to store the response ASCII representation.
					responseData = String.Empty;

					// Read the first batch of the TcpServer response bytes.
					bytes = m_ClientStreams[i].Read(data, 0, data.Length);
					responseData = System.Text.Encoding.Default.GetString(data, 0, bytes);
					Console.WriteLine("Received: {0}", responseData);         
					//Sleep(200);
					message = password + Environment.NewLine;

					// Translate the passed message into ASCII and store it as a Byte array.
					data1 = System.Text.Encoding.Default.GetBytes(message);

					// Send the message to the connected TcpServer. 
					m_ClientStreams[i].Write(data1, 0, data1.Length);

					Console.WriteLine("Sent: {0}", message);         

					// Receive the TcpServer.response.

					// Read the first batch of the TcpServer response bytes.
					bytes = m_ClientStreams[i].Read(data, 0, data.Length);
					responseData = System.Text.Encoding.Default.GetString(data, 0, bytes);
					Console.WriteLine("Received: {0}", responseData);         
					if (responseData.StartsWith("User Logged In"))
					{
						//相机触发端口登陆成功
						worker.ReportProgress(0,string.Format("相机({0}[{1}])触发端口登陆成功",ip,sendport));
					}
					else
					{
						//登陆失败
						worker.ReportProgress(0,string.Format("相机({0}[{1}])触发端口登陆失败，请检查用户名密码端口号等",ip,sendport));
					}
#endif
					// 获取一个连接，同步方法，在此处中断
					TcpClient client = new TcpClient();//  listener.AcceptTcpClient();
					client.Connect(ip, recivePort);
					// 打印连接到的客户端信息
					//                string msg = string.Format("Client Connected！{0} <-- {1}", client.Client.LocalEndPoint, client.Client.RemoteEndPoint);
					//                Debug.WriteLine(msg);
					if(worker != null)
					{
						worker.ReportProgress(-1,ResString.GetResString("Shizixiu_Connected"));
						worker.ReportProgress(0,string.Format("Client({0}[{1}]) Connected",ip, recivePort));
					}
					// 获得流
					NetworkStream streamToClient = client.GetStream();
					buffers.Add(new byte[BufferSize]);
					streams[i] = streamToClient;
					clients[i] = client;
					this.BeginRead(streamToClient, (byte[])buffers[i],i);
					//break;
				}
				this.Connected = true;
			}
			}
			catch(Exception ex)
			{
//				worker.ReportProgress(0,ex.Message);
				if(bShowMsg)
					MessageBox.Show(ex.Message);
				this.Connected = false;
			}
			return this.Connected;
		}

		public bool StartListening(bool bShowMsg)
		{
			try
			{
				m_SendClients = new TcpClient[ipAddress.Length];
				m_ClientStreams = new NetworkStream[ipAddress.Length];

				buffers = new ArrayList();
				clients = new TcpClient[ipAddress.Length];
				streams = new NetworkStream[ipAddress.Length];
				//					for(int i = 0; i< this.ipAddress.Length; i++)
				for(int i = 0; i< 1; i++)
				{
					string[] para = this.ipAddress[i].Split(new char[]{'-'});
					IPAddress ip = IPAddress.Parse(para[0]);
					int sendport = int.Parse(para[1]);
					int recivePort = int.Parse(para[2]);

					listener = new TcpListener(ip, recivePort);
					listener.Start();
					while (true)
					{
						if(Connected)
						{
							Thread.Sleep(50);
						}
						else
						{

							// 中断，等待远程连接
							TcpClient client = listener.AcceptTcpClient();
							Console.WriteLine("Start sending file...");

							if(worker != null)
							{
								worker.ReportProgress(-1,ResString.GetResString("Shizixiu_Connected"));
								worker.ReportProgress(0,string.Format("Client({0}[{1}]) Connected",ip, recivePort));
							}
							// 获得流
							NetworkStream streamToClient = client.GetStream();
							buffers.Add(new byte[BufferSize]);
							streams[i] = streamToClient;
							clients[i] = client;
							this.BeginRead(streamToClient, (byte[])buffers[i],i);
							//break;
							this.Connected = true;
						}
					}
				}
			}
			catch(Exception ex)
			{
				//				worker.ReportProgress(0,ex.Message);
				if(bShowMsg)
					MessageBox.Show(ex.Message);
				this.Connected = false;
			}
			return this.Connected;
		}

		// 开始进行读取
		public void BeginRead(NetworkStream stream,byte[] buffer,int index)
		{
			try
			{
#if true
				AsyncCallback callBack = new AsyncCallback(OnReadComplete);
				stream.BeginRead(buffer, 0, BufferSize, callBack, stream);
#else
				// 从缓存buffer中读入到文件流中
				byte[] fileBuffer = new byte[BufferSize];		// 每次传1KB
				int bytesRead;
				int totalBytes = 0;
				do
				{
					bytesRead = stream.Read(buffer, 0, BufferSize);
					Buffer.BlockCopy(buffer, 0, fileBuffer, totalBytes, bytesRead);
					totalBytes += bytesRead;
					Debug.WriteLine(string.Format("Receiving {0} bytes ...", totalBytes));
				
					if (bytesRead != 0)
					{
						string msg = Encoding.Default.GetString(this.buffer, 0, bytesRead).ToLower();				
						//				string msg1 = Encoding.Default.GetString(this.buffer, 0, totalBytes).ToLower();				
						//				string msg2 = Encoding.ASCII.GetString(this.buffer, 0, totalBytes).ToLower();				
						Array.Clear(this.buffer, 0, this.buffer.Length); // 清空缓存，避免脏读

						// 获取protocol数组
						//ArrayList protocolArray = handler.GetProtocol(msg);
						string [] protocolArray = handler.GetProtocol(msg);
						ToleranceBand tolBand = new ToleranceBand();
						tolBand.LeftTol_X = (int)(Convert.ToSingle((string)protocolArray[2]) /25.4f * 720.0f);

						foreach (string pro in protocolArray)
						{
							// 这里异步调用，不然这里可能会比较耗时
							//						CoreInterface.SetParasFromTcpIp(pro);
							worker.ReportProgress(0,pro);
						}
						CoreInterface.SetToleranceBand(ref tolBand);
						CoreInterface.Printer_PauseOrResume();
					}
					else
					{
						worker.ReportProgress(-1, ResString.GetResString("Shizixiu_UnConnected"));
						return;
						//throw new Exception("收到0字节");
					}
				} while (bytesRead > 0);
#endif
			}
			catch (Exception ex)
			{
				if ((TcpClient)clients[index] != null)
					((TcpClient)clients[index]).Close();
				if (worker != null)
				{
					worker.ReportProgress(0, ex.Message);
				}
				Debug.WriteLine(ex.Message);		// 捕获异常时退出程序
				MessageBox.Show(ex.Message);
				this.Reset();
			}
		}

		// 再读取完成时进行回调
		private void OnReadComplete(IAsyncResult ar)
		{
			int bytesRead = 0;
			try
			{
				NetworkStream stream = (NetworkStream)ar.AsyncState;
				lock (stream)
				{
					bytesRead = stream.EndRead(ar);
					Debug.WriteLine(string.Format("Reading data, {0} bytes ...",  bytesRead));
				}
				if(this.streams == null)                 
				{
					if(worker!=null)
					{
						worker.ReportProgress(-1, ResString.GetResString("Shizixiu_UnConnected"));
						worker.ReportProgress(0,"all UnConneted");
					}
					return;
				}
				int i = 0;				
				for(; i < this.streams.Length; i++)
					if(streams[i] == stream)
						break;
				byte[] buffer = (byte[])buffers[i];
				if (bytesRead != 0)
				{
					string msg = Encoding.Default.GetString(buffer, 0, bytesRead);
					Array.Clear(buffer, 0, buffer.Length);		// 清空缓存，避免脏读
                    if (PubFunc.IsFactoryUser())
                    {
                        LogWriter.WriteLog(new string[] { "OnReadComplete==" + msg }, true);
                    }

					// 获取protocol数组
					ArrayList protocolArray = handler.GetProtocol(msg);
					for  (int j= 0; j< protocolArray.Count;j++)
					{
						// 这里异步调用，不然这里可能会比较耗时
						//						ParameterizedThreadStart start = new ParameterizedThreadStart(this.HandleProtocol);
						//						start.BeginInvoke(pro, null, null);
						this.HandleProtocol((string)protocolArray[j],i);
					}
				}
				else
				{
					if(worker!=null)
					{
						worker.ReportProgress(-1, ResString.GetResString("Shizixiu_UnConnected"));
						worker.ReportProgress(0,string.Format("Client({0}) UnConneted",ipAddress[i]));
					}
					this.Connected = false;
					return;
					//throw new Exception("收到0字节");
				}
				// 再次调用BeginRead()，完成时调用自身，形成无限循环
				lock (stream)
				{
					AsyncCallback callBack = new AsyncCallback(OnReadComplete);
					stream.BeginRead(buffer, 0, BufferSize, callBack, stream);
				}
			}
			catch (Exception ex)
			{
				if (worker != null)
				{
					//                    Protocol status = new Protocol(TcpIpCmdEnum.StatusDirty, 0, ex.Message, CmdReturnValueEnum.Error, 0, 0);
					worker.ReportProgress(0, ex.Message);
					worker.ReportProgress(-1, ResString.GetResString("Shizixiu_UnConnected"));
				}
				Debug.WriteLine(ex.Message);		// 捕获异常时退出程序
				//OnTcpClientClosed(new EventArgs());
				MessageBox.Show(ex.Message);
				this.Reset();
			}
		}


		// 处理protocol
#if false
		private float Y_StepL = 0;
		private float Y_StepR = 0;


        private void HandleProtocol(string protocol,int i)
        {
			try
			{
				lock(this.tbandMonitor)
				{
					if(curStatus != JetStatusEnum.Pause && !bManualTrigger)
						return;
					string[] temp = protocol.Split(new char[]{';'});
					//float X_OFFSET = 0;
					float lXDistance =0,rXDistance = 0,lYDistance =0,rYDistance = 0,lWidth = 0,lHeight = 0;
					float lXCellCount = 0,rXCellCount = 0,lYCellCount = 0,rYCellCount = 0,rWidth = 0,rHeight = 0;
		#region 解析并处理返回的数据
					switch(i)
					{
						case 0:
		#region 左侧相机
							Y_StepL = 0;
							if(!this.tbandMonitor.bLeftUpdated)
							{
								lYDistance = Convert.ToSingle(temp[6]);
								lYCellCount = Convert.ToInt32(temp[5]);
								lXDistance = Convert.ToSingle(temp[8]);
								lXCellCount = Convert.ToInt32(temp[7]);
								lHeight = Convert.ToSingle(temp[9]);
								lWidth = Convert.ToSingle(temp[10]);

								// 如果x方向识别成功
								if(Convert.ToInt32(temp[4])==1)
								{							
									this.tBand.LeftTol_X = this.GetLeftTol_X(Convert.ToSingle(temp[2]));
								}
								else
								{
									tBand.LeftTol_X = PreBand.LeftTol_X;
								}
								// 如果y方向识别成功
								if(Convert.ToInt32(temp[3])==1)
								{							
									Y_StepL =this.GetY_Step(Convert.ToSingle(temp[1]),bandCount);
								}
								else
								{
									tBand.LeftTol_Y = PreBand.LeftTol_Y;
								}							
							}
							else
							{
								worker.ReportProgress(0,"Error:连续两次Left");
							}
							if(bManualTrigger)
							{
								bManualTrigger = false;
//								if(mTcpipRespondTimer.Enabled)
//								{
//									mTcpipRespondTimer.Stop();
//								}
								worker.ReportProgress(1,this.tBand);
							}
							else
							{
								this.tbandMonitor.bLeftUpdated = true;
							}
							Debug.WriteLine(string.Format("bLeftUpdated = {0}: {1};bandCount={2}",tBand.LeftTol_X, protocol,bandCount));
							if (worker != null)
							{
								worker.ReportProgress(0,string.Format("Left:{0};bandCount={1}",protocol,bandCount) );
								worker.ReportProgress(0,string.Format("Left offset point:{0};bandCount={1}",this.tBand.LeftTol_X,bandCount) );								
							}
							else
								MessageBox.Show("worker is null");
		#endregion
							break;
						case 1:
		#region 右侧相机
							Y_StepR = 0;
							rYDistance = Convert.ToSingle(temp[6]);
							rYCellCount = Convert.ToInt32(temp[5]);
							rXDistance = Convert.ToSingle(temp[8]);
							rXCellCount = Convert.ToInt32(temp[7]);
							rHeight = Convert.ToSingle(temp[9]);
							rWidth = Convert.ToSingle(temp[10]);
							if(!this.tbandMonitor.bRightUpdated)
							{
								this.tbandMonitor.bRightUpdated = true;
								// 如果x方向识别成功
								if(Convert.ToInt32(temp[4])==1)
								{							
									this.tBand.RightTol_X = this.GetLeftTol_X(Convert.ToSingle(temp[2]));
								}
								else
								{
									tBand.RightTol_X = PreBand.RightTol_X;
								}
								// 如果y方向识别成功
								if(Convert.ToInt32(temp[3])==1)
								{							
									Y_StepR = this.GetY_Step(Convert.ToSingle(temp[1]),bandCount);
								}									
								else
								{
									tBand.RightTol_Y = PreBand.RightTol_Y;
								}
							}
							else
							{
								worker.ReportProgress(0,"Error:连续两次Right");
							}
							Debug.WriteLine(string.Format("bRightUpdated = {0}: {1}",tbandMonitor.bRightUpdated, protocol));
							if (worker != null)
								worker.ReportProgress(0,string.Format("Right:{0};bandCount={1}",protocol,bandCount) );
							else
								MessageBox.Show("worker is null");
		#endregion
							break;
					}
		#endregion
					if(tbandMonitor.HasReady)
					{
						int pass = 0;
						SPrinterSetting ss= this.m_iPrinterChange.GetAllParam().PrinterSetting;
						SPrinterProperty sp = this.m_iPrinterChange.GetAllParam().PrinterProperty;	
						UIPreference up = this.m_iPrinterChange.GetAllParam().Preference;
						worker.ReportProgress(0,string.Format("CalibrationSetting.nStepPerHead={0}",ss.sCalibrationSetting.nStepPerHead));
						int CellCountInBigCell = 10;

						// 验证数据
						if(lYDistance == 0 || lYCellCount==0 || lHeight == 0)
						{
							lYDistance = lYCellCount= lHeight =0;
						}

						if(rYDistance == 0 || rYCellCount==0 || rHeight == 0)
						{
							rYDistance = rYCellCount= rHeight =0;
						}

						if(lXDistance == 0 || lXCellCount==0 || lWidth == 0)
						{
							lXDistance = lXCellCount= lWidth =0;
						}

						if(rXDistance == 0 || rXCellCount==0 || rWidth == 0)
						{
							rXDistance = rXCellCount= rWidth =0;
						}
		#region 步进修正（方案1：隔一个pass重新修正一次步进；方案2：第一次修正，后面基于原始步进值动态调整）
						worker.ReportProgress(0,string.Format("GetStepRevCrossStitch Y_StepL={0};bandCount={1}",Y_StepL,bandCount));
#if false
					int step = CoreInterface.GetStepRevCrossStitch((Y_StepL+Y_StepR)/2,ref pass);
#else		
						int step = CoreInterface.GetStepRevCrossStitch(Y_StepL,ref pass);
#endif	
						worker.ReportProgress(0,string.Format("GetStepRevCrossStitch return pass={0};step={1};Y_StepL={2};bandCount={2}",pass,step,Y_StepL,bandCount));


						switch(bandCount)	
						{
//							case 0:
							case 1:
							{
								if(bandCount == 0)
								{
									if(ss.nKillBiDirBanding != 0)
										this.CurStep =	ss.sCalibrationSetting.nPassStepArray[pass/2 -1];
									else
										this.CurStep = ss.sCalibrationSetting.nPassStepArray[pass -1];
								}
								if(ss.nKillBiDirBanding != 0)
									ss.sCalibrationSetting.nPassStepArray[pass/2 -1] = step;
								else
									ss.sCalibrationSetting.nPassStepArray[pass -1] = step;
								worker.ReportProgress(0,string.Format("y方向步进 newstep={0};CurStep={1};bandCount={2}",step,CurStep,bandCount));					

								CoreInterface.SetPrinterSetting(ref ss);
								this.m_iPrinterChange.OnPrinterSettingChange(ss);
								break;
							}
//							case 2:
//							{
//								float y = up.YCoefficient;//1.0068027f;	
//								if(ss.nKillBiDirBanding != 0)
//								{
//									ss.sCalibrationSetting.nPassStepArray[pass/2 -1] = CurStep;
//								}
//								else
//								{
//									ss.sCalibrationSetting.nPassStepArray[pass -1] = CurStep;
//								}
//								worker.ReportProgress(0,string.Format("y方向步进 newstep={0};CurStep={1};bandCount={2}",CurStep,CurStep,bandCount));					
//								//								worker.ReportProgress(0,string.Format("y方向步进 缩放比例系数 y={0};delta={1};CurStep={2}",y,step - CurStep,CurStep));					
//
//								CoreInterface.SetPrinterSetting(ref ss);
//								this.m_iPrinterChange.OnPrinterSettingChange(ss);
//								break;
//							}
							default:
							{
								float y = up.YOffset;//1.0068027f;	
								if(ss.nKillBiDirBanding != 0)
								{
									ss.sCalibrationSetting.nPassStepArray[pass/2 -1] = CurStep;
								}
								else
								{
									ss.sCalibrationSetting.nPassStepArray[pass -1] = CurStep;
								}
								worker.ReportProgress(0,string.Format("y方向步进 newstep={0};CurStep={1};bandCount={2}",CurStep,CurStep,bandCount));					
								//								worker.ReportProgress(0,string.Format("y方向步进 缩放比例系数 y={0};delta={1};CurStep={2}",y,step - CurStep,CurStep));					

								CoreInterface.SetPrinterSetting(ref ss);
								worker.ReportProgress(0,string.Format("GetStepRevCrossStitch Y_StepL={0};bandCount={1}",Y_StepL,bandCount));
#if false
					int step = CoreInterface.GetStepRevCrossStitch((Y_StepL+Y_StepR)/2,ref pass);
#else		
								step = CoreInterface.GetStepRevCrossStitch(Y_StepL,ref pass);
#endif	
								worker.ReportProgress(0,string.Format("GetStepRevCrossStitch return pass={0};step={1};Y_StepL={2};bandCount={3}",pass,step,Y_StepL,bandCount));

//								if(bandCount % 2 ==0)
								{
									if(ss.nKillBiDirBanding != 0)
										ss.sCalibrationSetting.nPassStepArray[pass/2 -1] = step;
									else
										ss.sCalibrationSetting.nPassStepArray[pass -1] = step;
									worker.ReportProgress(0,string.Format("y方向步进 newstep={0};CurStep={1};bandCount={2}",step,CurStep,bandCount));					

									CoreInterface.SetPrinterSetting(ref ss);
									this.m_iPrinterChange.OnPrinterSettingChange(ss);
								}
								break;
							}
		#region 注释掉的代码
								//							default:
//							{ 
//								float y = up.YCoefficient;//1.0068027f;	
//								if(ss.nKillBiDirBanding != 0)
//								{
//									ss.sCalibrationSetting.nPassStepArray[pass/2 -1] = CurStep;
//								}
//								else
//								{
//									ss.sCalibrationSetting.nPassStepArray[pass -1] = CurStep;
//								}
//								worker.ReportProgress(0,string.Format("y方向步进 newstep={0};CurStep={1};bandCount={2}",CurStep,CurStep,bandCount));					
//								//								worker.ReportProgress(0,string.Format("y方向步进 缩放比例系数 y={0};delta={1};CurStep={2}",y,step - CurStep,CurStep));					
//
//								CoreInterface.SetPrinterSetting(ref ss);
//								this.m_iPrinterChange.OnPrinterSettingChange(ss);
//								break;
//							}
//							default:
//							{
//																float y = up.YCoefficient;//1.0068027f;	
//								#if false
//														int cellCountY = 0;						
//														if(lYDistance!=0 &&lYCellCount!=0&&lHeight!=0)
//															cellCountY+=CellCountInBigCell;					
//														if(rYDistance!=0 &&rYCellCount!=0&&rHeight!=0)
//															cellCountY+=CellCountInBigCell;
//													
//														if(lYCellCount+rYCellCount!=0&&lHeight+rHeight!=0)
//														{
//															y =(lYDistance+rYDistance)*(lYCellCount+rYCellCount)/(lYCellCount+rYCellCount)/(lHeight+rHeight);								
//														}
//														// 正的缩小，负的放大
//														step = CurStep + (int)(ss.sCalibrationSetting.nStepPerHead * (y-1f));
//								
//														worker.ReportProgress(0,string.Format("y方向缩放比例系数 y={0};delta={1};",y,step - CurStep));
//													
//														worker.ReportProgress(0,string.Format("GetStepRevCrossStitch return pass={0};step={1};",pass,step));
//														worker.ReportProgress(0,string.Format("old step:{0}； newstep={1}；value:{2};basestep:{3}"
//															,CurStep,step,Y_StepL*2.54f/3.61f*ss.sCalibrationSetting.nStepPerHead,ss.sCalibrationSetting.nStepPerHead) );
//								
//														if(Math.Abs(CurStep -step)!=Math.Abs(Y_StepL*2.54/3.61f*ss.sCalibrationSetting.nStepPerHead))
//															worker.ReportProgress(0,string.Format("step error!!!!!!{0}:{1}",Math.Abs(CurStep -step),Math.Abs(Y_StepL*2.54f/3.61f*ss.sCalibrationSetting.nStepPerHead)));
//														if(ss.nKillBiDirBanding != 0)
//															ss.sCalibrationSetting.nPassStepArray[pass/2 -1] = step;
//														else
//															ss.sCalibrationSetting.nPassStepArray[pass -1] = step;
//								
//								#else
//																if(ss.nKillBiDirBanding != 0)
//																{
//								//									step = CurStep + step - ss.sCalibrationSetting.nPassStepArray[pass/2 -1];
//																	ss.sCalibrationSetting.nPassStepArray[pass/2 -1] = step;
//																}
//																else
//																{
//								//									step =  CurStep + step - ss.sCalibrationSetting.nPassStepArray[pass -1];
//																	ss.sCalibrationSetting.nPassStepArray[pass -1] = step;
//																}
//																worker.ReportProgress(0,string.Format("y方向缩放比例系数 y={0};delta={1};CurStep={2}",y,step - CurStep,CurStep));					
//								#endif
//								
//																CoreInterface.SetPrinterSetting(ref ss);
//																this.m_iPrinterChange.OnPrinterSettingChange(ss);
//								break;
//							}
		#endregion
						}
		#endregion

		#region x方向缩放修正（前2pass应用固定修正系数，之后根据采集值动态调整）
						switch(bandCount)
						{
							case 0:
							case 1:
//							{
//								ToleranceBand tBand_cur1 = new ToleranceBand();
//								tBand_cur1.LeftTol_X = this.tBand.LeftTol_X;
//								float x1 = up.XCoefficient;//0.998f;//1.0068027f;
//
//								// 正的缩小，负的放大
//								tBand_cur1.RightTol_X = (int)(this.m_iPrinterChange.GetPrintingJobInfo().sImageInfo.nImageWidth *(1f-x1));
//								worker.ReportProgress(0,string.Format("x方向缩放比例系数 x={0};delta={1};bandCount={2}",x1,tBand_cur1.RightTol_X,bandCount));
//								CoreInterface.SetToleranceBand(ref tBand_cur1);
//								break;
//							}
							default:
							{
								worker.ReportProgress(0,string.Format("ss.sBaseSetting.eRev23={0};bandCount={1}",ss.sBaseSetting.eRev23,bandCount));
					
//								ToleranceBand tBand_cur = new ToleranceBand();
								tBand_curX.LeftTol_X = this.tBand.LeftTol_X;
								float x = up.XOffset;//0.998f;//1.0068027f;
#if false
					int cellCount = 0;
					if(lXDistance!=0 &&lXCellCount!=0&&lWidth!=0)
						cellCount+=CellCountInBigCell;					
					if(rXDistance!=0 &&rXCellCount!=0&&rWidth!=0)
						cellCount+=CellCountInBigCell;
					
					if(lXCellCount+rXCellCount!=0&&lWidth+rWidth!=0)
						x =(lXDistance+rXDistance)*cellCount/(lXCellCount+rXCellCount)/(lWidth+rWidth);
#endif
								// 正的缩小，负的放大
								worker.ReportProgress(0,string.Format("left x={0};right x={1}",tBand.LeftTol_X,tBand.RightTol_X));
								if(bandCount%2==0)
									tBand_curX.RightTol_X = (int)(-tBand.LeftTol_X+tBand.RightTol_X);
								worker.ReportProgress(0,string.Format("x方向缩放比例系数 x={0};delta={1};bandCount={2}",x,tBand_curX.RightTol_X,bandCount));
								CoreInterface.SetToleranceBand(ref tBand_curX);
								break;
							}
						}
		#endregion
						bandCount++;
						PreBand = tBand;
						CoreInterface.Printer_PauseOrResume();
						this.tbandMonitor.Reset();
//						if(mTcpipRespondTimer.Enabled)
//						{
//							mTcpipRespondTimer.Stop();
//						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);		
				if (worker != null)
					worker.ReportProgress(0,ex.Message);
			}
        }
#else
		private int curPassNum = 0;// 当前处理的pass索引号
		private int yOffsetCari = 0;
		private ArrayList passInfoList = new ArrayList();
		private void HandleProtocol(string protocol,int i)
		{
			try
			{
				lock(this.tbandMonitor)
				{
					CLockQueue mLinesQueue = this.m_iPrinterChange.GetLockQueue();
					if(mLinesQueue != null)
					{
						string[] temp = protocol.Split(new char[]{';'});
						DatatypeEnum dataType = (DatatypeEnum)byte.Parse(temp[1]);
						switch(dataType)
						{
							case DatatypeEnum.Print:
							{
								int reversePrint = int.Parse(temp[3]);
								TcpipCmdPara linearray = new TcpipCmdPara();
								linearray.CmdType = (int)dataType;
								linearray.PrtPath = temp[2];
								linearray.ReversePrint = reversePrint == 1;
								mLinesQueue.PutInQueue(linearray);
                                if ((PubFunc.GetUserPermission() == (int)UserPermission.SupperUser))
								{
									LogWriter.WriteLog(new string[]{protocol + "  " + mLinesQueue.GetCount()},true);
								}
								bandCount++;
								Debug.WriteLine(string.Format("line num ={0}",bandCount));
								break;
							}
                            case DatatypeEnum.DoubleSideCari:
						    {
                                if (worker != null)
                                    worker.ReportProgress((int) DatatypeEnum.DoubleSideCari, temp);
                                break;
						    }
                            case DatatypeEnum.PauseCmd:
                            {
                                PrinterOperate po = PrinterOperate.UpdateByPrinterStatus(curStatus);
                                if (po.CanPause)
                                    CoreInterface.Printer_Pause();
                                break;
                            }
                            case DatatypeEnum.ResumeCmd:
                            {
                                PrinterOperate po = PrinterOperate.UpdateByPrinterStatus(curStatus);
                                if (po.CanResume)
                                    CoreInterface.Printer_Resume();
                                break;
                            }
                            case DatatypeEnum.Error:
                            {
                                break;
                            }
							default:
								Debug.Assert(false,string.Format("未知的协议数据类型({0},temp[0])"));
								break;
						}
                        if (PubFunc.IsFactoryUser())
                        {
                            LogWriter.WriteLog(new string[] { protocol + "  " + mLinesQueue.GetCount() }, true);
                        }
					}
					else
					{
                        if ((PubFunc.GetUserPermission() == (int)UserPermission.SupperUser))
							LogWriter.WriteLog(new string[]{protocol + "  " + "mLinesQueue=null"},true);
					}
					this.tbandMonitor.Reset();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);		
				if (worker != null)
					worker.ReportProgress(0,ex.Message);
				MessageBox.Show(ex.Message+ex.StackTrace);
				this.Connected =false;
			}
		}
#endif

		public void TestConnect()
		{
		
		}

		ToleranceBand tBand_curX = new ToleranceBand();//保证相邻奇偶pass用相同缩放系数

		private int GetLeftTol_X(float leftdisX)
		{
			float rightTol_X = leftdisX /25.4f * 720.0f;
			float absvalue = Math.Abs(rightTol_X);
			if(absvalue > 10)
				rightTol_X = rightTol_X/2f;
			else
				rightTol_X = absvalue > 5f ?rightTol_X*5f/absvalue:rightTol_X;
			return Convert.ToInt32(rightTol_X);					
			
		}


		private float GetY_Step(float leftdisY,int bandCount)
		{
			
			if(bandCount > 1 && Math.Abs(leftdisY) > 0.3f)
				leftdisY = leftdisY/Math.Abs(leftdisY)*0.3f;
			
			float yStepR = leftdisY /25.4f;
			//									
			//			float absvalue = Math.Abs(yStepR);
			//			if(absvalue>=1f/11f)
			//			{
			//				yStepR = 0;
			//			}
			//			else
			//			{
			//				if(absvalue > 1f/11f/2f)
			//					yStepR = yStepR/2f;
			//				else
			//					yStepR = absvalue > 1f/11f/4f ?yStepR/absvalue*1f/11f/4f:yStepR;							
			//			}
			Debug.WriteLine(string.Format("GetY_Step input={0}; output={1}",leftdisY,yStepR));
			worker.ReportProgress(0,string.Format("GetY_Step input={0}; output={1}",leftdisY,yStepR));
			return yStepR;
		}

		private void Reset()
		{
			this.clients = null;
			this.buffers = null;
			this.streams = null;
			this.Connected = false;
			curPassNum = 0;
            passInfoList.Clear();
		}

		public void Dispose()
		{
			if(m_ClientStreams != null)
			{
				for(int i = 0; i < m_ClientStreams.Length; i++)
					if(m_ClientStreams[i]!=null)
						m_ClientStreams[i].Close();
			}

			if(m_SendClients != null)
			{
				for(int i = 0; i < m_SendClients.Length; i++)
					if(m_SendClients[i]!=null)
						m_SendClients[i].Close();
			}

			if(clients!= null)
			{
				for(int i = 0; i < this.clients.Length; i++)
					if(clients[i]!=null)
						((TcpClient)clients[i]).Close();
			}
			if (worker != null)
			{
				worker.ReportProgress(-1, ResString.GetResString("Shizixiu_UnConnected"));
				worker.ReportProgress(0,"All UnConneted");
			}
			Reset();
		}

		public void OnPrinterStatusChanged(JetStatusEnum status)
		{
			try
			{
				if(curStatus == JetStatusEnum.Ready)
				{
					bandCount = 0;
				}

				curStatus = status;
				lock(this.tbandMonitor)
				{
					this.tbandMonitor.Reset();
				}
				//				if(status == JetStatusEnum.Pause)
				//				{
				//					if(this.m_ClientStreams != null)
				//					{
				//						byte[] data = System.Text.Encoding.ASCII.GetBytes("se8"+Environment.NewLine);
				//						for(int i =0 ; i < m_ClientStreams.Length; i++)
				//						{
				//							this.m_ClientStreams[i].Write(data,0,data.Length);
				//							worker.ReportProgress(0,"se8");
				//						}
				//						mTcpipRespondTimer.Start();
				//					}
				//				}
				//				else
				//				{
				//					if(mTcpipRespondTimer.Enabled)
				//					{
				//						mTcpipRespondTimer.Stop();
				//					}
				//				}

			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.Message);		
				if (worker != null)
					worker.ReportProgress(0,ex.Message);
			}
		}

		public void ManualTriggerTakePictures()
		{
			try
			{
				if(this.m_ClientStreams != null)
				{
					byte[] data = System.Text.Encoding.ASCII.GetBytes("se8"+Environment.NewLine);
					bManualTrigger = true;
					//					mTcpipRespondTimer.Start();
					this.m_ClientStreams[0].Write(data,0,data.Length);
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.Message);		
				if (worker != null)
					worker.ReportProgress(0,ex.Message);
			}
		}

		private void mTcpipRespondTimer_Tick(object sender, EventArgs e)
		{
			if(this.curStatus == JetStatusEnum.Pause)
			{
				CoreInterface.Printer_Resume();
			}
			if(bManualTrigger)
			{
				bManualTrigger = false;
				MessageBox.Show("time out!");
			}
			//			mTcpipRespondTimer.Stop();
		}

		class TbandMonitor
		{
			public bool bLeftUpdated;
			public bool bRightUpdated;

			public bool HasReady
			{
				get
				{
					return bLeftUpdated && bRightUpdated;
				}
			}

			public void Reset()
			{
				bLeftUpdated = bRightUpdated = false;
			}
		}

		public enum DatatypeEnum:byte
		{
			Print =1,  // 打印prt文件
			PauseCmd = 2,// 此时数据体为空
			ResumeCmd=3, // 此时数据体为空
			Error = 4, // 此时数据体格式为”错误号;错误信息;”
			LineData =5,  // 保留为十字绣兼容使用
			DoubleSideCari =6,  // 双面喷校准参数
		}

		public struct PassInfo
		{
			public double PassNum;
            public double StartIndex;
            public double EndIndex;
		}

    }
}
