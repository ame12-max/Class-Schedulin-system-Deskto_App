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

namespace ClassSchedulingSystem
{
    public partial class DepartmentHeadDashboard : Form
    {
        private string _username;
        private int _userId;
        private string _Role;
        private int _DepartmentId;
        public DepartmentHeadDashboard(string username, int userId,string Role, int DepartmentId)
        {
            InitializeComponent();
            _username = username;
            _userId = userId;
            _Role = Role;
            _DepartmentId = DepartmentId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateScheduleForm f = new CreateScheduleForm(_DepartmentId);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateScheduleForm f = new UpdateScheduleForm(_DepartmentId);
            f.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteScheduleForm f = new DeleteScheduleForm();
            f.Show();
        }

        private void btnViewSchedule_Click(object sender, EventArgs e)
        {
            ViewScheduleForm f = new ViewScheduleForm();
            f.Show();
        }

        private void btnViewFeedback_Click(object sender, EventArgs e)
        {
            ViewFeedbackForm f = new ViewFeedbackForm();
            f.Show();
        }

        private void DepartmentHeadDashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "👤 " + _username;
            lblName.Text = "👤 " + _username;
            lblRolee.Text = " 📌  " + _Role;
            LoadDepartmentName();
        }


        private void LoadDepartmentName()
        {
            string cs = @"Data Source=.\SQLEXPRESS;
                  Initial Catalog=Class_scheduling;
                  Integrated Security=True;
                  TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT DepartmentName FROM Departments WHERE DepartmentId = @deptId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@deptId", _DepartmentId);

                con.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    lblDepartment.Text = "🏫 " + result.ToString();
                }
                else
                {
                    lblDepartment.Text = "🏫 Unknown Department";
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            loginForm login = new loginForm();
            login.Show();
            this.Close();
        }

    }
}
