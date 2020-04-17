// -----------------------------------------------------------------------
// <copyright file="MyBackgroundWorker.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows.Forms;

namespace WAF_OnePass.Domain.Utility
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows.Threading;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MyBackgroundWorker
    {
        public Thread m_Worker;

        public event DoWorkEventHandler DoWork;

        public void OnDoWork(object o)
        {
            DoWorkEventHandler handler = this.DoWork;
            if (handler != null)
            {
                handler(this, new DoWorkEventArgs(o));
            }
        }

        //
        // Summary:
        //     Occurs when System.ComponentModel.BackgroundWorker.ReportProgress(System.Int32)
        //     is called.
        public event ProgressChangedEventHandler ProgressChanged;

        public void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChangedEventHandler handler = this.ProgressChanged;
            if (handler != null)
            {
                _mainForm.Invoke(handler, new object[] { this, e });
            }
        }

        //
        // Summary:
        //     Occurs when the background operation has completed, has been canceled, or
        //     has raised an exception.
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        private Form _mainForm = null;
        public MyBackgroundWorker(Form mainForm)
        {
            _mainForm = mainForm;
        }

        private void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompletedEventHandler handler = this.RunWorkerCompleted;
            if (handler != null)
            {
                _mainForm.Invoke(handler,new object[]{this, e});
            }
        }

        public bool CancellationPending { get; set; }
        public bool IsBusy
        {
            get
            {
                if (m_Worker == null)
                    return false;
                return m_Worker.IsAlive;
            }
        }

        public void CancelAsync()
        {
            CancellationPending = true;
            if (m_Worker != null)
                m_Worker.Abort();
        }

        public void RunWorkerAsync()
        {
            this.RunWorkerAsync(null);
        }

        public void RunWorkerAsync(object argument)
        {
            Exception error = null;
            DoWorkEventArgs args = new DoWorkEventArgs(argument);
            try
            {
                if (this.IsBusy) throw new InvalidOperationException("Thread is busy!");
                ParameterizedThreadStart threadStart = new ParameterizedThreadStart(OnDoWork);
                m_Worker = new Thread(threadStart);
                m_Worker.CurrentCulture = Thread.CurrentThread.CurrentCulture;
                m_Worker.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                m_Worker.IsBackground = true;
                m_Worker.Priority = ThreadPriority.AboveNormal;
                m_Worker.Start(argument);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(args.Result, error, args.Cancel));
            }
        }

        public void ReportProgress(int percentProgress, object userState)
        {
            OnProgressChanged(new ProgressChangedEventArgs(percentProgress, userState));
        }

    }

}
