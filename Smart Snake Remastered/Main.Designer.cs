using System.Drawing;
using System.Windows.Forms;

namespace Smart_Snake_Remastered
{
    partial class Main
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Start = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BoldestSnake = new System.Windows.Forms.TextBox();
            this.StaminaSnake = new System.Windows.Forms.TextBox();
            this.VisionSnake = new System.Windows.Forms.TextBox();
            this.HearingSnake = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SmellSnake = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Stamina = new System.Windows.Forms.Label();
            this.Boldness = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Enabled = false;
            this.Start.Location = new System.Drawing.Point(561, 434);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(143, 48);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(564, 316);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(140, 22);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(561, 296);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Grid Size";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(561, 406);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(143, 22);
            this.numericUpDown2.TabIndex = 4;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(561, 386);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Number of Snakes";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 500);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.grid_Paint);
            // 
            // BoldestSnake
            // 
            this.BoldestSnake.Enabled = false;
            this.BoldestSnake.Location = new System.Drawing.Point(72, 21);
            this.BoldestSnake.Name = "BoldestSnake";
            this.BoldestSnake.Size = new System.Drawing.Size(140, 22);
            this.BoldestSnake.TabIndex = 7;
            this.BoldestSnake.Visible = false;
            this.BoldestSnake.TextChanged += new System.EventHandler(this.BoldestSnake_TextChanged);
            // 
            // StaminaSnake
            // 
            this.StaminaSnake.Enabled = false;
            this.StaminaSnake.Location = new System.Drawing.Point(72, 49);
            this.StaminaSnake.Name = "StaminaSnake";
            this.StaminaSnake.Size = new System.Drawing.Size(140, 22);
            this.StaminaSnake.TabIndex = 8;
            this.StaminaSnake.Visible = false;
            this.StaminaSnake.TextChanged += new System.EventHandler(this.StaminaSnake_TextChanged);
            // 
            // VisionSnake
            // 
            this.VisionSnake.Enabled = false;
            this.VisionSnake.Location = new System.Drawing.Point(72, 77);
            this.VisionSnake.Name = "VisionSnake";
            this.VisionSnake.Size = new System.Drawing.Size(140, 22);
            this.VisionSnake.TabIndex = 9;
            this.VisionSnake.Visible = false;
            this.VisionSnake.TextChanged += new System.EventHandler(this.VisionSnake_TextChanged);
            // 
            // HearingSnake
            // 
            this.HearingSnake.Enabled = false;
            this.HearingSnake.Location = new System.Drawing.Point(72, 105);
            this.HearingSnake.Name = "HearingSnake";
            this.HearingSnake.Size = new System.Drawing.Size(140, 22);
            this.HearingSnake.TabIndex = 10;
            this.HearingSnake.Visible = false;
            this.HearingSnake.TextChanged += new System.EventHandler(this.HearingSnake_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.SmellSnake);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.Stamina);
            this.groupBox1.Controls.Add(this.Boldness);
            this.groupBox1.Controls.Add(this.BoldestSnake);
            this.groupBox1.Controls.Add(this.StaminaSnake);
            this.groupBox1.Controls.Add(this.VisionSnake);
            this.groupBox1.Controls.Add(this.HearingSnake);
            this.groupBox1.Location = new System.Drawing.Point(528, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 173);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Smartest Snake";
            this.groupBox1.Visible = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Smell";
            this.label3.Visible = false;
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // SmellSnake
            // 
            this.SmellSnake.Enabled = false;
            this.SmellSnake.Location = new System.Drawing.Point(72, 134);
            this.SmellSnake.Name = "SmellSnake";
            this.SmellSnake.Size = new System.Drawing.Size(139, 22);
            this.SmellSnake.TabIndex = 15;
            this.SmellSnake.Visible = false;
            this.SmellSnake.TextChanged += new System.EventHandler(this.SmellSnake_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Hearing";
            this.label6.Visible = false;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Sight";
            this.label5.Visible = false;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // Stamina
            // 
            this.Stamina.AutoSize = true;
            this.Stamina.Location = new System.Drawing.Point(7, 52);
            this.Stamina.Name = "Stamina";
            this.Stamina.Size = new System.Drawing.Size(59, 17);
            this.Stamina.TabIndex = 12;
            this.Stamina.Text = "Stamina";
            this.Stamina.Visible = false;
            this.Stamina.Click += new System.EventHandler(this.Stamina_Click);
            // 
            // Boldness
            // 
            this.Boldness.AutoSize = true;
            this.Boldness.Location = new System.Drawing.Point(30, 26);
            this.Boldness.Name = "Boldness";
            this.Boldness.Size = new System.Drawing.Size(36, 17);
            this.Boldness.TabIndex = 11;
            this.Boldness.Text = "Bold";
            this.Boldness.Visible = false;
            this.Boldness.Click += new System.EventHandler(this.label3_Click);
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(564, 361);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(140, 22);
            this.numericUpDown3.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(561, 341);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Number of Bunnies";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(758, 552);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "Smart Snake Remastered";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label2;
        private PictureBox pictureBox1;
        private TextBox BoldestSnake;
        private TextBox StaminaSnake;
        private TextBox VisionSnake;
        private TextBox HearingSnake;
        private GroupBox groupBox1;
        private Label Boldness;
        private Label label6;
        private Label label5;
        private Label Stamina;
        private TextBox SmellSnake;
        private Label label3;
        private NumericUpDown numericUpDown3;
        private Label label4;
    }
}

