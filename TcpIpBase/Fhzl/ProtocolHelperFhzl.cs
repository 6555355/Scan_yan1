using System.Collections.Generic;
using System.Text;

namespace TcpIpBase {
    using System;

    using WAF_OnePass.Domain.Utility;

    public class ProtocolHelperFhzl:ProtocolHelper
    {

        private byte[] header;// = protocolEncoding.GetBytes("FHZL01");
        private byte[] ending;// = protocolEncoding.GetBytes("BYHX");
        public ProtocolHelperFhzl()
        {
            ProtocolEncoding = Encoding.Unicode;
            header = ProtocolEncoding.GetBytes("FHZL01");
            ending = ProtocolEncoding.GetBytes("BYHX");
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
        public override Protocol Praser(string strProtocol)
        {
            ProtocolFhzl protocol = new ProtocolFhzl();
            byte[] buf = ProtocolEncoding.GetBytes(strProtocol);
            byte len = buf[header.Length];
            protocol.Cmd = (TcpIpCmdFhzl)(buf[header.Length+1]);
            protocol.SubCmd = buf[header.Length+2];
            int startindex = header.Length+3;
            protocol.Parameter = ProtocolEncoding.GetString(buf, startindex, len - 3);
            return protocol;
        }

        public override Protocol MakeInfoProtocol(string message)
        {
            return new ProtocolFhzl() { Parameter = message };
        }

        public override byte[] ToBytes(Protocol protocol)
        {
            ProtocolFhzl protocolFhzl = (ProtocolFhzl) protocol;
            byte[] bufParas = ProtocolEncoding.GetBytes(protocolFhzl.Parameter);
            byte len = (byte)(3 + bufParas.Length);//
            List<byte> buf = new List<byte>();
            buf.AddRange(header);//协议起始字节	字符串( “FHZL01” )
            buf.Add(len);//数据长度	1+1+1+N	序号2~5内容的长度
            buf.Add((byte)protocolFhzl.Cmd);//主命令部分
            buf.Add(protocolFhzl.SubCmd);//副命令部分
            buf.AddRange(bufParas); 
            byte sum = 0;
            for (int i = header.Length; i < buf.Count; i++)
            {
                sum += buf[i];
            }
            buf.Add(sum);
            buf.AddRange(ending);//协议起始字节	字符串( “BYHX” )
            return buf.ToArray();
        }

    }
}
