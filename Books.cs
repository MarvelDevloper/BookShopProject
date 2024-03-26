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

namespace BookShopProject
{
    public partial class Books : Form
    {
        public Books()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sanskar sanas\source\repos\BookShopProject\BookshopDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=false");
        private void populate()
        {
            conn.Open();
            String query = "select * from BookTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query,conn);
            SqlCommandBuilder sbuilder= new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];  
            conn.Close();
        }

        private void Fillter()
        {
            conn.Open();
            String query = "select * from BookTbl where BCategory='"+Catcbsearch.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder sbuilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (Btitle.Text == "" || Bauthor.Text == "" || Qtytb.Text == "" || BookPrice.Text == "" || Bcatcb.SelectedIndex == -1)
            {
                MessageBox.Show("Please Fill All Attributes !!!");
            }
            else
            {
                try
                {
                    conn.Open();
                    String query = "insert into BookTbl values('" + Btitle.Text + "','" + Bauthor.Text + "','" + Bcatcb.SelectedItem.ToString() + "','" + Qtytb.Text + "','" + BookPrice.Text + "')";
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

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void Catcbsearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fillter();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            populate();
            Catcbsearch.SelectedIndex = -1;
        }

        private void Catcbsearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void Reseet()
        {
            Btitle.Text = string.Empty;
            Bauthor.Text = string.Empty;
            BookPrice.Text = string.Empty;
            Qtytb.Text = string.Empty;
            Bcatcb.Text = string.Empty; 
            Bcatcb.SelectedIndex = -1;
        }
        private void Reset_Click(object sender, EventArgs e)
        {
            Reseet();
        }

        int key = 0;
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Btitle.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            Bauthor.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString();
            Qtytb.Text = BookDGV.SelectedRows[0].Cells[3].Value.ToString();
            BookPrice.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Users u=new Users();
            u.ShowDialog();
            this.Hide();
        }
    }
}
