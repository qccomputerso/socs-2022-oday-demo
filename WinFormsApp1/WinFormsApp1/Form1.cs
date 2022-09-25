using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static int w;
        static int h;
        static int t = 0;
        static PerlinAccessor3d landscape;
        static Bitmap bmp;

        private void Form1_Load(object sender, EventArgs e)
        {
            w = canvas.Width;
            h = canvas.Height;
            Accessor<Perlin3d>[] landscapeAccessor = {
                new Accessor<Perlin3d>(new Perlin3d(31415926), 97, 0.7),
                new Accessor<Perlin3d>(new Perlin3d(314259), 55, 0.3),
            };
            landscape = new PerlinAccessor3d(landscapeAccessor);

            bmp = new Bitmap(w, h);
        }

        private unsafe void timer_Tick(object sender, EventArgs e)
        {
            t += 5;
            BitmapData tmpBmp = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            for (int y = 0; y < h; y++)
            {
                byte* tmpRow = (byte*)tmpBmp.Scan0 + (y * tmpBmp.Stride);
                for (int x = 0; x < w; x++)
                {
                    double l = Math.Pow((1.02 - Math.Abs(landscape.valueAt(x, y, t))), 20);
                    Vec3 colorVec = new Vec3(l, l * 0.9, l * 1.2).clampToColor();
                    tmpRow[x * 4] = Convert.ToByte(colorVec.z);
                    tmpRow[x * 4 + 1] = Convert.ToByte(colorVec.y);
                    tmpRow[x * 4 + 2] = Convert.ToByte(colorVec.x);
                    tmpRow[x * 4 + 3] = 255;
                }
            }
            bmp.UnlockBits(tmpBmp);

            canvas.Image = bmp;
        }
    }
}