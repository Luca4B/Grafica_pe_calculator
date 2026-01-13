using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab_09
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;

        float Ax = 0, Ay = 0, Az = 0;
        float Bx = 1, By = 0, Bz = 0;
        float Cx = 0, Cy = 1, Cz = 0;

        float Lx = 0.3f, Ly = 0.4f, Lz = 1.0f;

        private Color baseColor = Color.Orange;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            ClearBitmap(Color.Black);

            float Nx, Ny, Nz;
            ComputeNormalUnit(out Nx, out Ny, out Nz);

            Normalize(ref Lx, ref Ly, ref Lz);

            float intensity = Nx * Lx + Ny * Ly + Nz * Lz;
            if (intensity < 0) intensity = 0;

            Color shaded = ApplyIntensity(baseColor, intensity);

            Point[] tri2D = GetTriangle2D();

            FillTriangle(tri2D, shaded);
            DrawTriangle(tri2D, Color.Black);

            pictureBox1.Image = bitmap;
        }

        private void ComputeNormalUnit(out float Nx, out float Ny, out float Nz)
        {
            float ABx = Bx - Ax;
            float ABy = By - Ay;
            float ABz = Bz - Az;

            float ACx = Cx - Ax;
            float ACy = Cy - Ay;
            float ACz = Cz - Az;

            Nx = ABy * ACz - ABz * ACy;
            Ny = ABz * ACx - ABx * ACz;
            Nz = ABx * ACy - ABy * ACx;

            Normalize(ref Nx, ref Ny, ref Nz);
        }

        private void Normalize(ref float x, ref float y, ref float z)
        {
            float len = (float)Math.Sqrt(x * x + y * y + z * z);
            if (len == 0) return;

            x /= len;
            y /= len;
            z /= len;
        }

        private Color ApplyIntensity(Color col, float k)
        {
            if (k < 0) k = 0;
            if (k > 1) k = 1;

            int r = (int)(col.R * k);
            int g = (int)(col.G * k);
            int b = (int)(col.B * k);

            return Color.FromArgb(255, r, g, b);
        }

        private Point[] GetTriangle2D()
        {
            return new Point[]
            {
                new Point(200, 120),
                new Point(420, 180),
                new Point(260, 380)
            };
        }

        private void DrawTriangle(Point[] t, Color c)
        {
            DrawLineBresenham(t[0].X, t[0].Y, t[1].X, t[1].Y, c);
            DrawLineBresenham(t[1].X, t[1].Y, t[2].X, t[2].Y, c);
            DrawLineBresenham(t[2].X, t[2].Y, t[0].X, t[0].Y, c);
        }

        private void FillTriangle(Point[] t, Color c)
        {
            int ymin = Math.Min(t[0].Y, Math.Min(t[1].Y, t[2].Y));
            int ymax = Math.Max(t[0].Y, Math.Max(t[1].Y, t[2].Y));

            for (int y = ymin; y <= ymax; y++)
            {
                int[] xs = new int[3];
                int count = 0;

                IntersectEdge(t[0], t[1], y, xs, ref count);
                IntersectEdge(t[1], t[2], y, xs, ref count);
                IntersectEdge(t[2], t[0], y, xs, ref count);

                if (count >= 2)
                {
                    Array.Sort(xs, 0, count);
                    int xStart = xs[0];
                    int xEnd = xs[1];

                    for (int x = xStart; x <= xEnd; x++)
                        SetSafePixel(x, y, c);
                }
            }
        }

        private void IntersectEdge(Point p1, Point p2, int y, int[] xs, ref int count)
        {
            if (p1.Y == p2.Y) return;
            if (y < Math.Min(p1.Y, p2.Y) || y >= Math.Max(p1.Y, p2.Y)) return;

            int x = p1.X + (y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y);
            xs[count] = x;
            count++;
        }

        private void DrawLineBresenham(int x1, int y1, int x2, int y2, Color c)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx;
            if (x1 < x2) sx = 1;
            else sx = -1;

            int sy;
            if (y1 < y2) sy = 1;
            else sy = -1;

            int err = dx - dy;

            while (true)
            {
                SetSafePixel(x1, y1, c);
                if (x1 == x2 && y1 == y2) break;

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

        private void SetSafePixel(int x, int y, Color c)
        {
            if (x < 0 || y < 0 || x >= bitmap.Width || y >= bitmap.Height) return;
            bitmap.SetPixel(x, y, c);
        }
    }
}
