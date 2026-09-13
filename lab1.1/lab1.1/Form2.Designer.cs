namespace lab1._1
{
    partial class Form2
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
            dataGridView1 = new DataGridView();
            A = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ControlLight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { A });
            dataGridView1.Location = new Point(26, 24);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(196, 339);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // A
            // 
            A.HeaderText = "A";
            A.Name = "A";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(304, 106);
            label1.Name = "label1";
            label1.Size = new Size(44, 25);
            label1.TabIndex = 1;
            label1.Text = "N =";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F);
            label3.Location = new Point(304, 151);
            label3.Name = "label3";
            label3.Size = new Size(173, 25);
            label3.TabIndex = 3;
            label3.Text = "Добуток кратних 7";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(430, 207);
            label4.Name = "label4";
            label4.Size = new Size(0, 15);
            label4.TabIndex = 4;
            label4.Click += label4_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(354, 108);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(76, 23);
            textBox1.TabIndex = 5;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.Yellow;
            button1.Font = new Font("Segoe UI", 14F);
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(581, 147);
            button1.Name = "button1";
            button1.Size = new Size(123, 32);
            button1.TabIndex = 6;
            button1.Text = "Обрахувати";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Coral;
            button2.Font = new Font("Segoe UI", 14F);
            button2.Location = new Point(440, 322);
            button2.Name = "button2";
            button2.Size = new Size(123, 41);
            button2.TabIndex = 7;
            button2.Text = "Закрити";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MistyRose;
            ClientSize = new Size(754, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn A;
        private Label label1;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
    }
}