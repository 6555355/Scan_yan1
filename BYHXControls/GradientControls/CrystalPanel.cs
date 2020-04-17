#region gnu_license

/*
    Crystal Controls - C# control library containing the following tools:
        CrystalControl - base class
        CrystalGradientControl - a control that can either have a gradient background or be totally transparent.
        CrystalLabel - a homegrown label that can have a gradient or transparent background.
        CrystalPanel - a panel that can have a gradient or transparent background.
        CrystalTrackBar - a homegrown trackbar that can have a gradient or transparent background.
        CrystalToolStripTrackBar - a host for CrystalTrackBar that allows it to work in a ToolStrip.
        
        CrystalImageGridView - a control that hosts thumbnail images in a virtual grid.
        CrystalImageGridModel - a data model that holds a collection of CrystalImageItems
                                to feed to CrystalImageGridView.
        CrystalImageItem - a class that describes an Image file.
        CrystalThumbnailer - provides thumbnailing methods for images.

        CrystalCollector - a base class for a controller that links 
                            CrystalImageGridView to the CrystalImageGridModel.
        CrystalFileCollector - a controller that works on disk-based Image files.
        CrystalDesignCollector - a controller that works in Visual Studio toolbox designer.
        CrystalMemoryCollector - a controller that can be used to add images from memory.
        CrystalMemoryZipCollector - a controller that accesses images in zip files by streaming them into memory.
        CrystalZipCollector - a controller that accesses images in zip files by unpacking them.
        CrystalRarCollector - a controller that accesses images in rar files by unpacking them.

        CrystalPictureBox - a picture box control, derived from CrystalGradientControl.
        CrystalPictureShow - a control for viewing images and processing slideshows.
        CrystalComicShow - a control for viewing comic-book images in the CDisplay format.
 
    Copyright (C) 2006, 2008 Richard Guion
    Attilan Software Factory: http://www.attilan.com
    Contact: richard@attilan.com

   Version 1.0.0
        This is a work in progress: USE AT YOUR OWN RISK!  Interfaces/Methods may change!
 
    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
 */

#endregion

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BYHXPrinterManager.GradientControls
{
    /// <summary>
    /// CrystalPanel is a Panel replacement that can have a gradient or
    /// transparent background.
    /// </summary>
    [DesignerCategory("code")]
    [
        ToolboxItem(true),
        ToolboxBitmap(typeof (Panel))
    ]
    public class CrystalPanel : CrystalGradientControl
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public CrystalPanel()
        {
            // Set default values for our control's properties
            this.borderSide = System.Windows.Forms.Border3DSide.All;
            this.border3DStyle = System.Windows.Forms.Border3DStyle.Etched;

            // Set the base control BorderStyle to None, as there's no need for multiple borders
#if LIYUUSB
            base.BorderStyle = System.Windows.Forms.BorderStyle.None;
#endif
            SetDefaultSize();

            //Double buffer the control
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.DoubleBuffer, true);
        }

        /// <summary>
        /// Sets the default size.
        /// </summary>
        protected void SetDefaultSize()
        {
            Size = new Size(100, 100);
        }


		private bool m_Divider = false;

		/// <summary>
		/// Specifies the sides of the panel to apply a three-dimensional border to.
		/// </summary>
		[Bindable(true), Category("Border Options"), DefaultValue(System.Windows.Forms.Border3DSide.All),
		Description("控制渐变面板是否在其顶部绘制三维线")]
		public bool Divider
		{
			get{return m_Divider;}
			set
			{
				m_Divider = value;
				this.Invalidate();
			}
		}

        private System.Windows.Forms.Border3DSide borderSide;
        private System.Windows.Forms.Border3DStyle border3DStyle;
        /// <summary>
        /// Specifies the sides of the panel to apply a three-dimensional border to.
        /// </summary>
        [Bindable(true), Category("Border Options"), DefaultValue(System.Windows.Forms.Border3DSide.All),
        Description("Specifies the sides of the panel to apply a three-dimensional border to.")]
        public System.Windows.Forms.Border3DSide DividerSide
        {
            get { return this.borderSide; }
            set
            {
                if (this.borderSide != value)
                {
                    this.borderSide = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Specifies the style of the three-dimensional border.
        /// </summary>
        [Bindable(true), Category("Border Options"), DefaultValue(System.Windows.Forms.Border3DStyle.Etched),
        Description("Specifies the style of the three-dimensional border.")]
        public System.Windows.Forms.Border3DStyle Divider3DStyle
        {
            get { return this.border3DStyle; }
            set
            {
                if (this.border3DStyle != value)
                {
                    this.border3DStyle = value;
                    this.Invalidate();
                }
            }
        }
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			// allow normal control painting to occur first
			base.OnPaint(e);

			// add our custom border
			if(m_Divider)
			{
                switch (borderSide)
                { 
                    case Border3DSide.Top:
                        e.Graphics.DrawLine(SystemPens.ControlDark, this.ClientRectangle.Location, new Point(this.Width, 0));
                        e.Graphics.DrawLine(SystemPens.ControlLightLight, new Point(0, (int)SystemPens.ControlLightLight.Width), new Point(this.Width, (int)SystemPens.ControlLightLight.Width));
                        break;
                    case Border3DSide.Left:
                        e.Graphics.DrawLine(SystemPens.ControlDark, this.ClientRectangle.Location, new Point(0, this.Height));
                        e.Graphics.DrawLine(SystemPens.ControlLightLight, new Point((int)SystemPens.ControlLightLight.Width, 0), new Point((int)SystemPens.ControlLightLight.Width, this.Height));
                        break;
                    case Border3DSide.Right:
                        e.Graphics.DrawLine(SystemPens.ControlDark, new Point(this.Width - (int)SystemPens.ControlDark.Width, 0), new Point(this.Width - (int)SystemPens.ControlDark.Width, this.Height));
                        e.Graphics.DrawLine(SystemPens.ControlLightLight, new Point(this.Width - (int)SystemPens.ControlLightLight.Width - (int)SystemPens.ControlDark.Width, 0), new Point(this.Width - (int)SystemPens.ControlLightLight.Width - (int)SystemPens.ControlDark.Width, this.Height));
                        break;
                    case Border3DSide.Bottom:
                        e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, this.Height-(int)SystemPens.ControlDark.Width), new Point(this.Width, this.Height-(int)SystemPens.ControlDark.Width));
                        e.Graphics.DrawLine(SystemPens.ControlLightLight, new Point(0, this.Height -(int)SystemPens.ControlLightLight.Width-(int)SystemPens.ControlDark.Width), new Point(this.Width, this.Height - (int)SystemPens.ControlLightLight.Width-(int)SystemPens.ControlDark.Width));
                        break;
                    case Border3DSide.Middle:
                        break;
                    case Border3DSide.All:
                        e.Graphics.DrawLine(SystemPens.ControlDark, this.ClientRectangle.Location, new Point(this.Width, 0));
                        e.Graphics.DrawLine(SystemPens.ControlLightLight, new Point(0, (int)SystemPens.ControlLightLight.Width), new Point(this.Width, (int)SystemPens.ControlLightLight.Width));
                        e.Graphics.DrawLine(SystemPens.ControlDark, this.ClientRectangle.Location, new Point(0, this.Height));
                        e.Graphics.DrawLine(SystemPens.ControlLightLight, new Point((int)SystemPens.ControlLightLight.Width, 0), new Point((int)SystemPens.ControlLightLight.Width, this.Height));
                        e.Graphics.DrawLine(SystemPens.ControlDark, new Point(this.Width - (int)SystemPens.ControlDark.Width, 0), new Point(this.Width - (int)SystemPens.ControlDark.Width, this.Height));
                        e.Graphics.DrawLine(SystemPens.ControlLightLight, new Point(this.Width - (int)SystemPens.ControlLightLight.Width - (int)SystemPens.ControlDark.Width, 0), new Point(this.Width - (int)SystemPens.ControlLightLight.Width - (int)SystemPens.ControlDark.Width, this.Height));
                        e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, this.Height-(int)SystemPens.ControlDark.Width), new Point(this.Width, this.Height-(int)SystemPens.ControlDark.Width));
                        e.Graphics.DrawLine(SystemPens.ControlLightLight, new Point(0, this.Height -(int)SystemPens.ControlLightLight.Width-(int)SystemPens.ControlDark.Width), new Point(this.Width, this.Height - (int)SystemPens.ControlLightLight.Width-(int)SystemPens.ControlDark.Width));
                        break;
                    default:
                        break;
                }
                //                e.Graphics.DrawLine(SystemPens.ControlDark,this.ClientRectangle.Location,new Point(this.Width,0));
                //e.Graphics.DrawLine(SystemPens.ControlLightLight,new Point(0,(int)SystemPens.ControlLightLight.Width),new Point(this.Width,(int)SystemPens.ControlLightLight.Width));
                //System.Windows.Forms.ControlPaint.DrawBorder3D(
                //    e.Graphics,
                //    this.ClientRectangle,
                //    this.border3DStyle,
                //    this.borderSide);
			}		
		}

		protected override bool IsInputKey(Keys keyData)
		{
			if(keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
				return true;
			else
				return base.IsInputKey (keyData);
		}

    }
}