using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BYHXPrinterManager
{
    public class Common
    {
        public const int DBT_DEVTYP_DEVICEINTERFACE = 0x00000005;  // device interface class
        public const int DEVICE_NOTIFY_WINDOW_HANDLE = (0x00000000);
        public const int DEVICE_NOTIFY_ALL_INTERFACE_CLASSES = (0x00000004);
        public static Guid GUID_CLASS_ITOKEN2 = new Guid(0x171638f7, 0x1ead, 0x4873, new byte[] { 0xba, 0x98, 0xc9, 0x66, 0xab, 0xcf, 0x1, 0x42 });
        /*
* The following messages are for WM_DEVICECHANGE. The immediate list
* is for the wParam. ALL THESE MESSAGES PASS A POINTER TO A STRUCT
* STARTING WITH A DWORD SIZE AND HAVING NO POINTER IN THE STRUCT.
*
*/
        public const int DBT_DEVICEARRIVAL = 0x8000;  // system detected a new device
        public const int DBT_DEVICEQUERYREMOVE = 0x8001;  // wants to remove, may fail
        public const int DBT_DEVICEQUERYREMOVEFAILED = 0x8002;  // removal aborted
        public const int DBT_DEVICEREMOVEPENDING = 0x8003;  // about to remove, still avail.
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004;  // device is gone
        public const int DBT_DEVICETYPESPECIFIC = 0x8005;  // type specific event

        /*
	Print an array in Hex.	
	Parameters:
		fp[in]  : file pointer  
		data[in]: data to be printed
		len[in] : data length
		
	Return:
		none.

	Remarks:
		none.
*/
        //void hexprint(FILE *fp, void *data, int len)
        //{
        //    unsigned char *pdata = (unsigned char *)data;
        //    int i = 0;

        //    for (; i<len ;i++)
        //    {
        //        if (!(i%16))
        //            fprintf(fp,"\n");
        //        fprintf(fp, "%02x:",*(pdata+i));
        //    }
        //    fprintf(fp, "\n");
        //}

        /*
            Select and open specified EliteIV device  
            Parameters:
                index[in]    : device index
                pstS4Ctx[out]: pointer to EliteIV context
		
            Return: 
                If the function succeeds, it will return 1(TRUE), otherwise 
                it returns 0(FALSE).

            Remarks:
                none.
        */
        public static BYHX_SL_RetValue OpenS4ByIndex(int index, ref SENSE4_CONTEXT pstS4Ctx)
        {
			if(index < 0)
				return BYHX_SL_RetValue.NOFOUNDDOG;
            SENSE4_CONTEXT[] pstS4CtxList = null;
            uint dwCtxListSize = 0;
            uint dwResult = 0;
            uint dwDeviceNum = 0;

            //if (null == pstS4Ctx) 
            //{
            //    MessageBox.Show("Invalid pointer!\n");
            //    return 0;
            //}
            /*
                Use S4Enum(...) to Enumerate all the devices connected to the host.
                Using null as the 1st parameter will instruct S4Enum to return the buffer size needed to hold
                the corresponding EliteIV context in the 2nd parameter:dwCtxListSize.
                Generally only two results possible on return: (dwCtxListSize == 0) which means no EliteIV device 
                present or (dwCtxListSize != 0) and that's saying there are  dwCtxListSize/sizeof(SENSE4_CONTEXT) sets 
                of EliteIV devices connected currently.
            */
            dwResult = S4_API.S4Enum(null, ref dwCtxListSize);
            if (dwResult != S4_API.S4_SUCCESS && dwResult != S4_API.S4_INSUFFICIENT_BUFFER)
            {
#if DEBUG
                string msg = string.Format("Enumerate EliteIV failed! <error code: 0x%08x>\n", dwResult);
                MessageBox.Show(msg);
#endif
                return BYHX_SL_RetValue.NOFOUNDDOG;
            }

            if (0 == dwCtxListSize)
            {
#if DEBUG
                MessageBox.Show("EliteIV not found!\n");
#endif
                return BYHX_SL_RetValue.NOFOUNDDOG;
            }

            // allocate memory for EliteIV context list/array
            pstS4CtxList = new SENSE4_CONTEXT[dwCtxListSize / Marshal.SizeOf(typeof(SENSE4_CONTEXT))]; ;
            if (null == pstS4CtxList)
            {
#if DEBUG
                MessageBox.Show("Not enough memory! \n");
#endif
                return BYHX_SL_RetValue.NOFOUNDDOG;
            }

            dwDeviceNum = (uint)(dwCtxListSize / Marshal.SizeOf(typeof(SENSE4_CONTEXT)));
            if (index + 1 > dwDeviceNum)
            {
#if DEBUG
                string msg = string.Format("Invalid index!<index should be within [0..%d]> \n", dwDeviceNum - 1);
                MessageBox.Show(msg);
#endif
                //free(pstS4CtxList);
                pstS4CtxList = null;
                return BYHX_SL_RetValue.NOFOUNDDOG;
            }

            /*
                This time, call S4Enum(...) to do the real Enumeration.
                All the fields of the EliteIV context(s) pointed by pstS4CtxList will be filled 
                with the corresponding device information upon a successful function return.
            */
            dwResult = S4_API.S4Enum(pstS4CtxList, ref dwCtxListSize);
            if (dwResult != S4_API.S4_SUCCESS)
            {
#if DEBUG
                string msg = string.Format("Open EliteIV failed! <error code: {0}x%08x>\n", dwResult);
                MessageBox.Show(msg);
#endif
                //free(pstS4CtxList);
                pstS4CtxList = null;
                return BYHX_SL_RetValue.ILLEGALDOG;
            }

            // use device with specified index  
            pstS4Ctx = pstS4CtxList[index];

            //free(pstS4CtxList);
            pstS4CtxList = null;

            // Call S4Open(...) to open the specified EliteIV device in "share mode".
            //S4OPENINFO s4openinfo  =new S4OPENINFO();
            //s4openinfo.dwS4OpenInfoSize = (short)Marshal.SizeOf(typeof(S4OPENINFO));
            //s4openinfo.dwShareMode = (short)S4_API.S4_EXCLUSIZE_MODE;
            //dwResult = S4_API.S4OpenEx(ref pstS4Ctx, s4openinfo);
            dwResult = S4_API.S4Open(ref pstS4Ctx);

            if (dwResult != S4_API.S4_SUCCESS)
            {
#if DEBUG
                string msg = string.Format("Open EliteIV failed! <error code: {0}x%08x>\n", dwResult);
                MessageBox.Show(msg);
#endif
                return BYHX_SL_RetValue.ILLEGALDOG;
            }
            return BYHX_SL_RetValue.SUCSESS;
        }
        /*
            Reset and close specified EliteIV device  
            Parameters:
                pstS4Ctx[out]: pointer to EliteIV context
		
            Return: 
                none.	

            Remarks:
                Use this function instead of S4Close() may enhance security in certain
                circumstances.
        */
        public static void ResetAndCloseS4(SENSE4_CONTEXT pstS4Ctx)
        {
            uint ret = 0;
            S4_API.S4Control(ref pstS4Ctx, S4_API.S4_RESET_DEVICE, null, 0, null, 0, ref ret);
            S4_API.S4Close(ref pstS4Ctx);
            return;
        }

        /// <summary>
        /// 功能：通过名柄，关闭指定设备的信息。(主要应用于清理非托管资源，并与RegisterDeviceNotification配对使用)
        /// </summary>
        /// <param name="hHandle"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint UnregisterDeviceNotification(IntPtr hHandle);

        /// <summary>
        /// 功能：注册设备或者设备类型，在指定的窗口返回相关的信息
        /// </summary>
        /// <param name="hRecipient"></param>
        /// <param name="NotificationFilter"></param>
        /// <param name="Flags"></param>
        /// <returns></returns>
           [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, DEV_BROADCAST_DEVICEINTERFACE NotificationFilter, UInt32 Flags);

		
		public static SENSE4_CONTEXT[] EnumAllS4()
		{
			SENSE4_CONTEXT[] pstS4CtxList = null;
			uint dwCtxListSize = 0;
			uint dwResult = 0;
			uint dwDeviceNum = 0;

			dwResult = S4_API.S4Enum(null, ref dwCtxListSize);
			if (dwResult != S4_API.S4_SUCCESS && dwResult != S4_API.S4_INSUFFICIENT_BUFFER)
			{
#if DEBUG
				string msg = string.Format("Enumerate EliteIV failed! <error code: 0x%08x>\n", dwResult);
				MessageBox.Show(msg);
#endif
				return null;
			}

			if (0 == dwCtxListSize)
			{
//#if DEBUG
//                MessageBox.Show("EliteIV not found!\n");
//#endif
				return null;
			}

			// allocate memory for EliteIV context list/array
			pstS4CtxList = new SENSE4_CONTEXT[dwCtxListSize / Marshal.SizeOf(typeof(SENSE4_CONTEXT))];

			dwDeviceNum = (uint)(dwCtxListSize / Marshal.SizeOf(typeof(SENSE4_CONTEXT)));
			/*
				This time, call S4Enum(...) to do the real Enumeration.
				All the fields of the EliteIV context(s) pointed by pstS4CtxList will be filled 
				with the corresponding device information upon a successful function return.
			*/
			dwResult = S4_API.S4Enum(pstS4CtxList, ref dwCtxListSize);
			if (dwResult != S4_API.S4_SUCCESS)
			{
#if DEBUG
				string msg = string.Format("Open EliteIV failed! <error code: {0}x%08x>\n", dwResult);
				MessageBox.Show(msg);
#endif
				//free(pstS4CtxList);
				pstS4CtxList = null;
			}
			return pstS4CtxList;
		}
    }

    // WM_DEVICECHANGE message       
    [StructLayout(LayoutKind.Sequential)]
    public class DEV_BROADCAST_DEVICEINTERFACE
    {
        public int dbcc_size;
        public int dbcc_devicetype;
        public int dbcc_reserved;
        public Guid dbcc_classguid;
        public byte dbcc_name;
    }
}
