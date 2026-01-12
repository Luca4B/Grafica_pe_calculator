using System.Windows.Forms;
using System.Drawing;

namespace lab_01
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;

        private bool hasFirstPoint = false;
        private int px1, py1;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseClick += pictureBox1_MouseClick;
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

        private void SetSafePixel(int x, int y, Color c)
        {
            if (x < 0) return;
            if (y < 0) return;
            if (x >= bitmap.Width) return;
            if (y >= bitmap.Height) return;
            bitmap.SetPixel(x, y, c);
        }

        private void DrawPoint(int x, int y, Color c)
        {
            for (int i = -2; i <= 2; i++)
                for (int j = -2; j <= 2; j++)
                    SetSafePixel(x + i, y + j, c);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Width = 800;
            Height = 500;

            bitmap = new Bitmap(Width, Height);

            DrawLineBresenham(100, 80, 300, 120, Color.Red);
            DrawLineBresenham(300, 120, 150, 260, Color.Blue);
            DrawLineBresenham(150, 260, 100, 80, Color.Green);



            pictureBox1.Image = bitmap;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            DrawPoint(x, y, Color.Black);

            if (!hasFirstPoint)
            {
                px1 = x;
                py1 = y;
                hasFirstPoint = true;
            }
            else
            {
                DrawLineBresenham(px1, py1, x, y, Color.Orange);
                hasFirstPoint = false;
            }

            pictureBox1.Image = bitmap;
        }
    }
}
