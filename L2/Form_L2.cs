using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L2
{
    public partial class Form_L2 : Form
    {
        public Form_L2()
        {
            InitializeComponent();
        }

        public void Draw(Graphics g, Rectangle r)
        {
            Bitmap bmp = new Bitmap(r.Width, r.Height);
            DrawF(bmp, g);
            g.DrawImage(bmp, r);
            bmp.Dispose();
        }

        public void DrawF(Bitmap bmp, Graphics g)
        {
            DrawFigures.DrawEllipseBr(bmp, Color.Black, 110, 70, 100, 60);
            DrawFigures.DrawEllipseWu(bmp, Color.Black, 400, 70, 100, 60);
            DrawFigures.DrawArc(bmp, 110, 240, 100, 26, 237);

            //DrawFigures.DrawLine(bmp, Color.Gray, 110, 340, 110, 140); //координатные оси для сектора
            //DrawFigures.DrawLine(bmp, Color.Gray, 10, 240, 210, 240);
        }
        
        private void Form_L2_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics, e.ClipRectangle);
        }

        private void Form_L2_Load(object sender, EventArgs e)
        {

        }

    }
}
