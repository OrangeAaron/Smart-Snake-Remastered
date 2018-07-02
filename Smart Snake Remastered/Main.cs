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
            var newEnvironment = new Grid((int)numericUpDown1.Value);
            Environment = newEnvironment;
            //for (int column = 0; column < Environment.BoxMatrix.GetLength(0); column++)
            //{
            //    DataGridViewImageColumn col = new DataGridViewImageColumn();
            //    col.ImageLayout = DataGridViewImageCellLayout.Stretch;
            //    col.Image = null;
            //    dataGridView1.Columns.Add(col);
            //}
            ////var matrixRow = Environment.BoxMatrix.GetLength(1) - 1;
            //for (int row = 0; row < Environment.BoxMatrix.GetLength(1); row++)
            //{
            //    var rowID = dataGridView1.Rows.Add();
            //    DataGridViewRow newRow = dataGridView1.Rows[rowID];
            //    newRow.Height = (dataGridView1.ClientRectangle.Height) / Environment.BoxMatrix.GetLength(1);
            //    for (int column = 0; column < Environment.BoxMatrix.GetLength(0); column++)
            //    {
            //        var item = Environment.BoxMatrix[column,row].ContainedObject;
            //        var image = Properties.Resources.empty;
            //        newRow.Cells[column].Value = image;
            //    }
            //  //  matrixRow--;
            //}


            //dataGridView1.ClearSelection();
            LifeForms = Business.CreateLife(Environment, (int)numericUpDown2.Value);
            timer1.Enabled = true;
            Start.Enabled = true;
            Application.UseWaitCursor = false;
        }

        //void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (e.RowIndex > -1 && e.ColumnIndex > -1)
        //    {
        //        var item = Environment.BoxMatrix[e.ColumnIndex, e.RowIndex].ContainedObject;
        //        if (item is Animal)
        //        {
        //            Bitmap image = null;
        //            var @switch = new Dictionary<Type, Action> {
        //            { typeof(Snake), () =>{
        //                var snake = (Snake)item;
        //                image =  new Bitmap(Snake.AnimalImage);
        //                if(snake.Direction == Direction.North) ;
        //                else if(snake.Direction == Direction.East) image.RotateFlip(RotateFlipType.Rotate90FlipNone);
        //                else if(snake.Direction == Direction.South) image.RotateFlip(RotateFlipType.Rotate180FlipNone);
        //                else if(snake.Direction == Direction.West) image.RotateFlip(RotateFlipType.Rotate270FlipNone);
        //            } }
        //            };
        //            @switch[item.GetType()]();
        //            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = image;
        //        }
        //        else
        //        {
        //            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.empty;
        //        }
        //    }
        //}

    //private void dataGridView1_SizeChanged(object sender, EventArgs e)
    //{
    //    foreach (DataGridViewRow row in dataGridView1.Rows)
    //    {
    //        row.Height = (dataGridView1.ClientRectangle.Height - dataGridView1.ColumnHeadersHeight) / dataGridView1.Rows.Count;
    //    }
    //}

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {
        var value = numericUpDown1.Value;
        if (value > 0 && value < MAXGRIDSIZE) Start.Enabled = true;
        else Start.Enabled = false;
    }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            Business.Live(LifeForms, Environment);
        }
    }
}
