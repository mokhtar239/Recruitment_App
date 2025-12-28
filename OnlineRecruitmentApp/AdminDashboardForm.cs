using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using OnlineRecruitmentApp.Helpers;

namespace OnlineRecruitmentApp
{
    public partial class AdminDashboardForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private ComboBox cmbRole;
        private DataGridView dgvUsers;
        private TabControl tabControl;

        public AdminDashboardForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form Settings
            this.ClientSize = new Size(1200, 750);
            this.Text = "Admin Dashboard - JobConnect";
            this.BackColor = UIHelper.BackgroundColor;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Sidebar
            Panel sidebar = CreateSidebar();
            this.Controls.Add(sidebar);

            // Main Content Area
            Panel mainContent = new Panel
            {
                Location = new Point(250, 0),
                Size = new Size(950, 750),
                BackColor = UIHelper.BackgroundColor,
                Padding = new Padding(30)
            };

            // Header
            Label headerLabel = new Label
            {
                Text = "👋 Welcome, " + Session.LoggedInUserName,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(30, 25)
            };
            mainContent.Controls.Add(headerLabel);

            Label subHeaderLabel = new Label
            {
                Text = "Manage users and system settings",
                Font = new Font("Segoe UI", 10),
                ForeColor = UIHelper.TextSecondary,
                AutoSize = true,
                Location = new Point(30, 55)
            };
            mainContent.Controls.Add(subHeaderLabel);

            // Tab Control
            tabControl = new TabControl
            {
                Location = new Point(30, 100),
                Size = new Size(890, 620),
                Font = new Font("Segoe UI", 10)
            };

            // Users Tab
            TabPage usersTab = new TabPage("👥 Users Management");
            usersTab.BackColor = Color.White;
            usersTab.Padding = new Padding(20);
            CreateUsersTab(usersTab);
            tabControl.TabPages.Add(usersTab);

            // Employers Tab
            TabPage employersTab = new TabPage("🏢 Employers");
            employersTab.BackColor = Color.White;
            employersTab.Padding = new Padding(20);
            CreateEmployersTab(employersTab);
            tabControl.TabPages.Add(employersTab);

            // Job Seekers Tab
            TabPage seekersTab = new TabPage("👤 Job Seekers");
            seekersTab.BackColor = Color.White;
            seekersTab.Padding = new Padding(20);
            CreateJobSeekersTab(seekersTab);
            tabControl.TabPages.Add(seekersTab);

            // Jobs Tab
            TabPage jobsTab = new TabPage("💼 Job Listings");
            jobsTab.BackColor = Color.White;
            jobsTab.Padding = new Padding(20);
            CreateJobsTab(jobsTab);
            tabControl.TabPages.Add(jobsTab);

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

            // Logo
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
                Text = "Administrator",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(200, 255, 255, 255),
                AutoSize = true,
                Location = new Point(30, 60)
            };
            sidebar.Controls.Add(roleLabel);

            // Separator
            Panel separator = new Panel
            {
                BackColor = Color.FromArgb(100, 255, 255, 255),
                Location = new Point(20, 100),
                Size = new Size(210, 1)
            };
            sidebar.Controls.Add(separator);

            // Menu Items
            string[] menuItems = { "📊 Dashboard", "👥 Users", "🏢 Employers", "👤 Job Seekers", "💼 Jobs", "⭐ Saved Jobs" };
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

            // Logout Button
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

        private void CreateUsersTab(TabPage tab)
        {
            // Form Section
            Label formTitle = UIHelper.CreateSectionTitle("Add / Edit User");
            formTitle.Location = new Point(20, 20);
            tab.Controls.Add(formTitle);

            // Username
            Label lblUsername = new Label { Text = "Username", Font = UIHelper.LabelFont, Location = new Point(20, 60), AutoSize = true };
            tab.Controls.Add(lblUsername);
            txtUsername = new TextBox { Width = 200, Font = UIHelper.InputFont, BorderStyle = BorderStyle.FixedSingle, Location = new Point(20, 82) };
            tab.Controls.Add(txtUsername);

            // Email
            Label lblEmail = new Label { Text = "Email", Font = UIHelper.LabelFont, Location = new Point(240, 60), AutoSize = true };
            tab.Controls.Add(lblEmail);
            txtEmail = new TextBox { Width = 200, Font = UIHelper.InputFont, BorderStyle = BorderStyle.FixedSingle, Location = new Point(240, 82) };
            tab.Controls.Add(txtEmail);

            // Password
            Label lblPassword = new Label { Text = "Password", Font = UIHelper.LabelFont, Location = new Point(460, 60), AutoSize = true };
            tab.Controls.Add(lblPassword);
            txtPassword = new TextBox { Width = 150, Font = UIHelper.InputFont, BorderStyle = BorderStyle.FixedSingle, Location = new Point(460, 82), UseSystemPasswordChar = true };
            tab.Controls.Add(txtPassword);

            // Role
            Label lblRole = new Label { Text = "Role", Font = UIHelper.LabelFont, Location = new Point(630, 60), AutoSize = true };
            tab.Controls.Add(lblRole);
            cmbRole = new ComboBox { Width = 120, Font = UIHelper.InputFont, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(630, 82) };
            cmbRole.Items.AddRange(new[] { "admin", "employer", "job seeker" });
            tab.Controls.Add(cmbRole);

            // Buttons
            Button btnInsert = UIHelper.CreatePrimaryButton("➕ Add", 100, 35);
            btnInsert.Location = new Point(20, 125);
            btnInsert.Click += BtnInsert_Click;
            tab.Controls.Add(btnInsert);

            Button btnUpdate = UIHelper.CreateSecondaryButton("✏️ Update", 100, 35);
            btnUpdate.Location = new Point(130, 125);
            btnUpdate.Click += BtnUpdate_Click;
            tab.Controls.Add(btnUpdate);

            Button btnDelete = UIHelper.CreateDangerButton("🗑️ Delete", 100, 35);
            btnDelete.Location = new Point(240, 125);
            btnDelete.Click += BtnDelete_Click;
            tab.Controls.Add(btnDelete);

            Button btnRefresh = UIHelper.CreateOutlineButton("🔄 Refresh", 100, 35);
            btnRefresh.Location = new Point(350, 125);
            btnRefresh.Click += BtnRefresh_Click;
            tab.Controls.Add(btnRefresh);

            // DataGridView
            dgvUsers = new DataGridView
            {
                Location = new Point(20, 180),
                Size = new Size(830, 380)
            };
            UIHelper.StyleDataGridView(dgvUsers);
            dgvUsers.CellClick += DgvUsers_CellClick;
            tab.Controls.Add(dgvUsers);

            LoadUsers();
        }

        private void CreateEmployersTab(TabPage tab)
        {
            DataGridView dgv = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(830, 480)
            };
            UIHelper.StyleDataGridView(dgv);
            dgv.Name = "dgvEmployers";

            Button btnLoad = UIHelper.CreatePrimaryButton("🔄 Load Employers", 150, 35);
            btnLoad.Location = new Point(20, 20);
            btnLoad.Click += (s, e) => LoadEmployers(dgv);
            tab.Controls.Add(btnLoad);

            tab.Controls.Add(dgv);
        }

        private void CreateJobSeekersTab(TabPage tab)
        {
            DataGridView dgv = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(830, 480)
            };
            UIHelper.StyleDataGridView(dgv);
            dgv.Name = "dgvSeekers";

            Button btnLoad = UIHelper.CreatePrimaryButton("🔄 Load Job Seekers", 160, 35);
            btnLoad.Location = new Point(20, 20);
            btnLoad.Click += (s, e) => LoadJobSeekers(dgv);
            tab.Controls.Add(btnLoad);

            tab.Controls.Add(dgv);
        }

        private void CreateJobsTab(TabPage tab)
        {
            DataGridView dgv = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(830, 480)
            };
            UIHelper.StyleDataGridView(dgv);
            dgv.Name = "dgvJobs";

            Button btnLoad = UIHelper.CreatePrimaryButton("🔄 Load Jobs", 120, 35);
            btnLoad.Location = new Point(20, 20);
            btnLoad.Click += (s, e) => LoadJobs(dgv);
            tab.Controls.Add(btnLoad);

            tab.Controls.Add(dgv);
        }

        private void CreateSavedJobsTab(TabPage tab)
        {
            DataGridView dgv = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(830, 480)
            };
            UIHelper.StyleDataGridView(dgv);
            dgv.Name = "dgvSavedJobs";

            Button btnLoad = UIHelper.CreatePrimaryButton("🔄 Load Saved Jobs", 150, 35);
            btnLoad.Location = new Point(20, 20);
            btnLoad.Click += (s, e) => LoadSavedJobs(dgv);
            tab.Controls.Add(btnLoad);

            tab.Controls.Add(dgv);
        }

        private void LoadUsers()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT USER_ID, USER_NAME, EMAIL, ROLE FROM [USER]", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvUsers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error loading users: " + ex.Message);
            }
        }

        private void LoadEmployers(DataGridView dgv)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM EMPLOYER", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error loading employers: " + ex.Message);
            }
        }

        private void LoadJobSeekers(DataGridView dgv)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM JOB_SEEKER", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error loading job seekers: " + ex.Message);
            }
        }

        private void LoadJobs(DataGridView dgv)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM JOB_VACANCY", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error loading jobs: " + ex.Message);
            }
        }

        private void LoadSavedJobs(DataGridView dgv)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM SAVED_VACANCY", conn);
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

        private void DgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
                txtUsername.Text = row.Cells["USER_NAME"].Value?.ToString();
                txtEmail.Text = row.Cells["EMAIL"].Value?.ToString();
                cmbRole.Text = row.Cells["ROLE"].Value?.ToString();
                txtPassword.Text = "";
            }
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) || cmbRole.SelectedIndex == -1)
            {
                UIHelper.ShowWarningMessage("Please fill in all fields.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO [USER] (USER_NAME, ROLE, EMAIL, PASSWORD) 
                        VALUES (@UserName, @Role, @Email, @Password);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);", conn);

                    cmd.Parameters.AddWithValue("@UserName", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Role", cmbRole.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                    int newUserId = (int)cmd.ExecuteScalar();

                    if (cmbRole.Text == "employer")
                    {
                        SqlCommand empCmd = new SqlCommand(
                            "INSERT INTO [EMPLOYER] (EMP_ID, USER_ID, COMPANY_NAME) VALUES (@ID, @ID, 'Not defined yet')", conn);
                        empCmd.Parameters.AddWithValue("@ID", newUserId);
                        empCmd.ExecuteNonQuery();
                    }
                    else if (cmbRole.Text == "job seeker")
                    {
                        SqlCommand seekerCmd = new SqlCommand(@"
                            INSERT INTO [JOB_SEEKER] (ID, USER_ID, FULL_NAME, CITY, CV_LINK, EXPERIENCE_YEARS, INDUSTRY) 
                            VALUES (@ID, @ID, 'Not defined yet', 'Not defined yet', 'Not defined yet', 0, 'Not defined yet')", conn);
                        seekerCmd.Parameters.AddWithValue("@ID", newUserId);
                        seekerCmd.ExecuteNonQuery();
                    }

                    UIHelper.ShowSuccessMessage("User added successfully!");
                    LoadUsers();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error: " + ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                UIHelper.ShowWarningMessage("Please select a user to update.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = "UPDATE [USER] SET ROLE = @Role, EMAIL = @Email";
                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        query += ", PASSWORD = @Password";
                    }
                    query += " WHERE USER_NAME = @UserName";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Role", cmbRole.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@UserName", txtUsername.Text.Trim());
                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    }

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        UIHelper.ShowSuccessMessage("User updated successfully!");
                        LoadUsers();
                        ClearForm();
                    }
                    else
                    {
                        UIHelper.ShowWarningMessage("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                UIHelper.ShowWarningMessage("Please select a user to delete.");
                return;
            }

            if (UIHelper.ShowConfirmDialog("Are you sure you want to delete this user and all related data?") != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlTransaction tx = conn.BeginTransaction();

                    try
                    {
                        // Get USER_ID
                        SqlCommand getUserId = new SqlCommand(
                            "SELECT USER_ID FROM [USER] WHERE USER_NAME = @u", conn, tx);
                        getUserId.Parameters.AddWithValue("@u", txtUsername.Text.Trim());

                        object uidObj = getUserId.ExecuteScalar();
                        if (uidObj == null)
                        {
                            UIHelper.ShowWarningMessage("User not found.");
                            return;
                        }
                        int userId = (int)uidObj;

                        // Delete from EMPLOYER if exists
                        SqlCommand getEmpId = new SqlCommand(
                            "SELECT EMP_ID FROM EMPLOYER WHERE USER_ID = @uid", conn, tx);
                        getEmpId.Parameters.AddWithValue("@uid", userId);
                        object empObj = getEmpId.ExecuteScalar();

                        if (empObj != null)
                        {
                            int empId = (int)empObj;

                            // Delete related job vacancies and applications
                            SqlCommand getVacIds = new SqlCommand(
                                "SELECT JOB_VACANCY_ID FROM JOB_VACANCY WHERE EMP_ID = @eid", conn, tx);
                            getVacIds.Parameters.AddWithValue("@eid", empId);

                            List<int> vacIds = new List<int>();
                            using (SqlDataReader rdr = getVacIds.ExecuteReader())
                                while (rdr.Read()) vacIds.Add(rdr.GetInt32(0));

                            foreach (int vid in vacIds)
                            {
                                SqlCommand delApply = new SqlCommand(
                                    "DELETE FROM APPLY WHERE JOB_VACANCY_ID = @vid", conn, tx);
                                delApply.Parameters.AddWithValue("@vid", vid);
                                delApply.ExecuteNonQuery();

                                SqlCommand delSaved = new SqlCommand(
                                    "DELETE FROM SAVED_VACANCY WHERE JOB_VACANCY_ID = @vid", conn, tx);
                                delSaved.Parameters.AddWithValue("@vid", vid);
                                delSaved.ExecuteNonQuery();
                            }

                            SqlCommand delVacancies = new SqlCommand(
                                "DELETE FROM JOB_VACANCY WHERE EMP_ID = @eid", conn, tx);
                            delVacancies.Parameters.AddWithValue("@eid", empId);
                            delVacancies.ExecuteNonQuery();

                            SqlCommand unsetEmpPtr = new SqlCommand(
                                "UPDATE [USER] SET EMP_ID = NULL WHERE USER_ID = @uid", conn, tx);
                            unsetEmpPtr.Parameters.AddWithValue("@uid", userId);
                            unsetEmpPtr.ExecuteNonQuery();

                            SqlCommand delEmployer = new SqlCommand(
                                "DELETE FROM EMPLOYER WHERE EMP_ID = @eid", conn, tx);
                            delEmployer.Parameters.AddWithValue("@eid", empId);
                            delEmployer.ExecuteNonQuery();
                        }

                        // Delete from JOB_SEEKER if exists
                        SqlCommand getSeekerId = new SqlCommand(
                            "SELECT ID FROM JOB_SEEKER WHERE USER_ID = @uid", conn, tx);
                        getSeekerId.Parameters.AddWithValue("@uid", userId);
                        object seekerObj = getSeekerId.ExecuteScalar();

                        if (seekerObj != null)
                        {
                            int seekerId = (int)seekerObj;

                            SqlCommand delApply = new SqlCommand(
                                "DELETE FROM APPLY WHERE ID = @sid", conn, tx);
                            delApply.Parameters.AddWithValue("@sid", seekerId);
                            delApply.ExecuteNonQuery();

                            SqlCommand delSaved = new SqlCommand(
                                "DELETE FROM SAVED_VACANCY WHERE ID = @sid OR ID_SEEKER = @sid", conn, tx);
                            delSaved.Parameters.AddWithValue("@sid", seekerId);
                            delSaved.ExecuteNonQuery();

                            SqlCommand unsetSeekerPtr = new SqlCommand(
                                "UPDATE [USER] SET ID = NULL WHERE USER_ID = @uid", conn, tx);
                            unsetSeekerPtr.Parameters.AddWithValue("@uid", userId);
                            unsetSeekerPtr.ExecuteNonQuery();

                            SqlCommand delSeeker = new SqlCommand(
                                "DELETE FROM JOB_SEEKER WHERE ID = @sid", conn, tx);
                            delSeeker.Parameters.AddWithValue("@sid", seekerId);
                            delSeeker.ExecuteNonQuery();
                        }

                        // Delete the USER
                        SqlCommand delUser = new SqlCommand(
                            "DELETE FROM [USER] WHERE USER_ID = @uid", conn, tx);
                        delUser.Parameters.AddWithValue("@uid", userId);
                        delUser.ExecuteNonQuery();

                        tx.Commit();
                        UIHelper.ShowSuccessMessage("User deleted successfully!");
                        LoadUsers();
                        ClearForm();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
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
            LoadUsers();
            ClearForm();
        }

        private void ClearForm()
        {
            txtUsername.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            cmbRole.SelectedIndex = -1;
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
