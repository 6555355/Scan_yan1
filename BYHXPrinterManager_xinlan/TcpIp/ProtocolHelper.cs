
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;

namespace BYHXPrinterManager.TcpIp {
    using System;
    using System.Diagnostics;
    using System.Xml;


    public class ProtocolHelper {

        //private XmlNode fileNode;
        private string strProtocol;
		
		public ProtocolHelper(string protocol)
		{
		    strProtocol = protocol;
		}

		// 此时的protocal一定为单条完整protocal
        //private TcpIpCmdEnum GetFileMode()
        //{
        //    string mode = fileNode.Attributes["mode"].Value;
        //    mode = mode.ToLower();
        //    if (mode == "send")
        //        return TcpIpCmdEnum.Send;
        //    else
        //        return TcpIpCmdEnum.Receive;
        //}

		// 获取单条协议包含的信息
        public Protocol GetProtocol()
        {
            return (Protocol)SerializationUnit.Deserialize(typeof(Protocol), strProtocol);
        }
    }
}
