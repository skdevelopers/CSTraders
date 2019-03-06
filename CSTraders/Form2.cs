using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CSTraders
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        DataTable table = new DataTable();
        
        private void Form2_Load(object sender, EventArgs e)
        {
            SqlConnection DBconn = new SqlConnection("Data Source=.; Initial Catalog=BSTraders;Integrated Security=True;Pooling=False");
            SqlDataAdapter SDA = new SqlDataAdapter("SELECT * FROM Student", DBconn);
            DataTable dt = new DataTable();
            SDA.Fill(dt);
            dataGridView1.DataSource = dt;
       
            // set datatable columns values
            table.Columns.Add("ProductNo", typeof(string));// data type string
            table.Columns.Add("Product", typeof(string));// datatype string
            table.Columns.Add("Qty", typeof(int));// datatype int
            table.Columns.Add("Units", typeof(int));// data type int
            table.Columns.Add("Price", typeof(decimal));// data type decimal
            table.Columns.Add("Total", typeof(decimal));// data type decimal

            dataGridView1.DataSource = table;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();
            Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //textBox5.Text = (Convert.ToInt32(textBox3.Text) * Convert.ToInt32(textBox4.Text)).ToString();
            Updateval(); UpdateTotal();
            table.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, TextBox5.Text, textBox6.Text);
            dataGridView1.DataSource = table;
        }

        private void TextBox5_TextChanged(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Tab)
                {
                    MessageBox.Show("The tab key was pressed while holding these modifier keys: "
                            + e.Modifiers.ToString());
                    Updateval();
                }
               
            }
            catch(System.FormatException ex) {
                MessageBox.Show(text: $"Error.\n{ex.Message}", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        //Used expression body for method
        public void Updateval() => textBox6.Text = (Convert.ToInt32(textBox3.Text) * Convert.ToInt32(textBox4.Text) * Convert.ToInt32(TextBox5.Text)).ToString();


        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=.; Initial Catalog=BSTraders;Integrated Security=True;Pooling=False");
            for (int i = 0; i < dataGridView1.Rows.Count -1; i++)
            {

                    string Qry = "INSERT INTO Product (ProductNo,Product,Qty,Units,Price,Total)VALUES(@ProductNo,@Product ,@Qty,@Units, @Price ,@Total) ";
                    SqlCommand comd = new SqlCommand(Qry, conn);
                    comd.Parameters.AddWithValue("@ProductNo", (dataGridView1.Rows[i].Cells[0].Value != DBNull.Value) ? dataGridView1.Rows[i].Cells[0].Value : "N/A");
                    comd.Parameters.AddWithValue("@Product", (dataGridView1.Rows[i].Cells[1].Value != DBNull.Value) ? dataGridView1.Rows[i].Cells[1].Value : "N/A");
                    comd.Parameters.AddWithValue("@Qty", (dataGridView1.Rows[i].Cells[2].Value != DBNull.Value) ? dataGridView1.Rows[i].Cells[2].Value : "N/A");
                    comd.Parameters.AddWithValue("@Units", (dataGridView1.Rows[i].Cells[3].Value != DBNull.Value) ? dataGridView1.Rows[i].Cells[3].Value : "N/A");
                    comd.Parameters.AddWithValue("@Price", (dataGridView1.Rows[i].Cells[4].Value != DBNull.Value) ? dataGridView1.Rows[i].Cells[4].Value : "N/A");
                    comd.Parameters.AddWithValue("@Total", (dataGridView1.Rows[i].Cells[5].Value != DBNull.Value) ? dataGridView1.Rows[i].Cells[5].Value : "N/A");
                    conn.Open();
                    comd.ExecuteNonQuery();
                    conn.Close();

                }
                
                //dataGridView1.DataSource = null;
                //    dataGridView1.Rows.Clear();
                //dataGridView1.Columns.Clear();
                //dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
               throw ex;
            }
        }
        private void PerformClick()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
        }
        private void UpdateTotal()
        {
            foreach(DataGridViewRow item in dataGridView1.Rows)
            {
                int sum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
                }
                textBox7.Text = sum.ToString();
                
            }
        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //it checks if the row index of the cell is greater than or equal to zero
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                TextBox5.Text = row.Cells[4].Value.ToString();
                textBox6.Text = row.Cells[5].Value.ToString();

            }
        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            UpdateTotal();
        }
    }
}
