/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace DividerPanel
{
	/// <summary>
	/// A Panel control with selectable border appearance
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(DividerPanel))]
	[DesignerAttribute(typeof(DividerPanelDesigner))]
	public class DividerPanel : System.Windows.Forms.Panel
	{
		// This system of private properties with public accessors is a
		// best practice coding style.
		// Note that our private properties are in hungarianNotation -
		// the first letter is lower case, and additional word boundaries are capitalized.

		private System.Windows.Forms.Border3DSide borderSide;
		private System.Windows.Forms.Border3DStyle border3DStyle;

		// This is the Constructor for our class. Any private variables we have defined
		// should have their initial values set here. It is good practice to always
		// initialize every variable to a specific value, rather then leaving it as
		// an inferred value. For example, all bool's should be set to false rather than
		// leaving them as an inferred false, and any objects referenced
		// that are initially null should be explicitly set to null. (eg. myObject = null)
		// By doing this you will certainly increase the understandability of your
		// code when you or someone else has to read it later.

		public DividerPanel()
		{
			// Set default values for our control's properties
			this.borderSide = System.Windows.Forms.Border3DSide.All;
			this.border3DStyle = System.Windows.Forms.Border3DStyle.Etched;

			// Set the base control BorderStyle to None, as there's no need for multiple borders
			base.BorderStyle = System.Windows.Forms.BorderStyle.None;
		}

		// Next we have our public accessors, Note that for public accessors
		// we use CamelCasing - the first letter is capitalized and additional
		// word boundaries are also capitalized.

		/// <summary>
		/// Specifies the sides of the panel to apply a three-dimensional border to.
		/// </summary>
		[Bindable(true), Category("Border Options"), DefaultValue(System.Windows.Forms.Border3DSide.All),
		Description("Specifies the sides of the panel to apply a three-dimensional border to.")]
		public System.Windows.Forms.Border3DSide BorderSide
		{
			get { return this.borderSide; }
			set
			{
				if( this.borderSide != value )
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
		public System.Windows.Forms.Border3DStyle Border3DStyle
		{
			get { return this.border3DStyle; }
			set
			{
				if( this.border3DStyle != value )
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
			System.Windows.Forms.ControlPaint.DrawBorder3D (
				e.Graphics,
				this.ClientRectangle,
				this.border3DStyle,
				this.borderSide );
		}
	}
	public class DividerPanelDesigner : System.Windows.Forms.Design.ScrollableControlDesigner
	{
		public DividerPanelDesigner()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		protected override void PreFilterProperties(System.Collections.IDictionary properties)
		{
			properties.Remove("BorderStyle");
		}

	}

}

