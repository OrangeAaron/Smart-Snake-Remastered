using Smart_Snake_Remastered.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart_Snake_Remastered
{
    public partial class Main : Form
    {

        public Grid FormGrid;
        public const uint MAXGRIDSIZE = 40;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            FormGrid = new Grid((int)numericUpDown1.Value);
            Application.UseWaitCursor = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var value = numericUpDown1.Value;
            if (value > 0 && value < MAXGRIDSIZE)  Start.Enabled = true;
            else Start.Enabled = false;
        }
    }
}
