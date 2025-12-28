using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using OnlineRecruitmentApp.Helpers;

namespace OnlineRecruitmentApp
{
    public partial class EmployerDashboardForm : Form
    {
        private TextBox txtTitle;
        private TextBox txtDescription;
        private TextBox txtField;
        private TextBox txtLocation;
        private ComboBox cmbExperience;
        private DataGridView dgvJobs;

        public EmployerDashboardForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form Settings
            this.ClientSize = new Size(1100, 700);
            this.Text = "Employer Dashboard - JobConnect";
            this.BackColor = UIHelper.BackgroundColor;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Sidebar
            Panel sidebar = CreateSidebar();
            this.Controls.Add(sidebar);

            // Main Content
            Panel mainContent = new Panel
            {
                Location = new Point(250, 0),
                Size = new Size(850, 700),
                BackColor = UIHelper.BackgroundColor,
                Padding = new Padding(30)
            };

            // Header
            Label headerLabel = new Label
            {
                Text = "🏢 Employer Dashboard",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(30, 20)
            };
            mainContent.Controls.Add(headerLabel);

            Label welcomeLabel = new Label
            {
                Text = $"Welcome back, {Session.LoggedInUserName}! Post and manage your job listings.",
                Font = new Font("Segoe UI", 10),
                ForeColor = UIHelper.TextSecondary,
                AutoSize = true,
                Location = new Point(30, 55)
            };
            mainContent.Controls.Add(welcomeLabel);

            // Post Job Card
            Panel postJobCard = new Panel
            {
                Location = new Point(30, 100),
                Size = new Size(790, 280),
                BackColor = Color.White
            };
            postJobCard.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, postJobCard.ClientRectangle,
                UIHelper.BorderColor, 1, ButtonBorderStyle.Solid,
                UIHelper.BorderColor, 1, ButtonBorderStyle.Solid,
                UIHelper.BorderColor, 1, ButtonBorderStyle.Solid,
                UIHelper.BorderColor, 1, ButtonBorderStyle.Solid);

            Label cardTitle = new Label
            {
                Text = "📝 Post New Job",
                Font = new Font("Segoe UI Semibold", 14),
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            postJobCard.Controls.Add(cardTitle);

            // Row 1
            Label lblTitle = new Label { Text = "Job Title", Font = UIHelper.LabelFont, Location = new Point(20, 55), AutoSize = true };
            postJobCard.Controls.Add(lblTitle);
            txtTitle = new TextBox { Width = 350, Font = UIHelper.InputFont, BorderStyle = BorderStyle.FixedSingle, Location = new Point(20, 75) };
            postJobCard.Controls.Add(txtTitle);

            Label lblField = new Label { Text = "Industry/Field", Font = UIHelper.LabelFont, Location = new Point(400, 55), AutoSize = true };
            postJobCard.Controls.Add(lblField);
            txtField = new TextBox { Width = 180, Font = UIHelper.InputFont, BorderStyle = BorderStyle.FixedSingle, Location = new Point(400, 75) };
            postJobCard.Controls.Add(txtField);

            Label lblExp = new Label { Text = "Experience (Years)", Font = UIHelper.LabelFont, Location = new Point(600, 55), AutoSize = true };
            postJobCard.Controls.Add(lblExp);
            cmbExperience = new ComboBox { Width = 160, Font = UIHelper.InputFont, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(600, 75) };
            for (int i = 0; i <= 20; i++) cmbExperience.Items.Add(i + " years");
            cmbExperience.SelectedIndex = 0;
            postJobCard.Controls.Add(cmbExperience);

            // Row 2
            Label lblLocation = new Label { Text = "Location", Font = UIHelper.LabelFont, Location = new Point(20, 115), AutoSize = true };
            postJobCard.Controls.Add(lblLocation);
            txtLocation = new TextBox { Width = 350, Font = UIHelper.InputFont, BorderStyle = BorderStyle.FixedSingle, Location = new Point(20, 135) };
            postJobCard.Controls.Add(txtLocation);

            Label lblDesc = new Label { Text = "Description", Font = UIHelper.LabelFont, Location = new Point(400, 115), AutoSize = true };
            postJobCard.Controls.Add(lblDesc);
            txtDescription = new TextBox { Width = 360, Height = 60, Font = UIHelper.InputFont, BorderStyle = BorderStyle.FixedSingle, Location = new Point(400, 135), Multiline = true };
            postJobCard.Controls.Add(txtDescription);

            // Buttons
            Button btnPost = UIHelper.CreatePrimaryButton("📤 Post Job", 130, 40);
            btnPost.Location = new Point(20, 220);
            btnPost.Click += BtnPost_Click;
            postJobCard.Controls.Add(btnPost);

            Button btnHide = UIHelper.CreateOutlineButton("👁️ Hide Job", 120, 40);
            btnHide.Location = new Point(160, 220);
            btnHide.Click += BtnHide_Click;
            postJobCard.Controls.Add(btnHide);

            Button btnShow = UIHelper.CreateSecondaryButton("👁️ Show Job", 120, 40);
            btnShow.Location = new Point(290, 220);
            btnShow.Click += BtnShow_Click;
            postJobCard.Controls.Add(btnShow);

            mainContent.Controls.Add(postJobCard);

            // My Jobs Section
            Label myJobsLabel = new Label
            {
                Text = "📋 My Posted Jobs",
                Font = new Font("Segoe UI Semibold", 14),
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(30, 400)
            };
            mainContent.Controls.Add(myJobsLabel);

            Button btnRefresh = UIHelper.CreateOutlineButton("🔄 Refresh", 100, 35);
            btnRefresh.Location = new Point(720, 395);
            btnRefresh.Click += BtnRefresh_Click;
            mainContent.Controls.Add(btnRefresh);

            dgvJobs = new DataGridView
            {
                Location = new Point(30, 440),
                Size = new Size(790, 230)
            };
            UIHelper.StyleDataGridView(dgvJobs);
            dgvJobs.CellClick += DgvJobs_CellClick;
            mainContent.Controls.Add(dgvJobs);

            this.Controls.Add(mainContent);

            LoadMyJobs();
            this.ResumeLayout(false);
        }

        private Panel CreateSidebar()
        {
            Panel sidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = UIHelper.PrimaryColor
            };

            Label logoLabel = new Label
            {
                Text = "💼 JobConnect",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(30, 30)
            };
            sidebar.Controls.Add(logoLabel);

            Label roleLabel = new Label
            {
                Text = "Employer Account",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(200, 255, 255, 255),
                AutoSize = true,
                Location = new Point(30, 60)
            };
            sidebar.Controls.Add(roleLabel);

            Panel separator = new Panel
            {
                BackColor = Color.FromArgb(100, 255, 255, 255),
                Location = new Point(20, 100),
                Size = new Size(210, 1)
            };
            sidebar.Controls.Add(separator);

            // Menu
            string[] menuItems = { "📊 Dashboard", "📝 Post Job", "📋 My Jobs", "👁️ Manage Visibility" };
            int y = 130;
            foreach (var item in menuItems)
            {
                Label menuLabel = new Label
                {
                    Text = item,
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(30, y),
                    Cursor = Cursors.Hand
                };
                menuLabel.MouseEnter += (s, e) => ((Label)s).ForeColor = Color.FromArgb(200, 255, 255, 255);
                menuLabel.MouseLeave += (s, e) => ((Label)s).ForeColor = Color.White;
                sidebar.Controls.Add(menuLabel);
                y += 40;
            }

            // Logout
            Button btnLogout = new Button
            {
                Text = "🚪 Logout",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(50, 255, 255, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Size = new Size(200, 40),
                Location = new Point(25, 630),
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += BtnLogout_Click;
            sidebar.Controls.Add(btnLogout);

            return sidebar;
        }

        private void LoadMyJobs()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(
                        "SELECT JOB_VACANCY_ID, TITLE, FIELD, LOCATION, EXPERIENCE_REQUIRED, IS_HIDDEN, DATE_POSTED FROM JOB_VACANCY WHERE EMP_ID = @empId",
                        conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@empId", Session.LoggedInUserId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvJobs.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error loading jobs: " + ex.Message);
            }
        }

        private void DgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvJobs.Rows[e.RowIndex];
                txtTitle.Text = row.Cells["TITLE"].Value?.ToString();
                txtField.Text = row.Cells["FIELD"].Value?.ToString();
                txtLocation.Text = row.Cells["LOCATION"].Value?.ToString();
                int exp = Convert.ToInt32(row.Cells["EXPERIENCE_REQUIRED"].Value ?? 0);
                cmbExperience.SelectedIndex = exp <= 20 ? exp : 0;
            }
        }

        private void BtnPost_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtField.Text))
            {
                UIHelper.ShowWarningMessage("Please fill in at least Job Title and Field.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO JOB_VACANCY 
                        (EMP_ID, DATE_POSTED, EMPLOYER_ID, TITLE, DESRIPTION, EXPERIENCE_REQUIRED, IS_HIDDEN, FIELD, LOCATION) 
                        VALUES (@EmpId, @DatePosted, @EmployerId, @Title, @Description, @Experience, @IsHidden, @Field, @Location)", conn);

                    cmd.Parameters.AddWithValue("@EmpId", Session.LoggedInUserId);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.Parameters.AddWithValue("@EmployerId", Session.LoggedInUserId);
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Experience", cmbExperience.SelectedIndex);
                    cmd.Parameters.AddWithValue("@IsHidden", false);
                    cmd.Parameters.AddWithValue("@Field", txtField.Text.Trim());
                    cmd.Parameters.AddWithValue("@Location", txtLocation.Text.Trim());

                    cmd.ExecuteNonQuery();
                    UIHelper.ShowSuccessMessage("Job posted successfully!");
                    LoadMyJobs();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error: " + ex.Message);
            }
        }

        private void BtnHide_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                UIHelper.ShowWarningMessage("Please select or enter a job title.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE JOB_VACANCY SET IS_HIDDEN = 1 WHERE TITLE = @Title AND EMP_ID = @EmpId", conn);
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@EmpId", Session.LoggedInUserId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        UIHelper.ShowSuccessMessage("Job hidden successfully!");
                        LoadMyJobs();
                    }
                    else
                    {
                        UIHelper.ShowWarningMessage("No matching job found.");
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error: " + ex.Message);
            }
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                UIHelper.ShowWarningMessage("Please select or enter a job title.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE JOB_VACANCY SET IS_HIDDEN = 0 WHERE TITLE = @Title AND EMP_ID = @EmpId", conn);
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@EmpId", Session.LoggedInUserId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        UIHelper.ShowSuccessMessage("Job is now visible!");
                        LoadMyJobs();
                    }
                    else
                    {
                        UIHelper.ShowWarningMessage("No matching job found.");
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error: " + ex.Message);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadMyJobs();
        }

        private void ClearForm()
        {
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtField.Text = "";
            txtLocation.Text = "";
            cmbExperience.SelectedIndex = 0;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            WelcomeForm welcomeForm = new WelcomeForm();
            welcomeForm.Show();
            this.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
