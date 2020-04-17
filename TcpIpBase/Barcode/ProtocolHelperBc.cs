namespace TcpIpBase {
    using System;

    using WAF_OnePass.Domain.Utility;

    public class ProtocolHelperBc {

        //private XmlNode fileNode;
        private string strProtocol;

        public ProtocolHelperBc(string protocol)
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
        public ProtocolBc GetProtocol()
        {
            ProtocolBc protocol = new ProtocolBc();
            foreach (TcpIpCmdEnum cmdEnum in Enum.GetValues(typeof(TcpIpCmdEnum)))
            {
                if(strProtocol.Contains(cmdEnum.ToString()))
                {
                    protocol.Cmd = cmdEnum;
                    break;
                }
            }
            int startindex = strProtocol.IndexOf(protocol.Cmd.ToString()) + protocol.Cmd.ToString().Length;
            protocol.Parameter = strProtocol.Substring(startindex, strProtocol.Length-startindex-1);
            return protocol;
        }
    }
}
