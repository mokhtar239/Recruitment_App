using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using OnlineRecruitmentApp.Helpers;

namespace OnlineRecruitmentApp
{
    public partial class RegisterForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private ComboBox cmbRole;

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form Settings
            this.ClientSize = new Size(500, 680);
            this.Text = "Create Account - JobConnect";
            this.BackColor = UIHelper.BackgroundColor;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Main Card Panel
            Panel card = new Panel
            {
                Size = new Size(420, 600),
                Location = new Point(40, 40),
                BackColor = Color.White
            };
            card.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    UIHelper.BorderColor, 1, ButtonBorderStyle.Solid,
                    UIHelper.BorderColor, 1, ButtonBorderStyle.Solid,
                    UIHelper.BorderColor, 1, ButtonBorderStyle.Solid,
                    UIHelper.BorderColor, 1, ButtonBorderStyle.Solid);
            };

            // Logo
            Label logoLabel = new Label
            {
                Text = "💼",
                Font = new Font("Segoe UI", 36),
                ForeColor = UIHelper.PrimaryColor,
                AutoSize = true,
                Location = new Point(175, 20)
            };
            card.Controls.Add(logoLabel);

            // Title
            Label titleLabel = new Label
            {
                Text = "Create Account",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(115, 80)
            };
            card.Controls.Add(titleLabel);

            // Subtitle
            Label subtitleLabel = new Label
            {
                Text = "Join our community today",
                Font = new Font("Segoe UI", 10),
                ForeColor = UIHelper.TextSecondary,
                AutoSize = true,
                Location = new Point(130, 115)
            };
            card.Controls.Add(subtitleLabel);

            int startY = 160;
            int spacing = 70;

            // Username
            Label lblUsername = new Label
            {
                Text = "Username",
                Font = UIHelper.LabelFont,
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(50, startY)
            };
            card.Controls.Add(lblUsername);

            txtUsername = new TextBox
            {
                Width = 320,
                Font = UIHelper.InputFont,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(50, startY + 22)
            };
            card.Controls.Add(txtUsername);

            // Email
            Label lblEmail = new Label
            {
                Text = "Email Address",
                Font = UIHelper.LabelFont,
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(50, startY + spacing)
            };
            card.Controls.Add(lblEmail);

            txtEmail = new TextBox
            {
                Width = 320,
                Font = UIHelper.InputFont,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(50, startY + spacing + 22)
            };
            card.Controls.Add(txtEmail);

            // Password
            Label lblPassword = new Label
            {
                Text = "Password (min 8 characters)",
                Font = UIHelper.LabelFont,
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(50, startY + spacing * 2)
            };
            card.Controls.Add(lblPassword);

            txtPassword = new TextBox
            {
                Width = 320,
                Font = UIHelper.InputFont,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(50, startY + spacing * 2 + 22),
                UseSystemPasswordChar = true
            };
            card.Controls.Add(txtPassword);

            // Confirm Password
            Label lblConfirmPassword = new Label
            {
                Text = "Confirm Password",
                Font = UIHelper.LabelFont,
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(50, startY + spacing * 3)
            };
            card.Controls.Add(lblConfirmPassword);

            txtConfirmPassword = new TextBox
            {
                Width = 320,
                Font = UIHelper.InputFont,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(50, startY + spacing * 3 + 22),
                UseSystemPasswordChar = true
            };
            card.Controls.Add(txtConfirmPassword);

            // Role
            Label lblRole = new Label
            {
                Text = "I am a...",
                Font = UIHelper.LabelFont,
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(50, startY + spacing * 4)
            };
            card.Controls.Add(lblRole);

            cmbRole = new ComboBox
            {
                Width = 320,
                Font = UIHelper.InputFont,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(50, startY + spacing * 4 + 22)
            };
            cmbRole.Items.Add("Job Seeker");
            cmbRole.Items.Add("Employer");
            cmbRole.SelectedIndex = 0;
            card.Controls.Add(cmbRole);

            // Register Button
            Button btnRegister = UIHelper.CreatePrimaryButton("Create Account", 320, 45);
            btnRegister.Location = new Point(50, startY + spacing * 5 + 15);
            btnRegister.Font = new Font("Segoe UI Semibold", 11);
            btnRegister.Click += BtnRegister_Click;
            card.Controls.Add(btnRegister);

            // Login Link
            Label lblLogin = new Label
            {
                Text = "Already have an account?",
                Font = new Font("Segoe UI", 9),
                ForeColor = UIHelper.TextSecondary,
                AutoSize = true,
                Location = new Point(95, 560)
            };
            card.Controls.Add(lblLogin);

            LinkLabel lnkLogin = new LinkLabel
            {
                Text = "Sign In",
                Font = new Font("Segoe UI", 9),
                LinkColor = UIHelper.PrimaryColor,
                AutoSize = true,
                Location = new Point(260, 560)
            };
            lnkLogin.Click += LnkLogin_Click;
            card.Controls.Add(lnkLogin);

            this.Controls.Add(card);

            // Back Button
            Button btnBack = UIHelper.CreateOutlineButton("← Back", 80, 35);
            btnBack.Location = new Point(40, 650);
            btnBack.Font = new Font("Segoe UI", 9);
            btnBack.Click += BtnBack_Click;
            this.Controls.Add(btnBack);

            this.ResumeLayout(false);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                UIHelper.ShowWarningMessage("Please enter a username.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                UIHelper.ShowWarningMessage("Please enter a valid email address.");
                return;
            }

            if (txtPassword.Text.Length < 8)
            {
                UIHelper.ShowWarningMessage("Password must be at least 8 characters long.");
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                UIHelper.ShowWarningMessage("Passwords do not match.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Check if username or email exists
                    SqlCommand checkCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM [USER] WHERE USER_NAME = @UserName OR EMAIL = @Email", conn);
                    checkCmd.Parameters.AddWithValue("@UserName", txtUsername.Text.Trim());
                    checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    int userCount = (int)checkCmd.ExecuteScalar();

                    if (userCount > 0)
                    {
                        UIHelper.ShowWarningMessage("Username or Email already exists. Please use another.");
                        return;
                    }

                    string role = cmbRole.SelectedItem.ToString().ToLower();

                    // Insert user
                    SqlCommand insertCmd = new SqlCommand(@"
                        INSERT INTO [USER] (USER_NAME, ROLE, EMAIL, PASSWORD) 
                        VALUES (@UserName, @Role, @Email, @Password);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);", conn);

                    insertCmd.Parameters.AddWithValue("@UserName", txtUsername.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@Role", role);
                    insertCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                    int newUserId = (int)insertCmd.ExecuteScalar();

                    // Insert into role-specific table
                    if (role == "employer")
                    {
                        SqlCommand empCmd = new SqlCommand(
                            "INSERT INTO [EMPLOYER] (EMP_ID, USER_ID, COMPANY_NAME) VALUES (@ID, @ID, 'Not defined yet')", conn);
                        empCmd.Parameters.AddWithValue("@ID", newUserId);
                        empCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand seekerCmd = new SqlCommand(@"
                            INSERT INTO [JOB_SEEKER] (ID, USER_ID, FULL_NAME, CITY, CV_LINK, EXPERIENCE_YEARS, INDUSTRY) 
                            VALUES (@ID, @ID, 'Not defined yet', 'Not defined yet', 'Not defined yet', 0, 'Not defined yet')", conn);
                        seekerCmd.Parameters.AddWithValue("@ID", newUserId);
                        seekerCmd.ExecuteNonQuery();
                    }

                    UIHelper.ShowSuccessMessage("Account created successfully! Please sign in.");

                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error: " + ex.Message);
            }
        }

        private void LnkLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
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
