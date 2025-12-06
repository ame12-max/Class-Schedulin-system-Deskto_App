namespace ClassSchedulingSystem
{
    partial class InstructorDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.btnViewMySchedule = new System.Windows.Forms.Button();
            this.btnAddComment = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(242, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(409, 47);
            this.label2.TabIndex = 14;
            this.label2.Text = "Instructor Dashboard";
            // 
            // btnViewMySchedule
            // 
            this.btnViewMySchedule.BackColor = System.Drawing.Color.Lime;
            this.btnViewMySchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewMySchedule.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnViewMySchedule.Location = new System.Drawing.Point(125, 371);
            this.btnViewMySchedule.Name = "btnViewMySchedule";
            this.btnViewMySchedule.Size = new System.Drawing.Size(406, 75);
            this.btnViewMySchedule.TabIndex = 15;
            this.btnViewMySchedule.Text = "View My Schedule";
            this.btnViewMySchedule.UseVisualStyleBackColor = false;
            this.btnViewMySchedule.Click += new System.EventHandler(this.btnViewMySchedule_Click);
            // 
            // btnAddComment
            // 
            this.btnAddComment.BackColor = System.Drawing.Color.Lime;
            this.btnAddComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddComment.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAddComment.Location = new System.Drawing.Point(632, 376);
            this.btnAddComment.Name = "btnAddComment";
            this.btnAddComment.Size = new System.Drawing.Size(360, 70);
            this.btnAddComment.TabIndex = 20;
            this.btnAddComment.Text = "Add Comment";
            this.btnAddComment.UseVisualStyleBackColor = false;
            this.btnAddComment.Click += new System.EventHandler(this.btnAddComment_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Red;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnLogout.Location = new System.Drawing.Point(1093, 380);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(222, 66);
            this.btnLogout.TabIndex = 23;
            this.btnLogout.Text = "Log Out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // InstructorDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1437, 637);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnAddComment);
            this.Controls.Add(this.btnViewMySchedule);
            this.Controls.Add(this.label2);
            this.Name = "InstructorDashboard";
            this.Text = "InstructorDashboard";
            this.Load += new System.EventHandler(this.InstructorDashboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnViewMySchedule;
        private System.Windows.Forms.Button btnAddComment;
        private System.Windows.Forms.Button btnLogout;
    }
}