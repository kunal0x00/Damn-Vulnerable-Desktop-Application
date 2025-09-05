using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVDAv2
{
    public partial class ProductsForm : Form
    {
        private FlowLayoutPanel productsPanel;
        private Panel headerPanel;

        public ProductsForm()
        {
            InitializeComponent();
            InitializeForm();
            LoadProducts();
        }

        private void InitializeForm()
        {
            this.Text = "VulnMart - Product Catalog";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Header
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = ThemeManager.PrimaryColor
            };

            var titleLabel = new Label
            {
                Text = "🛒 VulnMart Product Catalog",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 18),
                AutoSize = true
            };

            headerPanel.Controls.Add(titleLabel);

            // Products panel
            productsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(20)
            };

            this.Controls.AddRange(new Control[] { headerPanel, productsPanel });
        }

        private void LoadProducts()
        {
            // Sample vulnerable product data
            var products = new[]
            {
                new { Id = 1, Name = "Laptop Pro", Price = 1299.99m, Description = "High-performance laptop", Stock = 10 },
                new { Id = 2, Name = "Smartphone X", Price = 899.99m, Description = "Latest smartphone", Stock = 25 },
                new { Id = 3, Name = "Tablet Air", Price = 599.99m, Description = "Lightweight tablet", Stock = 15 },
                new { Id = 4, Name = "Wireless Headphones", Price = 299.99m, Description = "Noise-canceling headphones", Stock = 30 },
                new { Id = 5, Name = "Smart Watch", Price = 399.99m, Description = "Fitness tracking watch", Stock = 20 },
                new { Id = 6, Name = "Gaming Console", Price = 499.99m, Description = "Next-gen gaming", Stock = 8 }
            };

            foreach (var product in products)
            {
                CreateProductCard(product.Id, product.Name, product.Price, product.Description, product.Stock);
            }
        }

        private void CreateProductCard(int id, string name, decimal price, string description, int stock)
        {
            var card = new Panel
            {
                Size = new Size(280, 350),
                Margin = new Padding(10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Product image placeholder
            var imagePanel = new Panel
            {
                Size = new Size(260, 180),
                Location = new Point(10, 10),
                BackColor = Color.LightGray
            };

            var imageLabel = new Label
            {
                Text = "📱",
                Font = new Font("Segoe UI", 48),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                ForeColor = Color.DarkGray
            };
            imagePanel.Controls.Add(imageLabel);

            // Product info
            var nameLabel = new Label
            {
                Text = name,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 200),
                Size = new Size(260, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var descLabel = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, 225),
                Size = new Size(260, 40),
                TextAlign = ContentAlignment.TopLeft,
                ForeColor = Color.Gray
            };

            var priceLabel = new Label
            {
                Text = $"${price:F2}",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(10, 270),
                Size = new Size(120, 25),
                ForeColor = ThemeManager.PrimaryColor
            };

            var stockLabel = new Label
            {
                Text = $"Stock: {stock}",
                Font = new Font("Segoe UI", 9),
                Location = new Point(180, 275),
                Size = new Size(80, 20),
                ForeColor = stock > 5 ? Color.Green : Color.Red
            };

            // Buttons
            var addToCartBtn = new Button
            {
                Text = "Add to Cart",
                Size = new Size(100, 30),
                Location = new Point(10, 305),
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            addToCartBtn.Click += (s, e) => AddToCart(id, name, price);

            var detailsBtn = new Button
            {
                Text = "Details",
                Size = new Size(80, 30),
                Location = new Point(120, 305),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            detailsBtn.Click += (s, e) => ShowProductDetails(id, name, description, price);

            // DA5 - Admin button visible to all users (improper authorization)
            var adminBtn = new Button
            {
                Text = "Admin",
                Size = new Size(60, 30),
                Location = new Point(210, 305),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            adminBtn.Click += (s, e) => AdminEditProduct(id, name);

            card.Controls.AddRange(new Control[] 
            { 
                imagePanel, nameLabel, descLabel, priceLabel, stockLabel, 
                addToCartBtn, detailsBtn, adminBtn 
            });

            productsPanel.Controls.Add(card);
        }

        private void AddToCart(int productId, string productName, decimal price)
        {
            // DA10 - Log sensitive purchase information
            System.IO.File.AppendAllText("app.log", 
                $"[{DateTime.Now}] Product added to cart: {productName} (${price}) by user session\n");

            MessageBox.Show($"'{productName}' added to cart!\n\nPrice: ${price:F2}\n\n" +
                "Note: This action was logged with sensitive information!",
                "Added to Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowProductDetails(int id, string name, string description, decimal price)
        {
            // DA6 - Expose internal product IDs and system information
            MessageBox.Show($"Product Details:\n\n" +
                $"Internal ID: {id}\n" +
                $"Name: {name}\n" +
                $"Description: {description}\n" +
                $"Price: ${price:F2}\n" +
                $"Server Time: {DateTime.Now}\n" +
                $"System Path: {Environment.CurrentDirectory}\n\n" +
                "WARNING: Internal system information exposed!",
                "Product Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AdminEditProduct(int productId, string productName)
        {
            // DA5 - No authorization check for admin functions
            var editForm = new Form
            {
                Text = "Admin - Edit Product",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterParent
            };

            var warningLabel = new Label
            {
                Text = "⚠️ VULNERABILITY: No authorization check performed!",
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(350, 40)
            };

            var nameTextBox = new TextBox
            {
                Text = productName,
                Location = new Point(20, 80),
                Size = new Size(340, 25)
            };

            var priceTextBox = new TextBox
            {
                Text = "999.99",
                Location = new Point(20, 120),
                Size = new Size(340, 25)
            };

            var saveBtn = new Button
            {
                Text = "Save Changes",
                Location = new Point(20, 160),
                Size = new Size(100, 30),
                BackColor = Color.Red,
                ForeColor = Color.White
            };
            saveBtn.Click += (s, e) => {
                // DA1 - Vulnerable update query (SQL injection possible)
                string updateQuery = $"UPDATE products SET name = '{nameTextBox.Text}', price = {priceTextBox.Text} WHERE id = {productId}";
                
                // DA10 - Log the vulnerable query
                System.IO.File.AppendAllText("app.log", 
                    $"[{DateTime.Now}] Admin action - SQL Query: {updateQuery}\n");

                MessageBox.Show($"Product updated!\n\nVulnerable SQL Query:\n{updateQuery}\n\n" +
                    "This query is vulnerable to SQL injection!", "Product Updated");
                editForm.Close();
            };

            editForm.Controls.AddRange(new Control[] 
            { 
                warningLabel, 
                new Label { Text = "Product Name:", Location = new Point(20, 60) },
                nameTextBox,
                new Label { Text = "Price:", Location = new Point(20, 100) },
                priceTextBox,
                saveBtn 
            });

            editForm.ShowDialog();
        }
    }
}
