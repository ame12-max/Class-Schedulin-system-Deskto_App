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
using static ClassSchedulingSystem.loginForm;

namespace ClassSchedulingSystem
{
    public partial class StudentDashboard : Form
    {
        private int loggedInUserId;
        private string _username;
        private string _Role;
        private int _DepartmentId;

        public StudentDashboard(int userId, string username,string Role, int DepartmentId)
        {
            InitializeComponent();
            loggedInUserId = userId;
            _username = username;
            _Role = Role;
            _DepartmentId = DepartmentId;
        }


        private void dgvSchedules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StudentDashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Welcome " + _username;
            lblName.Text = "👤 " + _username;
            lblRolee.Text = " 📌  " + _Role;

            LoadDepartmentName();
            LoadSchedules();
        }

        private void LoadSchedules()
        {
            string cs = @"Data Source=.\SQLEXPRESS;
                  Initial Catalog=Class_scheduling;
                  Integrated Security=True;
                  TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(cs))
            {
                string q = @"
        SELECT 
            c.CourseName,
            u.userName AS Instructor,
            s.DayOfWeek,
            s.StartTime,
            s.EndTime,
            s.Room
        FROM Schedules s
        JOIN Courses c ON s.CourseId = c.CourseId
        JOIN Users u ON s.InstructorId = u.UserId
        WHERE c.DepartmentId = @DepartmentId";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@DepartmentId", _DepartmentId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvSchedules.DataSource = dt;
            }
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


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSchedules();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddComment_Click(object sender, EventArgs e)
        {
            AddCommentForm f = new AddCommentForm(loggedInUserId);
            f.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            loginForm login = new loginForm();
            login.Show();
            this.Close();
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
