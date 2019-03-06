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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        SqlConnection DBconn = new SqlConnection("Data Source=.; Initial Catalog=BSTraders;Integrated Security=True;Pooling=False");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dataSearch;
        
        private void Button1_Click(object sender, EventArgs e)
        {
         if (textBox1.Text != "" & textBox2.Text != "" & textBox3.Text != "" & textBox4.Text != "" & comboBox1.Text != "")
            {            
                DBconn.Open();
                cmd.CommandText = $"INSERT INTO student(rollno,name,father,mobile,date,gender) Values('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{dateTimePicker1.Value.ToString()}','{comboBox1.Text}')";
                cmd.ExecuteNonQuery();
                DBconn.Close();
                MessageBox.Show("Data Saved Successfully ...");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.SelectedIndex = -1;
                Search();
            }
        }
        private void Search()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            cmd.CommandText = "SELECT * FROM Student";
            DBconn.Open();
            dataSearch = cmd.ExecuteReader();
            if (dataSearch.HasRows)
            {
                while (dataSearch.Read())
                {
                    listBox1.Items.Add((dataSearch["rollno"] != DBNull.Value) ? dataSearch["rollno"].ToString() : "N/A");
                    listBox2.Items.Add((dataSearch["name"] != DBNull.Value) ? dataSearch["name"].ToString() : "N/A");
                    listBox3.Items.Add((dataSearch["father"] != DBNull.Value) ? dataSearch["father"].ToString() : "N/A");
                    listBox4.Items.Add((dataSearch["mobile"] != DBNull.Value) ? dataSearch["mobile"].ToString() : "N/A");
                    listBox5.Items.Add((dataSearch["date"] != DBNull.Value) ? dataSearch["date"].ToString() : "N/A");
                    listBox6.Items.Add((dataSearch["gender"] != DBNull.Value) ? dataSearch["gender"].ToString() : "N/A");
                }
            }
            DBconn.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cmd.Connection = DBconn;
        }

        private void Button4_Click(object sender, EventArgs e) => Search();

        private void ListBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox list = sender as ListBox;
            if (list.SelectedIndex != -1)
            {
                listBox1.SelectedIndex = list.SelectedIndex;
                listBox2.SelectedIndex = list.SelectedIndex;
                listBox3.SelectedIndex = list.SelectedIndex;
                listBox4.SelectedIndex = list.SelectedIndex;
                listBox5.SelectedIndex = list.SelectedIndex;
                listBox6.SelectedIndex = list.SelectedIndex;

                //retrive to TextBox of AllowDrop Events

                textBox1.Text = listBox1.SelectedItem.ToString();
                textBox2.Text = listBox2.SelectedItem.ToString();
                textBox3.Text = listBox3.SelectedItem.ToString();
                textBox4.Text = listBox4.SelectedItem.ToString();
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.Text = listBox6.SelectedItem.ToString();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //Update
            if (textBox1.Text != "" & textBox2.Text != "" & textBox3.Text != "" & textBox4.Text != "" & comboBox1.Text != "")
            {
                DBconn.Open();
                cmd.CommandText = $"UPDATE student SET rollno ='{textBox1.Text}' ,name ='{textBox2.Text}',father ='{textBox3.Text}',mobile ='{textBox4.Text}',date ='{dateTimePicker1.Value.ToString()}',gender ='{comboBox1.Text}'  WHERE rollno = '{listBox1.SelectedItem.ToString()}'";
                cmd.ExecuteNonQuery();
                DBconn.Close();
                MessageBox.Show("Data Updated Successfully ...");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.SelectedIndex = 1;
                Search();
            }
        }
        enum weekdays
        {
            monday
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" )
            {
                DBconn.Open();
                cmd.CommandText = $"DELETE FROM student WHERE rollno ='{textBox1.Text}'";
                cmd.ExecuteNonQuery();
                DBconn.Close();
                MessageBox.Show("Data Delete Successfully ...");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.SelectedIndex = -1;
                Search();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
            Hide();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
} 
