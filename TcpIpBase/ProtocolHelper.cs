using System.Collections.Generic;
using System.Text;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;

namespace TcpIpBase {
    public class ProtocolHelper {

        private Encoding protocolEncoding = Encoding.Default;
        public ProtocolHelper()
		{
		}

        public Encoding ProtocolEncoding
        {
            get { return protocolEncoding; }
            set { protocolEncoding = value; }
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
        public virtual Protocol Praser(string protocol)
        {
            return (Protocol)SerializationUnit.Deserialize(typeof(Protocol), protocol);
        }

        public virtual Protocol MakeInfoProtocol(string message)
        {
            return new Protocol(){Parameter = message};
        }

        public virtual byte[] ToBytes(Protocol protocol)
        {
            List<byte> buf = new List<byte>();
            return buf.ToArray();
        }
    }
}
