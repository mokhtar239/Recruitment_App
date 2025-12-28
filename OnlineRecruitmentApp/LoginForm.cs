using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using OnlineRecruitmentApp.Helpers;

namespace OnlineRecruitmentApp
{
    public partial class LoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form Settings
            this.ClientSize = new Size(500, 550);
            this.Text = "Sign In - JobConnect";
            this.BackColor = UIHelper.BackgroundColor;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Main Card Panel
            Panel card = new Panel
            {
                Size = new Size(420, 480),
                Location = new Point(40, 35),
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
                Font = new Font("Segoe UI", 48),
                ForeColor = UIHelper.PrimaryColor,
                AutoSize = true,
                Location = new Point(170, 30)
            };
            card.Controls.Add(logoLabel);

            // Title
            Label titleLabel = new Label
            {
                Text = "Welcome Back",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(115, 110)
            };
            card.Controls.Add(titleLabel);

            // Subtitle
            Label subtitleLabel = new Label
            {
                Text = "Sign in to your account",
                Font = new Font("Segoe UI", 10),
                ForeColor = UIHelper.TextSecondary,
                AutoSize = true,
                Location = new Point(135, 150)
            };
            card.Controls.Add(subtitleLabel);

            // Username Label
            Label lblUsername = new Label
            {
                Text = "Username",
                Font = UIHelper.LabelFont,
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(50, 200)
            };
            card.Controls.Add(lblUsername);

            // Username TextBox
            txtUsername = new TextBox
            {
                Width = 320,
                Height = 35,
                Font = UIHelper.InputFont,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(50, 225)
            };
            card.Controls.Add(txtUsername);

            // Password Label
            Label lblPassword = new Label
            {
                Text = "Password",
                Font = UIHelper.LabelFont,
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(50, 275)
            };
            card.Controls.Add(lblPassword);

            // Password TextBox
            txtPassword = new TextBox
            {
                Width = 320,
                Height = 35,
                Font = UIHelper.InputFont,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(50, 300),
                UseSystemPasswordChar = true
            };
            card.Controls.Add(txtPassword);

            // Login Button
            Button btnLogin = UIHelper.CreatePrimaryButton("Sign In", 320, 45);
            btnLogin.Location = new Point(50, 365);
            btnLogin.Font = new Font("Segoe UI Semibold", 11);
            btnLogin.Click += BtnLogin_Click;
            card.Controls.Add(btnLogin);

            // Register Link
            Label lblRegister = new Label
            {
                Text = "Don't have an account?",
                Font = new Font("Segoe UI", 9),
                ForeColor = UIHelper.TextSecondary,
                AutoSize = true,
                Location = new Point(100, 430)
            };
            card.Controls.Add(lblRegister);

            LinkLabel lnkRegister = new LinkLabel
            {
                Text = "Create one",
                Font = new Font("Segoe UI", 9),
                LinkColor = UIHelper.PrimaryColor,
                AutoSize = true,
                Location = new Point(250, 430)
            };
            lnkRegister.Click += LnkRegister_Click;
            card.Controls.Add(lnkRegister);

            this.Controls.Add(card);

            // Back Button
            Button btnBack = UIHelper.CreateOutlineButton("← Back", 80, 35);
            btnBack.Location = new Point(40, 525);
            btnBack.Font = new Font("Segoe UI", 9);
            btnBack.Click += BtnBack_Click;
            this.Controls.Add(btnBack);

            // Enable Enter key for login
            this.AcceptButton = btnLogin;

            this.ResumeLayout(false);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                UIHelper.ShowWarningMessage("Please enter both username and password.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "SELECT * FROM [USER] WHERE USER_NAME = @username AND PASSWORD = @password", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Session.LoggedInUserId = Convert.ToInt32(reader["USER_ID"]);
                        Session.LoggedInUserName = username;
                        Session.UserRole = reader["ROLE"].ToString();
                        Session.UserEmail = reader["EMAIL"].ToString();

                        reader.Close();

                        Form nextForm;
                        if (Session.IsAdmin)
                        {
                            nextForm = new AdminDashboardForm();
                        }
                        else if (Session.IsEmployer)
                        {
                            nextForm = new EmployerDashboardForm();
                        }
                        else
                        {
                            nextForm = new JobSeekerDashboardForm();
                        }

                        nextForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        UIHelper.ShowErrorMessage("Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage("Error: " + ex.Message);
            }
        }

        private void LnkRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
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
