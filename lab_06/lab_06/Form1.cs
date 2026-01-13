namespace lab_06
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;

        private List<Point> controlPoints = new List<Point>();

        public Form1()
        {
            InitializeComponent();        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);


            pictureBox1.Image = bitmap;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (controlPoints.Count >= 4)
                return;

            controlPoints.Add(new Point(e.X, e.Y));

            RedrawAll();
        }

        private void RedrawAll()
        {
            ClearBitmap(Color.White);

            for (int i = 0; i < controlPoints.Count; i++)
                DrawPoint(controlPoints[i].X, controlPoints[i].Y, Color.Red);

            if (controlPoints.Count >= 2)
                for (int i = 0; i < controlPoints.Count - 1; i++)
                    DrawLineBresenham(controlPoints[i].X, controlPoints[i].Y,
                                      controlPoints[i + 1].X, controlPoints[i + 1].Y,
                                      Color.Gray);

            if (controlPoints.Count == 4)
                DrawBezierCubic(controlPoints[0], controlPoints[1],
                                controlPoints[2], controlPoints[3],
                                Color.Blue);

            pictureBox1.Refresh();
        }

        private void DrawBezierCubic(Point p0, Point p1, Point p2, Point p3, Color c)
        {
            int steps = 400;

            Point prev = BezierPoint(p0, p1, p2, p3, 0.0);

            for (int i = 1; i <= steps; i++)
            {
                double t = i / (double)steps;
                Point next = BezierPoint(p0, p1, p2, p3, t);

                DrawLineBresenham(prev.X, prev.Y, next.X, next.Y, c);

                prev = next;
            }
        }

        private Point BezierPoint(Point p0, Point p1, Point p2, Point p3, double t)
        {
            double u = 1.0 - t;

            double x =
                (u * u * u) * p0.X +
                (3 * u * u * t) * p1.X +
                (3 * u * t * t) * p2.X +
                (t * t * t) * p3.X;

            double y =
                (u * u * u) * p0.Y +
                (3 * u * u * t) * p1.Y +
                (3 * u * t * t) * p2.Y +
                (t * t * t) * p3.Y;

            return new Point((int)Math.Round(x), (int)Math.Round(y));
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

        private void DrawPoint(int x, int y, Color c)
        {
            for (int i = -3; i <= 3; i++)
                for (int j = -3; j <= 3; j++)
                    SetSafePixel(x + i, y + j, c);
        }

        private void ClearBitmap(Color bg)
        {
            if (bitmap == null) return;

            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                    bitmap.SetPixel(x, y, bg);
        }

        private void SetSafePixel(int x, int y, Color c)
        {
            if (bitmap == null) return;
            if (x < 0 || y < 0 || x >= bitmap.Width || y >= bitmap.Height) return;
            bitmap.SetPixel(x, y, c);
        }
    }
}