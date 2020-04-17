// -----------------------------------------------------------------------
// <copyright file="TcpIpCmdEnum.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TcpIpBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public enum TcpIpCmdEnum
    {
        Error =  -1,
        StatusDirty = 0,
        ReportSettings,
        UpdateSettings,
        SetSettings,
        SendFile,
        RipFile,
        CancelRipFile,
        StartPrint,
        StopPrint
    }
}
