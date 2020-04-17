using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class GraphicsPanel2 : UserControl
    {
        public GraphicsPanel2()
        {
            InitializeComponent();
        }

        private void GraphicsPanel2_Paint(object sender, PaintEventArgs e)
        {
            DrawRectXY(30, 30);
        }

        private void DrawRectXY(int y1, int y2)
        {
            Graphics g = this.CreateGraphics();//
            Pen p = new Pen(Color.DarkGray, 2);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawLine(p, nStartX1, nStartY1, nStartX2, nStartY2 + y2);//参照位置
            g.DrawLine(p, nStartX1, nStartY1 + y1, nStartX2, nStartY2);
            p = new Pen(Color.Green, 1);
            p.DashStyle = DashStyle.Solid;//箭头
            p.EndCap = LineCap.ArrowAnchor;
            g.DrawLine(p, nStartX1 , nStartY1, nStartX1 , nStartY1 + y1);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath(); 
            gp.AddString("left", this.Font.FontFamily, (int)FontStyle.Bold, 10, new RectangleF(nStartX1-30,nStartY1+y1/2, 30, 30), null);
            g.DrawPath(Pens.Black, gp);
            g.DrawLine(p, nStartX2 , nStartY2 , nStartX2 , nStartY2 +y2);
            gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddString("right", this.Font.FontFamily, (int)FontStyle.Bold, 10, new RectangleF(nStartX2 + 10, nStartY2 + y2 / 2, 30, 30), null);
            g.DrawPath(Pens.Black, gp);
            p.Dispose();
            g.Dispose();
        }

        private const int nStartX1 = 50;
        private const int nStartY1 = 20;
        private const int nStartX2 = 250;
        private const int nStartY2 = 20;
    }
}
