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
    public partial class InstructorViewScheduleForm : Form
    {
        private string instructorUsername;

        public InstructorViewScheduleForm(string username)
        {
            InitializeComponent();
            instructorUsername = username;
        }


        private void InstructorViewScheduleForm_Load(object sender, EventArgs e)
        {
            LoadMySchedules();
        }
        private void LoadMySchedules()
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
                d.DepartmentName AS Department,
                s.DayOfWeek,
                s.StartTime,
                s.EndTime,
                s.Room
            FROM Schedules s
            JOIN Courses c ON s.CourseId = c.CourseId
            JOIN Users u ON s.InstructorId = u.UserId
            JOIN Departments d ON u.DepartmentId = d.DepartmentId
            WHERE u.userName = @username";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@username", instructorUsername);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvSchedules.DataSource = dt;
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMySchedules();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
