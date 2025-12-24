using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClassSchedulingSystem
{
    public partial class UpdateScheduleForm : Form
    {
        private readonly int _DepartmentId;

        // 🔹 Department-aware constructor
        public UpdateScheduleForm(int departmentId)
        {
            InitializeComponent();
            _DepartmentId = departmentId;
        }

        private void UpdateScheduleForm_Load(object sender, EventArgs e)
        {
            LoadSchedules();

            // 🔐 Department-filtered loading
            ScheduleDataHelper.LoadCourses(cmbCourse, _DepartmentId);
            ScheduleDataHelper.LoadInstructors(cmbInstructor, _DepartmentId);
            ScheduleDataHelper.LoadDays(cmbDay);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSchedules();
        }

        // ================= LOAD SCHEDULES (DEPARTMENT ONLY) =================
        private void LoadSchedules()
        {
            string cs =
                @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

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
                    JOIN Users u ON s.InstructorId = u.UserId
                    WHERE c.DepartmentId = @d";

                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.SelectCommand.Parameters.AddWithValue("@d", _DepartmentId);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvSchedules.DataSource = dt;
            }
        }

        // ================= GRID SELECT =================
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

        // ================= UPDATE =================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvSchedules.CurrentRow == null)
            {
                MessageBox.Show("Select a schedule first");
                return;
            }

            if (dtStartTime.Value >= dtEndTime.Value)
            {
                MessageBox.Show("Start time must be earlier than End time");
                return;
            }

            int scheduleId = Convert.ToInt32(
                dgvSchedules.CurrentRow.Cells["ScheduleId"].Value);

            int courseId = Convert.ToInt32(cmbCourse.SelectedValue);
            int instructorId = Convert.ToInt32(cmbInstructor.SelectedValue);

            string cs =
                @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

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
                cmd.Parameters.AddWithValue("@d", cmbDay.Text);
                cmd.Parameters.AddWithValue("@s", dtStartTime.Value.TimeOfDay);
                cmd.Parameters.AddWithValue("@e", dtEndTime.Value.TimeOfDay);
                cmd.Parameters.AddWithValue("@r", txtRoom.Text.Trim());
                cmd.Parameters.AddWithValue("@id", scheduleId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Schedule Updated Successfully");
            LoadSchedules();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
