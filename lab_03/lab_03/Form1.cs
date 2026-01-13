namespace lab_03
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;

        Point[] triangle =
        {
            new Point(100, 100),
            new Point(130, 30),
            new Point(250, 100)
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            DrawPolygon(triangle, Color.Cyan);

            pictureBox1.Image = bitmap;
        }

        void ClearBitmap(Color bg)
        {
            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                    bitmap.SetPixel(x, y, bg);
        }

        void SetSafePixel(int x, int y, Color c)
        {
            if (x < 0 || y < 0 || x >= bitmap.Width || y >= bitmap.Height) return;
            bitmap.SetPixel(x, y, c);
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

        void DrawPolygon(Point[] p, Color c)
        {
            for (int i = 0; i < p.Length; i++)
            {
                int j = (i + 1) % p.Length;
                DrawLineBresenham(p[i].X, p[i].Y, p[j].X, p[j].Y, c);
            }
        }

        Point[] Translate(Point[] p, int dx, int dy)
        {
            Point[] r = new Point[p.Length];
            for (int i = 0; i < p.Length; i++)
                r[i] = new Point(p[i].X + dx, p[i].Y + dy);
            return r;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearBitmap(Color.Gray);
            DrawPolygon(triangle, Color.Cyan);
            DrawPolygon(Translate(triangle, 20, 30), Color.DeepPink);
            pictureBox1.Image = bitmap;
        }

        Point[] Scale(Point[] tri, float s)
        {
            Point[] result = new Point[tri.Length];
            for (int i = 0; i < tri.Length; i++)
                result[i] = new Point(
                    (int)(tri[i].X * s),
                    (int)(tri[i].Y * s)
                );
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearBitmap(Color.Gray);
            DrawPolygon(triangle, Color.Cyan);
            DrawPolygon(Scale(triangle, 2), Color.DeepPink);
            pictureBox1.Refresh();
        }

        Point[] Rotate(Point[] p, double angle)
        {
            Point[] r = new Point[p.Length];
            double c = Math.Cos(angle);
            double s = Math.Sin(angle);

            for (int i = 0; i < p.Length; i++)
                r[i] = new Point(
                    (int)(p[i].X * c - p[i].Y * s),
                    (int)(p[i].X * s + p[i].Y * c)
                );
            return r;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearBitmap(Color.Gray);
            DrawPolygon(triangle, Color.Cyan);
            DrawPolygon(Rotate(triangle, Math.PI / 4), Color.DeepPink);
            pictureBox1.Image = bitmap;
        }
    }
}
