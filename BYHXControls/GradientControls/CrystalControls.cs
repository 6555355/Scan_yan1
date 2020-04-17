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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

/// <summary>
/// This internal class helps us locate the resource for ToolboxBitmap items.
/// http://www.bobpowell.net/toolboxbitmap.htm
/// </summary>
internal class resfinder
{
}

namespace BYHXPrinterManager.GradientControls
{
    /// <summary>
    /// Base class for all Crystal controls, derives from ScrollableControl.
    /// </summary>
    [DesignerCategory("code")]
    [ToolboxItem(false)]
    public class CrystalControl : UserControl//ScrollableControl
    {
        #region Gui

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Container components = null;

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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

        #endregion
    }

    /// <summary>
    /// A base control class that provides either a gradient or transparent background.
    /// A gradient background may make your application more colorful.
    /// A transparent background is useful if your Form's background has a bitmap image,
    /// and also if you are hosting the control within a toolstrip (see CrystalToolStripTrackBar).
    /// </summary>
    [DesignerCategory("code")]
    [ToolboxItem(false)]
    public class CrystalGradientControl : CrystalControl
    {
        private const int TransparentBit = 0x00000020; //WS_EX_TRANSPARENT 
		private Style _GradientColors = new Style();
		private bool _bThreeColor = false;
        private LinearGradientMode _GradientMode = LinearGradientMode.Vertical;
        private bool _transparentMode = false;

        #region Fields

        /// <summary>
        /// The first color of the gradient.  If TransparencyMode is true, the gradient will be ignored.
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            DefaultValue(1),
            Description(
                "the colors for the gradient effect. If TransparencyMode is true, the gradient will be ignored."
                )
        ]
		public Style GradientColors
		{
			get { return _GradientColors; }
			set
			{
				if (value != _GradientColors)
				{
					_GradientColors = value;
					InvalidateEx();
				}
			}
		}

		/// <summary>
		/// The second color of the gradient.  If TransparencyMode is true, the gradient will be ignored.
		/// </summary>
		[
		Browsable(true),
		Category("Appearance"),
		DefaultValue(false),
		Description(
			"Indicates the third color for the gradient effect. If TransparencyMode is true, the gradient will be ignored."
			)
		]
		public bool TreeColorGradient
		{
			get { return _bThreeColor; }
			set
			{
				_bThreeColor = value;
				InvalidateEx();
			}
		}

        /// <summary>
        /// The angle of the gradient.
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            DefaultValue(180f),
            Description("Indicates the angle of the color gradient.")
        ]
        public LinearGradientMode GradientMode
        {
            get { return _GradientMode; }
            set
            {
                if (value != _GradientMode)
                {
                    _GradientMode = value;
                    InvalidateEx();
                }
            }
        }

        /// <summary>
        /// Background color is ignored, except when set to Transparent, which sets
        /// TransparentMode to true.
        /// </summary>
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                if (base.BackColor != value)
                {
                    base.BackColor = value;
                    TransparentMode = (base.BackColor == Color.Transparent);
                    InvalidateEx();
                }
            }
        }

        /// <summary>
        /// Determines whether the control will be in transparent or non-transparent mode.
        /// When set to true, Color1 and Color2 will be ignored.
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            DefaultValue(false),
            Description(
                "Indicates that the background of this control should be totally transparent to allow the background of the parent window."
                )
        ]
        public virtual bool TransparentMode
        {
            get { return _transparentMode; }
            set
            {
                if (value != _transparentMode)
                {
                    _transparentMode = value;
                    RecreateHandle();
                }
            }
        }

        #endregion

        #region Constructors, Destructors

        /// <summary>
        /// Constructor for CrystalGradientControl
        /// </summary>
        public CrystalGradientControl()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Returns the Creation bits for the controls.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (TransparentMode)
                    cp.ExStyle |= TransparentBit; // add WS_EX_TRANSPARENT 
                else
                    cp.ExStyle &= TransparentBit; // subtract WS_EX_TRANSPARENT 
                return cp;
            }
        }

        /// <summary>
        /// Paints a graident background using Color1 and Color2.
        /// </summary>
        /// <param name="gfx">The graphics object.</param>
        protected void PaintGradientBackground(Graphics gfx)
        {
            if ((Width < 1) || (Height < 1))
                return;

            // Creating the rectangle for the gradient
            Rectangle rBackground = new Rectangle(0, 0, Width, Height);

//            // Creating the lineargradient
//            LinearGradientBrush bBackground
//                = new LinearGradientBrush(rBackground, _Color1, _Color2, _ColorAngle);
//
//            // Draw the gradient onto the form
//            gfx.FillRectangle(bBackground, rBackground);
//
//            // Disposing of the resources held by the brush
//            bBackground.Dispose();
            if (_GradientColors != null)
            {
                if (_bThreeColor)
                    CrystalDrawing.LinearFillRoundRect(gfx, rBackground, _GradientColors.Color1, _GradientColors.Color2, _GradientColors.Color1, 1, _GradientMode);
                else
                    CrystalDrawing.LinearFillRoundRect(gfx, rBackground, _GradientColors.Color1, _GradientColors.Color2, 1, _GradientMode);
            }

			//Draw BackImage if there is one----
			if(this.BackgroundImage!=null)
			{
				gfx.DrawImage(this.BackgroundImage, rBackground);
			}
        }

        /// <summary>
        /// Paints the background for the control.  If the TransparencyMode is true,
        /// no background is painted.
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (TransparentMode)
            {
                base.OnPaintBackground(pevent);
                return;
            }

            PaintGradientBackground(pevent.Graphics);
        }

        #endregion

        /// <summary>
        /// Invalidates the control in transparency mode by calling the parent to invalidate the given rect.
        /// If Transparency mode is false, the control's rect is invalidated.
        /// </summary>
        /// <param name="rc">The rectangle to invalidate.</param>
        protected virtual void InvalidateEx(Rectangle rc)
        {
            if (TransparentMode)
            {
                if (Parent == null)
                {
//                    CrystalLogger.LogDebug("CrystalGradientControl: InvalidateEx: Parent is NULL");
                    return;
                }

                Parent.Invalidate(rc, true);

                // THIS IS CRITICAL
                // If you don't update the parent here, then the rect may
                // get invalidated after you have repainted the control.
                Parent.Update();
            }
            else
                Invalidate(rc);
        }

        /// <summary>
        /// Invalidates the control in transparency mode by calling the parent to invalidate the given rect.
        /// If Transparency mode is false, the control's rect is invalidated.
        /// </summary>
        /// <param name="rc">The rectangle to invalidate.</param>
        protected virtual void InvalidateEx(RectangleF rc)
        {
            Rectangle newRect = new Rectangle();
            newRect.X = Convert.ToInt32(rc.X);
            newRect.Y = Convert.ToInt32(rc.Y);
            newRect.Width = Convert.ToInt32(rc.Width);
            newRect.Height = Convert.ToInt32(rc.Height);

            InvalidateEx(newRect);
        }

        /// <summary>
        /// Invalidates the entire rectangle for this control.
        /// </summary>
        protected virtual void InvalidateEx()
        {
            if (TransparentMode)
            {
                Rectangle rc = new Rectangle(Location, Size);
                InvalidateEx(rc);
            }
            else
                Invalidate();
        }

        /// <summary>
        /// Forces OnPaint to be called for this control.
        /// </summary>
        protected virtual void RedrawControl()
        {
            Invalidate();
            Update();
        }
    }
}