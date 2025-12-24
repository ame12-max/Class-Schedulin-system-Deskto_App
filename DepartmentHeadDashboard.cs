using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassSchedulingSystem
{
    public partial class DepartmentHeadDashboard : Form
    {
        private string _username;
        private int _userId;
        private string _Role;
        public DepartmentHeadDashboard(string username, int userId,string Role)
        {
            InitializeComponent();
            _username = username;
            _userId = userId;
            _Role = Role;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateScheduleForm f = new CreateScheduleForm();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateScheduleForm f = new UpdateScheduleForm();
            f.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteScheduleForm f = new DeleteScheduleForm();
            f.Show();
        }

        private void btnViewSchedule_Click(object sender, EventArgs e)
        {
            ViewScheduleForm f = new ViewScheduleForm();
            f.Show();
        }

        private void btnViewFeedback_Click(object sender, EventArgs e)
        {
            ViewFeedbackForm f = new ViewFeedbackForm();
            f.Show();
        }

        private void DepartmentHeadDashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "👤 " + _username;
            lblName.Text = "👤 " + _username;
            lblRolee.Text = " 📌  " + _Role;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            loginForm login = new loginForm();
            login.Show();
            this.Close();
        }

    }
}
