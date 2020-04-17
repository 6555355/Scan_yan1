// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the PRINTERPORT_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// PRINTERPORT_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef PRINTERPORT_EXPORTS
#define PRINTERPORT_API __declspec(dllexport)
#else
#define PRINTERPORT_API __declspec(dllimport)
#endif
enum PrinterPortErrorCode
{
	SoftWareNotExist = -1,  //; means Printer software exit. Need not send.
	PrinterIsPoweroff = -2, //mean Printer power off. Need not send.
	PrinterInError = -3,   // mean Printer has error. Need not send.
	LaunchError = -4,       //mean Not found Printer Manager Software. Can not launch software.
	ProcessPortError = -5,       //mean Process communication error.
	ProcessPortCreateError = -6,       //mean Process communication error.
	ProcessPortBusyError = -7,       //mean Process communication error.

};



extern "C"
{
// Printer Manager: Check whether  Printer Manager Software has launched in system, if not launched, launch  Printer Manager Software. Then, open Cross Process Port, if successful; return the Cross Process Port handle. Else, return error code, represents with negative.
//RIP software: Should call this function before print one job. The return value is the port handle, used for send data and close. If the return value <0, then display the error code, error code describe as 5 Error code.
PRINTERPORT_API int OpenPrinterPort();
// Printer Manager: Send one package of printer data to  Printer Manager Software, buffer contains print data, and buffer size represents the size of print data. The return value, represent the true sent size to printer. Now, use the synchronous send method, call this function will not return, until all data send finished, or error occur.
//RIP software: Send the  printer file through this virtual port. If the return value <0, then display the error code, error code describe as 5 Error code.
PRINTERPORT_API int SendDataPrintrPort(int handle, unsigned char * buffer, int buffersize);
// Printer Manager: Close the connection of between Rip software and  Printer Manager Software. Release the resource of Cross Process Port. Now, use the synchronous method, call this function will not return, until finish printing the current printer job. Note, although the Printer Manager Software is launched by RIP, but it will not close the Printer Manager Software automatically, because maybe the Printer Manager Software is launched by user
//RIP software: After job is printed, close printer port. If the return value <0, then display the error code, error code describe as 5 Error code...  
PRINTERPORT_API int ClosePrinterPort(int handle);
// Printer Manager: Stop print and exit immediately.
//RIP software: Suggest, above three functions live in one send printer data thread, because the synchronous method used in port design, will not return until finished transfer, that will not refreshed UI. On the other hand, if the function is blocked by printer, but the RIP software wants Exit, so call this function will break the synchronous method, the printer job will abort.
PRINTERPORT_API int AbortPrinterPort();
}