using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVDAv2
{
    public partial class SecurityLabForm : MaterialForm
    {
        private Panel mainPanel;
        private Panel headerPanel;
        private FlowLayoutPanel vulnerabilityPanel;

        public SecurityLabForm()
        {
            InitializeComponent();
            InitializeForm();
            CreateVulnerabilityTests();
        }

        private void InitializeForm()
        {
            ThemeManager.ApplyMaterialTheme(this);
            
            this.Text = "DVDA - Security Lab";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(800, 600);

            // Header panel
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = ThemeManager.DangerColor
            };

            var titleLabel = new Label
            {
                Text = "🔓 DVDA Security Lab - OWASP Top 10 Desktop",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(800, 40),
                Location = new Point(20, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var subtitleLabel = new Label
            {
                Text = "Click buttons below to test various security vulnerabilities",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(800, 20),
                Location = new Point(20, 50),
                TextAlign = ContentAlignment.MiddleLeft
            };

            headerPanel.Controls.AddRange(new Control[] { titleLabel, subtitleLabel });

            // Main scrollable panel
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White
            };

            vulnerabilityPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                Padding = new Padding(20)
            };

            mainPanel.Controls.Add(vulnerabilityPanel);
            this.Controls.AddRange(new Control[] { headerPanel, mainPanel });
        }

        private void CreateVulnerabilityTests()
        {
            // DA1 - Injection
            CreateVulnerabilityCard(
                "DA1 - Injection Vulnerabilities", 
                "Test SQL injection and command injection",
                ThemeManager.DangerColor,
                new string[] { "Test SQL Injection", "Test Command Injection" },
                new Action[] { TestSQLInjection, TestCommandInjection }
            );

            // DA2 - Broken Authentication
            CreateVulnerabilityCard(
                "DA2 - Broken Authentication & Session Management",
                "Test weak authentication and session management",
                Color.FromArgb(255, 87, 34),
                new string[] { "Create Weak Session", "Test Session Security" },
                new Action[] { TestWeakSession, TestSessionSecurity }
            );

            // DA3 - Sensitive Data Exposure
            CreateVulnerabilityCard(
                "DA3 - Sensitive Data Exposure",
                "Test data exposure in memory and logs",
                Color.FromArgb(156, 39, 176),
                new string[] { "Expose Hardcoded Secrets", "Log Sensitive Data" },
                new Action[] { TestHardcodedSecrets, TestSensitiveLogging }
            );

            // DA4 - Improper Cryptography
            CreateVulnerabilityCard(
                "DA4 - Improper Cryptography Usage",
                "Test weak cryptographic implementations",
                Color.FromArgb(103, 58, 183),
                new string[] { "Use Weak Hashing", "Show Crypto Flaws" },
                new Action[] { TestWeakCrypto, ShowCryptoFlaws }
            );

            // DA5 - Improper Authorization
            CreateVulnerabilityCard(
                "DA5 - Improper Authorization",
                "Test authorization bypass",
                Color.FromArgb(63, 81, 181),
                new string[] { "Bypass Authorization", "Test Role Security" },
                new Action[] { TestAuthBypass, TestRoleSecurity }
            );

            // Continue with remaining vulnerabilities...
            CreateVulnerabilityCard(
                "DA6 - Security Misconfiguration",
                "Test security misconfigurations",
                Color.FromArgb(33, 150, 243),
                new string[] { "Upload File", "Show Stack Trace" },
                new Action[] { TestFileUpload, ShowStackTrace }
            );

            CreateVulnerabilityCard(
                "DA7 - Insecure Communication",
                "Test insecure data transmission",
                Color.FromArgb(0, 188, 212),
                new string[] { "Send Unencrypted Data", "Test Communication" },
                new Action[] { TestUnencryptedComm, TestCommunication }
            );

            CreateVulnerabilityCard(
                "DA8 - Poor Code Quality",
                "Test code quality issues",
                Color.FromArgb(0, 150, 136),
                new string[] { "Buffer Overflow", "Race Condition" },
                new Action[] { TestBufferOverflow, TestRaceCondition }
            );

            CreateVulnerabilityCard(
                "DA9 - Using Components with Known Vulnerabilities",
                "Test vulnerable component usage",
                Color.FromArgb(76, 175, 80),
                new string[] { "Check Vulnerable Components", "Component Info" },
                new Action[] { TestVulnerableComponents, ShowComponentInfo }
            );

            CreateVulnerabilityCard(
                "DA10 - Insufficient Logging & Monitoring",
                "Test logging and monitoring issues",
                Color.FromArgb(139, 195, 74),
                new string[] { "Test Logging", "View Logs" },
                new Action[] { TestLogging, ViewLogs }
            );
        }

        private void CreateVulnerabilityCard(string title, string description, Color cardColor, string[] buttonTexts, Action[] buttonActions)
        {
            var card = new Panel
            {
                Size = new Size(920, 120),
                Margin = new Padding(0, 10, 0, 10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var colorStrip = new Panel
            {
                Size = new Size(5, 120),
                Location = new Point(0, 0),
                BackColor = cardColor
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(20, 15),
                Size = new Size(500, 25),
                ForeColor = Color.FromArgb(33, 33, 33)
            };

            var descLabel = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 9),
                Location = new Point(20, 45),
                Size = new Size(500, 40),
                ForeColor = Color.FromArgb(117, 117, 117)
            };

            card.Controls.AddRange(new Control[] { colorStrip, titleLabel, descLabel });

            // Add buttons
            int buttonX = 540;
            for (int i = 0; i < buttonTexts.Length && i < buttonActions.Length; i++)
            {
                var btn = new MaterialRaisedButton
                {
                    Text = buttonTexts[i],
                    Size = new Size(160, 35),
                    Location = new Point(buttonX, 20 + (i * 45)),
                    Primary = true
                };

                int index = i; // Capture for closure
                btn.Click += (s, e) => buttonActions[index]?.Invoke();
                card.Controls.Add(btn);
            }

            vulnerabilityPanel.Controls.Add(card);
        }

        // Test Methods for each vulnerability
        private void TestSQLInjection()
        {
            var form = new Form { Text = "SQL Injection Test", Size = new Size(500, 300) };
            var input = new TextBox { Location = new Point(20, 20), Size = new Size(400, 25) };
            input.Text = "admin' OR '1'='1' --";
            
            var btn = new Button { Text = "Test Login", Location = new Point(20, 60), Size = new Size(100, 30) };
            btn.Click += (s, e) => {
                bool result = VulnerabilityManager.VulnerableLogin(input.Text, "password");
                MessageBox.Show($"Login result: {result}\nQuery executed with input: {input.Text}");
            };

            form.Controls.AddRange(new Control[] { 
                new Label { Text = "Username (try SQL injection):", Location = new Point(20, 5) },
                input, btn 
            });
            form.ShowDialog();
        }

        private void TestCommandInjection()
        {
            var input = "test & calc"; // Simplified for now
            
            if (!string.IsNullOrEmpty(input))
            {
                VulnerabilityManager.ExecuteSystemCommand(input);
                MessageBox.Show("Command executed! Check if calculator opened.");
            }
        }

        private void TestWeakSession()
        {
            string session = VulnerabilityManager.CreateWeakSession("testuser");
            MessageBox.Show($"Weak session created: {session}\n\nNotice the predictable pattern!", "Session Created");
        }

        private void TestSessionSecurity()
        {
            MessageBox.Show("Session Security Issues:\n\n" +
                "• No session timeout\n" +
                "• Predictable session IDs\n" +
                "• No session invalidation\n" +
                "• Multiple concurrent sessions allowed", "Session Security");
        }

        private void TestHardcodedSecrets()
        {
            MessageBox.Show($"Hardcoded Secrets Found:\n\n" +
                $"Password: {VulnerabilityManager.HardcodedPassword}\n" +
                $"DB Password: {VulnerabilityManager.DatabasePassword}\n" +
                $"API Key: {VulnerabilityManager.ApiKey}", "Hardcoded Secrets");
        }

        private void TestSensitiveLogging()
        {
            VulnerabilityManager.LogAction("User entered credit card: 4532-1234-5678-9012");
            MessageBox.Show("Sensitive data logged! Check app.log file.", "Sensitive Logging");
        }

        private void TestWeakCrypto()
        {
            string password = "MyPassword123";
            string hash = VulnerabilityManager.WeakHashPassword(password);
            MessageBox.Show($"Password: {password}\nMD5 Hash: {hash}\n\nMD5 is cryptographically broken!", "Weak Cryptography");
        }

        private void ShowCryptoFlaws()
        {
            MessageBox.Show("Cryptographic Flaws:\n\n" +
                "• Using MD5 (broken algorithm)\n" +
                "• No salt for password hashing\n" +
                "• Predictable encryption keys\n" +
                "• No key rotation", "Crypto Flaws");
        }

        private void TestAuthBypass()
        {
            bool hasAccess = VulnerabilityManager.HasAdminAccess("normaluser_admin");
            MessageBox.Show($"User 'normaluser_admin' has admin access: {hasAccess}\n\n" +
                "Authorization can be bypassed by including 'admin' in username!", "Authorization Bypass");
        }

        private void TestRoleSecurity()
        {
            MessageBox.Show("Role Security Issues:\n\n" +
                "• No proper role-based access control\n" +
                "• String-based role checking\n" +
                "• No privilege escalation protection\n" +
                "• Missing principle of least privilege", "Role Security");
        }

        private void TestFileUpload()
        {
            var openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                bool success = VulnerabilityManager.ProcessFileUpload(openDialog.FileName);
                MessageBox.Show($"File upload result: {success}\n\nNo file type validation performed!", "File Upload");
            }
        }

        private void ShowStackTrace()
        {
            try
            {
                throw new Exception("Intentional error for demonstration");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Details Exposed:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Stack Trace Exposure");
            }
        }

        private void TestUnencryptedComm()
        {
            string sensitiveData = "Credit Card: 4532-1234-5678-9012, SSN: 123-45-6789";
            VulnerabilityManager.SendUnencryptedData(sensitiveData);
            MessageBox.Show("Sensitive data sent without encryption!\nCheck network_data.txt file.", "Unencrypted Communication");
        }

        private void TestCommunication()
        {
            MessageBox.Show("Communication Security Issues:\n\n" +
                "• No encryption for sensitive data\n" +
                "• Weak TLS configuration\n" +
                "• No certificate validation\n" +
                "• Plain text protocols", "Communication Issues");
        }

        private void TestBufferOverflow()
        {
            string longInput = new string('A', 50); // Longer than buffer
            VulnerabilityManager.ProcessUserInput(longInput);
            MessageBox.Show("Buffer overflow attempted!\nCheck errors.log for details.", "Buffer Overflow");
        }

        private void TestRaceCondition()
        {
            // Start multiple threads to demonstrate race condition
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() => VulnerabilityManager.IncrementCounter());
            }
            MessageBox.Show("Race condition test initiated!\nMultiple threads accessing shared resource.", "Race Condition");
        }

        private void TestVulnerableComponents()
        {
            VulnerabilityManager.UseVulnerableComponent();
            MessageBox.Show("Using potentially vulnerable components:\n\n" +
                "• Old SQLite version\n" +
                "• Outdated libraries\n" +
                "• Unpatched dependencies", "Vulnerable Components");
        }

        private void ShowComponentInfo()
        {
            MessageBox.Show("Component Vulnerability Info:\n\n" +
                "• Check for known CVEs\n" +
                "• Regular security updates needed\n" +
                "• Dependency scanning required\n" +
                "• Version management important", "Component Security");
        }

        private void TestLogging()
        {
            VulnerabilityManager.LogAction("User performed sensitive action with data: SECRET123");
            MessageBox.Show("Logging test completed!\n\n" +
                "Issues:\n" +
                "• Sensitive data in logs\n" +
                "• No log rotation\n" +
                "• No access controls on logs", "Logging Test");
        }

        private void ViewLogs()
        {
            try
            {
                string logContent = System.IO.File.ReadAllText("app.log");
                var logForm = new Form { Text = "Application Logs", Size = new Size(600, 400) };
                var textBox = new TextBox { 
                    Multiline = true, 
                    ScrollBars = ScrollBars.Both, 
                    Dock = DockStyle.Fill,
                    Text = logContent,
                    ReadOnly = true
                };
                logForm.Controls.Add(textBox);
                logForm.ShowDialog();
            }
            catch
            {
                MessageBox.Show("No log file found or unable to read logs.", "View Logs");
            }
        }
    }
}
