namespace TcpIpBase {
    using System;

    [Serializable]
    public class ProtocolBc : Protocol
    {
        private TcpIpCmdEnum cmd;
		private int id;
		private string parameter;

        private CmdReturnValueEnum cmdReturnValue;
        private bool processed;

        private int port;
        private long ip;
        public ProtocolBc(byte cmdEnum, int cmdid, string para, CmdReturnValueEnum cmdReturnValue, int port, long ipadd)
            : base(cmdEnum, cmdid, para, cmdReturnValue, port)
		{
            this.cmd = (TcpIpCmdEnum) cmdEnum;
            this.id = cmdid;
            this.parameter = para;
            this.ip = ipadd;
            this.Port = port;
		    this.CmdReturnValue = cmdReturnValue;
		    this.processed = false;
        }
        public ProtocolBc(byte cmdEnum, string para, CmdReturnValueEnum cmdReturnValue, int port, long ipadd)
            : base(cmdEnum, para, cmdReturnValue, port)
        {
            this.cmd = (TcpIpCmdEnum) cmdEnum;
            this.id = 0;
            this.parameter = para;
            this.ip = ipadd;
            this.Port = port;
            this.CmdReturnValue = cmdReturnValue;
            this.processed = false;
        }

        public ProtocolBc():base()
        {
            
        }

        public long Ip
        {
            get
            {
                return this.ip;
            }
            set
            {
                this.ip = value;
            }
        }

        public int RepeatPos { get; set; }
        public bool IsDirty
        {
            get;
            set;
        }
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
            //return SerializationUnit.Serialize(this);
            return string.Format("${0}{1}", cmdReturnValue, parameter);
        }
    }
}
