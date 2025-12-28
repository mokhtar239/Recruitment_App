using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OnlineRecruitmentApp.Helpers
{
    public static class UIHelper
    {
        // Modern Color Palette
        public static Color PrimaryColor = Color.FromArgb(79, 70, 229);      // Indigo
        public static Color PrimaryDark = Color.FromArgb(67, 56, 202);       // Darker Indigo
        public static Color SecondaryColor = Color.FromArgb(16, 185, 129);   // Emerald
        public static Color DangerColor = Color.FromArgb(239, 68, 68);       // Red
        public static Color WarningColor = Color.FromArgb(245, 158, 11);     // Amber
        public static Color BackgroundColor = Color.FromArgb(249, 250, 251); // Light Gray
        public static Color CardColor = Color.White;
        public static Color TextPrimary = Color.FromArgb(31, 41, 55);        // Dark Gray
        public static Color TextSecondary = Color.FromArgb(107, 114, 128);   // Gray
        public static Color BorderColor = Color.FromArgb(229, 231, 235);     // Light Border

        // Fonts
        public static Font TitleFont = new Font("Segoe UI", 24, FontStyle.Bold);
        public static Font SubtitleFont = new Font("Segoe UI", 14, FontStyle.Regular);
        public static Font HeadingFont = new Font("Segoe UI Semibold", 16);
        public static Font LabelFont = new Font("Segoe UI", 10, FontStyle.Regular);
        public static Font ButtonFont = new Font("Segoe UI Semibold", 10);
        public static Font InputFont = new Font("Segoe UI", 10);

        public static void StyleForm(Form form, string title)
        {
            form.Text = title;
            form.BackColor = BackgroundColor;
            form.Font = new Font("Segoe UI", 9);
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
            form.StartPosition = FormStartPosition.CenterScreen;
        }

        public static Panel CreateHeaderPanel(string title, string subtitle = "")
        {
            Panel header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = PrimaryColor,
                Padding = new Padding(30, 20, 30, 20)
            };

            Label titleLabel = new Label
            {
                Text = title,
                Font = TitleFont,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(30, 20)
            };
            header.Controls.Add(titleLabel);

            if (!string.IsNullOrEmpty(subtitle))
            {
                Label subtitleLabel = new Label
                {
                    Text = subtitle,
                    Font = SubtitleFont,
                    ForeColor = Color.FromArgb(200, 255, 255, 255),
                    AutoSize = true,
                    Location = new Point(30, 55)
                };
                header.Controls.Add(subtitleLabel);
            }

            return header;
        }

        public static Panel CreateCard(int width, int height)
        {
            Panel card = new Panel
            {
                Size = new Size(width, height),
                BackColor = CardColor,
                Padding = new Padding(20)
            };
            card.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    BorderColor, 1, ButtonBorderStyle.Solid,
                    BorderColor, 1, ButtonBorderStyle.Solid,
                    BorderColor, 1, ButtonBorderStyle.Solid,
                    BorderColor, 1, ButtonBorderStyle.Solid);
            };
            return card;
        }

        public static Button CreatePrimaryButton(string text, int width = 150, int height = 40)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(width, height),
                FlatStyle = FlatStyle.Flat,
                BackColor = PrimaryColor,
                ForeColor = Color.White,
                Font = ButtonFont,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = PrimaryDark;

            // Rounded corners effect
            btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 8, 8));

            return btn;
        }

        public static Button CreateSecondaryButton(string text, int width = 150, int height = 40)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(width, height),
                FlatStyle = FlatStyle.Flat,
                BackColor = SecondaryColor,
                ForeColor = Color.White,
                Font = ButtonFont,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(5, 150, 105);
            btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 8, 8));

            return btn;
        }

        public static Button CreateDangerButton(string text, int width = 150, int height = 40)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(width, height),
                FlatStyle = FlatStyle.Flat,
                BackColor = DangerColor,
                ForeColor = Color.White,
                Font = ButtonFont,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 38, 38);
            btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 8, 8));

            return btn;
        }

        public static Button CreateOutlineButton(string text, int width = 150, int height = 40)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(width, height),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = PrimaryColor,
                Font = ButtonFont,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = PrimaryColor;
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(238, 242, 255);

            return btn;
        }

        public static TextBox CreateModernTextBox(int width = 250)
        {
            TextBox txt = new TextBox
            {
                Width = width,
                Height = 35,
                Font = InputFont,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            return txt;
        }

        public static Panel CreateTextBoxWithLabel(string labelText, int width = 250, bool isPassword = false)
        {
            Panel container = new Panel
            {
                Width = width,
                Height = 70,
                BackColor = Color.Transparent
            };

            Label label = new Label
            {
                Text = labelText,
                Font = LabelFont,
                ForeColor = TextPrimary,
                AutoSize = true,
                Location = new Point(0, 0)
            };
            container.Controls.Add(label);

            TextBox txt = new TextBox
            {
                Width = width,
                Height = 32,
                Font = InputFont,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(0, 25),
                Name = "txt" + labelText.Replace(" ", "")
            };
            if (isPassword)
            {
                txt.UseSystemPasswordChar = true;
            }
            container.Controls.Add(txt);

            return container;
        }

        public static ComboBox CreateModernComboBox(int width = 250)
        {
            ComboBox cmb = new ComboBox
            {
                Width = width,
                Height = 32,
                Font = InputFont,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White
            };
            return cmb;
        }

        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = BorderColor;
            dgv.EnableHeadersVisualStyles = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;

            // Header style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Row style
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = TextPrimary;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255);
            dgv.DefaultCellStyle.SelectionForeColor = TextPrimary;
            dgv.DefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgv.RowTemplate.Height = 45;

            // Alternating row style
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
        }

        public static Label CreateSectionTitle(string text)
        {
            return new Label
            {
                Text = text,
                Font = HeadingFont,
                ForeColor = TextPrimary,
                AutoSize = true
            };
        }

        public static void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowConfirmDialog(string message)
        {
            return MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        // Windows API for rounded corners
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
    }
}
