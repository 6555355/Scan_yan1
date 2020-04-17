
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;

namespace BYHXPrinterManager.TcpIp {
    using System;

    [Serializable]
    public struct Protocol {
        private TcpIpCmdEnum cmd;
		private int id;
		private string parameter;

        private CmdReturnValueEnum cmdReturnValue;
        private bool processed;

        private int port;
		public Protocol(TcpIpCmdEnum cmdEnum, int cmdid, string para, CmdReturnValueEnum cmdReturnValue, int port)
		{
            this.cmd = cmdEnum;
            this.id = cmdid;
            this.parameter = para;
		    this.port = port;
		    this.cmdReturnValue = cmdReturnValue;
		    this.processed = false;
        }
        public Protocol(TcpIpCmdEnum cmdEnum, string para, CmdReturnValueEnum cmdReturnValue, int port)
        {
            this.cmd = cmdEnum;
            this.id = 0;
            this.parameter = para;
            this.port = port;
            this.cmdReturnValue = cmdReturnValue;
            this.processed = false;
        }

//        public bool IsDirty
//        {
//            get;
//            set;
//        }
        public TcpIpCmdEnum Cmd
        {
			get { return cmd; }
            set
            {
                cmd = value;
            }
		}

		public int Id {
			get { return id; }
            set
            {
                id = value;
            }
		}

        public string Parameter
        {
			get { return parameter; }
            set
            {
                parameter = value;
            }
		}

        public bool Processed
        {
            get
            {
                return this.processed;
            }
            set
            {
                this.processed = value;
            }
        }

        public CmdReturnValueEnum CmdReturnValue
        {
            get
            {
                return this.cmdReturnValue;
            }
            set
            {
                this.cmdReturnValue = value;
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

        public override string ToString()
        {
            return SerializationUnit.Serialize(this);
        }
    }
}
