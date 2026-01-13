namespace lab_7
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            DrawScene();
            
            pictureBox1.Image = bitmap;
        }

        private void DrawScene()
        {
            ClearBitmap(Color.SkyBlue);

            float cx = bitmap.Width / 2f;
            float cy = bitmap.Height / 2f + 40;
            float radius = 110;

            DrawFractalCloud(cx, cy, radius, 5);

            DrawFractalCloud(cx - 60, cy + 20, radius * 0.7f, 4);
            DrawFractalCloud(cx + 60, cy + 20, radius * 0.7f, 4);
        }

        private void DrawFractalCloud(float x, float y, float r, int depth)
        {
            if (depth <= 0 || r < 2) return;

            FillSoftCircle(x, y, r);

            int children = 3;

            for (int i = 0; i < children; i++)
            {
                double angle = rand.NextDouble() * 2.0 * Math.PI;

                float dist = r * (0.55f + (float)rand.NextDouble() * 0.25f);

                float nx = x + (float)Math.Cos(angle) * dist;
                float ny = y + (float)Math.Sin(angle) * dist;

                float nr = r * (0.45f + (float)rand.NextDouble() * 0.15f);

                DrawFractalCloud(nx, ny, nr, depth - 1);
            }
        }

        private void FillSoftCircle(float cx, float cy, float r)
        {
            int minX = (int)(cx - r - 2);
            int maxX = (int)(cx + r + 2);
            int minY = (int)(cy - r - 2);
            int maxY = (int)(cy + r + 2);

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    float dx = x - cx;
                    float dy = y - cy;
                    float d = (float)Math.Sqrt(dx * dx + dy * dy);

                    float jitter = (float)(rand.NextDouble() * 2.0 - 1.0) * (r * 0.06f);

                    if (d <= r + jitter)
                    {
                        float t = d / r;
                        int alpha = 220;

                        if (t > 0.85f)
                        {
                            float k = (t - 0.85f) / 0.15f;
                            alpha = (int)(220 - k * 120);
                        }

                        SetSafePixel(x, y, Color.FromArgb(alpha, 255, 255, 255));
                    }
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
