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
using System.Windows.Forms;

namespace BYHXPrinterManager.GradientControls
{
    /// <summary>
    /// CrystalLabel is a Label replacement that can have a gradient or
    /// transparent background.
    /// </summary>
    [DesignerCategory("code")]
    [
        ToolboxItem(true),  // Decided to take this out of the toolbox: drawing problems.
        ToolboxBitmap(typeof (Label))
    ]
    public class CrystalLabel : CrystalGradientControl
    {
        /// <summary>
        /// Default constructor for the CrystalLabel control.
        /// </summary>
        public CrystalLabel()
        {
            Size = new Size(70, 13);

            //Double buffer the control
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.DoubleBuffer, true);
        }

        /// <summary>
        /// Sets the default size for the control, used for display in the Forms designer.
        /// </summary>
        protected virtual void SetDefaultSize()
        {
            // Set default size so that user will see the control in the designer
            Size = new Size(70, 13);
        }

        /// <summary>
        /// Event handler that responds to a change in the text for the control.
        /// </summary>
        /// <param name="e">Event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
			Graphics g = this.CreateGraphics();
//            Size = g.MeasureString(Text, Font).ToSize();
            RedrawControl();
			g.Dispose();
            base.OnTextChanged(e);
        }

        /// <summary>
        /// Event handler that responds to a change in the font for the control.
        /// </summary>
        /// <param name="e">Event data.</param>
        protected override void OnFontChanged(EventArgs e)
        {
			Graphics g = this.CreateGraphics();
//            Size = g.MeasureString(Text, Font).ToSize();
            RedrawControl();
			g.Dispose();
            base.OnFontChanged(e);
        }

        /// <summary>
        /// Draws the text for the label.
        /// </summary>
        /// <param name="pe">Provides data for the OnPaint event.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            // Paint text
            Rectangle rc = new Rectangle(0, 0, Width, Height);
			StringFormat strf = new StringFormat(StringFormat.GenericDefault);
			strf.Alignment = this.TextAlignment;
			strf.LineAlignment = this.TextAlignment;
			SolidBrush brush = new SolidBrush(this.ForeColor);
            pe.Graphics.DrawString(Text, Font,brush,rc,strf);
			brush.Dispose();
            // Calling the base class OnPaint
            base.OnPaint(pe);
        }
		private StringAlignment m_TextAlignment = StringAlignment.Center;
		public StringAlignment TextAlignment
		{
			get{ return m_TextAlignment;}
			set{m_TextAlignment = value;}
		}
    }
}