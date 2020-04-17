using System;
using System.Text;
using System.Runtime.InteropServices;

namespace BYHXPrinterManager
{
    public class S4_API
    {
        //Sense4 API
        public const string c_S4DllName = "Sense4.dll";

        #region const var
        //@{ 
        /** 
                device share mode definition
        */
        static public uint S4_EXCLUSIZE_MODE = 0;                               /** exclusive mode*/
        static public uint S4_SHARE_MODE = 1;                               /** sharable mode*/
        //@}                                                         

        //@{
        /**
                the control code value definition
        */
        static public uint S4_LED_UP = 0x00000004;                      /** LED up*/
        static public uint S4_LED_DOWN = 0x00000008;                      /** LED down*/
        static public uint S4_LED_WINK = 0x00000028;                      /** LED wink*/
        static public uint S4_GET_DEVICE_TYPE = 0x00000025;                      /** get the device type*/
        static public uint S4_GET_SERIAL_NUMBER = 0X00000026;                      /** get the device serial number*/
        static public uint S4_GET_VM_TYPE = 0X00000027;                      /** get the virtual machine type*/
        static public uint S4_GET_DEVICE_USABLE_SPACE = 0x00000029;                      /** get the total space of the device*/
        static public uint S4_SET_DEVICE_ID = 0x0000002a;                      /** set the device ID*/
        static public uint S4_RESET_DEVICE = 0x00000002;                      /** reset the device*/
        static public uint S4_DF_AVAILABLE_SPACE = 0x00000031;                      /** get the free space of current directory*/
        static public uint S4_EF_INFO = 0x00000032;                      /** get specified file information in current directory*/
        static public uint S4_SET_USB_MODE = 0x00000041;                      /** set the device as a normal usb device*/
        static public uint S4_SET_HID_MODE = 0x00000042;                      /** set the device as a HID device*/
        static public uint S4_GET_CUSTOMER_NAME = 0x0000002b;                      /** get the customer number*/
        static public uint S4_GET_MANUFACTURE_DATE = 0x0000002c;                      /** get the manufacture date of the device*/
        static public uint S4_GET_CURRENT_TIME = 0x0000002d;                      /** get the current time of the clock device*/
        static public uint S4_SET_NET_CONFIG = 0x00000030;                      /** set netlock config */
        //@}                                                                  



        //@} 

        //@{
        /**
                device type definition
        */
        public const byte S4_LOCAL_DEVICE = 0x00;                            /** local device*/
        public const byte S4_MASTER_DEVICE = 0x01;                            /** net master device*/
        public const byte S4_SLAVE_DEVICE = 0x02;                            /** net slave device*/

        //@} 

        //@{
        /**
                virtual machine type definition
        */
        static public uint S4_VM_51 = 0x00;                            /** inter 51*/
        static public uint S4_VM_251_BINARY = 0x01;                            /** inter 251, binary mode*/
        static public uint S4_VM_251_SOURCE = 0X02;                            /** inter 251, source mode*/


        //@}

        //@{
        /**
                NetLock license mode
        */
        static public uint S4_MODULE_MODE = 0x00000000;                            /** Module mode*/
        static public uint S4_IP_MODE = 0x00000001;                            /** IP mode*/

        //@}

        //@{
        /**
                PIN and key type definition
        */
        static public uint S4_USER_PIN = 0x000000a1;                      /** user PIN*/
        static public uint S4_DEV_PIN = 0x000000a2;                      /** developer PIN*/
        static public uint S4_AUTHEN_PIN = 0x000000a3;                      /** authentication key of net device*/

        //@}

        //@{
        /**
                file type definition
        */

        static public uint S4_RSA_PUBLIC_FILE = 0x00000006;                      /** RSA public key file*/
        static public uint S4_RSA_PRIVATE_FILE = 0x00000007;                      /** RSA private key file*/
        static public uint S4_EXE_FILE = 0x00000008;                      /** executable file of virtual machine*/
        static public uint S4_DATA_FILE = 0x00000009;                      /** data file*/
        static public uint S4_XA_EXE_FILE = 0x0000000b;                      /** executable file of XA User mode*/


        //@}

        //@{
        /**
                flag value definition
        */
        static public uint S4_CREATE_NEW = 0x000000a5;                      /** create a new file*/
        static public uint S4_UPDATE_FILE = 0x000000a6;                      /** write data to the specified file*/
        static public uint S4_KEY_GEN_RSA_FILE = 0x000000a7;                      /** generate RSA key pair files*/
        static public uint S4_SET_LICENCES = 0x000000a8;                      /** set the max license number of the current module for the net device*/
        static public uint S4_CREATE_ROOT_DIR = 0x000000ab;                      /** create root directory*/
        static public uint S4_CREATE_SUB_DIR = 0x000000ac;                      /** create child directory for current directory*/
        static public uint S4_CREATE_MODULE = 0x000000ad;                      /** create a module directory for the net device */
        /** the following three flags can only be used when creating a new executable file */
        static public uint S4_FILE_READ_WRITE = 0x00000000;                      /** the new executable file can be read and written by executable file */
        static public uint S4_FILE_EXECUTE_ONLY = 0x00000100;                      /** the new executable file can't be read or written by executable file*/
        static public uint S4_CREATE_PEDDING_FILE = 0x00002000;                      /** create a padding file*/


        //@}

        //@{
        /** 
                execuable file executing mode definition
        */
        static public uint S4_VM_EXE = 0x00000000;                      /** executing on virtual machine*/
        static public uint S4_XA_EXE = 0x00000001;                      /** executing on XA User mode   */

        //@}

        //@{
        /**
                return value definition
        */

        static public uint S4_SUCCESS = 0x00000000;                      /** success*/
        static public uint S4_UNPOWERED = 0x00000001;                      /** the device has been powered off*/
        static public uint S4_INVALID_PARAMETER = 0x00000002;                      /** invalid parameter*/
        static public uint S4_COMM_ERROR = 0x00000003;                      /** communication error*/
        static public uint S4_PROTOCOL_ERROR = 0x00000004;                      /** communication protocol error*/
        static public uint S4_DEVICE_BUSY = 0x00000005;                      /** the device is busy*/
        static public uint S4_KEY_REMOVED = 0x00000006;                      /** the device has been removed */
        static public uint S4_INSUFFICIENT_BUFFER = 0x00000011;                      /** the input buffer is insufficient*/
        static public uint S4_NO_LIST = 0x00000012;                      /** find no device*/
        static public uint S4_GENERAL_ERROR = 0x00000013;                      /** general error, commonly indicates not enough memory*/
        static public uint S4_UNSUPPORTED = 0x00000014;                      /** the function isn't supported*/
        static public uint S4_DEVICE_TYPE_MISMATCH = 0x00000020;                      /** the device type doesn't match*/
        static public uint S4_FILE_SIZE_CROSS_7FFF = 0x00000021;                      /** the execuable file crosses address 0x7FFF*/
        static public uint S4_CURRENT_DF_ISNOT_MF = 0x00000201;                      /** a net module must be child directory of the root directory*/
        static public uint S4_INVAILABLE_MODULE_DF = 0x00000202;                      /** the current directory is not a module*/
        static public uint S4_FILE_SIZE_TOO_LARGE = 0x00000203;                      /** the file size is beyond address 0x7FFF*/
        static public uint S4_DF_SIZE = 0x00000204;                      /** the specified directory size is too small*/
        static public uint S4_DEVICE_UNSUPPORTED = 0x00006a81;                      /** the request can't be supported by the device*/
        static public uint S4_FILE_NOT_FOUND = 0x00006a82;                      /** the specified file or directory can't be found */
        static public uint S4_INSUFFICIENT_SECU_STATE = 0x00006982;                      /** the security state doesn't match*/
        static public uint S4_DIRECTORY_EXIST = 0x00006901;                      /** the specified directory has already existed*/
        static public uint S4_FILE_EXIST = 0x00006a80;                      /** the specified file or directory has already existed*/
        static public uint S4_INSUFFICIENT_SPACE = 0x00006a84;                      /** the space is insufficient*/
        static public uint S4_OFFSET_BEYOND = 0x00006B00;                      /** the offset is beyond the file size*/
        static public uint S4_PIN_BLOCK = 0x00006983;                      /** the specified pin or key has been locked*/
        static public uint S4_FILE_TYPE_MISMATCH = 0x00006981;                      /** the file type doesn't match*/
        static public uint S4_CRYPTO_KEY_NOT_FOUND = 0x00009403;                      /** the specified pin or key cann't be found*/
        static public uint S4_APPLICATION_TEMP_BLOCK = 0x00006985;                      /** the directory has been temporarily locked*/
        static public uint S4_APPLICATION_PERM_BLOCK = 0x00009303;                      /** the directory has been locked*/
        static public uint S4_DATA_BUFFER_LENGTH_ERROR = 0x00006700;                      /** invalid data length*/
        static public uint S4_CODE_RANGE = 0x00010000;                      /** the PC register of the virtual machine is out of range*/
        static public uint S4_CODE_RESERVED_INST = 0x00020000;                      /** invalid instruction*/
        static public uint S4_CODE_RAM_RANGE = 0x00040000;                      /** internal ram address is out of range*/
        static public uint S4_CODE_BIT_RANGE = 0x00080000;                      /** bit address is out of range*/
        static public uint S4_CODE_SFR_RANGE = 0x00100000;                      /** SFR address is out of range*/
        static public uint S4_CODE_XRAM_RANGE = 0x00200000;                      /** external ram address is out of range*/
        static public uint S4_ERROR_UNKNOWN = 0xffffffff;                      /** unknown error*/


        //@}

        static public uint MAX_ATR_LEN = 56;                              /** max ATR length */
        static public uint MAX_ID_LEN = 8;                               /** max device ID length */
        static public uint S4_RSA_MODULUS_LEN = 128;                             /** RSA key modules length,in bytes */
        static public uint S4_RSA_PRIME_LEN = 64;                              /** RSA key prime length,in bytes*/
        #endregion

        //Assume that Sense4user.dll in c:\, if not, modify the lines below
        [DllImport(c_S4DllName)]
        public static extern uint S4Enum([MarshalAs(UnmanagedType.LPArray), Out] SENSE4_CONTEXT[] s4_context, ref uint size);
        [DllImport(c_S4DllName)]
        public static extern uint S4Open(ref SENSE4_CONTEXT s4_context);
        [DllImport(c_S4DllName)]
        public static extern uint S4OpenEx(ref SENSE4_CONTEXT s4_context,S4OPENINFO pS4OpenInfo);
        [DllImport(c_S4DllName)]
        public static extern uint S4Close(ref SENSE4_CONTEXT s4_context);
        [DllImport(c_S4DllName)]
        public static extern uint S4Control(ref SENSE4_CONTEXT s4Ctx, uint ctlCode, byte[] inBuff,
            uint inBuffLen, byte[] outBuff, uint outBuffLen, ref uint BytesReturned);
        [DllImport(c_S4DllName)]
        public static extern uint S4CreateDir(ref SENSE4_CONTEXT s4Ctx, string DirID, uint DirSize, uint Flags);
        [DllImport(c_S4DllName)]
        public static extern uint S4ChangeDir(ref SENSE4_CONTEXT s4Ctx, string Path);
        [DllImport(c_S4DllName)]
        public static extern uint S4EraseDir(ref SENSE4_CONTEXT s4Ctx, string DirID);
        [DllImport(c_S4DllName)]
        public static extern uint S4VerifyPin(ref SENSE4_CONTEXT s4Ctx, byte[] Pin, uint PinLen, uint PinType);
        [DllImport(c_S4DllName)]
        private static extern uint S4ChangePin(ref SENSE4_CONTEXT s4Ctx, byte[] OldPin, uint OldPinLen,
            byte[] NewPin, uint NewPinLen, uint PinType);
        [DllImport(c_S4DllName)]
        public static extern uint S4WriteFile(ref SENSE4_CONTEXT s4Ctx, string FileID, uint Offset,
            byte[] Buffer, uint BufferSize, uint FileSize, ref uint BytesWritten, uint Flags,
            uint FileType);
        [DllImport(c_S4DllName)]
        public static extern uint S4Execute(ref SENSE4_CONTEXT s4Ctx, string FileID, byte[] InBuffer,
            uint InbufferSize, byte[] OutBuffer, uint OutBufferSize, ref uint BytesReturned);
        //[DllImport(c_S4DllName)]
        //public static extern uint S4Execute(ref SENSE4_CONTEXT s4Ctx, string FileID, IO_PACKAGE InBuffer,
        //    uint InbufferSize, IO_PACKAGE OutBuffer, uint OutBufferSize, ref uint BytesReturned);
    }
}
