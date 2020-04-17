using System;
using System.Collections.Generic;
using System.Text;

namespace TcpIpBase
{
    public class TcpIpConst
    {
        public const int Port = 5001;
        public const string NetPrintPattern = "(^<\\?xml version=\"1.0\" encoding=\"utf-16\"\\?>\\s*<protocol xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">.*?</protocol>)";
        public const string BarcodePattern = "(^\\$.*?)";
        public const string FhzlPattern = "(^FHZL01.*?BYHX)";//"(^start;.*?end)";
        public const string ByhxPattern = "(^BYHX01.*?BYHX)";//"(^start;.*?end)";

    }
}
