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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRUD_Operations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string path = AppDomain.CurrentDomain.BaseDirectory.ToLower().Replace("\\bin", "").Replace("\\debug", "").Replace("\\release", "").TrimEnd('\\');
        static string connStr = @"Data Source=(localdb)\elpekaan;AttachDbFilename=" + path + "\\PersonalDB.mdf;Integrated Security=True";
        static string addStr = "INSERT INTO PersonalTable VALUES (@Name, @Surname, @Tel, @City)";
        static string updateStr = "UPDATE PersonalTable SET Name = @Name, Surname = @Surname, Tel = @Tel, City = @City where Id = @Id";
        static string deleteStr = "DELETE FROM PersonalTable WHERE Id = @Id";
        static string getTableStr = "Select * From PersonalTable";

        private void Form1_Load(object sender, EventArgs e)
        {
            getTable();
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            createTable();
            clearTable();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateTable();
            clearTable();

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteTable();
            clearTable();

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearTable();
            clearTable();

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getDataScreen();
        }

        public void getTable()
        {
            Form1 form = new Form1();
            SqlConnection con = new SqlConnection(connStr);

            con.Open();
            SqlCommand cmd = new SqlCommand(getTableStr, con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
        public void createTable()
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(mtxtTel.Text) || string.IsNullOrEmpty(cmbCity.Text))
                {
                    MessageBox.Show("Please fill in all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlConnection con = new SqlConnection(connStr);

                con.Open();
                SqlCommand cmd = new SqlCommand(addStr, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Surname", txtSurname.Text);
                cmd.Parameters.AddWithValue("@Tel", mtxtTel.Text);
                cmd.Parameters.AddWithValue("@City", cmbCity.Text);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Successfully Saved", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlDataAdapter adapter = new SqlDataAdapter(getTableStr, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        public void updateTable()
        {
            try
            {
                SqlConnection con = new SqlConnection(connStr);

                con.Open();
                SqlCommand cmd = new SqlCommand(updateStr, con);
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(txtId.Text));
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Surname", txtSurname.Text);
                cmd.Parameters.AddWithValue("@Tel", mtxtTel.Text);
                cmd.Parameters.AddWithValue("@City", cmbCity.Text);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Successfully Updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlDataAdapter adapter = new SqlDataAdapter(getTableStr, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        public void deleteTable()
        {
            try
            {
                SqlConnection con = new SqlConnection(connStr);

                con.Open();
                SqlCommand cmd = new SqlCommand(deleteStr, con);
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(txtId.Text));
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Successfully Deleted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlDataAdapter adapter = new SqlDataAdapter(getTableStr, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        public void clearTable()
        {
            try
            {
                txtId.Text = "";
                txtName.Text = "";
                txtSurname.Text = "";
                mtxtTel.Text = "";
                cmbCity.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        public void getDataScreen()
        {
            int selected = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[selected].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[selected].Cells[1].Value.ToString();
            txtSurname.Text = dataGridView1.Rows[selected].Cells[2].Value.ToString();
            mtxtTel.Text = dataGridView1.Rows[selected].Cells[3].Value.ToString();
            cmbCity.Text = dataGridView1.Rows[selected].Cells[4].Value.ToString();
        }
    }
}
