using Smart_Snake_Remastered.Logic;
using Smart_Snake_Remastered.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        public const uint MAXGRIDSIZE = 2000;
        public static Color empty = Color.White;
        public Animal SmartestAnimal;
        
        //multithreading variables
        private System.Timers.Timer timer1 = null;
        private BackgroundWorker worker;
        
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Start.Enabled = true;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Start.Enabled = false;
            if (timer1 != null) timer1.Enabled = false;
            Environment = null;
            var size = (int)numericUpDown1.Value;
            var newEnvironment = new Grid(new Size(size, size));

            Environment = newEnvironment;
            Environment.InitializeTo(Color.White);
            Environment.WorldLock.WaitOne();
            using (Graphics g = Graphics.FromImage(Environment.World))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.DrawImage(Environment.World, pictureBox1.Bounds.X, pictureBox1.Bounds.Y, pictureBox1.Width, pictureBox1.Height);
            }
            this.Refresh();
            Environment.WorldLock.ReleaseMutex();
            

            LifeForms = Business.CreateLife(Environment, (int)numericUpDown2.Value);
            worker = new BackgroundWorker();
            worker.DoWork += RunAct;
            timer1 = new System.Timers.Timer();
            this.timer1.Interval = Convert.ToDouble(50);
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            timer1.Enabled = true;
            Start.Enabled = true;
            Application.UseWaitCursor = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var value = numericUpDown1.Value;
            if (value > 0 && value <= MAXGRIDSIZE) Start.Enabled = true;
            else Start.Enabled = false;
        }
        
        
        private void grid_Paint(System.Object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (Environment != null && Environment.WorldLock != null)
            {
                Environment.WorldLock.WaitOne();
                if (Environment.World != null)
                {
                    Graphics g = e.Graphics;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                    g.DrawImage(Environment.World, pictureBox1.Bounds.X, pictureBox1.Bounds.Y, pictureBox1.Width, pictureBox1.Height);
                }
                Environment.WorldLock.ReleaseMutex();
            }
        }

        private void RunAct(object sender, EventArgs e)
        {
            var smartestAnimal = Business.Live(LifeForms, Environment);
            if (smartestAnimal != null)
            {
                SmartestAnimal = smartestAnimal;
                }
        }

        private void ShowSmartestAnimal()
        {
            groupBox1.Visible = true;
            BoldestSnake.Visible = true;
            BoldestSnake.Text = SmartestAnimal.Boldness.ToString();
            HearingSnake.Visible = true;
            HearingSnake.Text = SmartestAnimal.Hearing.ToString();
            SmellSnake.Visible = true;
            SmellSnake.Text = SmartestAnimal.Smell.ToString();
            StaminaSnake.Visible = true;
            StaminaSnake.Text = SmartestAnimal.Stamina.ToString();
            VisionSnake.Visible = true;
            VisionSnake.Text = SmartestAnimal.Vision.ToString();
            label3.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            Boldness.Visible = true;
            Stamina.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
                try
                {
                    Invoke(new Action(() =>
                    {
                     if (SmartestAnimal != null)
                     {
                            ShowSmartestAnimal();
                    }
                        pictureBox1.Refresh();
                    }));
                }
                catch (Exception ex)
                {
                    //do something here
                }
            }
        }
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void SmellSnake_TextChanged(object sender, EventArgs e)
        {

        }

        private void HearingSnake_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void VisionSnake_TextChanged(object sender, EventArgs e)
        {

        }

        private void Stamina_Click(object sender, EventArgs e)
        {

        }

        private void StaminaSnake_TextChanged(object sender, EventArgs e)
        {

        }

        private void BoldestSnake_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
