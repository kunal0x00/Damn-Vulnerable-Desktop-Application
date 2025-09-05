using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DVDAv2
{
    public partial class AdminLoginForm : Form
    {
        private TextBox txtAdminUsername;
        private TextBox txtAdminPassword;
        public AdminLoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtAdminUsername.Text;
            string password = txtAdminPassword.Text;

            // ❗ DELIBERATE SQL Injection vulnerability
            string query = $"SELECT * FROM admin WHERE username = '{username}' AND password = '{password}'";

            try
            {
                using (var con = new SQLiteConnection("Data Source=vulnmart.db"))
                {
                    con.Open();
                    var cmd = new SQLiteCommand(query, con);
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        MessageBox.Show("Admin Login successful!");
                        this.Hide();
                        new AdminDashboardForm().ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
