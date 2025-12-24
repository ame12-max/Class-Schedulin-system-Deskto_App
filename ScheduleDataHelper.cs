using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClassSchedulingSystem
{
    public static class ScheduleDataHelper
    {
        private static string cs =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=Class_scheduling;Integrated Security=True;TrustServerCertificate=True";

        // ================= COURSES =================

        // 🔹 Existing (ALL departments)
        public static void LoadCourses(ComboBox cmbCourse)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string q = "SELECT CourseId, CourseName FROM Courses";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbCourse.DisplayMember = "CourseName";
                cmbCourse.ValueMember = "CourseId";
                cmbCourse.DataSource = dt;
                cmbCourse.SelectedIndex = -1;
            }
        }

        // 🔹 NEW (Filtered by Department)
        public static void LoadCourses(ComboBox cmbCourse, int departmentId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string q = "SELECT CourseId, CourseName FROM Courses WHERE DepartmentId=@d";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.SelectCommand.Parameters.AddWithValue("@d", departmentId);

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
                string q = "SELECT UserId, userName FROM Users WHERE Role='Instructor'";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbInstructor.DisplayMember = "userName";
                cmbInstructor.ValueMember = "UserId";
                cmbInstructor.DataSource = dt;
                cmbInstructor.SelectedIndex = -1;
            }
        }

        public static void LoadInstructors(ComboBox cmbInstructor, int departmentId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string q = @"SELECT UserId, userName 
                             FROM Users 
                             WHERE Role='Instructor' AND DepartmentId=@d";

                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.SelectCommand.Parameters.AddWithValue("@d", departmentId);

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
