// -----------------------------------------------------------------------
// <copyright file="TcpIpCmdEnum.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BYHXPrinterManager.TcpIp
{
    using System;
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
