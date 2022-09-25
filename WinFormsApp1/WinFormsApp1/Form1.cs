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
        static Bitmap bmp = new Bitmap(0, 0);
        static Graphics g = Graphics.FromImage(new Bitmap(0, 0));

        private void Form1_Load(object sender, EventArgs e)
        {
            w = canvas.Width;
            h = canvas.Height;
            bmp = new Bitmap(w, h);
            g = Graphics.FromImage(bmp);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }
    }
}