using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1._1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            Form form = Application.OpenForms[0];
            form.Show();
            this.Close();
        
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int n = Convert.ToInt32(textBox1.Text); 
                int m = Convert.ToInt32(textBox2.Text); 
                Arrays2D twoDimArray = new Arrays2D(n, m);
                dataGridView1.Columns.Clear();
                DataGridViewTextBoxColumn dvage;
                for (int i = 0; i < m; i++)
                {
                    dvage = new DataGridViewTextBoxColumn();
                    dvage.Width = 70;
                    dataGridView1.Columns.Add(dvage);
                }
                dataGridView1.Rows.Clear();
                dataGridView1.RowCount = twoDimArray.X_length;
                dataGridView1.ColumnCount = twoDimArray.Y_length;
                for (int i = 0; i < twoDimArray.X_length; i++)
                {
                    for (int j = 0; j < twoDimArray.Y_length; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = twoDimArray[i, j].ToString();
                    }
                }

                int[] sums = twoDimArray.Sum();
                label3.Text = "Сума: \n";
                for (int i = 0; i < sums.Length; i++)
                {
                    label3.Text += $"Рядок {i + 1}: {sums[i]}\n";

                }
                label4.Text = twoDimArray.max().ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
