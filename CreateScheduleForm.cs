using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClassSchedulingSystem
{
    public partial class CreateScheduleForm : Form
    {
        private int _DepartmentId;

        
        public CreateScheduleForm(int departmentId)
        {
            InitializeComponent();
            _DepartmentId = departmentId;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbCourse.SelectedIndex = -1;
            cmbInstructor.SelectedIndex = -1;
            cmbDay.SelectedIndex = -1;
            txtRoom.Clear();
        }

        private void CreateScheduleForm_Load(object sender, EventArgs e)
        {
            ScheduleDataHelper.LoadCourses(cmbCourse, _DepartmentId);
            ScheduleDataHelper.LoadInstructors(cmbInstructor, _DepartmentId);
            ScheduleDataHelper.LoadDays(cmbDay);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbCourse.SelectedIndex == -1 ||
                cmbInstructor.SelectedIndex == -1 ||
                cmbDay.SelectedIndex == -1 ||
                txtRoom.Text == "")
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            int courseId = Convert.ToInt32(cmbCourse.SelectedValue);
            int instructorId = Convert.ToInt32(cmbInstructor.SelectedValue);
            string day = cmbDay.Text;
            TimeSpan startTime = dtStartTime.Value.TimeOfDay;
            TimeSpan endTime = dtEndTime.Value.TimeOfDay;
            string room = txtRoom.Text.Trim();

            if (startTime >= endTime)
            {
                MessageBox.Show("The End Time Must Be Greater");
                return;
            }

            string cs =
                @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"INSERT INTO Schedules
                                 (CourseId, InstructorId, DayOfWeek, StartTime, EndTime, Room)
                                 VALUES
                                 (@c, @i, @d, @s, @e, @r)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@c", courseId);
                cmd.Parameters.AddWithValue("@i", instructorId);
                cmd.Parameters.AddWithValue("@d", day);
                cmd.Parameters.AddWithValue("@s", startTime);
                cmd.Parameters.AddWithValue("@e", endTime);
                cmd.Parameters.AddWithValue("@r", room);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Schedule Added Successfully!");

            btnClear_Click(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
