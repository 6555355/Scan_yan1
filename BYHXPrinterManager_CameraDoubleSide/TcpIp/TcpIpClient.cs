using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics ;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace BYHXPrinterManager.TcpIp
{
	public delegate void ConnectStatusChangedEventHandler(object sender, ConnectStatusChangedEventArgs eventArgs);
	
	/// <summary>
	/// Summary description for TcpIpClient.
	/// </summary>
	public class TcpIpClient
	{
		private const int BufferSize = 8192;
		private IPAddress conectIpAddress;
		private int conectPort;
		private int listenPort;
		private TcpClient workClient;
		private byte[] buffer;
		private NetworkStream workStream;
		private bool connected = false;
//		private Dictionary<int, Protocol> unProcessedCmd = new Dictionary<int, Protocol>();
		private ArrayList unProcessedCmd = new ArrayList();
		public event ConnectStatusChangedEventHandler ConnectStatusChanged;
		private TcpListener listener = null;
		private TcpClient localClient;
		private NetworkStream receiveStream;

		public TcpIpClient(IPAddress toConectIp,int toConectPort,int tolistenPort)
		{
			//
			// TODO: Add constructor logic here
			//
			this.conectIpAddress = toConectIp;
			this.conectPort = toConectPort;
			this.listenPort = tolistenPort;

			buffer = new byte[BufferSize];

			connected = false;

		}
		public IPAddress ConectedIpAddress
		{
			get
			{
				return this.conectIpAddress;
			}
		}


		public bool Connected
		{
			get
			{
				return this.connected;
			}

			set
			{
				if(this.connected != value)
				{
					bool oldstatus = this.connected;
					this.connected = value;
					OnConnectStatusChanged(new ConnectStatusChangedEventArgs(connected,oldstatus));
				}
			}
		}

		public void Conncect()
		{
			try
			{
				workClient = new TcpClient();
				workClient.Connect(conectIpAddress, conectPort);		// 与服务器连接

				// 打印连接到的服务端信息
				//				Debug.WriteLine(string.Format("Server Connected！{0} --> {1}\n",
				//					workClient.Client.LocalEndPoint, workClient.Client.RemoteEndPoint));

				workStream = workClient.GetStream();

				IPAddress local = TcpipHelper.GetCurrentIpAddress();
				listener = new TcpListener(local, this.listenPort);
				listener.Start();
				// 中断，等待远程连接
				localClient = listener.AcceptTcpClient();
				Console.WriteLine("Start sending file...");
				receiveStream = localClient.GetStream();

				Connected = true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				Connected = false;
				return;
			}

			//connectCheckTimer.Interval = new TimeSpan(0, 0, 0, 5);
			//connectCheckTimer.Tick += new EventHandler(connectCheckTimer_Tick);
			//connectCheckTimer.Start();
		}


		public void Close()
		{
			workClient.Close();
			localClient.Close();
			listener.Stop();
		}

		// 发送测试消息到服务端
		public bool SendTestMessage(ref string debuginfo)
		{
			try
			{
				SendCmd("SE8"+Environment.NewLine);


				// 从缓存buffer中读入到文件流中
				byte[] fileBuffer = new byte[BufferSize];		// 每次传1KB
				int bytesRead;
				int totalBytes = 0;
				do
				{
					bytesRead = receiveStream.Read(buffer, 0, BufferSize);
					Buffer.BlockCopy(buffer, 0, fileBuffer, totalBytes, bytesRead);
					totalBytes += bytesRead;
					Debug.WriteLine(string.Format("Receiving {0} bytes ...", totalBytes));
				} while (bytesRead > 0);


				string msg = Encoding.Default.GetString(this.buffer, 0, totalBytes).ToLower();				
//				string msg1 = Encoding.Default.GetString(this.buffer, 0, totalBytes).ToLower();				
//				string msg2 = Encoding.ASCII.GetString(this.buffer, 0, totalBytes).ToLower();				
				Array.Clear(this.buffer, 0, this.buffer.Length); // 清空缓存，避免脏读
				
				if (msg.StartsWith("sun")&&msg.EndsWith("tony\r\n"))
				{
//					char[] splitor =  new char[]{';'};
//					string[] paras = msg.Split(splitor);	

//					CoreInterface.SetParasFromTcpIp(msg);
					debuginfo = msg;
				}
				else
				{
					throw new Exception("传输异常,数据格式不对.");
				}
				return true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("+++++");
				Debug.WriteLine(ex.Message);
				debuginfo = ex.Message;
				return false;
			}
		}

		private void SendCmd(string msg)
		{
			byte[] temp = Encoding.Unicode.GetBytes(msg);	// 获得缓存
			try
			{
				lock (workStream)
				{
					workStream.Write(temp, 0, temp.Length);	// 发往服务器
				}
				Debug.WriteLine("Sent: {0}", msg);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("+++++");
				Debug.WriteLine(ex.Message);
				MessageBox.Show(ex.Message);
				Connected = false;
				return;
			}
		}

		public void OnConnectStatusChanged(ConnectStatusChangedEventArgs eventargs)
		{
			ConnectStatusChangedEventHandler handler = this.ConnectStatusChanged;
			if (handler != null)
			{
				handler(this, eventargs);
			}
		}
	}
}
