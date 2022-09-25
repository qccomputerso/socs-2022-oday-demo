using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    double l = Math.Pow((1.02 - Math.Abs(landscape.valueAt(i, j, t))), 20);
                    bmp.SetPixel(i, j, new Vec3(l, l * 0.9, l * 1.3).toColour());
                }

            canvas.Image = bmp;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            t++;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    double l = Math.Pow((1.02 - Math.Abs(landscape.valueAt(i, j, t))), 20);
                    bmp.SetPixel(i, j, new Vec3(l, l * 0.9, l * 1.3).toColour());
                }

            canvas.Image = bmp;
        }
    }
}