using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClassSchedulingSystem
{
    public static class ScheduleDataHelper
    {
        private static string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

        public static void LoadCourses(ComboBox cmbCourse)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT CourseId, CourseName FROM Courses";
                SqlDataAdapter da = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbCourse.DisplayMember = "CourseName";
                cmbCourse.ValueMember = "CourseId";
                cmbCourse.DataSource = dt;
                cmbCourse.SelectedIndex = -1;
            }
        }

        public static void LoadInstructors(ComboBox cmbInstructor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT UserId, userName FROM Users WHERE Role='Instructor'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbInstructor.DisplayMember = "userName";
                cmbInstructor.ValueMember = "UserId";
                cmbInstructor.DataSource = dt;
                cmbInstructor.SelectedIndex = -1;
            }
        }

        public static void LoadDays(ComboBox cmbDay)
        {
            cmbDay.Items.Clear();

            cmbDay.Items.Add("Monday");
            cmbDay.Items.Add("Tuesday");
            cmbDay.Items.Add("Wednesday");
            cmbDay.Items.Add("Thursday");
            cmbDay.Items.Add("Friday");
            cmbDay.Items.Add("Saturday");

            cmbDay.SelectedIndex = -1;
        }
    }
}
