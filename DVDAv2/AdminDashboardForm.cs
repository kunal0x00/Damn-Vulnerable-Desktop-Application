using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace DVDAv2
{
    public partial class AdminDashboardForm : Form
    {
        private FlowLayoutPanel flowProducts;
        private List<Product> cart = new List<Product>();
        private TextBox txtNewUser;
        private TextBox txtNewPass;
        private Button btnCreateUser;
        public AdminDashboardForm()
        {
            InitializeComponent();
            InitializeDashboardUI();
            LoadProducts();
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string user = txtNewUser.Text;
            string pass = txtNewPass.Text;

            using (var con = new SQLiteConnection("Data Source=vulnmart.db"))
            {
                con.Open();
                var cmd = new SQLiteCommand("INSERT INTO users (username, password) VALUES (@u, @p)", con);
                cmd.Parameters.AddWithValue("@u", user);
                cmd.Parameters.AddWithValue("@p", pass);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Created!");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            new UserLoginForm().ShowDialog();
        }

        private void InitializeDashboardUI()
        {
            this.Text = "VulnMart - Admin Dashboard";
            this.Size = new Size(800, 600);

            flowProducts = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                Padding = new Padding(10)
            };

            Button btnCart = new Button
            {
                Text = "🛒 Go to Cart",
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.LightGreen
            };
            btnCart.Click += BtnGoToCart_Click;

            this.Controls.Add(flowProducts);
            this.Controls.Add(btnCart);
        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = new List<Product>
            {
                new Product { Name = "Wireless Mouse", Price = "₹699", ImagePath = "images/mouse.jpg" },
                new Product { Name = "USB Keyboard", Price = "₹499", ImagePath = "images/keyboard.jpg" },
                new Product { Name = "16GB Pendrive", Price = "₹349", ImagePath = "images/pendrive.jpg" },
                new Product { Name = "HD Monitor", Price = "₹5999", ImagePath = "images/monitor.jpg" },
                new Product { Name = "Laptop Stand", Price = "₹799", ImagePath = "images/stand.jpg" }
            };

            foreach (var product in products)
            {
                flowProducts.Controls.Add(CreateProductCard(product));
            }
        }

        private Panel CreateProductCard(Product product)
        {
            Panel card = new Panel
            {
                Width = 180,
                Height = 250,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            PictureBox pic = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 160,
                Height = 120,
                Location = new Point(10, 10),
                Image = Image.FromFile(product.ImagePath)
            };

            Label lblName = new Label
            {
                Text = product.Name,
                Location = new Point(10, 140),
                Width = 160,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            Label lblPrice = new Label
            {
                Text = product.Price,
                Location = new Point(10, 165),
                Width = 160,
                ForeColor = Color.DarkGreen
            };

            Button btnAdd = new Button
            {
                Text = "Add to Cart",
                Width = 160,
                Location = new Point(10, 190),
                Tag = product,
                BackColor = Color.LightBlue
            };
            btnAdd.Click += BtnAddToCart_Click;

            card.Controls.Add(pic);
            card.Controls.Add(lblName);
            card.Controls.Add(lblPrice);
            card.Controls.Add(btnAdd);

            return card;
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var product = (Product)btn.Tag;

            cart.Add(product);
            MessageBox.Show($"{product.Name} added to cart!", "Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnGoToCart_Click(object sender, EventArgs e)
        {
            // Convert Product list to string list for CartForm
            var cartItemNames = new List<string>();
            foreach (var product in cart)
            {
                cartItemNames.Add(product.Name);
            }
            var cartForm = new CartForm(cartItemNames);
            cartForm.ShowDialog();
        }
        private void btnOpenCart_Click(object sender, EventArgs e)
        {
            new CartForm().ShowDialog();
        }
        private void BtnCreateUser_Click(object sender, EventArgs e)
        {
            string user = txtNewUser.Text.Trim();
            string pass = txtNewPass.Text.Trim();

            if (user == "" || pass == "")
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var con = new SQLiteConnection("Data Source=vulnmart.db"))
            {
                con.Open();
                var cmd = new SQLiteCommand("INSERT INTO users (username, password) VALUES (@u, @p)", con);
                cmd.Parameters.AddWithValue("@u", user);
                cmd.Parameters.AddWithValue("@p", pass);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Created!");
            }

            txtNewUser.Text = "";
            txtNewPass.Text = "";
        }
    }

    public partial class Product
    {
        public new string Name { get; set; }
        public string Price { get; set; }
        public string ImagePath { get; set; }
    }
}