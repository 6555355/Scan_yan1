namespace TcpIpBase {
    using System;

    [Serializable]
    public class ProtocolFhzl : Protocol
    {
        private TcpIpCmdFhzl cmd;
		private byte _subcmd;
		private string parameter;

        private CmdReturnValueEnum cmdReturnValue;
        private bool processed;

        public ProtocolFhzl(TcpIpCmdFhzl cmdEnum, byte subcmd, string para)
		{
            this.cmd = (TcpIpCmdFhzl) cmdEnum;
            this._subcmd = subcmd;
            this.parameter = para;
		    this.CmdReturnValue = cmdReturnValue;
		    this.processed = false;
        }

        public ProtocolFhzl()
            : base()
        {
            
        }

        public int RepeatPos { get; set; }
        public bool IsDirty
        {
            get;
            set;
        }
        public TcpIpCmdFhzl Cmd
        {
			get { return cmd; }
            set
            {
                cmd = value;
            }
		}

        public byte SubCmd
        {
			get { return _subcmd; }
            set
            {
                _subcmd = value;
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

        public override string ToString()
        {
            //return SerializationUnit.Serialize(this);
            return string.Format("${0}{1}", cmdReturnValue, parameter);
        }
    }
}
