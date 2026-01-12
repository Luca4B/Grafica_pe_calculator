namespace lab_02
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            numR = new NumericUpDown();
            numN = new NumericUpDown();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numN).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.LightGray;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(609, 528);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // numR
            // 
            numR.Location = new Point(660, 175);
            numR.Maximum = new decimal(new int[] { 250, 0, 0, 0 });
            numR.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            numR.Name = "numR";
            numR.Size = new Size(170, 27);
            numR.TabIndex = 1;
            numR.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // numN
            // 
            numN.Location = new Point(660, 86);
            numN.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            numN.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            numN.Name = "numN";
            numN.Size = new Size(170, 27);
            numN.TabIndex = 2;
            numN.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // button1
            // 
            button1.BackColor = Color.Chocolate;
            button1.Font = new Font("Tahoma", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(660, 244);
            button1.Name = "button1";
            button1.Size = new Size(170, 46);
            button1.TabIndex = 3;
            button1.Text = "Deseneaza";
            button1.TextImageRelation = TextImageRelation.ImageAboveText;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(660, 37);
            label1.Name = "label1";
            label1.Size = new Size(169, 28);
            label1.TabIndex = 4;
            label1.Text = "Numar de laturi";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(660, 127);
            label2.Name = "label2";
            label2.Size = new Size(60, 28);
            label2.TabIndex = 5;
            label2.Text = "Raza";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(887, 572);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(numN);
            Controls.Add(numR);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numR).EndInit();
            ((System.ComponentModel.ISupportInitialize)numN).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private NumericUpDown numR;
        private NumericUpDown numN;
        private Button button1;
        private Label label1;
        private Label label2;
    }
}
