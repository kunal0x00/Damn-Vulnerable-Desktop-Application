using System;
using System.Windows.Forms;

namespace DVDAv2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Show security warning
            MessageBox.Show(
                "⚠️ DVDA - Damn Vulnerable Desktop Application ⚠️\n\n" +
                "This application contains intentional security vulnerabilities for educational purposes:\n\n" +
                "• SQL Injection\n" +
                "• Command Injection\n" +
                "• Weak Authentication\n" +
                "• Sensitive Data Exposure\n" +
                "• Poor Cryptography\n" +
                "• Authorization Bypass\n" +
                "• And more...\n\n" +
                "Use responsibly for learning and testing only!",
                "Security Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            // Start with Login Form
            Application.Run(new LoginForm());
        }
    }
}
