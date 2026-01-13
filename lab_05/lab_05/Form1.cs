namespace lab_05
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;

        bool hasFirst = false;
        int x1, y1, x2, y2;

        int xmin = 100, ymin = 80, xmax = 400, ymax = 300;

        const int INSIDE = 0;
        const int LEFT = 1;
        const int RIGHT = 2;
        const int BOTTOM = 4;
        const int TOP = 8;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);


            ClearBitmap(Color.White);
            DrawClipWindow();
            pictureBox1.Image = bitmap;
        }

        void ClearBitmap(Color bg)
        {
            if (bitmap == null) return;

            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                    bitmap.SetPixel(x, y, bg);
        }

        void SetSafePixel(int x, int y, Color c)
        {
            if (bitmap == null) return;
            if (x < 0 || y < 0 || x >= bitmap.Width || y >= bitmap.Height) return;
            bitmap.SetPixel(x, y, c);
        }

        void DrawLineBresenham(int x1, int y1, int x2, int y2, Color c)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx;
            if (x1 < x2) sx = 1; else sx = -1;

            int sy;
            if (y1 < y2) sy = 1; else sy = -1;

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

        void DrawClipWindow()
        {
            DrawLineBresenham(xmin, ymin, xmax, ymin, Color.Black);
            DrawLineBresenham(xmax, ymin, xmax, ymax, Color.Black);
            DrawLineBresenham(xmax, ymax, xmin, ymax, Color.Black);
            DrawLineBresenham(xmin, ymax, xmin, ymin, Color.Black);
        }

        int ComputeCode(int x, int y)
        {
            int code = INSIDE;

            if (x < xmin) code |= LEFT;
            else if (x > xmax) code |= RIGHT;

            if (y < ymin) code |= BOTTOM;
            else if (y > ymax) code |= TOP;

            return code;
        }

        bool CohenSutherlandClip(ref int ax, ref int ay, ref int bx, ref int by)
        {
            int code1 = ComputeCode(ax, ay);
            int code2 = ComputeCode(bx, by);

            while (true)
            {
                if ((code1 | code2) == 0)
                    return true;

                if ((code1 & code2) != 0)
                    return false;

                int codeOut;
                if (code1 != 0) codeOut = code1;
                else codeOut = code2;

                double dx = bx - ax;
                double dy = by - ay;

                int x = 0, y = 0;

                if ((codeOut & TOP) != 0)
                {
                    y = ymax;
                    if (dy == 0) return false;
                    x = (int)Math.Round(ax + dx * (ymax - ay) / dy);
                }
                else if ((codeOut & BOTTOM) != 0)
                {
                    y = ymin;
                    if (dy == 0) return false;
                    x = (int)Math.Round(ax + dx * (ymin - ay) / dy);
                }
                else if ((codeOut & RIGHT) != 0)
                {
                    x = xmax;
                    if (dx == 0) return false;
                    y = (int)Math.Round(ay + dy * (xmax - ax) / dx);
                }
                else if ((codeOut & LEFT) != 0)
                {
                    x = xmin;
                    if (dx == 0) return false;
                    y = (int)Math.Round(ay + dy * (xmin - ax) / dx);
                }

                if (codeOut == code1)
                {
                    ax = x; ay = y;
                    code1 = ComputeCode(ax, ay);
                }
                else
                {
                    bx = x; by = y;
                    code2 = ComputeCode(bx, by);
                }
            }
        }

        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            if (!hasFirst)
            {
                x1 = e.X;
                y1 = e.Y;
                hasFirst = true;

                DrawPoint(x1, y1, Color.Blue);
                pictureBox1.Refresh();
            }
            else
            {
                x2 = e.X;
                y2 = e.Y;
                hasFirst = false;

                ClearBitmap(Color.White);
                DrawClipWindow();

                DrawLineBresenham(x1, y1, x2, y2, Color.Green);

                int ax = x1, ay = y1, bx = x2, by = y2;
                bool ok = CohenSutherlandClip(ref ax, ref ay, ref bx, ref by);

                if (ok)
                    DrawLineBresenham(ax, ay, bx, by, Color.Red);

                pictureBox1.Refresh();
            }
        }

        void DrawPoint(int x, int y, Color c)
        {
            for (int i = -2; i <= 2; i++)
                for (int j = -2; j <= 2; j++)
                    SetSafePixel(x + i, y + j, c);
        }
    }
}
