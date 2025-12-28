using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using OnlineRecruitmentApp.Helpers;

namespace OnlineRecruitmentApp
{
    public partial class JobSeekerDashboardForm : Form
    {
        private DataGridView dgvJobs;
        private TextBox txtJobId;
        private TabControl tabControl;

        public JobSeekerDashboardForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form Settings
            this.ClientSize = new Size(1150, 750);
            this.Text = "Job Seeker Dashboard - JobConnect";
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
                Size = new Size(900, 750),
                BackColor = UIHelper.BackgroundColor,
                Padding = new Padding(30)
            };

            // Header
            Label headerLabel = new Label
            {
                Text = "👤 Job Seeker Dashboard",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(30, 20)
            };
            mainContent.Controls.Add(headerLabel);

            Label welcomeLabel = new Label
            {
                Text = $"Welcome, {Session.LoggedInUserName}! Find your dream job today.",
                Font = new Font("Segoe UI", 10),
                ForeColor = UIHelper.TextSecondary,
                AutoSize = true,
                Location = new Point(30, 55)
            };
            mainContent.Controls.Add(welcomeLabel);

            // Tab Control
            tabControl = new TabControl
            {
                Location = new Point(30, 100),
                Size = new Size(840, 620),
                Font = new Font("Segoe UI", 10)
            };

            // Browse Jobs Tab
            TabPage browseTab = new TabPage("💼 Browse Jobs");
            browseTab.BackColor = Color.White;
            browseTab.Padding = new Padding(20);
            CreateBrowseJobsTab(browseTab);
            tabControl.TabPages.Add(browseTab);

            // My Profile Tab
            TabPage profileTab = new TabPage("👤 My Profile");
            profileTab.BackColor = Color.White;
            profileTab.Padding = new Padding(20);
            CreateProfileTab(profileTab);
            tabControl.TabPages.Add(profileTab);

            // Saved Jobs Tab
            TabPage savedTab = new TabPage("⭐ Saved Jobs");
            savedTab.BackColor = Color.White;
            savedTab.Padding = new Padding(20);
            CreateSavedJobsTab(savedTab);
            tabControl.TabPages.Add(savedTab);

            mainContent.Controls.Add(tabControl);
            this.Controls.Add(mainContent);

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
                Text = "Job Seeker Account",
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
            string[] menuItems = { "📊 Dashboard", "💼 Browse Jobs", "👤 My Profile", "⭐ Saved Jobs", "📋 Applications" };
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
                Location = new Point(25, 680),
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += BtnLogout_Click;
            sidebar.Controls.Add(btnLogout);

            return sidebar;
        }

        private void CreateBrowseJobsTab(TabPage tab)
        {
            // Action Panel
            Panel actionPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(780, 60),
                BackColor = UIHelper.BackgroundColor
            };

            Label lblJobId = new Label
            {
                Text = "Job ID:",
                Font = UIHelper.LabelFont,
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(10, 20)
            };
            actionPanel.Controls.Add(lblJobId);

            txtJobId = new TextBox
            {
                Width = 100,
                Font = UIHelper.InputFont,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(70, 17)
            };
            actionPanel.Controls.Add(txtJobId);

            Button btnApply = UIHelper.CreatePrimaryButton("📤 Apply", 100, 35);
            btnApply.Location = new Point(200, 15);
            btnApply.Click += BtnApply_Click;
            actionPanel.Controls.Add(btnApply);

            Button btnSave = UIHelper.CreateSecondaryButton("⭐ Save Job", 110, 35);
            btnSave.Location = new Point(310, 15);
            btnSave.Click += BtnSave_Click;
            actionPanel.Controls.Add(btnSave);

            Button btnRefresh = UIHelper.CreateOutlineButton("🔄 Refresh", 100, 35);
            btnRefresh.Location = new Point(430, 15);
            btnRefresh.Click += (s, e) => LoadAvailableJobs();
            actionPanel.Controls.Add(btnRefresh);

            Button btnProfile = UIHelper.CreateOutlineButton("✏️ Update Profile", 130, 35);
            btnProfile.Location = new Point(540, 15);
            btnProfile.Click += (s, e) => tabControl.SelectedIndex = 1;
            actionPanel.Controls.Add(btnProfile);

            tab.Controls.Add(actionPanel);

            // Jobs Grid
            dgvJobs = new DataGridView
            {
                Location = new Point(20, 100),
                Size = new Size(780, 450)
            };
            UIHelper.StyleDataGridView(dgvJobs);
            dgvJobs.CellClick += DgvJobs_CellClick;
            tab.Controls.Add(dgvJobs);

            LoadAvailableJobs();
        }

        private void CreateProfileTab(TabPage tab)
        {
            Label title = UIHelper.CreateSectionTitle("📝 Update Your Profile");
            title.Location = new Point(20, 20);
            tab.Controls.Add(title);

            int y = 70;
            int spacing = 65;

            // Full Name
            Label lblName = new Label { Text = "Full Name", Font = UIHelper.LabelFont, Location = new Point(20, y), AutoSize = true };
            tab.Controls.Add(lblName);
            TextBox txtFullName = new TextBox { Name = "txtFullName", Width = 300, Font = UIHelper.InputFont, BorderStyle = BorderStyle.FixedSingle, Location = new Point(20, y + 22) };
            tab.Controls.Add(txtFullName);

            // City
            Label lblCity = new Label { Text = "City", Font = UIHelper.LabelFont, Location = new Point(350, y), AutoSize = true };
            tab.Controls.Add(lblCity);
            TextBox txtCity = new TextBox { Name = "txtCity", Width = 200, Font = UIHelper.InputFont, BorderStyle = BorderStyle.FixedSingle, Location = new Point(350, y + 22) };
            tab.Controls.Add(txtCity);

            y += spacing;

            // Industry
            Label lblIndustry = new Label { Text = "Industry", Font = UIHelper.LabelFont, Location = new Point(20, y), AutoSize = true };
            tab.Controls.Add(lblIndustry);
            TextBox txtIndustry = new TextBox { Name = "txtIndustry", Width = 300, Font = UIHelper.InputFont, BorderStyle = BorderStyle.FixedSingle, Location = new Point(20, y + 22) };
            tab.Controls.Add(txtIndustry);

            // Experience
            Label lblExp = new Label { Text = "Experience (Years)", Font = UIHelper.LabelFont, Location = new Point(350, y), AutoSize = true };
            tab.Controls.Add(lblExp);
            ComboBox cmbExp = new ComboBox { Name = "cmbExperience", Width = 150, Font = UIHelper.InputFont, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(350, y + 22) };
            for (int i = 0; i < 30; i++) cmbExp.Items.Add(i);
            cmbExp.SelectedIndex = 0;
            tab.Controls.Add(cmbExp);

            y += spacing;

            // CV Link
            Label lblCV = new Label { Text = "CV Link (URL)", Font = UIHelper.LabelFont, Location = new Point(20, y), AutoSize = true };
            tab.Controls.Add(lblCV);
            TextBox txtCV = new TextBox { Name = "txtCV", Width = 530, Font = UIHelper.InputFont, BorderStyle = BorderStyle.FixedSingle, Location = new Point(20, y + 22) };
            tab.Controls.Add(txtCV);

            y += spacing + 20;

            // Save Button
            Button btnSaveProfile = UIHelper.CreatePrimaryButton("💾 Save Profile", 150, 45);
            btnSaveProfile.Location = new Point(20, y);
            btnSaveProfile.Click += (s, e) => SaveProfile(tab);
            tab.Controls.Add(btnSaveProfile);

            // Load current profile
            LoadProfile(tab);
        }

        private void CreateSavedJobsTab(TabPage tab)
        {
            Button btnLoad = UIHelper.CreatePrimaryButton("🔄 Load Saved Jobs", 160, 35);
            btnLoad.Location = new Point(20, 20);
            tab.Controls.Add(btnLoad);

            DataGridView dgvSaved = new DataGridView
            {
                Name = "dgvSavedJobs",
                Location = new Point(20, 70),
                Size = new Size(780, 480)
            };
            UIHelper.StyleDataGridView(dgvSaved);
            tab.Controls.Add(dgvSaved);

            btnLoad.Click += (s, e) => LoadSavedJobs(dgvSaved);
        }

        private void LoadAvailableJobs()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT JOB_VACANCY_ID, TITLE, FIELD, LOCATION, EXPERIENCE_REQUIRED, DATE_POSTED FROM JOB_VACANCY WHERE IS_HIDDEN = 0", conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
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
                txtJobId.Text = dgvJobs.Rows[e.RowIndex].Cells["JOB_VACANCY_ID"].Value?.ToString();
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtJobId.Text))
            {
                UIHelper.ShowWarningMessage("Please select or enter a Job ID.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Check if already applied
                    SqlCommand checkCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM [APPLY] WHERE JOB_VACANCY_ID = @jobId AND ID = @id", conn);
                    checkCmd.Parameters.AddWithValue("@jobId", txtJobId.Text);
                    checkCmd.Parameters.AddWithValue("@id", Session.LoggedInUserId);

                    int existing = (int)checkCmd.ExecuteScalar();
                    if (existing > 0)
                    {
                        UIHelper.ShowWarningMessage("You have already applied to this job.");
                        return;
                    }

                    // Apply
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO [APPLY] (JOB_VACANCY_ID, ID, APPLY_DATE) VALUES (@jobId, @id, @date)", conn);
                    cmd.Parameters.AddWithValue("@jobId", txtJobId.Text);
                    cmd.Parameters.AddWithValue("@id", Session.LoggedInUserId);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);

                    cmd.ExecuteNonQuery();
                    UIHelper.ShowSuccessMessage("Applied successfully! Good luck!");
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error: " + ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtJobId.Text))
            {
                UIHelper.ShowWarningMessage("Please select or enter a Job ID.");
                return;
            }

            try
            {
                int jobId = int.Parse(txtJobId.Text);

                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Check if already saved
                    SqlCommand checkCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM SAVED_VACANCY WHERE JOB_VACANCY_ID = @jobId AND ID_SEEKER = @seekerId", conn);
                    checkCmd.Parameters.AddWithValue("@jobId", jobId);
                    checkCmd.Parameters.AddWithValue("@seekerId", Session.LoggedInUserId);

                    int existing = (int)checkCmd.ExecuteScalar();
                    if (existing > 0)
                    {
                        UIHelper.ShowWarningMessage("You have already saved this job.");
                        return;
                    }

                    // Save
                    SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO SAVED_VACANCY (ID_SEEKER, ID_VACANCY, ID, JOB_VACANCY_ID, DATE_SAVED)
                        VALUES (@seekerId, @jobId, @seekerId, @jobId, @date)", conn);
                    cmd.Parameters.AddWithValue("@seekerId", Session.LoggedInUserId);
                    cmd.Parameters.AddWithValue("@jobId", jobId);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);

                    cmd.ExecuteNonQuery();
                    UIHelper.ShowSuccessMessage("Job saved successfully!");
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error: " + ex.Message);
            }
        }

        private void LoadProfile(TabPage tab)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT FULL_NAME, CITY, INDUSTRY, EXPERIENCE_YEARS, CV_LINK FROM JOB_SEEKER WHERE ID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", Session.LoggedInUserId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ((TextBox)tab.Controls["txtFullName"]).Text = reader["FULL_NAME"]?.ToString();
                        ((TextBox)tab.Controls["txtCity"]).Text = reader["CITY"]?.ToString();
                        ((TextBox)tab.Controls["txtIndustry"]).Text = reader["INDUSTRY"]?.ToString();
                        ((TextBox)tab.Controls["txtCV"]).Text = reader["CV_LINK"]?.ToString();
                        int exp = Convert.ToInt32(reader["EXPERIENCE_YEARS"] ?? 0);
                        ((ComboBox)tab.Controls["cmbExperience"]).SelectedIndex = exp < 30 ? exp : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error loading profile: " + ex.Message);
            }
        }

        private void SaveProfile(TabPage tab)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        UPDATE [JOB_SEEKER] 
                        SET FULL_NAME = @fullname, EXPERIENCE_YEARS = @exp, CV_LINK = @cv, CITY = @city, INDUSTRY = @industry 
                        WHERE ID = @id", conn);

                    cmd.Parameters.AddWithValue("@id", Session.LoggedInUserId);
                    cmd.Parameters.AddWithValue("@fullname", ((TextBox)tab.Controls["txtFullName"]).Text);
                    cmd.Parameters.AddWithValue("@exp", ((ComboBox)tab.Controls["cmbExperience"]).SelectedIndex);
                    cmd.Parameters.AddWithValue("@cv", ((TextBox)tab.Controls["txtCV"]).Text);
                    cmd.Parameters.AddWithValue("@city", ((TextBox)tab.Controls["txtCity"]).Text);
                    cmd.Parameters.AddWithValue("@industry", ((TextBox)tab.Controls["txtIndustry"]).Text);

                    cmd.ExecuteNonQuery();
                    UIHelper.ShowSuccessMessage("Profile updated successfully!");
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error: " + ex.Message);
            }
        }

        private void LoadSavedJobs(DataGridView dgv)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT sv.*, jv.TITLE, jv.FIELD, jv.LOCATION 
                        FROM SAVED_VACANCY sv 
                        JOIN JOB_VACANCY jv ON sv.JOB_VACANCY_ID = jv.JOB_VACANCY_ID 
                        WHERE sv.ID_SEEKER = @seekerId", conn);
                    cmd.Parameters.AddWithValue("@seekerId", Session.LoggedInUserId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error loading saved jobs: " + ex.Message);
            }
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
