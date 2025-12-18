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
    public partial class DeleteScheduleForm : Form
    {
        public DeleteScheduleForm()
        {
            InitializeComponent();
        }

        private void DeleteScheduleForm_Load(object sender, EventArgs e)
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSchedules.CurrentRow == null)
            {
                MessageBox.Show("Please select a schedule to delete.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this schedule?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
                return;

            int scheduleId = Convert.ToInt32(
                dgvSchedules.CurrentRow.Cells["ScheduleId"].Value
            );

            string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(cs))
            {
                string q = "DELETE FROM Schedules WHERE ScheduleId = @id";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", scheduleId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Schedule deleted successfully.");
            LoadSchedules();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSchedules();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
