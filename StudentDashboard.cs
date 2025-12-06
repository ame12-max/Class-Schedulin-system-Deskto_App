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
    public partial class StudentDashboard : Form
    {
        private int loggedInUserId;

        public StudentDashboard(int userId)
        {
            InitializeComponent();
            loggedInUserId = userId;
        }


        private void dgvSchedules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StudentDashboard_Load(object sender, EventArgs e)
        {

            LoadSchedules();
        }

        private void LoadSchedules()
        {
            string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

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
        JOIN Users u ON s.InstructorId = u.UserId";

                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvSchedules.DataSource = dt;
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

    }
}
