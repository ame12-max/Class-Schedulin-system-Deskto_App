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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ClassSchedulingSystem
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;  
            }
        }

        public class User
        {
            public string UserName { get; set; }

            public string Role { get; set; }
            public string Password { get; set; }

            public int UserId { get; set; }
            public int Phone { get; set; }


        }

        private User GetUserByUsername(string userName)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT UserId, UserName, Password, Role, Phone FROM Users WHERE UserName=@u";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@u", userName);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),  // REQUIRED now
                        Role = reader["Role"].ToString(),
                        Phone = Convert.ToInt32(reader["Phone"])
                    };
                }
            }

            return null;
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            

            string userName = txtUser.Text;
            string password = txtPass.Text;
            int phone = int.Parse(txtPhone.Text);

            var user = GetUserByUsername(userName);

            if (user == null)
            {
                MessageBox.Show("Username does not exist");
                return;
            }

            if (user.Password != password)
            {
                MessageBox.Show("Incorrect password");
                return;
            }

            if (user.Phone != phone)
            {
                MessageBox.Show("Incorrect phone number");
                return;
            }

            MessageBox.Show($"Welcome {user.UserName}, Your Role: {user.Role}");

            if (user.Role == "Instructor")
                new InstructorDashboard(user.UserName, user.UserId,user.Role).Show();
            else if (user.Role == "Student")
                new StudentDashboard(user.UserId, user.UserName, user.Role).Show();
            else if (user.Role == "Department Head")
                new DepartmentHeadDashboard(user.UserName, user.UserId, user.Role).Show();

            this.Hide();
        }




        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void loginForm_Load(object sender, EventArgs e)
        {

        }
    }
    }
