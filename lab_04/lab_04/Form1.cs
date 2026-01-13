namespace lab_04
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;

        Point[] polygon =
        {
            new Point(100, 100),
            new Point(200, 50),
            new Point(300, 120),
            new Point(250, 200),
            new Point(150, 180)
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
            DrawPolygon(polygon, Color.Black);


            pictureBox1.Image = bitmap;
        }

        void DrawPolygon(Point[] p, Color c)
        {
            for (int i = 0; i < p.Length; i++)
            {
                int j = (i + 1) % p.Length;
                DrawLineBresenham(p[i].X, p[i].Y, p[j].X, p[j].Y, c);
            }
        }

        void FillPolygonScanline(Point[] p, Color fillColor)
        {
            int ymin = p.Min(pt => pt.Y);
            int ymax = p.Max(pt => pt.Y);

            for (int y = ymin; y <= ymax; y++)
            {
                List<int> intersections = new List<int>();

                for (int i = 0; i < p.Length; i++)
                {
                    Point p1 = p[i];
                    Point p2 = p[(i + 1) % p.Length];

                    if (p1.Y == p2.Y)
                        continue;

                    if (y >= Math.Min(p1.Y, p2.Y) && y < Math.Max(p1.Y, p2.Y))
                    {
                        int x = p1.X + (y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y);
                        intersections.Add(x);
                    }
                }

                intersections.Sort();

                for (int i = 0; i < intersections.Count - 1; i += 2)
                {
                    int xStart = intersections[i];
                    int xEnd = intersections[i + 1];

                    for (int x = xStart; x < xEnd; x++)
                        SetSafePixel(x, y, fillColor);
                }
            }
        }

        void DrawLineBresenham(int x1, int y1, int x2, int y2, Color c)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx = (x1 < x2) ? 1 : -1;
            int sy = (y1 < y2) ? 1 : -1;

            int err = dx - dy;

            while (true)
            {
                SetSafePixel(x1, y1, c);
                if (x1 == x2 && y1 == y2) break;

                int e2 = 2 * err;
                if (e2 > -dy) { err -= dy; x1 += sx; }
                if (e2 < dx) { err += dx; y1 += sy; }
            }
        }

        void SetSafePixel(int x, int y, Color c)
        {
            if (x < 0 || y < 0 || x >= bitmap.Width || y >= bitmap.Height) return;
            bitmap.SetPixel(x, y, c);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillPolygonScanline(polygon, Color.Orange);

            pictureBox1.Image = bitmap;
        }
    }
}
