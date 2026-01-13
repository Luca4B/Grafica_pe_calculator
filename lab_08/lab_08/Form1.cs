using System.Drawing;

namespace lab_08
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        Random rand = new Random();
        int iterations = 60000;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            DrawBarnsleyFern();

            pictureBox1.Image = bitmap;
        }

        void DrawBarnsleyFern()
        {
            ClearBitmap(Color.Black);

            double x = 0;
            double y = 0;

            for (int i = 0; i < iterations; i++)
            {
                double r = rand.NextDouble();
                double xNew, yNew;

                if (r < 0.01)
                {
                    xNew = 0;
                    yNew = 0.16 * y;
                }
                else if (r < 0.86)
                {
                    xNew = 0.85 * x + 0.04 * y;
                    yNew = -0.04 * x + 0.85 * y + 1.6;
                }
                else if (r < 0.93)
                {
                    xNew = 0.20 * x - 0.26 * y;
                    yNew = 0.23 * x + 0.22 * y + 1.6;
                }
                else
                {
                    xNew = -0.15 * x + 0.28 * y;
                    yNew = 0.26 * x + 0.24 * y + 0.44;
                }

                x = xNew;
                y = yNew;

                int px = (int)(bitmap.Width / 2 + x * 50);
                int py = (int)(bitmap.Height - y * 50);

                SetSafePixel(px, py, Color.Green);
            }
        }

        void ClearBitmap(Color bg)
        {
            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                    bitmap.SetPixel(x, y, bg);
        }

        void SetSafePixel(int x, int y, Color c)
        {
            if (x < 0 || y < 0 || x >= bitmap.Width || y >= bitmap.Height)
                return;

            bitmap.SetPixel(x, y, c);
        }
    }
}
