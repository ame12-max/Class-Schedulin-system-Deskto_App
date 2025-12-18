using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace ClassSchedulingSystem
{
  public partial class CreateScheduleForm : Form

    {
        public CreateScheduleForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

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
            ScheduleDataHelper.LoadCourses(cmbCourse);
            ScheduleDataHelper.LoadInstructors(cmbInstructor);
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

            string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

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
                if (startTime >= endTime)
                {
                    MessageBox.Show("The End Time Must Be Greater");

                }
                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Schedule Added Successfully!");

            // Clear after save
            cmbCourse.SelectedIndex = -1;
            cmbInstructor.SelectedIndex = -1;
            cmbDay.SelectedIndex = -1;
            txtRoom.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
