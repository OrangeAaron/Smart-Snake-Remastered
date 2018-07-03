using Smart_Snake_Remastered.Logic;
using Smart_Snake_Remastered.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Smart_Snake_Remastered
{
    public partial class Main : Form
    {

        public Grid Environment;
        public List<Animal> LifeForms;
        public const uint MAXGRIDSIZE = 100;
        private System.Timers.Timer timer1 = null;
        public static Color empty = Color.Black;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Start.Enabled = false;
            if (timer1 != null) timer1.Enabled = false;
            timer1 = new System.Timers.Timer();
            this.timer1.Interval = Convert.ToDouble(400);
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            //dataGridView1.Rows.Clear();
            //dataGridView1.Columns.Clear();
            //dataGridView1.Refresh();
            Environment = null;
            var size = (int)numericUpDown1.Value;
            var newEnvironment = new Grid(new Size(size, size));

            Environment = newEnvironment;
            Environment.World.InitializeTo(Color.White);
            using (Graphics g = Graphics.FromImage(Environment.World))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.DrawImage(Environment.World, pictureBox1.Bounds.X, pictureBox1.Bounds.Y, pictureBox1.Width, pictureBox1.Height);
            }
            this.Refresh();

            LifeForms = Business.CreateLife(Environment, (int)numericUpDown2.Value);
            timer1.Enabled = true;
            Start.Enabled = true;
            Application.UseWaitCursor = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var value = numericUpDown1.Value;
            if (value > 0 && value < MAXGRIDSIZE) Start.Enabled = true;
            else Start.Enabled = false;
        }

        private void grid_Paint(System.Object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (Environment != null && Environment.World != null)
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                e.Graphics.DrawImage(Environment.World, pictureBox1.Bounds.X, pictureBox1.Bounds.Y, pictureBox1.Width, pictureBox1.Height);
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Business.Live(LifeForms, Environment);
            Invoke(new Action(() =>
            {
                pictureBox1.Refresh();
            }));
        }
    }
}
