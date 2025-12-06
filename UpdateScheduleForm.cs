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
    public partial class UpdateScheduleForm : Form
    {
        public UpdateScheduleForm()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSchedules();
        }

        private void UpdateScheduleForm_Load(object sender, EventArgs e)
        {
            LoadSchedules(); // your local method
            ScheduleDataHelper.LoadCourses(cmbCourse);
            ScheduleDataHelper.LoadInstructors(cmbInstructor);
            ScheduleDataHelper.LoadDays(cmbDay);
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

        private void dgvSchedules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            cmbCourse.Text = dgvSchedules.CurrentRow.Cells["CourseName"].Value.ToString();
            cmbInstructor.Text = dgvSchedules.CurrentRow.Cells["Instructor"].Value.ToString();
            cmbDay.Text = dgvSchedules.CurrentRow.Cells["DayOfWeek"].Value.ToString();
            dtStartTime.Value = Convert.ToDateTime(dgvSchedules.CurrentRow.Cells["StartTime"].Value);
            dtEndTime.Value = Convert.ToDateTime(dgvSchedules.CurrentRow.Cells["EndTime"].Value);
            txtRoom.Text = dgvSchedules.CurrentRow.Cells["Room"].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvSchedules.CurrentRow == null)
            {
                MessageBox.Show("Select a schedule first");
                return;
            }

            int scheduleId = Convert.ToInt32(
                dgvSchedules.CurrentRow.Cells["ScheduleId"].Value
            );

            int courseId = Convert.ToInt32(cmbCourse.SelectedValue);
            int instructorId = Convert.ToInt32(cmbInstructor.SelectedValue);
            string day = cmbDay.Text;
            TimeSpan start = dtStartTime.Value.TimeOfDay;
            TimeSpan end = dtEndTime.Value.TimeOfDay;
            string room = txtRoom.Text.Trim();

            string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(cs))
            {
                string q = @"
        UPDATE Schedules 
        SET CourseId=@c,
            InstructorId=@i,
            DayOfWeek=@d,
            StartTime=@s,
            EndTime=@e,
            Room=@r
        WHERE ScheduleId=@id";

                SqlCommand cmd = new SqlCommand(q, con);

                cmd.Parameters.AddWithValue("@c", courseId);
                cmd.Parameters.AddWithValue("@i", instructorId);
                cmd.Parameters.AddWithValue("@d", day);
                cmd.Parameters.AddWithValue("@s", start);
                cmd.Parameters.AddWithValue("@e", end);
                cmd.Parameters.AddWithValue("@r", room);
                cmd.Parameters.AddWithValue("@id", scheduleId);
                if (dtStartTime.Value >= dtEndTime.Value)
                {
                    MessageBox.Show("Start time must be earlier than End time");
                    return;
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Schedule Updated Successfully");
            LoadSchedules();
        }

    }
}
