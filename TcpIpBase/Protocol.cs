using System.Collections.Generic;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;

namespace TcpIpBase {
    using System;

    [Serializable]
    public class Protocol {
        private TcpIpCmdEnum cmd;
		private int id;
		private string parameter;

        private CmdReturnValueEnum cmdReturnValue;
        private bool processed;

        private int port;
		public Protocol(byte cmdEnum, int cmdid, string para, CmdReturnValueEnum cmdReturnValue, int port)
		{
            this.cmd = (TcpIpCmdEnum) cmdEnum;
            this.id = cmdid;
            this.parameter = para;
		    this.Port = port;
		    this.CmdReturnValue = cmdReturnValue;
		    this.processed = false;
        }
        public Protocol(byte cmdEnum, string para, CmdReturnValueEnum cmdReturnValue, int port)
        {
            this.cmd = (TcpIpCmdEnum) cmdEnum;
            this.id = 0;
            this.parameter = para;
            this.Port = port;
            this.CmdReturnValue = cmdReturnValue;
            this.processed = false;
        }

        public Protocol()
        {
            //throw new NotImplementedException();
        }

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
            return SerializationUnit.Serialize(this);
        }
    }
}
