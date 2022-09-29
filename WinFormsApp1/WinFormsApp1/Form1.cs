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

        enum PerlinMode
        {
            energy,
            wind
        }
        static PerlinMode mode = PerlinMode.energy;
        static int w;
        static int h;
        static int t = 0;
        static PerlinAccessor3d energyEffect;
        class VectorField
        {
            PerlinAccessor3d x;
            PerlinAccessor3d y;
            public VectorField(PerlinAccessor3d x, PerlinAccessor3d y)
            {
                this.x = x;
                this.y = y;
            }

            public Vec2 valueAt(int x, int y, int z)
            {
                return new Vec2(this.x.valueAt(x, y, z), this.y.valueAt(x, y, z));
            }
        }
        static VectorField screensaverEffect;
        static Bitmap bmp;

        private void Form1_Load(object sender, EventArgs e)
        {
            w = canvas.Width;
            h = canvas.Height;
            Random random = new Random();
            Accessor<Perlin3d>[] energyEffectAccessor = {
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 314159)), 63, 0.65),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 414159)), 35, 0.35),
            };
            energyEffect = new PerlinAccessor3d(energyEffectAccessor);

            Accessor<Perlin3d>[] screensaverEffectAccessorX = {
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 354159)), 97, 0.5),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 454159)), 63, 0.25),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 554159)), 44, 0.15),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 654159)), 29, 0.1),
            };
            Accessor<Perlin3d>[] screensaverEffectAccessorY = {
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 374159)), 97, 0.5),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 474159)), 63, 0.25),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 574159)), 44, 0.15),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 674159)), 29, 0.1),
            };
            screensaverEffect = new VectorField(
                new PerlinAccessor3d(screensaverEffectAccessorX),
                new PerlinAccessor3d(screensaverEffectAccessorY)
            );

            bmp = new Bitmap(w, h);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            t++;
            switch (mode)
            {
                case PerlinMode.energy:
                    energyEffectTick();
                    break;
                default:
                    break;
            }
        }


        private unsafe void energyEffectTick()
        {
            BitmapData tmpBmp = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            // Cover the image in black pixels first for optimisation
            for (int y = 0; y < h; y++)
            {
                byte* tmpRow = (byte*)tmpBmp.Scan0 + (y * tmpBmp.Stride);
                for (int x = 0; x < w; x++)
                {
                    tmpRow[x * 4] = 0;
                    tmpRow[x * 4 + 1] = 0;
                    tmpRow[x * 4 + 2] = 0;
                    tmpRow[x * 4 + 3] = 255;
                }
            }

            // Actually calculating what colour the pixels should be
            for (int y = 0; y < h; y++)
            {
                byte* tmpRow = (byte*)tmpBmp.Scan0 + (y * tmpBmp.Stride);
                for (int x = 0; x < w; x++)
                {
                    double v = Math.Abs(energyEffect.valueAt(x, y, t));
                    // Skip the calculation ahead to avoid repeatedly calculating black pixels to improve performance
                    if (v > 0.1)
                    {
                        x += Convert.ToInt32(30 * v);
                        continue;
                    }
                    double l = Math.Pow((1.01 - v), 40);
                    Vec3 colorVec = new Vec3(l * 0.95, l * 0.75, l * 1.1).clampToColor();
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