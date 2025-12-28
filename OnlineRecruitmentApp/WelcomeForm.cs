using System;
using System.Drawing;
using System.Windows.Forms;
using OnlineRecruitmentApp.Helpers;

namespace OnlineRecruitmentApp
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form Settings
            this.ClientSize = new Size(900, 600);
            this.Text = "Online Recruitment System";
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Left Panel - Branding (Purple side)
            Panel leftPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(450, 600),
                BackColor = UIHelper.PrimaryColor
            };

            // Logo/Icon placeholder
            Label logoLabel = new Label
            {
                Text = "💼",
                Font = new Font("Segoe UI", 72),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(170, 150)
            };
            leftPanel.Controls.Add(logoLabel);

            Label brandTitle = new Label
            {
                Text = "JobConnect",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(120, 280)
            };
            leftPanel.Controls.Add(brandTitle);

            Label brandSubtitle = new Label
            {
                Text = "Find Your Dream Career",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.FromArgb(200, 255, 255, 255),
                AutoSize = true,
                Location = new Point(130, 330)
            };
            leftPanel.Controls.Add(brandSubtitle);

            Label featureText = new Label
            {
                Text = "✓ Browse thousands of jobs\n✓ Connect with top employers\n✓ Manage applications easily",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(220, 255, 255, 255),
                AutoSize = true,
                Location = new Point(120, 400)
            };
            leftPanel.Controls.Add(featureText);

            this.Controls.Add(leftPanel);

            // ============================================
            // RIGHT SIDE CONTENT (directly on form)
            // ============================================

            Label welcomeTitle = new Label
            {
                Text = "Welcome Back!",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = UIHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(530, 150)
            };
            this.Controls.Add(welcomeTitle);

            Label welcomeSubtitle = new Label
            {
                Text = "Sign in to continue to your account\nor create a new one to get started.",
                Font = new Font("Segoe UI", 11),
                ForeColor = UIHelper.TextSecondary,
                AutoSize = true,
                Location = new Point(530, 205)
            };
            this.Controls.Add(welcomeSubtitle);

            // Login Button - Primary Style
            Button btnLogin = new Button
            {
                Text = "Sign In",
                Size = new Size(280, 50),
                Location = new Point(530, 300),
                FlatStyle = FlatStyle.Flat,
                BackColor = UIHelper.PrimaryColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 12),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);

            // Register Button - Outline Style
            Button btnRegister = new Button
            {
                Text = "Create Account",
                Size = new Size(280, 50),
                Location = new Point(530, 370),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = UIHelper.PrimaryColor,
                Font = new Font("Segoe UI Semibold", 12),
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderColor = UIHelper.PrimaryColor;
            btnRegister.FlatAppearance.BorderSize = 2;
            btnRegister.Click += BtnRegister_Click;
            this.Controls.Add(btnRegister);

            // Footer
            Label footerLabel = new Label
            {
                Text = "© 2024 JobConnect - Online Recruitment System",
                Font = new Font("Segoe UI", 9),
                ForeColor = UIHelper.TextSecondary,
                AutoSize = true,
                Location = new Point(530, 520)
            };
            this.Controls.Add(footerLabel);

            this.ResumeLayout(false);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
