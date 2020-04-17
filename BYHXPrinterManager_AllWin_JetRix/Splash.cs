/*
 * Created by SharpDevelop.
 * User: mjackson
 * Date: 25/10/2006
 * Time: 08:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace MainWindow
{
    /// <summary>
    /// Description of Splash.
    /// </summary>
    internal sealed class Splash : System.Windows.Forms.Form
    {

        #region initialisation

        public Splash(string imagePath): base()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //	Size to the image so as to display it fully and position the form in the center screen with no border.
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Size = this.BackgroundImage.Size;

            //	Force the splash to stay on top while the mainform renders but don't show it in the taskbar.
            this.TopMost = true;
            this.ShowInTaskbar = false;

            //	Make the backcolour Fuchia and set that to be transparent
            //	so that the image can be shown with funny shapes, round corners etc.
            //this.BackColor = System.Drawing.Color.Fuchsia;
            //this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.BackColor = SystemColors.Control;
            this.TransparencyKey = SystemColors.Control;

            //	Initialise a timer to do the fade out
            if (this.components == null)
            {
                this.components = new System.ComponentModel.Container();
            }
            this.fadeTimer = new System.Windows.Forms.Timer(this.components);

            this.BackgroundImage = new Bitmap(imagePath);
        }

        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Timer timer2;

        public System.Windows.Forms.Timer fadeTimer;

        #endregion

        #region Close Splash Methods

        protected override void OnClick(System.EventArgs e)
        {
            //	If we are displaying as a about dialog we need to provide a way out.
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            //	Close immediatly is the timer interval is set to 1 indicating no fade.
            if (this.fadeTimer.Interval == 1)
            {
                e.Cancel = false;
                return;
            }

            //	Only use the timer to fade out if we have a mainform running otherwise there will be no message pump
            if (Application.OpenForms.Count > 1)
            {
                if (this.Opacity > 0)
                {
                    e.Cancel = true;
                    this.Opacity -= 0.05;

                    //	use the timer to iteratively call the close method thereby keeping the GUI thread available for other processes.
                    this.fadeTimer.Tick -= new System.EventHandler(this.FadeoutTick);
                    this.fadeTimer.Tick += new System.EventHandler(this.FadeoutTick);
                    this.fadeTimer.Start();
                }
                else
                {
                    e.Cancel = false;
                    this.fadeTimer.Stop();

                    //	Clear the instance variable so we can reshow the splash, and ensure that we don't try to close it twice
                    //Instance = null;
                }
            }
            else
            {
                if (this.Opacity > 0)
                {
                    //	Sleep on this thread to slow down the fade as there is no message pump running
                    System.Threading.Thread.Sleep(this.fadeTimer.Interval);
                    this.Opacity -= 0.05;

                    //	iteratively call the close method
                    this.Close();
                }
                else
                {
                    e.Cancel = false;

                    //	Clear the instance variable so we can reshow the splash, and ensure that we don't try to close it twice
                    //Instance = null;
                }
            }

        }

        void FadeoutTick(object sender, System.EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Designer stuff

        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Splash
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.DoubleBuffered = true;
            this.Name = "Splash";
            this.Text = "Splash";
            this.Load += new System.EventHandler(this.Splash_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity >= 1)
            {
                this.timer1.Stop();
                this.timer2.Interval = 2000;
                this.timer2.Start();
                return;
            }
            this.Opacity += 0.05;
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            //this.timer1.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.timer2.Stop();
            this.Close();
        }

    }
    public static class Splasher
    {
        internal static Splash Instance = null;
        //internal static System.Threading.Thread splashThread = null;
        internal static System.Windows.Forms.Timer fadeTimer_1 = new System.Windows.Forms.Timer();
        public static void ShowSplash()
        {
            //	Show Splash with no fading
            ShowSplash(0);
        }

        public static void ShowSplash(int fadeinTime)
        {
            //	Only show if not showing already
            if (Instance == null)
            {
                string imagePath = Path.Combine(Application.StartupPath, "setup\\splash.png");
                Instance = new Splash(imagePath);

                //	Hide initially so as to avoid a nasty pre paint flicker
                Instance.Opacity = 0;
                //// Perform the fade in
                //if (fadeinTime > 0)
                //{
                //    //	Set the timer interval so that we fade out at the same speed.
                //    int fadeStep = (int)System.Math.Round((double)fadeinTime / 20);
                //    Instance.timer1.Interval = fadeStep;
                //}
                //else
                //{
                //    //	Set the timer interval so that we fade out instantly.
                //    Instance.fadeTimer.Interval = 1;
                //}

                Instance.Show();

                //	Process the initial paint events
                Application.DoEvents();

                // Perform the fade in
                if (fadeinTime > 0)
                {
                    //	Set the timer interval so that we fade out at the same speed.
                    int fadeStep = (int)System.Math.Round((double)fadeinTime / 20);
                    Instance.fadeTimer.Interval = fadeStep;

                    for (int i = 0; i <= fadeinTime; i += fadeStep)
                    {
                        System.Threading.Thread.Sleep(fadeStep);
                        Instance.Opacity += 0.05;
                    }
                }
                else
                {
                    //	Set the timer interval so that we fade out instantly.
                    Instance.fadeTimer.Interval = 1;
                }
                Instance.Opacity = 1;
            }
        }

        public static void Fadeout()
        {
            //	Only fadeout if we are currently visible.
            if (Instance != null)
            {
                Instance.Close();//Instance.BeginInvoke(new MethodInvoker(Instance.Close));
                //	Process the Close Message on the Splash Thread.
                Application.DoEvents();
            }
        }
    }
}
