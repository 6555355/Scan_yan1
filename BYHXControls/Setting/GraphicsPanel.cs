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
    public partial class GraphicsPanel : UserControl
    {
        public GraphicsPanel()
        {
            InitializeComponent();
        }

        private void GraphicsPanel_Load(object sender, EventArgs e)
        {
        }

        private void DrawRectXY(int x,int y)
        {
            Graphics g = this.CreateGraphics();//
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.DarkGray, 3);
            g.DrawRectangle(p, nStartX, nStartY, nStartX + nWith, nStartY + nHeight);//实际位置
            p.DashStyle = DashStyle.Dot;//虚线
            g.DrawRectangle(p, nStartX + x, nStartY + y, nStartX + nWith + x, nStartY + nHeight+y);//参照位置
            p = new Pen(Color.Green, 1);
            p.DashStyle = DashStyle.Solid;//箭头
            p.EndCap = LineCap.ArrowAnchor;
            g.DrawLine(p, nStartX + nWith + 60, nStartY, nStartX + nWith + 60, nStartY + nHeight);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddString("x", this.Font.FontFamily, (int)FontStyle.Bold, 10, new RectangleF(nStartX + nWith + 80, nStartY, 30, 30), null);
            g.DrawPath(Pens.Black, gp);
            g.DrawLine(p, nStartX, nStartY + nHeight + 60, nStartX + nWith, nStartY + nHeight + 60);
            gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddString("y", this.Font.FontFamily, (int)FontStyle.Bold, 10, new RectangleF(nStartX, nStartY + nHeight + 60, 30, 30), null);
            g.DrawPath(Pens.Black, gp);
            p.Dispose();
            g.Dispose();
        }

        private const int nStartX = 20;
        private const int nStartY = 20;
        private const int nWith = 100;
        private const int nHeight = 40;
        private void GraphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawRectXY(10, 10);
        }
    }
}
