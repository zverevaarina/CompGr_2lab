using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace L2
{
    class DrawFigures
    {
        public static void DrawEllipseBr(Bitmap bmp, Color color, int x, int y, int a, int b)
        {
            //из (_x,_y) начинаем рисовать
            int _x = 0;
            int _y = b;

            int a_sqr = a * a;
            int b_sqr = b * b;
            //d для первой области
            int d = 4 * b_sqr * ((_x + 1) * (_x + 1)) + a_sqr * ((2 * _y - 1) * (2 * _y - 1)) - 4 * a_sqr * b_sqr;

            while (a_sqr * (2 * _y - 1) > 2 * b_sqr * (_x + 1))
            {
                SetPixels(bmp, x, y, _x, _y, color);
                if (d < 0)
                {
                    //двигаемся в точку (x+1,y)
                    _x++;
                    d += 4 * b_sqr * (2 * _x + 3);
                }
                else
                {
                    //двигаемся в точку (x+1,y-1)
                    _x++; _y--;
                    d = d - 8 * a_sqr * (_y - 1) + 4 * b_sqr * (2 * _x + 3);
                }
            }

            //d для второй области
            d = b_sqr * ((2 * _x + 1) * (2 * _x + 1)) + 4 * a_sqr * ((_y + 1) * (_y + 1)) - 4 * a_sqr * b_sqr;

            while (_y + 1 != 0)
            {
                SetPixels(bmp, x, y, _x, _y, color);
                if (d < 0)
                {
                    //двигаемся в точку (x,y-1)
                    _y--;
                    d += 4 * a_sqr * (2 * _y + 3);
                }
                else
                {
                    //двигаемся в точку (x+1,y-1)
                    _y--; _x++;
                    d = d - 8 * b_sqr * (_x + 1) + 4 * a_sqr * (2 * _y + 3);
                }
            }
        }

        public static void DrawEllipseWu(Bitmap bmp, Color color, int x, int y, int a, int b)
        {
            int _x = 0;
            int _y = b;

            int a_sqr = a * a;
            int b_sqr = b * b;

            int d = 4 * b_sqr * ((_x + 1) * (_x + 1)) + a_sqr * ((2 * _y - 1) * (2 * _y - 1)) - 4 * a_sqr * b_sqr;

            while (a_sqr * (2 * _y - 1) > 2 * b_sqr * (_x + 1))
            {
                SetPixels(bmp, x, y, _x, _y, color);
                if (d < 0)
                {
                    _x++;
                    d += 4 * b_sqr * (2 * _x + 3);
                }
                else
                {
                    _x++;
                    SetPixels(bmp, x, y, _x, _y, Color.Gray);
                    _y--;
                    SetPixels(bmp, x, y, _x - 1, _y, Color.Gray);
                    d = d - 8 * a_sqr * (_y - 1) + 4 * b_sqr * (2 * _x + 3);
                }
            }

            d = b_sqr * ((2 * _x + 1) * (2 * _x + 1)) + 4 * a_sqr * ((_y + 1) * (_y + 1)) - 4 * a_sqr * b_sqr;

            while (_y + 1 != 0)
            {
                SetPixels(bmp, x, y, _x, _y, color);
                if (d < 0)
                {
                    _y--;
                    d += 4 * a_sqr * (2 * _y + 3);
                }
                else
                {
                    _y--;
                    SetPixels(bmp, x, y, _x, _y, Color.Gray);
                    _x++;
                    SetPixels(bmp, x, y, _x, _y + 1, Color.Gray);
                    d = d - 8 * b_sqr * (_x + 1) + 4 * a_sqr * (2 * _y + 3);
                }
            }
        }

        public static void SetPixels(Bitmap bmp, int x, int y, int _x, int _y, Color color)
        {
            bmp.SetPixel(x + _x, y - _y, color);
            bmp.SetPixel(x - _x, y - _y, color);
            bmp.SetPixel(x - _x, y + _y, color);
            bmp.SetPixel(x + _x, y + _y, color);
        }

        public static void DrawArc(Bitmap bmp, int x, int y, int r, int sAngle, int fAngle)
        {
            int sAngleCopy, fAngleCopy, _x, _y, max, d;
            if(fAngle>360)
            {
                fAngle = fAngle - 360;
                int t = sAngle; sAngle = fAngle; fAngle = t;
            }
            //1
            if (sAngle <= 90)
            {
                sAngleCopy = sAngle;

                if (fAngle > 90) fAngleCopy = 90;
                else fAngleCopy = fAngle;

                _x = Math.Abs((int)(r * Math.Cos((fAngleCopy * (Math.PI / 180)))));
                _y = Math.Abs((int)(r * Math.Sin((fAngleCopy * (Math.PI / 180)))));
                max = Math.Abs((int)(r * Math.Sin((sAngleCopy * (Math.PI / 180)))));
                d = 1 - r;
                DrawAll(bmp, x, y, r, sAngle, fAngle, 1, max, _x, _y, d);
            }
            //2
            if ((sAngle > 90 && fAngle <= 180) || (sAngle <= 90 && fAngle > 90) || (sAngle > 90 && sAngle < 180 && fAngle >= 180))
            {
                if (fAngle > 180) fAngleCopy = 180;
                else fAngleCopy = fAngle;

                if (sAngle < 90) sAngleCopy = 90;
                else sAngleCopy = sAngle;

                _x = Math.Abs((int)(r * Math.Cos((sAngleCopy * (Math.PI / 180)))));
                _y = Math.Abs((int)(r * Math.Sin((sAngleCopy * (Math.PI / 180)))));
                d = 1 - r;
                max = Math.Abs((int)(r * Math.Sin((fAngleCopy * (Math.PI / 180)))));
                DrawAll(bmp, x, y, r, sAngle, fAngle, 2, max, _x, _y, d);
            }
            //3
            if ((sAngle > 180 && fAngle <= 270) || (sAngle <= 180 && fAngle > 180) || (sAngle > 180 && sAngle < 270 && fAngle >= 270))
            {
                if (fAngle > 270) fAngleCopy = 270;
                else fAngleCopy = fAngle;

                if (sAngle < 180) sAngleCopy = 180;
                else sAngleCopy = sAngle;

                _x = Math.Abs((int)(r * Math.Cos((fAngleCopy * (Math.PI / 180)))));
                _y = Math.Abs((int)(r * Math.Sin((fAngleCopy * (Math.PI / 180)))));
                d = 1 - r;
                max = Math.Abs((int)(r * Math.Sin((sAngleCopy * (Math.PI / 180)))));
                DrawAll(bmp, x, y, r, sAngle, fAngle, 3, max, _x, _y, d);
            }
            //4
            if ((sAngle > 270 && fAngle <= 360) || (sAngle <= 270 && fAngle > 270) || (sAngle > 270))
            {
                if (fAngle > 360) fAngleCopy = 360;
                else fAngleCopy = fAngle;

                if (sAngle < 270) sAngleCopy = 270;
                else sAngleCopy = sAngle;

                _x = Math.Abs((int)(r * Math.Cos((sAngleCopy * (Math.PI / 180)))));
                _y = Math.Abs((int)(r * Math.Sin((sAngleCopy * (Math.PI / 180)))));
                d = 1 - r;
                max = Math.Abs((int)(r * Math.Sin((fAngleCopy * (Math.PI / 180)))));
                DrawAll(bmp, x, y, r, sAngle, fAngle, 4, max, _x, _y, d);
            }

            //для достроения до сектора
            //кординаты начала сектора
            int xs= Math.Abs((int)(r * Math.Cos((sAngle * (Math.PI / 180)))));
            int ys= Math.Abs((int)(r * Math.Sin((sAngle * (Math.PI / 180)))));
            if (sAngle < 90) DrawLine(bmp, Color.Black, x, y, x + xs, y - ys);
            else if (sAngle >= 90 && sAngle < 180) DrawLine(bmp, Color.Black, x, y, x - xs, y - ys);
            else if (sAngle >= 180 && sAngle < 270) DrawLine(bmp, Color.Black, x, y, x - xs, y + ys);
            else if (sAngle >= 270) DrawLine(bmp, Color.Black, x, y, x + xs, y + ys);

            //координаты конца сектора
            int xf = Math.Abs((int)(r * Math.Cos((fAngle * (Math.PI / 180)))));
            int yf = Math.Abs((int)(r * Math.Sin((fAngle * (Math.PI / 180)))));
            if (fAngle < 90) DrawLine(bmp, Color.Black, x, y, x + xf, y - yf);
            else if (fAngle >= 90 && fAngle < 180) DrawLine(bmp, Color.Black, x, y, x - xf, y - yf);
            else if (fAngle >= 180 && fAngle < 270) DrawLine(bmp, Color.Black, x, y, x - xf, y + yf);
            else if (fAngle >= 270) DrawLine(bmp, Color.Black, x, y, x + xf, y + yf);
        }

        public static void DrawAll(Bitmap bmp, int x, int y, int r, int sAngle, int fAngle, int k, int max, int _x, int _y, int d)
        {
            while (_y >= max)
            {
                switch(k)
                {
                    case 1: bmp.SetPixel(x + _x, y - _y, Color.Black); break;
                    case 2: bmp.SetPixel(x - _x, y - _y, Color.Black); break;
                    case 3: bmp.SetPixel(x - _x, y + _y, Color.Black); break;
                    case 4: bmp.SetPixel(x + _x, y + _y, Color.Black); break;
                }

                int e = 2 * (d + _y) - 1;
                if (d < 0 && e <= 0)
                {
                    _x++;
                    d += 2 * _x + 1;
                    continue;
                }
                if (d > 0 && e > 0)
                {
                    _y--;
                    d -= 2 * _y + 1;
                    continue;
                }
                _x++;
                d += 2 * (_x - _y);
                _y--;
            }
        }
        
        public static void DrawLine(Bitmap bmp, Color color, int x1, int y1, int x2, int y2)
        {
            if (x1 > x2)
            {
                int tmp = x1; x1 = x2; x2 = tmp;
                tmp = y1; y1 = y2; y2 = tmp;
            }

            double L = Math.Max(Math.Abs(x2 - x1), Math.Abs(y2 - y1));
            double dy = (y2 - y1) / L;
            double dx = (x2 - x1) / L;
            double x = x1, y = y1;
            for (int i = 0; i <= L; i++)
            {
                x += dx;
                y += dy;
                bmp.SetPixel((int)x, (int)y, color);
            }
        }
    }
}