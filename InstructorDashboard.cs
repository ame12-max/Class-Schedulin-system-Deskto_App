using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ClassSchedulingSystem.loginForm;

namespace ClassSchedulingSystem
{
    public partial class InstructorDashboard : Form
    {
        private string loggedInUsername;
        private int loggedInUserId;
        private string loggedInRole;


        private void InstructorDashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "👤  " + loggedInUsername;
            lblName.Text = "👤 " + loggedInUsername;
            lblRolee.Text = " 📌  " + loggedInRole;
        }

        public InstructorDashboard(string username, int userId,string Role)
        {
            InitializeComponent();
            loggedInUsername = username;
            loggedInUserId = userId;
            loggedInRole = Role;

        }


        private void btnViewMySchedule_Click(object sender, EventArgs e)
        {
            InstructorViewScheduleForm f =
                new InstructorViewScheduleForm(loggedInUsername);
            f.Show();
        }

        private void btnAddComment_Click(object sender, EventArgs e)
        {
            InstructorAddComment f = new InstructorAddComment(loggedInUsername , loggedInUserId);
            f.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            loginForm login = new loginForm();
            login.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}