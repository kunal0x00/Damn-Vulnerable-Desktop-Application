using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace DVDAv2
{
    public partial class UserLoginForm : Form
    {
        private string dbPath = "Data Source=vulnmart.db";
        private TextBox txtUsername;
        private TextBox txtPassword;

        public UserLoginForm()
        {
            InitializeFormComponents();
            SetupDarkThemeUI();
        }

        private void InitializeFormComponents()
        {
            this.SuspendLayout();
            
            // Form properties
            this.Text = "DVDA - User Authentication";
            this.Size = new Size(450, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = ThemeManager.PrimaryColor;
            this.ForeColor = ThemeManager.TextLight;
            this.Font = ThemeManager.DefaultFont;
            
            this.ResumeLayout(false);
        }

        private void SetupDarkThemeUI()
        {
            // Create main container panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeManager.PrimaryColor,
                Padding = new Padding(40)
            };
            this.Controls.Add(mainPanel);

            // Header section
            Panel headerPanel = new Panel
            {
                Height = 120,
                Dock = DockStyle.Top,
                BackColor = ThemeManager.PrimaryColor
            };
            mainPanel.Controls.Add(headerPanel);

            // Logo/Title
            Label titleLabel = new Label
            {
                Text = "🔐 DVDA Security Lab",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40,
                Padding = new Padding(0, 10, 0, 0)
            };
            headerPanel.Controls.Add(titleLabel);

            Label subtitleLabel = new Label
            {
                Text = "User Authentication Portal",
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = ThemeManager.TextLight,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 30
            };
            headerPanel.Controls.Add(subtitleLabel);

            Label warningLabel = new Label
            {
                Text = "⚠️ Vulnerable Login Interface - For Testing Only",
                Font = new Font("Arial", 9, FontStyle.Italic),
                ForeColor = ThemeManager.WarningColor,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 25
            };
            headerPanel.Controls.Add(warningLabel);

            // Login form section
            Panel formPanel = new Panel
            {
                Height = 280,
                Dock = DockStyle.Top,
                BackColor = ThemeManager.SecondaryColor,
                Padding = new Padding(30),
                Margin = new Padding(0, 20, 0, 20)
            };
            formPanel.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, formPanel.ClientRectangle, 
                    ThemeManager.BorderDark, ButtonBorderStyle.Solid);
            };
            mainPanel.Controls.Add(formPanel);

            // Username section
            Label lblUsername = new Label
            {
                Text = "Username:",
                Font = new Font("Arial", 11, FontStyle.Bold),
                ForeColor = ThemeManager.TextLight,
                Location = new Point(0, 20),
                Size = new Size(100, 25)
            };
            formPanel.Controls.Add(lblUsername);

            txtUsername = new TextBox
            {
                Location = new Point(0, 45),
                Size = new Size(280, 30),
                Font = new Font("Arial", 11),
                BackColor = ThemeManager.ContentColor,
                ForeColor = ThemeManager.TextPrimary,
                BorderStyle = BorderStyle.FixedSingle,
                Text = "test_user"
            };
            formPanel.Controls.Add(txtUsername);

            // Username hint
            Label usernameHint = new Label
            {
                Text = "Try: admin, test_user, or SQL injection payloads",
                Font = new Font("Arial", 8, FontStyle.Italic),
                ForeColor = ThemeManager.InfoColor,
                Location = new Point(0, 75),
                Size = new Size(280, 15)
            };
            formPanel.Controls.Add(usernameHint);

            // Password section
            Label lblPassword = new Label
            {
                Text = "Password:",
                Font = new Font("Arial", 11, FontStyle.Bold),
                ForeColor = ThemeManager.TextLight,
                Location = new Point(0, 105),
                Size = new Size(100, 25)
            };
            formPanel.Controls.Add(lblPassword);

            txtPassword = new TextBox
            {
                Location = new Point(0, 130),
                Size = new Size(280, 30),
                Font = new Font("Arial", 11),
                BackColor = ThemeManager.ContentColor,
                ForeColor = ThemeManager.TextPrimary,
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true,
                Text = "password123"
            };
            formPanel.Controls.Add(txtPassword);

            // Password hint
            Label passwordHint = new Label
            {
                Text = "Try: password123, admin, or ' OR '1'='1",
                Font = new Font("Arial", 8, FontStyle.Italic),
                ForeColor = ThemeManager.InfoColor,
                Location = new Point(0, 160),
                Size = new Size(280, 15)
            };
            formPanel.Controls.Add(passwordHint);

            // Buttons section
            Panel buttonPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = ThemeManager.PrimaryColor,
                Padding = new Padding(0, 10, 0, 0)
            };
            mainPanel.Controls.Add(buttonPanel);

            Button btnLogin = new Button
            {
                Text = "🔑 Login",
                Size = new Size(130, 40),
                Location = new Point(40, 10),
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 200);
            btnLogin.Click += BtnLogin_Click;
            buttonPanel.Controls.Add(btnLogin);

            Button btnRegister = new Button
            {
                Text = "📝 Register",
                Size = new Size(130, 40),
                Location = new Point(180, 10),
                BackColor = ThemeManager.SuccessColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 130, 50);
            btnRegister.Click += BtnRegister_Click;
            buttonPanel.Controls.Add(btnRegister);

            // Vulnerability info section
            Panel infoPanel = new Panel
            {
                Height = 120,
                Dock = DockStyle.Fill,
                BackColor = ThemeManager.PrimaryColor,
                Padding = new Padding(20, 10, 20, 10)
            };
            mainPanel.Controls.Add(infoPanel);

            Label infoTitle = new Label
            {
                Text = "🔍 Security Testing Information",
                Font = new Font("Arial", 11, FontStyle.Bold),
                ForeColor = ThemeManager.DangerColor,
                Dock = DockStyle.Top,
                Height = 25
            };
            infoPanel.Controls.Add(infoTitle);

            Label infoText = new Label
            {
                Text = "• This login form contains intentional SQL injection vulnerabilities\n" +
                       "• Try bypassing authentication with: ' OR '1'='1 --\n" +
                       "• Error messages reveal database structure information\n" +
                       "• Registration accepts any input without validation",
                Font = new Font("Arial", 9),
                ForeColor = ThemeManager.TextSecondary,
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 0, 0, 0)
            };
            infoPanel.Controls.Add(infoText);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (var con = new SQLiteConnection(dbPath))
                {
                    con.Open();

                    // ⚠️ VULNERABLE CODE: Direct string concatenation enables SQL injection
                    string query = $"SELECT * FROM users WHERE username = '{txtUsername.Text}' AND password = '{txtPassword.Text}'";
                    
                    // Log the vulnerable query for educational purposes
                    string logMessage = $"VULNERABLE QUERY EXECUTED:\n{query}\n\n" +
                                      $"Input Username: {txtUsername.Text}\n" +
                                      $"Input Password: {txtPassword.Text}\n\n" +
                                      "This demonstrates DA1 - SQL Injection vulnerability.";

                    var cmd = new SQLiteCommand(query, con);
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string username = reader["username"].ToString();
                        MessageBox.Show($"✅ Login Successful!\n\nWelcome, {username}!\n\n{logMessage}", 
                                      "Authentication Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Open user dashboard or main application
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"❌ Login Failed!\n\nInvalid credentials.\n\n{logMessage}", 
                                      "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                // DA6 - Security Misconfiguration: Exposing detailed error messages
                string errorInfo = $"🚨 SQL ERROR EXPOSED:\n\n{ex.Message}\n\n" +
                                 $"Query: SELECT * FROM users WHERE username = '{txtUsername.Text}' AND password = '{txtPassword.Text}'\n\n" +
                                 "This error message reveals database structure - a security vulnerability!";
                
                MessageBox.Show(errorInfo, "Database Error - Security Vulnerability", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter both username and password.", "Validation Error", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var con = new SQLiteConnection(dbPath))
                {
                    con.Open();
                    
                    // Using parameterized query for registration (more secure)
                    string insertQuery = "INSERT INTO users (username, password) VALUES (@username, @password)";
                    var cmd = new SQLiteCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    
                    MessageBox.Show($"✅ User Registration Successful!\n\nUsername: {txtUsername.Text}\n" +
                                  "You can now login with these credentials.\n\n" +
                                  "Note: Password is stored in plaintext (DA3 - Data Exposure vulnerability)", 
                                  "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration Error: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static class UserLoginLauncher
        {
            public static void ShowUserLoginForm()
            {
                UserLoginForm form = new UserLoginForm();
                form.ShowDialog();
            }
        }
    }
}
