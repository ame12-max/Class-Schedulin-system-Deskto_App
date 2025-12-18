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
    public partial class AddCommentForm : Form
    {
        private int loggedInUserId;

        public AddCommentForm(int userId)
        {
            InitializeComponent();
            loggedInUserId = userId;
        }


        private void AddCommentForm_Load(object sender, EventArgs e)
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
            s.ScheduleId,
            c.CourseName,
            s.DayOfWeek,
            s.StartTime,
            s.EndTime,
            s.Room
        FROM Schedules s
        JOIN Courses c ON s.CourseId = c.CourseId";

                SqlDataAdapter da = new SqlDataAdapter(q, con);
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

        private void btnAddComment_Click(object sender, EventArgs e)
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

        private void btnClear(object sender, EventArgs e)
        {
            txtComment.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
