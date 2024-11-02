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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace nam
{
    public partial class Form1 : Form

    {
        private SqlConnection connection;
        public Form1()
        {
            InitializeComponent();
            string connectionString = "Server=.;Database=Student;Integrated Security=True;";
            connection = new SqlConnection(connectionString);
            LoadStudents();
        }
        private void LoadStudents()
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Students", connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            listView1.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row["Id"].ToString());
                item.SubItems.Add(row["Name"].ToString());
                item.SubItems.Add(row["Address"].ToString());
                item.SubItems.Add(row["Phone"].ToString());
                listView1.Items.Add(item);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string address = txtAddress.Text;
            string phone = txtPhone.Text;

            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Students (Name, Address, Phone) VALUES (@name, @address, @phone)", connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@phone", phone);
            command.ExecuteNonQuery();
            connection.Close();

            LoadStudents();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên để sửa.");
                return;
            }

            int id = Convert.ToInt32(listView1.SelectedItems[0].Text);
            string name = txtName.Text;
            string address = txtAddress.Text;
            string phone = txtPhone.Text;

            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE Students SET Name=@name, Address=@address, Phone=@phone WHERE Id=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@phone", phone);
            command.ExecuteNonQuery();
            connection.Close();

            LoadStudents();
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên để xóa.");
                return;
            }

            int id = Convert.ToInt32(listView1.SelectedItems[0].Text);

            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Students WHERE Id=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();

            LoadStudents();
            ClearFields();
        }

        private void ClearFields()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            ListViewItem item = listView1.SelectedItems[0];
            txtId.Text = item.Text;
            txtName.Text = item.SubItems[1].Text;
            txtAddress.Text = item.SubItems[2].Text;
            txtPhone.Text = item.SubItems[3].Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
   
