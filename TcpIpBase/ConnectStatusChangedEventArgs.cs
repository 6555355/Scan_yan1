// -----------------------------------------------------------------------
// <copyright file="ConnectStatusChangedEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TcpIpBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConnectStatusChangedEventArgs : EventArgs
    {
        readonly bool newStatus;

        private readonly bool oldStatus;

        public ConnectStatusChangedEventArgs(bool newStatus, bool oldStatus)
        {
            this.newStatus = newStatus;
            this.oldStatus = oldStatus;
        }

        public bool NewStatus
        {
            get
            {
                return newStatus;
            }
        }

        public bool OldStatus
        {
            get
            {
                return this.oldStatus;
            }
        }
    }
}
