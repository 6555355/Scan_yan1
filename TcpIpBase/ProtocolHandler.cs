namespace TcpIpBase {
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class ProtocolHandler {
        internal string partialProtocal;	// 保存不完整的协议
        private string pattern;// 正则表达式匹配规则
        public ProtocolHandler(string strPattern)
        {
			partialProtocal = "";
            pattern = strPattern;
            Helper = new ProtocolHelper();
        }

        public ProtocolHelper Helper { get; set; }
        
        public virtual List<Protocol> GetProtocol(string input)
        {
			return GetProtocol(input, null);
		}
		
		// 获得协议
        private List<Protocol> GetProtocol(string input, List<Protocol> outputList)
        {			
			
			if (outputList == null)
                outputList = new List<Protocol>();

			if (String.IsNullOrEmpty(input))
				return outputList;

			if (!String.IsNullOrEmpty(partialProtocal))
				input = partialProtocal + input;

            //string pattern = "(^<\\?xml version=\"1.0\" encoding=\"utf-16\"\\?>\\s*<protocol xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">.*?</protocol>)";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			// 如果有匹配，说明已经找到了，是完整的协议
            if (regex.IsMatch(input))
            {
				// 获取匹配的值
                string match = regex.Match(input).Groups[0].Value;
                outputList.Add(Helper.Praser(match));
				partialProtocal = "";

				// 缩短input的长度
				input = input.Substring(match.Length);

				// 递归调用
				GetProtocol(input, outputList);

			} else {
				// 如果不匹配，说明协议的长度不够，
				// 那么先缓存，然后等待下一次请求
				partialProtocal = input;
			}

			return outputList;
		}		
	}
}
