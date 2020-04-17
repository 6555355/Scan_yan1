using System.Web.Hosting;

namespace BYHXPrinterManager.TcpIp {
    using System;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
	using System.Collections;

    public class ProtocolHandler {

		private string partialProtocal;	// 保存不完整的协议
		
		public ProtocolHandler() {
			partialProtocal = "";		
		}

        public ArrayList GetProtocol(string input)
        {
			
//			if (input.StartsWith("sun")&&input.EndsWith("tony\r\n"))
//			{
//				char[] splitor =  new char[]{';'};
//				string[] paras = input.Split(splitor);	
//				return paras; 
//			}
//			else
//			{
//				throw new Exception("传输异常,数据格式不对.");
//			}

			return GetProtocol(input, null);
		}
		
		// 获得协议
        private ArrayList GetProtocol(string input, ArrayList outputList)
        {			
			
			if (outputList == null)
                outputList = new ArrayList();

			if (input ==null && input == string.Empty)
				return outputList;

			if (partialProtocal!=null && partialProtocal != string.Empty)
				input = partialProtocal + input;

            string pattern = "(sun;.*?tony)";//"(^start;.*?end)";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			// 如果有匹配，说明已经找到了，是完整的协议
            if (regex.IsMatch(input))
            {
				// 获取匹配的值
                string match = regex.Match(input).Groups[0].Value;
                outputList.Add(match);
				partialProtocal = "";

				// 缩短input的长度
                input = input.Substring(input.IndexOf(match)+ match.Length);

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
