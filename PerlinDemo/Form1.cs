using Logic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PerlinDemoForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enum PerlinMode
        {
            none,
            energy,
            wind,
            mountains
        }
        static PerlinMode mode = PerlinMode.none;
        static int w;
        static int h;
        static int t = 0;
        static PerlinAccessor3d energyEffect;
        static Random random = new Random();
        class VectorField
        {
            PerlinAccessor3d x;
            PerlinAccessor3d y;
            public VectorField(PerlinAccessor3d x, PerlinAccessor3d y)
            {
                this.x = x;
                this.y = y;
            }

            public Vec2 valueAt(double x, double y, double z)
            {
                return new Vec2(this.x.valueAt(x, y, z), this.y.valueAt(x, y, z));
            }
        }
        static VectorField windEffect;
        static Bitmap bmp;
        static Graphics g;
        static string debugText = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            w = canvas.Width;
            h = canvas.Height;
            Accessor<Perlin3d>[] energyEffectAccessor = {
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 314159)), 63, 0.65),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 414159)), 35, 0.35),
            };
            energyEffect = new PerlinAccessor3d(energyEffectAccessor);

            Accessor<Perlin3d>[] windEffectAccessorX = {
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 354159)), 97, 0.5),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 454159)), 63, 0.25),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 554159)), 44, 0.15),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 654159)), 29, 0.1),
            };
            Accessor<Perlin3d>[] windEffectAccessorY = {
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 374159)), 127, 0.65),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 474159)), 93, 0.2),
                new Accessor<Perlin3d>(new Perlin3d(Convert.ToInt32((random.NextDouble() + 1) * 574159)), 74, 0.15),
            };
            windEffect = new VectorField(
                new PerlinAccessor3d(windEffectAccessorX),
                new PerlinAccessor3d(windEffectAccessorY)
            );

            bmp = new Bitmap(w, h);
            g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Black);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            t++;
            checkboxMountainsContour.Visible = mode == PerlinMode.mountains;
            switch (mode)
            {
                case PerlinMode.energy:
                    EnergyEffect.update();
                    break;
                case PerlinMode.wind:
                    WindEffect.update();
                    break;
                case PerlinMode.mountains:
                    MountainsEffect.useContour = checkboxMountainsContour.Checked;
                    MountainsEffect.update();
                    break;
                default:
                    break;
            }
            modeLabel.Text = $"Current Mode: {Enum.GetName(typeof(PerlinMode), mode)}";

            debug.Text = debugText;

            canvas.Image = bmp;
            canvas.Update();
            canvas.Show();
        }

        class EnergyEffect
        {
            public static void initialize()
            {
                g.Clear(Color.Black);
            }

            public static unsafe void update()
            {
                // Cover the image in black pixels first for optimisation
                g.Clear(Color.Black);
                BitmapData tmpBmp = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                // Actually calculating what colour the pixels should be
                for (int y = 0; y < h; y++)
                {
                    byte* tmpRow = (byte*)tmpBmp.Scan0 + (y * tmpBmp.Stride);
                    for (int x = 0; x < w; x++)
                    {
                        double v = Math.Abs(energyEffect.valueAt(x, y, t));
                        // Avoids repeatedly calculating mostly-black pixels to improve efficiency
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
            }
        }

        class WindEffect
        {
            class Dot
            {
                Vec2 pos;
                Vec2 vel;
                double time;
                Color colour;
                const int diameter = 5;
                List<Vec2> previousPos = new List<Vec2>(){
                    new Vec2(-30, -30),
                    new Vec2(-30, -30),
                    new Vec2(-30, -30),
                    new Vec2(-30, -30),
                    new Vec2(-30, -30),
                    new Vec2(-30, -30),
                    new Vec2(-30, -30),
                    new Vec2(-30, -30),
                    new Vec2(-30, -30)
                };

                public Dot(Vec2 pos, Vec3 colour)
                {
                    this.vel = new Vec2(0, 0);
                    this.pos = pos;
                    this.time = t;
                    this.colour = colour.toColour();
                }

                public void update()
                {
                    Vec2 v = windEffect.valueAt(this.pos.x, this.pos.y, t / 3);
                    double theta = Math.Pow(Math.Abs(v.y), 0.6) * Math.Sign(v.y) * Math.PI * 1.8;
                    double mag = Math.Abs(v.x) * 0.13 + 0.06;
                    this.vel += new Vec2(mag * Math.Cos(theta), mag * Math.Sin(theta));
                    this.vel *= 0.97;
                    this.pos += this.vel;
                }

                public void draw()
                {
                    if ((t - this.time) % 4 == 0)
                    {
                        previousPos.RemoveAt(0);
                        previousPos.Add(this.pos);
                    }
                    double opacity = Math.Min(6 - (t - this.time) / 200, 1) * 255;
                    Brush brush = new SolidBrush(Color.FromArgb(Convert.ToInt32(opacity), this.colour));
                    g.FillEllipse(brush, Convert.ToInt32(this.pos.x), Convert.ToInt32(this.pos.y), diameter, diameter);
                    for (int i = 0; i < previousPos.Count; i++)
                    {
                        brush = new SolidBrush(Color.FromArgb(Convert.ToInt32(opacity * (0.05 + 0.03 * i)), this.colour));
                        g.FillEllipse(brush, Convert.ToInt32(this.previousPos[i].x), Convert.ToInt32(this.previousPos[i].y), diameter, diameter);
                    }
                }

                public bool shouldExist()
                {
                    return t < this.time + 1200;
                }
            }

            static List<Dot> dots = new List<Dot>();

            public static void initialize()
            {
                g.Clear(Color.Black);
                dots = new List<Dot>();
            }

            static Vec3 getColour(int x)
            {
                return new Vec3(0.7 - x * 0.5 / (w + h), 1, 0.3 + x * 0.7 / (w + h));
            }

            public static void update()
            {
                g.Clear(Color.Black);
                for (int i = 0; i < dots.Count; i++)
                {
                    dots[i].update();
                    dots[i].draw();
                }
                dots = dots.FindAll(dot => dot.shouldExist());
                if ((t % 60) == 1)
                {
                    int wh = w + h;
                    for (int x = 0; x <= w; x += 40)
                    {
                        dots.Add(new Dot(new Vec2(x, -5), getColour(x)));
                        dots.Add(new Dot(new Vec2(x, h + 5), getColour(x + h)));
                    }
                    for (int y = 0; y <= h; y += 40)
                    {
                        dots.Add(new Dot(new Vec2(-5, y), getColour(y)));
                        dots.Add(new Dot(new Vec2(w + 5, y), getColour(y + w)));
                    }
                }
            }
        }

        class MountainsEffect
        {
            public static bool useContour = false;

            static Vec3[] sunsetGradientPoints = {
                new Vec3(3, 26, 65),
                new Vec3(62, 88, 121),
                new Vec3(155, 165, 174),
                new Vec3(220, 182, 151),
                new Vec3(255, 135, 25),
                new Vec3(221, 114, 60),
                new Vec3(173, 74, 40),
                new Vec3(4, 3, 8),
            };

            static Accessor<Perlin2d>[] mountainsEffectAccessor = {
                new Accessor<Perlin2d>(new Perlin2d(Convert.ToInt32((random.NextDouble() + 1) * 324159)), 137, 0.4),
                new Accessor<Perlin2d>(new Perlin2d(Convert.ToInt32((random.NextDouble() + 1) * 424159)), 105, 0.3),
                new Accessor<Perlin2d>(new Perlin2d(Convert.ToInt32((random.NextDouble() + 1) * 524159)), 44, 0.2),
                new Accessor<Perlin2d>(new Perlin2d(Convert.ToInt32((random.NextDouble() + 1) * 624159)), 31, 0.1),
            };

            static PerlinAccessor2d mountainsEffect = new PerlinAccessor2d(mountainsEffectAccessor);

            public static void initialize()
            {
                g.Clear(Color.Black);
                mountainsEffect.startCache();
            }

            public static unsafe void update()
            {
                if ((t % 100) == 0) mountainsEffect.clearCache();
                double stride = h * 1.0 / (sunsetGradientPoints.Length - 1);
                for (int i = 0; i < h; i++)
                {
                    Vec3 colour1 = sunsetGradientPoints[i * (sunsetGradientPoints.Length - 1) / h] / 255;
                    Vec3 colour2 = sunsetGradientPoints[i * (sunsetGradientPoints.Length - 1) / h + 1] / 255;
                    Vec3 colour = colour1 * (1 - (i % stride) / stride) + colour2 * (i % stride) / stride;
                    g.FillRectangle(new SolidBrush(colour.toColour()), new Rectangle(0, i, w, 1));
                }

                BitmapData tmpBmp = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                byte* tmpBmpPtr = (byte*)tmpBmp.Scan0;
                int w2 = w / 2;
                int view = t;
                for (int x = 0; x < w; x++)
                {
                    int yMax = h;
                    int ptrOffset = x * 4;
                    for (int z = 0; z < 160; z++)
                    {
                        int dist = 280 + z * 4;
                        double brightness = (160 - z) / 130.0;
                        int water = z;
                        int v = h - Convert.ToInt32(water +
                            Math.Max(0,
                            mountainsEffect.valueAt(dist, view + (x - w2) * dist / 500) * h * (1 - z * 0.005)
                            - Math.Max((20 - z) * 5, 0))
                        );
                        if (v >= yMax) continue;
                        if (useContour)
                        {
                            tmpBmpPtr[v * tmpBmp.Stride + ptrOffset] = 255;
                            tmpBmpPtr[v * tmpBmp.Stride + ptrOffset + 1] = 255;
                            tmpBmpPtr[v * tmpBmp.Stride + ptrOffset + 2] = 0;
                            for (int y = Math.Max(v + 1, 0); y < yMax; y++)
                            {
                                byte r = 20;
                                byte g = 5;
                                byte b = 40;
                                tmpBmpPtr[y * tmpBmp.Stride + ptrOffset] = b;
                                tmpBmpPtr[y * tmpBmp.Stride + ptrOffset + 1] = g;
                                tmpBmpPtr[y * tmpBmp.Stride + ptrOffset + 2] = r;
                            }
                        }
                        else
                        {
                            for (int y = Math.Max(v, 0); y < yMax; y++)
                            {
                                Vec3 colour = new Vec3(30, 85, 15);
                                if (h - y <= water) colour = new Vec3(10, 20, 80);
                                else if (h - y <= water + 3) colour = new Vec3(50, 60, 30);
                                colour *= brightness;
                                byte r = Convert.ToByte(colour.x);
                                byte g = Convert.ToByte(colour.y);
                                byte b = Convert.ToByte(colour.z);
                                tmpBmpPtr[y * tmpBmp.Stride + ptrOffset] = b;
                                tmpBmpPtr[y * tmpBmp.Stride + ptrOffset + 1] = g;
                                tmpBmpPtr[y * tmpBmp.Stride + ptrOffset + 2] = r;
                            }
                        }
                        yMax = v;
                    }
                }

                bmp.UnlockBits(tmpBmp);
            }
        }

        private void buttonEnergyEffect_Click(object sender, EventArgs e)
        {
            mode = PerlinMode.energy;
            EnergyEffect.initialize();
        }

        private void buttonWindEffect_Click(object sender, EventArgs e)
        {
            mode = PerlinMode.wind;
            WindEffect.initialize();
        }

        private void buttonMountainsEffect_Click(object sender, EventArgs e)
        {
            mode = PerlinMode.mountains;
            MountainsEffect.initialize();
        }
    }
}