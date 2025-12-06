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
    public partial class ViewFeedbackForm : Form
    {
        public ViewFeedbackForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewFeedbackForm_Load(object sender, EventArgs e)
        {

            LoadFeedback();
        }
        private void LoadFeedback()
        {
            string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(cs))
            {
                string q = @"
        SELECT 
            u.userName AS CommentBy,
            u.Role,
            c.CourseName,
            s.DayOfWeek,
            s.StartTime,
            s.EndTime,
            s.Room,
            cm.CommentText,
            cm.CommentDate
        FROM Comments cm
        JOIN Users u ON cm.UserId = u.UserId
        JOIN Schedules s ON cm.ScheduleId = s.ScheduleId
        JOIN Courses c ON s.CourseId = c.CourseId
        ORDER BY cm.CommentDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvFeedback.DataSource = dt;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadFeedback();

        }
    }
}

