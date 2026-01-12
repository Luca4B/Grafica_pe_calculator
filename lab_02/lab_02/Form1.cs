using System.Drawing;
using System.Windows.Forms;

namespace lab_02
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            DrawCircleExplicit(150, 150, 80, Color.Red);
            DrawCirclePolar(450, 150, 80, Color.Blue);
            DrawCircleMidpoint(150, 380, 80, Color.Green);
            DrawCircleBresenham(450, 380, 80, Color.Purple);

            pictureBox1.Image = bitmap;
        }

        void SetSafePixel(int x, int y, Color c)
        {
            if (x < 0) return;
            if (y < 0) return;
            if (x >= bitmap.Width) return;
            if (y >= bitmap.Height) return;
            bitmap.SetPixel(x, y, c);
        }

        void DrawCircleExplicit(int x0, int y0, int r, Color c)
        {
            for (int x = x0 - r; x <= x0 + r; x++)
            {
                double dx = x - x0;
                double inside = r * r - dx * dx;
                if (inside < 0) continue;

                int y1 = y0 + (int)Math.Round(Math.Sqrt(inside));
                int y2 = y0 - (int)Math.Round(Math.Sqrt(inside));

                SetSafePixel(x, y1, c);
                SetSafePixel(x, y2, c);
            }
        }

        void DrawCirclePolar(int x0, int y0, int r, Color c)
        {
            double step = 0.01;
            double theta = 0;

            while (theta <= 2 * Math.PI)
            {
                int x = x0 + (int)Math.Round(r * Math.Cos(theta));
                int y = y0 + (int)Math.Round(r * Math.Sin(theta));

                SetSafePixel(x, y, c);
                theta += step;
            }
        }

        void DrawCircleMidpoint(int x0, int y0, int r, Color c)
        {
            int x = 0;
            int y = r;
            int d = 1 - r;

            while (x <= y)
            {
                DrawSymmetricPoints(x0, y0, x, y, c);

                if (d < 0)
                    d = d + 2 * x + 3;
                else
                {
                    d = d + 2 * (x - y) + 5;
                    y--;
                }
                x++;
            }
        }

        void DrawCircleBresenham(int x0, int y0, int r, Color c)
        {
            int x = 0, y = r;
            int d = 3 - 2 * r;

            while (x <= y)
            {
                DrawSymmetricPoints(x0, y0, x, y, c);

                if (d < 0)
                    d = d + 4 * x + 6;
                else
                {
                    d = d + 4 * (x - y) + 10;
                    y--;
                }
                x++;
            }
        }

        void DrawSymmetricPoints(int x0, int y0, int x, int y, Color c)
        {
            SetSafePixel(x0 + x, y0 + y, c);
            SetSafePixel(x0 - x, y0 + y, c);
            SetSafePixel(x0 + x, y0 - y, c);
            SetSafePixel(x0 - x, y0 - y, c);

            SetSafePixel(x0 + y, y0 + x, c);
            SetSafePixel(x0 - y, y0 + x, c);
            SetSafePixel(x0 + y, y0 - x, c);
            SetSafePixel(x0 - y, y0 - x, c);
        }

        void DrawRegularPolygonInCircle(int x0, int y0, int r, int n, Color c)
        {
            if (n < 3) return;

            int[] xs = new int[n];
            int[] ys = new int[n];

            for (int i = 0; i < n; i++)
            {
                double ang = 2 * Math.PI * i / n;
                xs[i] = x0 + (int)Math.Round(r * Math.Cos(ang));
                ys[i] = y0 + (int)Math.Round(r * Math.Sin(ang));
            }

            for (int i = 0; i < n; i++)
            {
                int j = (i + 1) % n;
                DrawLineBresenham(xs[i], ys[i], xs[j], ys[j], c);
            }
        }

        private void DrawLineBresenham(int x1, int y1, int x2, int y2, Color c)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx;
            if (x1 < x2)
                sx = 1;
            else
                sx = -1;

            int sy;
            if (y1 < y2)
                sy = 1;
            else
                sy = -1;

            int err = dx - dy;

            while (true)
            {
                SetSafePixel(x1, y1, c);

                if (x1 == x2 && y1 == y2)
                    break;

                int e2 = 2 * err;

                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }
        private void ClearBitmap(Color bg)
        {
            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                    bitmap.SetPixel(x, y, bg);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = (int)numN.Value;
            int r = (int)numR.Value;

            ClearBitmap(Color.LightGray);

            int x0 = pictureBox1.Width / 2;
            int y0 = pictureBox1.Height / 2;

            DrawCircleMidpoint(x0, y0, r, Color.Black);

            DrawRegularPolygonInCircle(x0, y0, r, n, Color.Black);

            pictureBox1.Image = bitmap;
        }
    }
}
