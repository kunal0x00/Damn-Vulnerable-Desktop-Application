using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVDAv2
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            SetupModernUI();
        }

        private void SetupModernUI()
        {
            // Form settings
            this.Text = "🔓 DVDA - Vulnerable Desktop Application";
            this.Size = new Size(500, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(245, 245, 245);

            // Clear existing controls from designer
            this.Controls.Clear();

            // Main login panel
            var modernLoginPanel = new Panel
            {
                Size = new Size(400, 500),
                Location = new Point(50, 80),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            // Add shadow effect
            modernLoginPanel.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, modernLoginPanel.ClientRectangle,
                    Color.LightGray, 1, ButtonBorderStyle.Solid,
                    Color.LightGray, 1, ButtonBorderStyle.Solid,
                    Color.LightGray, 1, ButtonBorderStyle.Solid,
                    Color.LightGray, 1, ButtonBorderStyle.Solid);
            };

            // Logo panel
            var logoPanel = new Panel
            {
                Size = new Size(100, 100),
                Location = new Point(150, 30),
                BackColor = Color.FromArgb(244, 67, 54),
                BorderStyle = BorderStyle.None
            };

            var logoLabel = new Label
            {
                Text = "🔓",
                Font = new Font("Segoe UI", 42, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            logoPanel.Controls.Add(logoLabel);

            // Title
            var titleLabel = new Label
            {
                Text = "DVDA LOGIN",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                Size = new Size(400, 40),
                Location = new Point(0, 150),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Subtitle
            var subtitleLabel = new Label
            {
                Text = "Damn Vulnerable Desktop Application\nEducational Security Testing Platform",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(117, 117, 117),
                Size = new Size(400, 40),
                Location = new Point(0, 195),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Username field
            var usernameBox = new TextBox
            {
                Location = new Point(50, 260),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 12),
                Text = "admin",
                BorderStyle = BorderStyle.FixedSingle
            };

            var userLabel = new Label
            {
                Text = "Username (try: admin or admin' OR '1'='1' --)",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(117, 117, 117),
                Location = new Point(50, 240),
                AutoSize = true
            };

            // Password field
            var passwordBox = new TextBox
            {
                Location = new Point(50, 320),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 12),
                Text = "admin123",
                UseSystemPasswordChar = false,
                BorderStyle = BorderStyle.FixedSingle
            };

            var passLabel = new Label
            {
                Text = "Password (try: admin123 or anything)",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(117, 117, 117),
                Location = new Point(50, 300),
                AutoSize = true
            };

            // Show password checkbox
            var showPasswordCheck = new CheckBox
            {
                Text = "Show Password",
                Location = new Point(50, 360),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(117, 117, 117)
            };

            showPasswordCheck.CheckedChanged += (s, e) => {
                passwordBox.UseSystemPasswordChar = !showPasswordCheck.Checked;
            };

            // Login button
            var loginButton = new Button
            {
                Text = "LOGIN",
                Location = new Point(50, 400),
                Size = new Size(300, 45),
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.Click += (sender, e) => PerformLogin(usernameBox, passwordBox);

            // Add controls to login panel
            modernLoginPanel.Controls.AddRange(new Control[] {
                logoPanel, titleLabel, subtitleLabel, userLabel, usernameBox, 
                passLabel, passwordBox, showPasswordCheck, loginButton
            });

            // Add login panel to form
            this.Controls.Add(modernLoginPanel);

            // Warning label at bottom
            var warningLabel = new Label
            {
                Text = "⚠️ WARNING: This application contains intentional security vulnerabilities for educational purposes only!",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Red,
                Size = new Size(480, 40),
                Location = new Point(10, 570),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(warningLabel);
        }

        private void PerformLogin(TextBox usernameBox, TextBox passwordBox)
        {
            string username = usernameBox.Text.Trim();
            string password = passwordBox.Text;

            try
            {
                bool authSuccess = false;
                string successMessage = "";
                
                // Check for SQL injection vulnerability
                if (CheckVulnerableLogin(username, password))
                {
                    authSuccess = true;
                    successMessage = "SQL Injection Successful!";
                    VulnerabilityManager.DA2_BrokenAuthentication.StoreSessionInRegistry(username, password, username.ToLower().Contains("admin"));
                }
                else if (username.ToLower() == "admin" && password == "admin123")
                {
                    authSuccess = true;
                    successMessage = "Admin Login Successful!";
                    VulnerabilityManager.DA2_BrokenAuthentication.StoreSessionInRegistry(username, password, true);
                }
                else if (username.ToLower() == "testuser" && password == "password123")
                {
                    authSuccess = true;
                    successMessage = "Test User Login Successful!";
                    VulnerabilityManager.DA2_BrokenAuthentication.StoreSessionInRegistry(username, password, false);
                }
                else if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ShowError("Please enter both username and password.");
                    return;
                }
                else
                {
                    ShowError("Invalid credentials! Try: admin/admin123 or testuser/password123");
                    return;
                }
                
                if (authSuccess)
                {
                    ShowSuccessAndNavigate(successMessage);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Login error: {ex.Message}");
            }
        }

        private void ShowSuccessAndNavigate(string message)
        {
            MessageBox.Show($"{message}\n\nWelcome to DVDA!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
            var mainForm = new MainForm();
            mainForm.FormClosed += (s, args) => this.Close();
            mainForm.Show();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // SQL Injection vulnerability check
        private bool CheckVulnerableLogin(string username, string password)
        {
            string[] injectionPatterns = { "'", " or ", "--", "union", "1=1" };
            
            foreach (string pattern in injectionPatterns)
            {
                if (username.ToLower().Contains(pattern) || password.ToLower().Contains(pattern))
                {
                    return true;
                }
            }
            
            return false;
        }

    }
}
