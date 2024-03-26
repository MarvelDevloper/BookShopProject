using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopProject
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sanskar sanas\source\repos\BookShopProject\BookshopDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=false");

        private void populate()
        {
            conn.Open();
            String query = "select * from UserTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder sbuilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void Fillter()
        {
            conn.Open();
            String query = "select * from UserTbl where Uname='" + Catcbsearch.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder sbuilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            conn.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please Fill All Attributes !!!");
            }
            else
            {
                try
                {
                    conn.Open();
                    String query = "insert into UserTbl values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text.ToString() + "','" + textBox3.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Save Successfully");
                    conn.Close();
                    populate();
                    Reseet();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Catcbsearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fillter();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            populate();
            Catcbsearch.SelectedIndex = -1;
        }

        public void Reseet()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox3.Text = string.Empty;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Reseet();
        }
    }
}
