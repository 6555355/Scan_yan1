/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using System.Threading;

namespace BYHXPrinterManager
{
	/// <summary>
	/// Summary description for JetStatus.
	/// </summary>
	public class PrinterOperate
	{
		public bool CanPrint;
		public bool CanOnline;
		public bool CanOffline;
		public bool CanClean;
		public bool CanSpray;
		public bool CanAbort;
		public bool CanPause;
		public bool CanResume;
		public bool CanMoveLeft;
		public bool CanMoveRight;
		public bool CanMoveForward;
		public bool CanMoveBackward;
		public bool CanMoveUp;
		public bool CanMoveDown;
		public bool CanMoveStop;
		public bool CanMoveOriginal;
		public bool CanSaveLoadSettings;
		public bool CanUpdate;


		public PrinterOperate()
		{
			Initialize(false);
			//UpdateByPrinterStatus(status);
		}

		static public  PrinterOperate UpdateByPrinterStatus(JetStatusEnum status)
		{
			PrinterOperate po = new  PrinterOperate();
			if(CoreInterface.Printer_IsOpen() != 0)
			{
				po.CanAbort = true;
			}
            po.CanUpdate = status != JetStatusEnum.PowerOff;
			switch(status)
			{
				case JetStatusEnum.PowerOff:
				{
					break;
				}
				case JetStatusEnum.Cleaning:
				{
					//PO.CanClean = true;
					//PO.CanSpray = true;

					break;
				}
				case JetStatusEnum.Aborting:
				{
					po.CanClean = true;
					po.CanSpray = true;
					po.CanAbort = false;
					break;
				}
				case JetStatusEnum.Ready:
				{
					po.CanOffline = true;
					po.CanClean = true;
					po.CanSpray = true;
					po.CanMoveLeft = true;
					po.CanMoveRight = true;
					po.CanMoveForward = true;
					po.CanMoveBackward = true;
					po.CanMoveOriginal = true;
					po.CanPrint = true;
					po.CanSaveLoadSettings = true;
					po.CanMoveUp = true;
					po.CanMoveDown = true;;
                    po.CanAbort = false;

					break;
				}
				case JetStatusEnum.Busy:
				{
					po.CanClean = true;
					po.CanSpray = true;
					po.CanAbort = true;
					po.CanPause = true;
					
					break;
				}
				case JetStatusEnum.Pause:
				{
                    po.CanClean = true; //SPrinterProperty.IsAllPrint() ? false : true;
					po.CanSpray = true;
					po.CanAbort = true;
					po.CanResume = true;
					po.CanMoveLeft = true;
					po.CanMoveRight = true;
					po.CanMoveForward = false;
					po.CanMoveBackward = false;
					po.CanMoveUp = true;
					po.CanMoveDown = true;;
					po.CanMoveOriginal = true;
					po.CanSaveLoadSettings = true;
					break;
				}
				case JetStatusEnum.Error:
				{
#if true
					po.CanOffline = true;
					po.CanClean = true;
					po.CanSpray = true;
					po.CanMoveLeft = false;
					po.CanMoveRight = false;
					po.CanMoveForward = false;
					po.CanMoveBackward = false;
					po.CanMoveOriginal = true;
                    po.CanSaveLoadSettings = PubFunc.GetUserPermission() != (int)UserPermission.Operator; //防止因参数错误造成界面死锁无法修正
#endif
					break;
				}
				case JetStatusEnum.Moving:
				{
					po.CanMoveStop = true;
					break;
				}
				case JetStatusEnum.Initializing:
					break;
				case JetStatusEnum.Measuring:
				{
					po.CanMoveStop = true;
					break;
				}
				case JetStatusEnum.Updating:
					break;
				case JetStatusEnum.Spraying:
					break;
				case JetStatusEnum.Offline:
					break;
			}
			return po;
		}

		private void Initialize(bool enabled)
		{
			this.CanOnline = enabled;
			this.CanOffline = enabled;
			this.CanClean = enabled;
			this.CanSpray = enabled;
			this.CanAbort = enabled;
			this.CanPause = enabled;
			this.CanResume = enabled;
			this.CanMoveLeft = enabled;
			this.CanMoveRight = enabled;
			this.CanMoveForward = enabled;
			this.CanMoveBackward = enabled;
			this.CanMoveStop = enabled;
			this.CanMoveOriginal = enabled;
			CanSaveLoadSettings = enabled;
			CanUpdate = enabled;
		}
	}
	public class JobItemOperate
	{
		public bool CanOpenJob;
		public bool CanPrintJob;
		public bool CanAbortJob;
		public bool CanAbortPrint;
		public bool CanPausePrint;
		public bool CanResumePrint;
		public bool CanDeleteJob;
		public bool CanEditJob;
		public bool CanResetJob;

		public JobItemOperate()
		{
			Initialize(false);
		}

		public JobItemOperate(JobStatus jobStatus,JetStatusEnum printerStatus)
		{
			Initialize(false);

			switch(jobStatus)
			{
				case JobStatus.Idle:
					CanPrintJob = true;
					CanDeleteJob = true;
					CanEditJob = true;
					break;
				case JobStatus.Printing:
				{
					CanAbortJob = true;
					//CanPausePrint = true;

					ProcessPrinterStatus(printerStatus);
					break;
				}
				case JobStatus.Paused:
				{
					CanAbortJob = true;
					//CanResumePrint = true;
					Debug.Assert(false);
					break;
				}
				case JobStatus.Printed:
					CanPrintJob = true;
					CanDeleteJob = true;
					CanEditJob = true;
					CanResetJob = true;
					break;
				case JobStatus.Waiting:
				{
					CanAbortJob = true;

					//ProcessPrinterStatus(printerStatus);

					break;
				}
				case JobStatus.Error:
					CanDeleteJob = true;
					CanEditJob = true;
					CanResetJob = true;
					break;
				case JobStatus.Unknown:
					Debug.Assert(false);
					break;
			}

			/*
			switch(printerStatus)
			{
				case JetStatusEnum.Busy:
					CanAbortPrint = true;
					CanPausePrint = true;
					break;
				case JetStatusEnum.Pause:
					CanAbortPrint = true;
					CanResumePrint = true;
					break;
				case JetStatusEnum.PowerOff:
				{
					this.CanPrintJob = false;
					break;
				}
				default:
					break;
			}
			*/

			if(printerStatus == JetStatusEnum.PowerOff||
				printerStatus == JetStatusEnum.Measuring)
			{
				this.CanPrintJob = false;
			}
		}

		private void ProcessPrinterStatus(JetStatusEnum printerStatus)
		{
			if(printerStatus == JetStatusEnum.Busy)
			{
				CanAbortPrint = true;
				CanPausePrint = true;
			}
			else if(printerStatus == JetStatusEnum.Pause)
			{
				CanAbortPrint = true;
				CanResumePrint = true;
			}
		}

		public JobItemOperate(bool bEnabled)
		{
			Initialize(bEnabled);
		}

		private void Initialize(bool bEnabled)
		{
			CanOpenJob = true;
			CanPrintJob = bEnabled;
			CanAbortJob = bEnabled;
			CanAbortPrint = bEnabled;
			CanPausePrint = bEnabled;
			CanResumePrint = bEnabled;
			CanDeleteJob = bEnabled;
			CanEditJob = bEnabled;
			CanResetJob = bEnabled;
		}

		public void MultipleJob(JobItemOperate multiOp)
		{
			CanOpenJob &= multiOp.CanOpenJob;
			CanPrintJob &= multiOp.CanPrintJob;
			CanAbortJob &= multiOp.CanAbortJob;
			CanAbortPrint &= multiOp.CanAbortPrint;
			CanPausePrint &= multiOp.CanPausePrint;
			CanResumePrint &= multiOp.CanResumePrint;
			CanDeleteJob &= multiOp.CanDeleteJob;
			CanEditJob &= multiOp.CanEditJob;
			CanResetJob &= multiOp.CanResetJob;
		}

		public void NoSelectedJobs()
		{
			CanPrintJob = false;
			CanAbortJob = false;
			CanAbortPrint = false;
			CanPausePrint = false;
			CanResumePrint = false;
			CanDeleteJob = false;
			CanEditJob = false;
			CanResetJob = false;
		}

		public void UpdateByStatus(JobStatus jobStatus,JetStatusEnum printerStatus)
		{
			JobItemOperate jobOperate = new JobItemOperate(jobStatus,printerStatus);

			MultipleJob(jobOperate);
		}

		static public bool IsPrinterCanPrint()
		{
			return IsPrinterCanPrint(true);
		}

		static public bool IsPrinterCanPrint(bool bJob)
		{
			bool bCanPrint = true;

			JetStatusEnum status = CoreInterface.GetBoardStatus();
			switch(status)
			{
				case JetStatusEnum.PowerOff:
				case JetStatusEnum.Initializing:
				case JetStatusEnum.Aborting:
                    bCanPrint = false;
                    break;				
                case JetStatusEnum.Cleaning:
				case JetStatusEnum.Moving:
                    //Tony : add for If Printer is open, then Printer Status: Printering, Cleaning,Printing
                    //But when Cleaning, add job will report error
                    if (CoreInterface.Printer_IsOpen() == 1)
                        bCanPrint = true;
                    else
					    bCanPrint = false;
					break;
					//				case JetStatusEnum..Offline:
					//					if(bJob)
					//						bCanPrint = false;
					//					break;
			}
			return bCanPrint;
		}
	}




}
