using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVDAv2
{
    public static class ThemeManager
    {
        // Professional Dark Cyber Security Theme
        public static Color PrimaryColor = Color.FromArgb(33, 37, 41);        // Dark charcoal
        public static Color SecondaryColor = Color.FromArgb(52, 58, 64);      // Medium dark
        public static Color AccentColor = Color.FromArgb(0, 123, 255);        // Professional blue
        public static Color DangerColor = Color.FromArgb(220, 53, 69);        // Error red
        public static Color SuccessColor = Color.FromArgb(40, 167, 69);       // Success green
        public static Color WarningColor = Color.FromArgb(255, 193, 7);       // Warning amber
        public static Color InfoColor = Color.FromArgb(23, 162, 184);         // Info cyan
        
        // Background colors
        public static Color BackgroundColor = Color.FromArgb(248, 249, 250);  // Light background
        public static Color SidebarColor = Color.FromArgb(33, 37, 41);        // Dark sidebar
        public static Color ContentColor = Color.White;                       // White content area
        
        // Text colors
        public static Color TextPrimary = Color.FromArgb(33, 37, 41);         // Dark text
        public static Color TextSecondary = Color.FromArgb(108, 117, 125);    // Gray text
        public static Color TextLight = Color.White;                          // White text
        
        // Border colors
        public static Color BorderColor = Color.FromArgb(222, 226, 230);      // Light border
        public static Color BorderDark = Color.FromArgb(52, 58, 64);          // Dark border

        // Alias properties for compatibility
        public static Color TextColor => TextPrimary;
        public static Color ContentBackgroundColor => ContentColor;
        public static Color ButtonColor => SecondaryColor;
        public static Font DefaultFont => new Font("Segoe UI", 9, FontStyle.Regular);

        public static void ApplyDarkTheme(Form form)
        {
            form.BackColor = BackgroundColor;
            form.ForeColor = TextPrimary;
            ApplyThemeToControls(form);
        }

        private static void ApplyThemeToControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Panel panel)
                {
                    if (panel.Name == "sidebar" || panel.Dock == DockStyle.Left)
                    {
                        panel.BackColor = SidebarColor;
                        panel.ForeColor = TextLight;
                    }
                    else
                    {
                        panel.BackColor = ContentColor;
                        panel.ForeColor = TextPrimary;
                    }
                }
                else if (control is Button button)
                {
                    ApplyButtonTheme(button);
                }
                else if (control is TextBox textBox)
                {
                    textBox.BackColor = ContentColor;
                    textBox.ForeColor = TextPrimary;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is Label label)
                {
                    if (label.Parent?.Name == "sidebar" || label.Parent?.BackColor == SidebarColor)
                    {
                        label.ForeColor = TextLight;
                    }
                    else
                    {
                        label.ForeColor = TextPrimary;
                    }
                }

                // Recursively apply to child controls
                if (control.HasChildren)
                {
                    ApplyThemeToControls(control);
                }
            }
        }

        private static void ApplyButtonTheme(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            
            if (button.Name?.Contains("primary") == true || button.BackColor == Color.Red)
            {
                button.BackColor = AccentColor;
                button.ForeColor = TextLight;
                button.FlatAppearance.BorderColor = AccentColor;
            }
            else if (button.Name?.Contains("danger") == true)
            {
                button.BackColor = DangerColor;
                button.ForeColor = TextLight;
                button.FlatAppearance.BorderColor = DangerColor;
            }
            else if (button.Name?.Contains("success") == true)
            {
                button.BackColor = SuccessColor;
                button.ForeColor = TextLight;
                button.FlatAppearance.BorderColor = SuccessColor;
            }
            else
            {
                button.BackColor = SecondaryColor;
                button.ForeColor = TextLight;
                button.FlatAppearance.BorderColor = BorderDark;
            }
        }

        public static Panel CreateStatsCard(string title, string value, Color accentColor)
        {
            var card = new Panel
            {
                Size = new Size(200, 120),
                BackColor = ContentColor,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10),
                ForeColor = TextSecondary,
                Location = new Point(15, 15),
                AutoSize = true
            };

            var valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = accentColor,
                Location = new Point(15, 40),
                AutoSize = true
            };

            var accentBar = new Panel
            {
                Size = new Size(4, 120),
                BackColor = accentColor,
                Location = new Point(0, 0)
            };

            card.Controls.AddRange(new Control[] { accentBar, titleLabel, valueLabel });
            return card;
        }
    }
}
