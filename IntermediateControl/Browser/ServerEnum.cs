using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;

namespace BYHXPrinterManager.Browser
{
    public class ServerEnum : IEnumerable
    {
        enum ErrorCodes
        {
            NO_ERROR = 0,
            ERROR_NO_MORE_ITEMS = 259
        };

        [StructLayout(LayoutKind.Sequential)]
        private class NETRESOURCE
        {
            public ResourceScope dwScope = 0;
            public ResourceType dwType = 0;
            public ResourceDisplayType dwDisplayType = 0;
            public ResourceUsage dwUsage = 0;
            public string lpLocalName = null;
            public string lpRemoteName = null;
            public string lpComment = null;
            public string lpProvider = null;
        };


        private ArrayList aData = new ArrayList();


        public int Count
        {
            get { return aData.Count; }
        }

        [DllImport("Mpr.dll", EntryPoint = "WNetOpenEnumA", CallingConvention = CallingConvention.Winapi)]
        private static extern ErrorCodes WNetOpenEnum(ResourceScope dwScope, ResourceType dwType, ResourceUsage dwUsage, NETRESOURCE p, out IntPtr lphEnum);

        [DllImport("Mpr.dll", EntryPoint = "WNetCloseEnum", CallingConvention = CallingConvention.Winapi)]
        private static extern ErrorCodes WNetCloseEnum(IntPtr hEnum);

        [DllImport("Mpr.dll", EntryPoint = "WNetEnumResourceA", CallingConvention = CallingConvention.Winapi)]
        private static extern ErrorCodes WNetEnumResource(IntPtr hEnum, ref uint lpcCount, IntPtr buffer, ref uint lpBufferSize);


        private void EnumerateServers(NETRESOURCE pRsrc, ResourceScope scope, ResourceType type, ResourceUsage usage, ResourceDisplayType displayType, string kPath)
        {
            uint bufferSize = 16384;
            IntPtr buffer = Marshal.AllocHGlobal((int)bufferSize);
            IntPtr handle = IntPtr.Zero;
            ErrorCodes result;
            uint cEntries = 1;
            bool serverenum = false;

            result = WNetOpenEnum(scope, type, usage, pRsrc, out handle);

            if (result == ErrorCodes.NO_ERROR)
            {
                do
                {
                    result = WNetEnumResource(handle, ref cEntries, buffer, ref	bufferSize);

                    if ((result == ErrorCodes.NO_ERROR))
                    {
                        Marshal.PtrToStructure(buffer, pRsrc);

                        if (String.Compare(kPath, "") == 0)
                        {
                            if ((pRsrc.dwDisplayType == displayType) || (pRsrc.dwDisplayType == ResourceDisplayType.RESOURCEDISPLAYTYPE_DOMAIN))
                                aData.Add(pRsrc.lpRemoteName + "|" + pRsrc.dwDisplayType);

                            if ((pRsrc.dwUsage & ResourceUsage.RESOURCEUSAGE_CONTAINER) == ResourceUsage.RESOURCEUSAGE_CONTAINER)
                            {
                                if ((pRsrc.dwDisplayType == displayType))
                                {
                                    EnumerateServers(pRsrc, scope, type, usage, displayType, kPath);

                                }

                            }
                        }
                        else
                        {
                            if (pRsrc.dwDisplayType == displayType)
                            {
                                aData.Add(pRsrc.lpRemoteName);
                                EnumerateServers(pRsrc, scope, type, usage, displayType, kPath);
                                //return;
                                serverenum = true;
                            }
                            if (!serverenum)
                            {
                                if (pRsrc.dwDisplayType == ResourceDisplayType.RESOURCEDISPLAYTYPE_SHARE)
                                {
                                    aData.Add(pRsrc.lpRemoteName + "-share");
                                }
                            }
                            else
                            {
                                serverenum = false;
                            }
                            if ((kPath.IndexOf(pRsrc.lpRemoteName) >= 0) || (String.Compare(pRsrc.lpRemoteName, "Microsoft Windows Network") == 0))
                            {
                                EnumerateServers(pRsrc, scope, type, usage, displayType, kPath);
                                //return;

                            }
                            //}
                        }

                    }
                    else if (result != ErrorCodes.ERROR_NO_MORE_ITEMS)
                        break;
                } while (result != ErrorCodes.ERROR_NO_MORE_ITEMS);

                WNetCloseEnum(handle);
            }

            Marshal.FreeHGlobal((IntPtr)buffer);
        }

        public ServerEnum(ResourceScope scope, ResourceType type, ResourceUsage usage, ResourceDisplayType displayType, string kPath)
        {

            NETRESOURCE netRoot = new NETRESOURCE();
            EnumerateServers(netRoot, scope, type, usage, displayType, kPath);

        }
        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return aData.GetEnumerator();
        }

        #endregion
    }
}
