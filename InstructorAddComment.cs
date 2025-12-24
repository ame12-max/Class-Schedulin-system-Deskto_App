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
    public partial class InstructorAddComment : Form
    {
        string instructorUsername;
        int loggedInUserId;
        public InstructorAddComment(string userName, int userId)
        {
            InitializeComponent();
            instructorUsername = userName;
            loggedInUserId = userId;
        }


       
        private void LoadMySchedules()
        {
            string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(cs))
            {
                string q = @"
        SELECT 
            c.CourseName,
            s.ScheduleId,
            s.DayOfWeek,
            s.StartTime,
            s.EndTime,
            s.Room
        FROM Schedules s
        JOIN Courses c ON s.CourseId = c.CourseId
        JOIN Users u ON s.InstructorId = u.UserId
        WHERE u.userName = @username";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@username", instructorUsername);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvSchedules.DataSource = dt;
            }
        }



        private void dgvSchedules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }


      
     

        private void btnSave_Click(object sender, EventArgs e)
        {
   

            if (dgvSchedules.CurrentRow == null)
            {
                MessageBox.Show("Select a schedule first.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtComment.Text))
            {
                MessageBox.Show("Please enter a comment.");
                return;
            }

            int scheduleId = Convert.ToInt32(
                dgvSchedules.CurrentRow.Cells["ScheduleId"].Value
            );

            string comment = txtComment.Text.Trim();

            string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(cs))
            {
                string q = @"INSERT INTO Comments (UserId, ScheduleId, CommentText)
                     VALUES (@u, @s, @c)";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@u", loggedInUserId);
                cmd.Parameters.AddWithValue("@s", scheduleId);
                cmd.Parameters.AddWithValue("@c", comment);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Comment added successfully.");
            txtComment.Clear();
        }

        private void InstructorAddComment_Load(object sender, EventArgs e)
        {
            LoadMySchedules();

        }

        private void btnClearr_Click(object sender, EventArgs e)
        {
            txtComment.Clear();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
