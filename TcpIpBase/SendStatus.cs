namespace TcpIpBase {
    using System;
    using System.IO;

    // 即时计算发送文件的状态
	public class SendStatus {
		private FileInfo info;
		private long fileBytes;

		public SendStatus(string filePath) {
			info = new FileInfo(filePath);
			fileBytes = info.Length;
		}

		public void PrintStatus(int sent) {
			string percent = GetPercent(sent);
			Console.WriteLine("Sending {0} bytes, {1}% ...", sent, percent);
		}

		// 获得文件发送的百分比
		public string GetPercent(int sent) {

			decimal allBytes = Convert.ToDecimal(fileBytes);
			decimal currentSent = Convert.ToDecimal(sent);

			decimal percent = (currentSent / allBytes) * 100;
			percent = Math.Round(percent, 1);	//保留一位小数

			if (percent.ToString() == "100.0")
				return "100";
			else
				return percent.ToString();
		}
	}

}
