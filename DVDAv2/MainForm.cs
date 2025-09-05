using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DVDAv2
{
    public partial class MainForm : Form
    {
        private Panel sidebarPanel;
        private Panel contentPanel;

        public MainForm()
        {
            InitializeComponents();
            ApplyProfessionalTheme();
            SetupMainInterface();
        }

        private void InitializeComponents()
        {
            this.Text = "ShopVault - Premium Shopping Experience";
            this.Size = new Size(1600, 1000);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1400, 800);
            this.Icon = SystemIcons.Application;
            this.WindowState = FormWindowState.Maximized;
        }

        private void ApplyProfessionalTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;
            this.ForeColor = ThemeManager.TextColor;
            this.Font = ThemeManager.DefaultFont;
        }

        private void SetupMainInterface()
        {
            // Clear any existing controls
            this.Controls.Clear();

            // Create main container with modern layout
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.White
            };
            
            // Set row heights: Header (80px), Content (expand), Footer (40px)
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            this.Controls.Add(mainLayout);

            // Create modern header
            CreateModernHeader(mainLayout);
            
            // Create content area with sidebar
            CreateModernContent(mainLayout);

            // Create footer
            CreateModernFooter(mainLayout);

            // Show homepage by default
            ShowModernHomePage();
        }

        private bool isUserLoggedIn = false;
        private string currentUser = "";
        private Panel headerPanel;
        private Panel contentMainPanel;

        private void CreateModernHeader(TableLayoutPanel mainLayout)
        {
            headerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(35, 47, 62), // Dark blue
                Margin = new Padding(0)
            };
            mainLayout.Controls.Add(headerPanel, 0, 0);

            // Add a subtle shadow effect
            headerPanel.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, headerPanel.Height - 2, headerPanel.Width, 2),
                    Color.FromArgb(50, 0, 0, 0), ButtonBorderStyle.Solid);
            };

            // Header content layout
            TableLayoutPanel headerLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = new Padding(0),
                Padding = new Padding(15, 8, 15, 8)
            };
            
            // Column styles: Logo(200), Categories(160), Search(expand), Cart(80), Account(150)
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            headerPanel.Controls.Add(headerLayout);

            // Logo
            CreateHeaderLogo(headerLayout);
            
            // Categories dropdown
            CreateCategoriesDropdown(headerLayout);
            
            // Search bar
            CreateHeaderSearchBar(headerLayout);
            
            // Cart button
            CreateCartButton(headerLayout);
            
            // Account section
            CreateAccountSection(headerLayout);
        }

        private void CreateHeaderLogo(TableLayoutPanel headerLayout)
        {
            Panel logoPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 10, 10, 10)
            };
            
            Label logoLabel = new Label
            {
                Text = "🛒",
                Font = new Font("Segoe UI", 20),
                ForeColor = Color.FromArgb(255, 159, 28), // Orange color
                Location = new Point(0, 0),
                Size = new Size(30, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };
            logoPanel.Controls.Add(logoLabel);
            
            Label brandLabel = new Label
            {
                Text = "ShopVault",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(35, 0),
                Size = new Size(150, 40),
                TextAlign = ContentAlignment.MiddleLeft
            };
            logoPanel.Controls.Add(brandLabel);
            
            Label taglineLabel = new Label
            {
                Text = "Premium Shopping",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(200, 200, 200),
                Location = new Point(35, 25),
                Size = new Size(150, 15),
                TextAlign = ContentAlignment.TopLeft
            };
            logoPanel.Controls.Add(taglineLabel);
            
            logoPanel.Click += (s, e) => ShowModernHomePage();
            logoLabel.Click += (s, e) => ShowModernHomePage();
            brandLabel.Click += (s, e) => ShowModernHomePage();
            
            // Hover effects
            logoPanel.MouseEnter += (s, e) => logoPanel.BackColor = Color.FromArgb(20, 255, 255, 255);
            logoPanel.MouseLeave += (s, e) => logoPanel.BackColor = Color.Transparent;
            
            headerLayout.Controls.Add(logoPanel, 0, 0);
        }

        private void CreateCategoriesDropdown(TableLayoutPanel headerLayout)
        {
            Panel categoryPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 12, 8, 12)
            };
            
            ComboBox categoriesCombo = new ComboBox
            {
                Items = { "All Categories", "Electronics", "Clothing", "Books", "Home & Garden", "Gaming", "Sports", "Beauty" },
                SelectedIndex = 0,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 50, 50),
                FlatStyle = FlatStyle.Flat
            };
            
            categoriesCombo.SelectedIndexChanged += (s, e) => {
                if (categoriesCombo.SelectedIndex > 0)
                {
                    string category = categoriesCombo.SelectedItem.ToString();
                    ShowCategoryProducts(category);
                    categoriesCombo.SelectedIndex = 0; // Reset to "All Categories"
                }
            };
            
            categoryPanel.Controls.Add(categoriesCombo);
            headerLayout.Controls.Add(categoryPanel, 1, 0);
        }

        private void CreateHeaderSearchBar(TableLayoutPanel headerLayout)
        {
            Panel searchPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Margin = new Padding(10, 12, 10, 12)
            };
            headerLayout.Controls.Add(searchPanel, 2, 0);

            TextBox searchBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 14),
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0, 0, 50, 0),
                BackColor = Color.White
            };
            searchBox.Text = "Search for products, brands and more...";
            searchBox.ForeColor = Color.Gray;
            
            searchBox.Enter += (s, e) => {
                if (searchBox.Text == "Search for products, brands and more...")
                {
                    searchBox.Text = "";
                    searchBox.ForeColor = Color.Black;
                }
            };
            
            searchBox.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(searchBox.Text))
                {
                    searchBox.Text = "Search for products, brands and more...";
                    searchBox.ForeColor = Color.Gray;
                }
            };
            
            Button searchButton = new Button
            {
                Text = "🔍",
                Size = new Size(45, 36),
                Location = new Point(searchPanel.Width - 50, 0),
                BackColor = Color.FromArgb(255, 159, 28),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            searchButton.FlatAppearance.BorderSize = 0;
            searchButton.Click += (s, e) => {
                if (searchBox.Text != "Search for products, brands and more..." && !string.IsNullOrWhiteSpace(searchBox.Text))
                {
                    // DA1 - SQL Injection vulnerability in search
                    string result = VulnerabilityManager.SearchProducts(searchBox.Text);
                    ShowSearchResults(result);
                }
            };
            
            searchPanel.Controls.Add(searchBox);
            searchPanel.Controls.Add(searchButton);
            
            searchPanel.SizeChanged += (s, e) => {
                searchButton.Location = new Point(searchPanel.Width - 50, 2);
                searchBox.Size = new Size(searchPanel.Width - 55, searchBox.Height);
            };
        }

        private void CreateCartButton(TableLayoutPanel headerLayout)
        {
            Button cartButton = new Button
            {
                Text = "🛒\nCart (3)",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                Margin = new Padding(5, 10, 5, 10)
            };
            cartButton.FlatAppearance.BorderSize = 1;
            cartButton.FlatAppearance.BorderColor = Color.FromArgb(255, 159, 28);
            cartButton.Click += (s, e) => ShowShoppingCart();
            headerLayout.Controls.Add(cartButton, 3, 0);
        }

        private void CreateAccountSection(TableLayoutPanel headerLayout)
        {
            Panel accountPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Margin = new Padding(10, 10, 0, 10)
            };
            headerLayout.Controls.Add(accountPanel, 4, 0);

            if (isUserLoggedIn)
            {
                CreateLoggedInView(accountPanel);
            }
            else
            {
                CreateLoginView(accountPanel);
            }
        }

        private void CreateLoginView(Panel accountPanel)
        {
            Button loginButton = new Button
            {
                Text = "👤 Sign In",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(255, 159, 28),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(140, 35),
                Location = new Point(5, 15),
                Cursor = Cursors.Hand
            };
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.Click += (s, e) => ShowLoginSignupPanel();
            
            Label signupLabel = new Label
            {
                Text = "New user? Sign up",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(255, 159, 28),
                Location = new Point(15, 52),
                Size = new Size(120, 15),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            signupLabel.Click += (s, e) => ShowLoginSignupPanel(true);
            
            accountPanel.Controls.Add(loginButton);
            accountPanel.Controls.Add(signupLabel);
        }

        private void CreateLoggedInView(Panel accountPanel)
        {
            Label welcomeLabel = new Label
            {
                Text = $"Hello, {currentUser}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(5, 10),
                Size = new Size(130, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            Button accountButton = new Button
            {
                Text = "Account ▼",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(5, 30),
                Size = new Size(80, 25),
                Cursor = Cursors.Hand
            };
            accountButton.FlatAppearance.BorderSize = 0;
            accountButton.Click += (s, e) => ShowAccountDropdown();
            
            Button logoutButton = new Button
            {
                Text = "Logout",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(255, 159, 28),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(90, 30),
                Size = new Size(50, 25),
                Cursor = Cursors.Hand
            };
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.Click += (s, e) => LogoutUser();
            
            accountPanel.Controls.Add(welcomeLabel);
            accountPanel.Controls.Add(accountButton);
            accountPanel.Controls.Add(logoutButton);
        }

        private void CreateSidebarHeader()
        {
            Panel headerPanel = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = ThemeManager.SidebarColor,
                Margin = new Padding(0, 0, 0, 10)
            };
            sidebarPanel.Controls.Add(headerPanel);

            Label titleLabel = new Label
            {
                Text = "� ShopVault",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };
            headerPanel.Controls.Add(titleLabel);

            Label subtitleLabel = new Label
            {
                Text = "Premium Shopping Experience",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = ThemeManager.AccentColor,
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };
            headerPanel.Controls.Add(subtitleLabel);
        }

        private void CreateNavigationMenu()
        {
            Panel navPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeManager.SidebarColor,
                AutoScroll = true
            };
            sidebarPanel.Controls.Add(navPanel);

            int buttonY = 10;
            int buttonHeight = 40;
            int buttonSpacing = 5;

            // Dashboard
            CreateNavButton(navPanel, "🏠 Home", buttonY, ShowHomePage);
            buttonY += buttonHeight + buttonSpacing;

            // Shopping categories
            CreateSectionLabel(navPanel, "Shop by Category", buttonY);
            buttonY += 25;

            CreateNavButton(navPanel, "📱 Electronics", buttonY, () => ShowCategory("Electronics"));
            buttonY += buttonHeight + buttonSpacing;

            CreateNavButton(navPanel, "👔 Clothing", buttonY, () => ShowCategory("Clothing"));
            buttonY += buttonHeight + buttonSpacing;

            CreateNavButton(navPanel, "📚 Books", buttonY, () => ShowCategory("Books"));
            buttonY += buttonHeight + buttonSpacing;

            CreateNavButton(navPanel, "🏠 Home & Garden", buttonY, () => ShowCategory("Home"));
            buttonY += buttonHeight + buttonSpacing;

            CreateNavButton(navPanel, "🎮 Gaming", buttonY, () => ShowCategory("Gaming"));
            buttonY += buttonHeight + buttonSpacing;

            // User functions
            CreateSectionLabel(navPanel, "Your Account", buttonY);
            buttonY += 25;

            CreateNavButton(navPanel, "🛒 Shopping Cart", buttonY, ShowShoppingCart);
            buttonY += buttonHeight + buttonSpacing;

            CreateNavButton(navPanel, "📦 Order History", buttonY, ShowOrderHistory);
            buttonY += buttonHeight + buttonSpacing;

            CreateNavButton(navPanel, "💳 Payment Methods", buttonY, ShowPaymentMethods);
            buttonY += buttonHeight + buttonSpacing;

            CreateNavButton(navPanel, "⚙️ Settings", buttonY, ShowUserSettings);
            buttonY += buttonHeight + buttonSpacing;

            // Admin section (vulnerabilities hidden here)
            CreateSectionLabel(navPanel, "Admin Panel", buttonY);
            buttonY += 25;

            CreateNavButton(navPanel, "📊 Sales Dashboard", buttonY, ShowAdminDashboard);
            buttonY += buttonHeight + buttonSpacing;

            CreateNavButton(navPanel, "👥 Customer Management", buttonY, ShowCustomerManagement);
            buttonY += buttonHeight + buttonSpacing;

            CreateNavButton(navPanel, "📋 Inventory Manager", buttonY, ShowInventoryManager);
            buttonY += buttonHeight + buttonSpacing;

            CreateNavButton(navPanel, "🔒 Security Lab", buttonY, ShowSecurityLab);
            buttonY += buttonHeight + buttonSpacing;
        }

        private void CreateSectionLabel(Panel parent, string text, int y)
        {
            Label label = new Label
            {
                Text = text,
                Location = new Point(10, y),
                Size = new Size(250, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                TextAlign = ContentAlignment.MiddleLeft
            };
            parent.Controls.Add(label);
        }

        private void CreateNavButton(Panel parent, string text, int y, Action onClick)
        {
            Button button = new Button
            {
                Text = text,
                Location = new Point(10, y),
                Size = new Size(250, 35),
                BackColor = ThemeManager.ButtonColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = ThemeManager.AccentColor;
            button.Click += (s, e) => onClick();
            parent.Controls.Add(button);
        }

        private void CreateLoginButtons()
        {
            Panel loginPanel = new Panel
            {
                Height = 100,
                Dock = DockStyle.Bottom,
                BackColor = ThemeManager.SidebarColor,
                Padding = new Padding(10)
            };
            sidebarPanel.Controls.Add(loginPanel);

            Button userLoginBtn = new Button
            {
                Text = "👤 Sign In",
                Dock = DockStyle.Top,
                Height = 35,
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Margin = new Padding(0, 0, 0, 5)
            };
            userLoginBtn.FlatAppearance.BorderSize = 0;
            userLoginBtn.Click += UserLogin_Click;
            loginPanel.Controls.Add(userLoginBtn);

            Button adminLoginBtn = new Button
            {
                Text = "🔐 Admin Access",
                Dock = DockStyle.Top,
                Height = 35,
                BackColor = ThemeManager.DangerColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            adminLoginBtn.FlatAppearance.BorderSize = 0;
            adminLoginBtn.Click += AdminLogin_Click;
            loginPanel.Controls.Add(adminLoginBtn);
        }

        private void CreateContentArea(TableLayoutPanel mainLayout)
        {
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeManager.ContentBackgroundColor,
                Padding = new Padding(30),
                Margin = new Padding(0),
                AutoScroll = true
            };
            mainLayout.Controls.Add(contentPanel, 1, 0);
        }

        private void CreateSectionHeader(Panel parent, string text, int y)
        {
            Label header = new Label
            {
                Text = text,
                Location = new Point(10, y),
                Size = new Size(240, 20),
                Font = new Font("Arial", 9, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                TextAlign = ContentAlignment.MiddleLeft
            };
            parent.Controls.Add(header);
        }

        private void CreateNavigationButton(Panel parent, string text, int y, Action onClick)
        {
            Button button = new Button
            {
                Text = text,
                Location = new Point(10, y),
                Size = new Size(240, 35),
                BackColor = ThemeManager.ButtonColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 9, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0)
            };
            button.FlatAppearance.BorderSize = 0;
            button.MouseEnter += (s, e) => button.BackColor = ThemeManager.AccentColor;
            button.MouseLeave += (s, e) => button.BackColor = ThemeManager.ButtonColor;
            button.Click += (s, e) => onClick();
            parent.Controls.Add(button);
        }

        private void ShowHomePage()
        {
            contentPanel.Controls.Clear();

            // Welcome header
            Label welcomeLabel = new Label
            {
                Text = "🛒 Welcome to ShopVault - Your Premium Shopping Destination",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 10),
                Size = new Size(contentPanel.Width - 60, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            contentPanel.Controls.Add(welcomeLabel);

            // Search bar (SQL Injection vulnerability hidden here)
            CreateSearchBar(60);

            // Featured products section
            CreateFeaturedProducts(120);

            // Special offers
            CreateSpecialOffers(400);

            // Newsletter signup (more vulnerabilities)
            CreateNewsletterSignup(580);
        }

        private void CreateSearchBar(int y)
        {
            Label searchLabel = new Label
            {
                Text = "� Search Products:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, y),
                Size = new Size(150, 25)
            };
            contentPanel.Controls.Add(searchLabel);

            TextBox searchBox = new TextBox
            {
                Location = new Point(160, y),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 11)
            };
            // Add placeholder text via event
            searchBox.Text = "Search for electronics, clothing, books...";
            searchBox.ForeColor = Color.Gray;
            searchBox.Enter += (s, e) => {
                if (searchBox.Text == "Search for electronics, clothing, books...")
                {
                    searchBox.Text = "";
                    searchBox.ForeColor = ThemeManager.TextColor;
                }
            };
            searchBox.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(searchBox.Text))
                {
                    searchBox.Text = "Search for electronics, clothing, books...";
                    searchBox.ForeColor = Color.Gray;
                }
            };
            contentPanel.Controls.Add(searchBox);

            Button searchBtn = new Button
            {
                Text = "Search",
                Location = new Point(580, y - 2),
                Size = new Size(80, 29),
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            searchBtn.FlatAppearance.BorderSize = 0;
            searchBtn.Click += (s, e) => {
                // DA1 - SQL Injection vulnerability in search
                string searchResult = VulnerabilityManager.SearchProducts(searchBox.Text);
                ShowSearchResults(searchResult);
            };
            contentPanel.Controls.Add(searchBtn);
        }

        private void CreateFeaturedProducts(int y)
        {
            Label featuredLabel = new Label
            {
                Text = "⭐ Featured Products - Hot Deals!",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, y),
                Size = new Size(400, 30)
            };
            contentPanel.Controls.Add(featuredLabel);

            // Product grid
            int productX = 0;
            int productY = y + 40;
            string[] products = {
                "📱 iPhone 15 Pro - $999",
                "💻 MacBook Air - $1199", 
                "🎧 AirPods Pro - $249",
                "⌚ Apple Watch - $399"
            };

            for (int i = 0; i < products.Length; i++)
            {
                CreateProductCard(products[i], productX, productY, i);
                productX += 280;
                if (i == 1) { productX = 0; productY += 80; }
            }
        }

        private void CreateProductCard(string productName, int x, int y, int productId)
        {
            Panel productPanel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(270, 70),
                BackColor = Color.FromArgb(45, 45, 48),
                BorderStyle = BorderStyle.FixedSingle
            };
            contentPanel.Controls.Add(productPanel);

            Label productLabel = new Label
            {
                Text = productName,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 10),
                Size = new Size(180, 40)
            };
            productPanel.Controls.Add(productLabel);

            Button buyBtn = new Button
            {
                Text = "Add to Cart",
                Location = new Point(190, 25),
                Size = new Size(70, 25),
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8)
            };
            buyBtn.FlatAppearance.BorderSize = 0;
            buyBtn.Click += (s, e) => {
                // DA2 - Weak authentication, DA5 - Authorization bypass
                string result = VulnerabilityManager.AddToCart(productId, 1);
                MessageBox.Show(result, "Cart Update");
            };
            productPanel.Controls.Add(buyBtn);
        }

        private void CreateSpecialOffers(int y)
        {
            Label offersLabel = new Label
            {
                Text = "🔥 Limited Time Offers - Save Big Today!",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.Orange,
                Location = new Point(0, y),
                Size = new Size(400, 25)
            };
            contentPanel.Controls.Add(offersLabel);

            Label offerDetails = new Label
            {
                Text = "• 50% off on Gaming Laptops\n• Free shipping on orders over $100\n• Buy 2 Get 1 Free on Books\n• Exclusive member discounts available",
                Font = new Font("Segoe UI", 11),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(20, y + 30),
                Size = new Size(500, 80)
            };
            contentPanel.Controls.Add(offerDetails);

            Button membershipBtn = new Button
            {
                Text = "Join Premium Membership",
                Location = new Point(0, y + 120),
                Size = new Size(200, 35),
                BackColor = Color.Gold,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            membershipBtn.FlatAppearance.BorderSize = 0;
            membershipBtn.Click += (s, e) => {
                // DA4 - Weak cryptography in payment processing
                ShowPaymentMethods();
            };
            contentPanel.Controls.Add(membershipBtn);
        }

        private void CreateNewsletterSignup(int y)
        {
            Label newsletterLabel = new Label
            {
                Text = "📧 Stay Updated - Get exclusive deals in your inbox!",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, y),
                Size = new Size(450, 25)
            };
            contentPanel.Controls.Add(newsletterLabel);

            TextBox emailBox = new TextBox
            {
                Location = new Point(0, y + 35),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 11)
            };
            emailBox.Text = "Enter your email address";
            emailBox.ForeColor = Color.Gray;
            emailBox.Enter += (s, e) => {
                if (emailBox.Text == "Enter your email address")
                {
                    emailBox.Text = "";
                    emailBox.ForeColor = ThemeManager.TextColor;
                }
            };
            contentPanel.Controls.Add(emailBox);

            Button subscribeBtn = new Button
            {
                Text = "Subscribe",
                Location = new Point(320, y + 33),
                Size = new Size(100, 29),
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            subscribeBtn.FlatAppearance.BorderSize = 0;
            subscribeBtn.Click += (s, e) => {
                // DA3 - Sensitive data exposure, DA10 - Insufficient logging
                string result = VulnerabilityManager.SubscribeNewsletter(emailBox.Text);
                MessageBox.Show(result, "Newsletter Subscription");
            };
            contentPanel.Controls.Add(subscribeBtn);
        }

        private void ShowSearchResults(string searchResult)
        {
            MessageBox.Show(searchResult, "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowVulnerability(string vulnId)
        {
            contentPanel.Controls.Clear();

            var vulnInfo = GetDesktopVulnerabilityInfo(vulnId);

            // Title
            Label titleLabel = new Label
            {
                Text = vulnInfo.Name,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 0),
                Size = new Size(contentPanel.Width - 60, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            contentPanel.Controls.Add(titleLabel);

            // Description
            Label descLabel = new Label
            {
                Text = vulnInfo.Description,
                Font = new Font("Segoe UI", 12),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, 50),
                Size = new Size(contentPanel.Width - 60, 80),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            contentPanel.Controls.Add(descLabel);

            // Testing tools info
            Label toolsLabel = new Label
            {
                Text = $"🔧 Detectable with: {vulnInfo.PentestingTools}",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ThemeManager.WarningColor,
                Location = new Point(0, 140),
                Size = new Size(contentPanel.Width - 60, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            contentPanel.Controls.Add(toolsLabel);

            // Create test interface
            CreateVulnerabilityInterface(vulnId, 190);
        }

        private (string Name, string Description, string PentestingTools) GetDesktopVulnerabilityInfo(string vulnId)
        {
            switch (vulnId)
            {
                case "DA1":
                    return (
                        "DA1 - Injections",
                        "SQLi, LDAP, XML, OS Command, etc. - SQL injection in database queries, " +
                        "LDAP injection in authentication, XML injection in data processing, " +
                        "and OS command injection through system calls.",
                        "SQLMap, Process Monitor, Wireshark, Command Line Audit, API Monitor"
                    );
                case "DA2":
                    return (
                        "DA2 - Broken Authentication & Session Management", 
                        "OS/DesktopApp account Authentication & Session Management, Auth. for Import/Export " +
                        "with external Drive, Auth. for Network Shared Drives or other Peripheral devices. " +
                        "Weak authentication mechanisms and improper session handling.",
                        "Process Hacker, Registry editors, Memory analyzers, Network monitors"
                    );
                case "DA3":
                    return (
                        "DA3 - Sensitive Data Exposure",
                        "Data in Memory post App Logout, Logs with Sensitive Info., Hardcoded Secrets in files, etc. " +
                        "Sensitive information remaining accessible after logout or stored insecurely.",
                        "Process Hacker, Memory dumps, Strings utility, Log analyzers, Hex editors"
                    );
                case "DA4":
                    return (
                        "DA4 - Improper Cryptography Usage",
                        "Weak Keys or Usage of Outdated Cryptographic Algorithms, Inappropriate usage of " +
                        "Cryptographic Functions, reuse of Cryptographic Parameters across all Installations, " +
                        "Improper usage of Cryptography for Integrity check.",
                        "CrypTool, Entropy analysis, Key analysis tools, Cryptographic scanners"
                    );
                case "DA5":
                    return (
                        "DA5 - Improper Authorization",
                        "Weak File/Folder Permission per User Role, Missing Principle of Least Privilege approach, " +
                        "Improper User Roles. Insufficient access controls and privilege management.",
                        "AccessChk, Process Monitor, File permission analyzers, Privilege escalation tools"
                    );
                case "DA6":
                    return (
                        "DA6 - Security Misconfiguration",
                        "Weak OS Hardening, Misconfigured Group Policies/Registry/Firewall rules etc., " +
                        "Missing File Type check for File Processing Apps, Misconfigured Named-Pipes, " +
                        "misconfigured 3rd party services, etc.",
                        "Process Monitor, Registry scanners, Group Policy analyzers, Service configuration tools"
                    );
                case "DA7":
                    return (
                        "DA7 - Insecure Communication",
                        "Usage of weak TLS or DTLS Cipher-suites or Protocols, Unencrypted DB Queries in Transit, " +
                        "Absent Encrypted standard/custom protocol communication like HTTP, MQTT, COAP, etc.",
                        "Wireshark, SSL/TLS analyzers, Protocol analyzers, Network security scanners"
                    );
                case "DA8":
                    return (
                        "DA8 - Poor Code Quality",
                        "Missing Code-Signing and Verification for File Integrity, Missing Code Obfuscation, " +
                        "Dll-Preloading or Injection, Race Conditions, lack of binary protection " +
                        "(Overflows, Null pointers, memory corruption) etc.",
                        "CFF Explorer, Dependency Walker, Code signing tools, Static analysis tools, Debuggers"
                    );
                case "DA9":
                    return (
                        "DA9 - Using Components with Known Vulnerabilities",
                        "Usage of Outdated Softwares, or Usage of Obsolete Components/Services of " +
                        "Windows/3rd Party vendors. Components with known security vulnerabilities.",
                        "OWASP Dependency-Check, Vulnerability scanners, Component analyzers, CVE databases"
                    );
                case "DA10":
                    return (
                        "DA10 - Insufficient Logging & Monitoring",
                        "Missing or Improper Logging of Activities, Missing Regular Monitoring to Detect Abuse. " +
                        "Inadequate security event logging and monitoring capabilities.",
                        "Event Viewer, SIEM tools, Log analysis tools, Security monitoring platforms"
                    );
                default:
                    return ("Unknown Vulnerability", "No information available", "Standard pentesting tools");
            }
        }

        private void CreateVulnerabilityInterface(string vulnId, int startY)
        {
            switch (vulnId)
            {
                case "DA1": CreateInjectionInterface(startY); break;
                case "DA2": CreateAuthInterface(startY); break;
                case "DA3": CreateDataExposureInterface(startY); break;
                case "DA4": CreateCryptographyInterface(startY); break;
                case "DA5": CreateAuthorizationInterface(startY); break;
                case "DA6": CreateMisconfigurationInterface(startY); break;
                case "DA7": CreateCommunicationInterface(startY); break;
                case "DA8": CreateCodeQualityInterface(startY); break;
                case "DA9": CreateVulnerableComponentsInterface(startY); break;
                case "DA10": CreateLoggingInterface(startY); break;
            }
        }

        private void CreateInjectionInterface(int startY)
        {
            Label testLabel = new Label
            {
                Text = "🔴 SQL Injection Test (Monitor with Process Monitor):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.DangerColor,
                Location = new Point(0, startY),
                Size = new Size(500, 25)
            };
            contentPanel.Controls.Add(testLabel);

            TextBox sqlInput = new TextBox
            {
                Location = new Point(0, startY + 35),
                Size = new Size(400, 25),
                Font = new Font("Consolas", 10),
                Text = "1' OR '1'='1' --"
            };
            contentPanel.Controls.Add(sqlInput);

            Button testBtn = new Button
            {
                Text = "Execute SQL Query",
                Location = new Point(420, startY + 34),
                Size = new Size(120, 27),
                BackColor = ThemeManager.DangerColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            testBtn.FlatAppearance.BorderSize = 0;
            testBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestSQLInjection(sqlInput.Text);
                ShowTestResult(result, startY + 80);
            };
            contentPanel.Controls.Add(testBtn);
        }

        private void CreateAuthInterface(int startY)
        {
            Label testLabel = new Label
            {
                Text = "🔴 Authentication Test (Check with Memory Analysis):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.DangerColor,
                Location = new Point(0, startY),
                Size = new Size(500, 25)
            };
            contentPanel.Controls.Add(testLabel);

            Button testBtn = new Button
            {
                Text = "Expose Hardcoded Credentials",
                Location = new Point(0, startY + 35),
                Size = new Size(220, 30),
                BackColor = ThemeManager.DangerColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            testBtn.FlatAppearance.BorderSize = 0;
            testBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestWeakAuthentication();
                ShowTestResult(result, startY + 80);
            };
            contentPanel.Controls.Add(testBtn);
        }

        private void CreateDataExposureInterface(int startY)
        {
            Label testLabel = new Label
            {
                Text = "🟡 Data Exposure Test (Analyze with Process Hacker):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.WarningColor,
                Location = new Point(0, startY),
                Size = new Size(500, 25)
            };
            contentPanel.Controls.Add(testLabel);

            Button testBtn = new Button
            {
                Text = "Store Sensitive Data in Memory",
                Location = new Point(0, startY + 35),
                Size = new Size(220, 30),
                BackColor = ThemeManager.WarningColor,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            testBtn.FlatAppearance.BorderSize = 0;
            testBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestDataExposure();
                ShowTestResult(result, startY + 80);
            };
            contentPanel.Controls.Add(testBtn);
        }

        private void CreateCryptographyInterface(int startY)
        {
            Label testLabel = new Label
            {
                Text = "🔴 Weak Cryptography (Analyze with Hex Editor):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.DangerColor,
                Location = new Point(0, startY),
                Size = new Size(500, 25)
            };
            contentPanel.Controls.Add(testLabel);

            TextBox cryptoInput = new TextBox
            {
                Location = new Point(0, startY + 35),
                Size = new Size(300, 25),
                Text = "SecretPassword123"
            };
            contentPanel.Controls.Add(cryptoInput);

            Button testBtn = new Button
            {
                Text = "Weak Encrypt (DES)",
                Location = new Point(320, startY + 34),
                Size = new Size(120, 27),
                BackColor = ThemeManager.DangerColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            testBtn.FlatAppearance.BorderSize = 0;
            testBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestWeakCryptography();
                ShowTestResult(result, startY + 80);
            };
            contentPanel.Controls.Add(testBtn);
        }

        private void CreateAuthorizationInterface(int startY)
        {
            Label testLabel = new Label
            {
                Text = "🟠 Authorization Test (Check with AccessChk):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Orange,
                Location = new Point(0, startY),
                Size = new Size(500, 25)
            };
            contentPanel.Controls.Add(testLabel);

            TextBox userInput = new TextBox
            {
                Location = new Point(0, startY + 35),
                Size = new Size(200, 25),
                Text = "regular_user"
            };
            contentPanel.Controls.Add(userInput);

            Button testBtn = new Button
            {
                Text = "Access Admin Function",
                Location = new Point(220, startY + 34),
                Size = new Size(150, 27),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            testBtn.FlatAppearance.BorderSize = 0;
            testBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestAccessControl(userInput.Text);
                ShowTestResult(result, startY + 80);
            };
            contentPanel.Controls.Add(testBtn);
        }

        private void CreateMisconfigurationInterface(int startY)
        {
            Label testLabel = new Label
            {
                Text = "🔴 Misconfiguration Test (Monitor Registry with ProcMon):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.DangerColor,
                Location = new Point(0, startY),
                Size = new Size(500, 25)
            };
            contentPanel.Controls.Add(testLabel);

            Button testBtn = new Button
            {
                Text = "Access Registry Secrets",
                Location = new Point(0, startY + 35),
                Size = new Size(180, 30),
                BackColor = ThemeManager.DangerColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            testBtn.FlatAppearance.BorderSize = 0;
            testBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestSecurityMisconfiguration();
                ShowTestResult(result, startY + 80);
            };
            contentPanel.Controls.Add(testBtn);
        }

        private void CreateCommunicationInterface(int startY)
        {
            Label testLabel = new Label
            {
                Text = "🔴 Insecure Communication (Capture with Wireshark):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.DangerColor,
                Location = new Point(0, startY),
                Size = new Size(500, 25)
            };
            contentPanel.Controls.Add(testLabel);

            Button testBtn = new Button
            {
                Text = "Send Unencrypted Data",
                Location = new Point(0, startY + 35),
                Size = new Size(180, 30),
                BackColor = ThemeManager.DangerColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            testBtn.FlatAppearance.BorderSize = 0;
            testBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestInsecureCommunication();
                ShowTestResult(result, startY + 80);
            };
            contentPanel.Controls.Add(testBtn);
        }

        private void CreateCodeQualityInterface(int startY)
        {
            Label testLabel = new Label
            {
                Text = "🔴 Code Quality Test (Check with CFF Explorer):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.DangerColor,
                Location = new Point(0, startY),
                Size = new Size(500, 25)
            };
            contentPanel.Controls.Add(testLabel);

            Button testBtn = new Button
            {
                Text = "Check Code Signing",
                Location = new Point(0, startY + 35),
                Size = new Size(150, 30),
                BackColor = ThemeManager.DangerColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            testBtn.FlatAppearance.BorderSize = 0;
            testBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestCodeQuality();
                ShowTestResult(result, startY + 80);
            };
            contentPanel.Controls.Add(testBtn);
        }

        private void CreateVulnerableComponentsInterface(int startY)
        {
            Label testLabel = new Label
            {
                Text = "🟡 Vulnerable Components (Scan with OWASP Dependency-Check):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.WarningColor,
                Location = new Point(0, startY),
                Size = new Size(500, 25)
            };
            contentPanel.Controls.Add(testLabel);

            Button testBtn = new Button
            {
                Text = "Check Vulnerable Libraries",
                Location = new Point(0, startY + 35),
                Size = new Size(180, 30),
                BackColor = ThemeManager.WarningColor,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            testBtn.FlatAppearance.BorderSize = 0;
            testBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestVulnerableComponents();
                ShowTestResult(result, startY + 80);
            };
            contentPanel.Controls.Add(testBtn);
        }

        private void CreateLoggingInterface(int startY)
        {
            Label testLabel = new Label
            {
                Text = "🟡 Insufficient Logging (Check Event Viewer):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.WarningColor,
                Location = new Point(0, startY),
                Size = new Size(500, 25)
            };
            contentPanel.Controls.Add(testLabel);

            Button testBtn = new Button
            {
                Text = "Perform Unlogged Action",
                Location = new Point(0, startY + 35),
                Size = new Size(180, 30),
                BackColor = ThemeManager.WarningColor,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            testBtn.FlatAppearance.BorderSize = 0;
            testBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestInsufficientLogging();
                ShowTestResult(result, startY + 80);
            };
            contentPanel.Controls.Add(testBtn);
        }

        private void ShowTestResult(string result, int y)
        {
            // Remove existing result
            var existingResults = contentPanel.Controls.OfType<TextBox>().Where(c => c.Name == "testResult").ToList();
            foreach (var ctrl in existingResults)
                contentPanel.Controls.Remove(ctrl);

            Label resultHeader = new Label
            {
                Text = "📋 Test Results (Analyze with pentesting tools):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, y),
                Size = new Size(400, 25)
            };
            contentPanel.Controls.Add(resultHeader);

            TextBox resultBox = new TextBox
            {
                Name = "testResult",
                Location = new Point(0, y + 30),
                Size = new Size(Math.Min(800, contentPanel.Width - 60), 150),
                Font = new Font("Consolas", 9),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Text = result,
                BackColor = Color.Black,
                ForeColor = Color.Lime,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            contentPanel.Controls.Add(resultBox);
        }

        private Panel GetContentPanel()
        {
            return contentPanel;
        }

        private void ShowCategory(string category)
        {
            contentPanel.Controls.Clear();

            Label categoryLabel = new Label
            {
                Text = $"🏷️ {category} - Browse Our Collection",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 10),
                Size = new Size(600, 40)
            };
            contentPanel.Controls.Add(categoryLabel);

            // Search within category (vulnerable)
            Label searchLabel = new Label
            {
                Text = $"Search {category}:",
                Font = new Font("Segoe UI", 12),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, 70),
                Size = new Size(120, 25)
            };
            contentPanel.Controls.Add(searchLabel);

            TextBox categorySearchBox = new TextBox
            {
                Location = new Point(130, 70),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 11)
            };
            contentPanel.Controls.Add(categorySearchBox);

            Button searchBtn = new Button
            {
                Text = "Search",
                Location = new Point(450, 68),
                Size = new Size(80, 29),
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            searchBtn.FlatAppearance.BorderSize = 0;
            searchBtn.Click += (s, e) => {
                // DA1 - SQL Injection in category search
                string result = VulnerabilityManager.SearchInCategory(category, categorySearchBox.Text);
                ShowSearchResults(result);
            };
            contentPanel.Controls.Add(searchBtn);

            // Sample products for this category
            CreateCategoryProducts(category, 120);
        }

        private void CreateCategoryProducts(string category, int startY)
        {
            string[] products;
            switch (category)
            {
                case "Electronics":
                    products = new[] { "📱 Smartphone Pro", "💻 Gaming Laptop", "📺 4K Smart TV", "🎧 Wireless Headphones" };
                    break;
                case "Clothing":
                    products = new[] { "👔 Business Suit", "👕 Casual T-Shirt", "👖 Designer Jeans", "👟 Running Shoes" };
                    break;
                case "Books":
                    products = new[] { "📚 Programming Guide", "📖 Mystery Novel", "📝 Cookbook", "🎓 Educational Text" };
                    break;
                case "Home":
                    products = new[] { "🪑 Office Chair", "🛏️ Queen Bed", "🍽️ Dining Set", "🌱 Garden Tools" };
                    break;
                case "Gaming":
                    products = new[] { "🎮 Gaming Console", "🕹️ Wireless Controller", "💿 Latest Game", "🖥️ Gaming Monitor" };
                    break;
                default:
                    products = new[] { "🛍️ General Product 1", "🛍️ General Product 2", "🛍️ General Product 3", "🛍️ General Product 4" };
                    break;
            }

            for (int i = 0; i < products.Length; i++)
            {
                CreateProductCard(products[i], (i % 2) * 300, startY + (i / 2) * 90, i + 100);
            }
        }

        private void ShowShoppingCart()
        {
            contentPanel.Controls.Clear();

            Label cartLabel = new Label
            {
                Text = "🛒 Your Shopping Cart",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 10),
                Size = new Size(400, 40)
            };
            contentPanel.Controls.Add(cartLabel);

            // Cart items display
            Label itemsLabel = new Label
            {
                Text = "Cart Items:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, 70),
                Size = new Size(200, 25)
            };
            contentPanel.Controls.Add(itemsLabel);

            // Sample cart items
            string cartContents = VulnerabilityManager.GetCartContents();
            TextBox cartDisplay = new TextBox
            {
                Location = new Point(0, 100),
                Size = new Size(600, 200),
                Font = new Font("Consolas", 10),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Text = cartContents,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White
            };
            contentPanel.Controls.Add(cartDisplay);

            // Checkout button
            Button checkoutBtn = new Button
            {
                Text = "💳 Proceed to Checkout",
                Location = new Point(0, 320),
                Size = new Size(200, 40),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            checkoutBtn.FlatAppearance.BorderSize = 0;
            checkoutBtn.Click += (s, e) => {
                // DA2 - Authentication bypass, DA4 - Weak crypto in payment
                ShowCheckout();
            };
            contentPanel.Controls.Add(checkoutBtn);
        }

        private void ShowCheckout()
        {
            contentPanel.Controls.Clear();

            Label checkoutLabel = new Label
            {
                Text = "💳 Secure Checkout Process",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 10),
                Size = new Size(400, 30)
            };
            contentPanel.Controls.Add(checkoutLabel);

            // Payment form (vulnerable)
            CreatePaymentForm(60);
        }

        private void CreatePaymentForm(int startY)
        {
            Label paymentLabel = new Label
            {
                Text = "Payment Information:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, startY),
                Size = new Size(200, 25)
            };
            contentPanel.Controls.Add(paymentLabel);

            // Credit card number
            Label cardLabel = new Label
            {
                Text = "Card Number:",
                Location = new Point(0, startY + 40),
                Size = new Size(100, 20),
                ForeColor = ThemeManager.TextColor
            };
            contentPanel.Controls.Add(cardLabel);

            TextBox cardNumberBox = new TextBox
            {
                Location = new Point(110, startY + 38),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 11),
                Text = "1234 5678 9012 3456"
            };
            contentPanel.Controls.Add(cardNumberBox);

            // CVV
            Label cvvLabel = new Label
            {
                Text = "CVV:",
                Location = new Point(0, startY + 80),
                Size = new Size(100, 20),
                ForeColor = ThemeManager.TextColor
            };
            contentPanel.Controls.Add(cvvLabel);

            TextBox cvvBox = new TextBox
            {
                Location = new Point(110, startY + 78),
                Size = new Size(60, 25),
                Font = new Font("Segoe UI", 11),
                PasswordChar = '*'
            };
            contentPanel.Controls.Add(cvvBox);

            // Process payment button
            Button payBtn = new Button
            {
                Text = "💰 Process Payment",
                Location = new Point(0, startY + 120),
                Size = new Size(160, 35),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            payBtn.FlatAppearance.BorderSize = 0;
            payBtn.Click += (s, e) => {
                // DA4 - Weak cryptography, DA7 - Insecure communication, DA3 - Data exposure
                string result = VulnerabilityManager.ProcessPayment(cardNumberBox.Text, cvvBox.Text);
                MessageBox.Show(result, "Payment Processing");
            };
            contentPanel.Controls.Add(payBtn);
        }

        private void ShowOrderHistory()
        {
            contentPanel.Controls.Clear();

            Label historyLabel = new Label
            {
                Text = "📦 Your Order History",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 10),
                Size = new Size(400, 30)
            };
            contentPanel.Controls.Add(historyLabel);

            // Order search (vulnerable to injection)
            Label searchLabel = new Label
            {
                Text = "Search Orders:",
                Font = new Font("Segoe UI", 12),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, 60),
                Size = new Size(120, 25)
            };
            contentPanel.Controls.Add(searchLabel);

            TextBox orderSearchBox = new TextBox
            {
                Location = new Point(130, 58),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 11),
                Text = "Order ID or date"
            };
            contentPanel.Controls.Add(orderSearchBox);

            Button searchOrderBtn = new Button
            {
                Text = "Search",
                Location = new Point(350, 56),
                Size = new Size(80, 29),
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            searchOrderBtn.FlatAppearance.BorderSize = 0;
            searchOrderBtn.Click += (s, e) => {
                // DA1 - SQL injection in order search
                string result = VulnerabilityManager.SearchOrders(orderSearchBox.Text);
                ShowOrderResults(result);
            };
            contentPanel.Controls.Add(searchOrderBtn);

            // Display sample orders
            ShowOrderResults("Loading recent orders...");
        }

        private void ShowOrderResults(string results)
        {
            // Remove existing results
            var existingResults = contentPanel.Controls.OfType<TextBox>().Where(c => c.Name == "orderResults").ToList();
            foreach (var ctrl in existingResults)
                contentPanel.Controls.Remove(ctrl);

            TextBox orderResults = new TextBox
            {
                Name = "orderResults",
                Location = new Point(0, 100),
                Size = new Size(700, 300),
                Font = new Font("Consolas", 10),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Text = results,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White
            };
            contentPanel.Controls.Add(orderResults);
        }

        private void ShowPaymentMethods()
        {
            contentPanel.Controls.Clear();

            Label paymentLabel = new Label
            {
                Text = "💳 Manage Payment Methods",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 10),
                Size = new Size(400, 30)
            };
            contentPanel.Controls.Add(paymentLabel);

            // Stored payment methods (vulnerable storage)
            Label storedLabel = new Label
            {
                Text = "Saved Payment Methods:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, 60),
                Size = new Size(250, 25)
            };
            contentPanel.Controls.Add(storedLabel);

            Button viewStoredBtn = new Button
            {
                Text = "View Stored Cards",
                Location = new Point(0, 90),
                Size = new Size(150, 30),
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            viewStoredBtn.FlatAppearance.BorderSize = 0;
            viewStoredBtn.Click += (s, e) => {
                // DA3 - Sensitive data exposure, DA4 - Weak crypto
                string result = VulnerabilityManager.GetStoredPaymentMethods();
                MessageBox.Show(result, "Stored Payment Methods");
            };
            contentPanel.Controls.Add(viewStoredBtn);

            // Add new payment method
            CreateAddPaymentMethodForm(140);
        }

        private void CreateAddPaymentMethodForm(int startY)
        {
            Label addLabel = new Label
            {
                Text = "Add New Payment Method:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, startY),
                Size = new Size(250, 25)
            };
            contentPanel.Controls.Add(addLabel);

            // Card details form
            string[] labels = { "Card Number:", "Expiry Date:", "CVV:", "Cardholder Name:" };
            TextBox[] textBoxes = new TextBox[4];

            for (int i = 0; i < labels.Length; i++)
            {
                Label fieldLabel = new Label
                {
                    Text = labels[i],
                    Location = new Point(0, startY + 40 + (i * 40)),
                    Size = new Size(120, 20),
                    ForeColor = ThemeManager.TextColor
                };
                contentPanel.Controls.Add(fieldLabel);

                textBoxes[i] = new TextBox
                {
                    Location = new Point(130, startY + 38 + (i * 40)),
                    Size = new Size(200, 25),
                    Font = new Font("Segoe UI", 11)
                };
                if (i == 2) textBoxes[i].PasswordChar = '*'; // CVV
                contentPanel.Controls.Add(textBoxes[i]);
            }

            Button saveCardBtn = new Button
            {
                Text = "💾 Save Payment Method",
                Location = new Point(0, startY + 200),
                Size = new Size(180, 35),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            saveCardBtn.FlatAppearance.BorderSize = 0;
            saveCardBtn.Click += (s, e) => {
                // DA3 - Data exposure, DA4 - Weak crypto storage
                string result = VulnerabilityManager.SavePaymentMethod(
                    textBoxes[0].Text, textBoxes[1].Text, textBoxes[2].Text, textBoxes[3].Text);
                MessageBox.Show(result, "Payment Method Saved");
            };
            contentPanel.Controls.Add(saveCardBtn);
        }

        private void ShowUserSettings()
        {
            contentPanel.Controls.Clear();

            Label settingsLabel = new Label
            {
                Text = "⚙️ Account Settings",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 10),
                Size = new Size(300, 30)
            };
            contentPanel.Controls.Add(settingsLabel);

            // Password change (vulnerable)
            CreatePasswordChangeForm(60);

            // Export data (vulnerable)
            CreateDataExportForm(220);

            // Import preferences (vulnerable)
            CreateImportForm(320);
        }

        private void CreatePasswordChangeForm(int startY)
        {
            Label pwdLabel = new Label
            {
                Text = "Change Password:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, startY),
                Size = new Size(200, 25)
            };
            contentPanel.Controls.Add(pwdLabel);

            // Current password
            Label currentLabel = new Label
            {
                Text = "Current Password:",
                Location = new Point(0, startY + 40),
                Size = new Size(120, 20),
                ForeColor = ThemeManager.TextColor
            };
            contentPanel.Controls.Add(currentLabel);

            TextBox currentPwdBox = new TextBox
            {
                Location = new Point(130, startY + 38),
                Size = new Size(200, 25),
                PasswordChar = '*'
            };
            contentPanel.Controls.Add(currentPwdBox);

            // New password
            Label newLabel = new Label
            {
                Text = "New Password:",
                Location = new Point(0, startY + 80),
                Size = new Size(120, 20),
                ForeColor = ThemeManager.TextColor
            };
            contentPanel.Controls.Add(newLabel);

            TextBox newPwdBox = new TextBox
            {
                Location = new Point(130, startY + 78),
                Size = new Size(200, 25),
                PasswordChar = '*'
            };
            contentPanel.Controls.Add(newPwdBox);

            Button changePwdBtn = new Button
            {
                Text = "🔐 Change Password",
                Location = new Point(0, startY + 120),
                Size = new Size(150, 30),
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            changePwdBtn.FlatAppearance.BorderSize = 0;
            changePwdBtn.Click += (s, e) => {
                // DA2 - Weak authentication, DA4 - Weak crypto
                string result = VulnerabilityManager.ChangePassword(currentPwdBox.Text, newPwdBox.Text);
                MessageBox.Show(result, "Password Change");
            };
            contentPanel.Controls.Add(changePwdBtn);
        }

        private void CreateDataExportForm(int startY)
        {
            Label exportLabel = new Label
            {
                Text = "Export Account Data:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, startY),
                Size = new Size(200, 25)
            };
            contentPanel.Controls.Add(exportLabel);

            TextBox exportPathBox = new TextBox
            {
                Location = new Point(0, startY + 35),
                Size = new Size(300, 25),
                Text = "C:\\temp\\export.csv",
                Font = new Font("Segoe UI", 11)
            };
            contentPanel.Controls.Add(exportPathBox);

            Button exportBtn = new Button
            {
                Text = "📊 Export Data",
                Location = new Point(320, startY + 33),
                Size = new Size(120, 29),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            exportBtn.FlatAppearance.BorderSize = 0;
            exportBtn.Click += (s, e) => {
                // DA1 - Command injection, DA8 - Path traversal
                string result = VulnerabilityManager.ExportUserData(exportPathBox.Text);
                MessageBox.Show(result, "Data Export");
            };
            contentPanel.Controls.Add(exportBtn);
        }

        private void CreateImportForm(int startY)
        {
            Label importLabel = new Label
            {
                Text = "Import User Preferences:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, startY),
                Size = new Size(250, 25)
            };
            contentPanel.Controls.Add(importLabel);

            TextBox importPathBox = new TextBox
            {
                Location = new Point(0, startY + 35),
                Size = new Size(300, 25),
                Text = "preferences.xml",
                Font = new Font("Segoe UI", 11)
            };
            contentPanel.Controls.Add(importPathBox);

            Button importBtn = new Button
            {
                Text = "📥 Import Settings",
                Location = new Point(320, startY + 33),
                Size = new Size(120, 29),
                BackColor = Color.Blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            importBtn.FlatAppearance.BorderSize = 0;
            importBtn.Click += (s, e) => {
                // DA1 - XXE injection, DA8 - File upload vulnerabilities
                string result = VulnerabilityManager.ImportUserPreferences(importPathBox.Text);
                MessageBox.Show(result, "Settings Import");
            };
            contentPanel.Controls.Add(importBtn);
        }

        private void ShowAdminDashboard()
        {
            contentPanel.Controls.Clear();

            Label adminLabel = new Label
            {
                Text = "📊 Sales Dashboard - Admin Only",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 10),
                Size = new Size(400, 30)
            };
            contentPanel.Controls.Add(adminLabel);

            // Admin login check (vulnerable)
            Label loginLabel = new Label
            {
                Text = "Admin Authentication Required:",
                Font = new Font("Segoe UI", 12),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, 60),
                Size = new Size(250, 25)
            };
            contentPanel.Controls.Add(loginLabel);

            TextBox adminUserBox = new TextBox
            {
                Location = new Point(0, 90),
                Size = new Size(150, 25),
                Text = "Admin Username"
            };
            contentPanel.Controls.Add(adminUserBox);

            TextBox adminPassBox = new TextBox
            {
                Location = new Point(160, 90),
                Size = new Size(150, 25),
                PasswordChar = '*',
                Text = "Admin Password"
            };
            contentPanel.Controls.Add(adminPassBox);

            Button adminLoginBtn = new Button
            {
                Text = "🔐 Admin Login",
                Location = new Point(320, 88),
                Size = new Size(100, 29),
                BackColor = ThemeManager.DangerColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            adminLoginBtn.FlatAppearance.BorderSize = 0;
            adminLoginBtn.Click += (s, e) => {
                // DA2 - Hardcoded credentials, DA5 - Authorization bypass
                bool isAuthenticated = VulnerabilityManager.AuthenticateAdmin(adminUserBox.Text, adminPassBox.Text);
                if (isAuthenticated)
                {
                    ShowAdminPanelContent();
                }
                else
                {
                    MessageBox.Show("❌ Invalid admin credentials!", "Admin Login");
                }
            };
            contentPanel.Controls.Add(adminLoginBtn);
        }

        private void ShowAdminPanelContent()
        {
            // Clear login form and show admin content
            contentPanel.Controls.Clear();

            Label adminLabel = new Label
            {
                Text = "📊 Admin Dashboard - Sales Analytics",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 10),
                Size = new Size(500, 30)
            };
            contentPanel.Controls.Add(adminLabel);

            // Sales report generation (vulnerable)
            CreateSalesReportForm(60);

            // Database query interface (very vulnerable)
            CreateDatabaseQueryForm(180);
        }

        private void CreateSalesReportForm(int startY)
        {
            Label reportLabel = new Label
            {
                Text = "Generate Sales Report:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, startY),
                Size = new Size(200, 25)
            };
            contentPanel.Controls.Add(reportLabel);

            TextBox dateRangeBox = new TextBox
            {
                Location = new Point(0, startY + 35),
                Size = new Size(200, 25),
                Text = "Date range (e.g., 2024-01-01)"
            };
            contentPanel.Controls.Add(dateRangeBox);

            Button generateBtn = new Button
            {
                Text = "📈 Generate Report",
                Location = new Point(220, startY + 33),
                Size = new Size(130, 29),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            generateBtn.FlatAppearance.BorderSize = 0;
            generateBtn.Click += (s, e) => {
                // DA1 - SQL injection in report generation
                string result = VulnerabilityManager.GenerateSalesReport(dateRangeBox.Text);
                MessageBox.Show(result, "Sales Report");
            };
            contentPanel.Controls.Add(generateBtn);
        }

        private void CreateDatabaseQueryForm(int startY)
        {
            Label queryLabel = new Label
            {
                Text = "Database Query Interface (Advanced):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Red,
                Location = new Point(0, startY),
                Size = new Size(350, 25)
            };
            contentPanel.Controls.Add(queryLabel);

            TextBox queryBox = new TextBox
            {
                Location = new Point(0, startY + 35),
                Size = new Size(500, 60),
                Multiline = true,
                Font = new Font("Consolas", 10),
                Text = "SELECT * FROM users WHERE role = 'customer'"
            };
            contentPanel.Controls.Add(queryBox);

            Button executeBtn = new Button
            {
                Text = "⚡ Execute Query",
                Location = new Point(0, startY + 105),
                Size = new Size(130, 30),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            executeBtn.FlatAppearance.BorderSize = 0;
            executeBtn.Click += (s, e) => {
                // DA1 - Direct SQL injection vulnerability
                string result = VulnerabilityManager.ExecuteRawQuery(queryBox.Text);
                ShowQueryResults(result, startY + 145);
            };
            contentPanel.Controls.Add(executeBtn);
        }

        private void ShowQueryResults(string results, int startY)
        {
            // Remove existing results
            var existingResults = contentPanel.Controls.OfType<TextBox>().Where(c => c.Name == "queryResults").ToList();
            foreach (var ctrl in existingResults)
                contentPanel.Controls.Remove(ctrl);

            TextBox queryResults = new TextBox
            {
                Name = "queryResults",
                Location = new Point(0, startY),
                Size = new Size(700, 200),
                Font = new Font("Consolas", 9),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Text = results,
                BackColor = Color.Black,
                ForeColor = Color.Lime
            };
            contentPanel.Controls.Add(queryResults);
        }

        private void ShowCustomerManagement()
        {
            contentPanel.Controls.Clear();

            Label customerLabel = new Label
            {
                Text = "👥 Customer Management Portal",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 10),
                Size = new Size(400, 30)
            };
            contentPanel.Controls.Add(customerLabel);

            // Customer search (vulnerable)
            Label searchLabel = new Label
            {
                Text = "Search Customers:",
                Font = new Font("Segoe UI", 12),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, 60),
                Size = new Size(150, 25)
            };
            contentPanel.Controls.Add(searchLabel);

            TextBox customerSearchBox = new TextBox
            {
                Location = new Point(160, 58),
                Size = new Size(250, 25),
                Text = "Customer name, email, or ID"
            };
            contentPanel.Controls.Add(customerSearchBox);

            Button searchCustomerBtn = new Button
            {
                Text = "🔍 Search",
                Location = new Point(420, 56),
                Size = new Size(80, 29),
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            searchCustomerBtn.FlatAppearance.BorderSize = 0;
            searchCustomerBtn.Click += (s, e) => {
                // DA1 - SQL injection in customer search
                // DA5 - Authorization bypass (no admin check)
                string result = VulnerabilityManager.SearchCustomers(customerSearchBox.Text);
                ShowCustomerResults(result);
            };
            contentPanel.Controls.Add(searchCustomerBtn);

            // Customer modification tools
            CreateCustomerModificationTools(100);
        }

        private void CreateCustomerModificationTools(int startY)
        {
            Label modifyLabel = new Label
            {
                Text = "Customer Account Modification:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, startY),
                Size = new Size(300, 25)
            };
            contentPanel.Controls.Add(modifyLabel);

            TextBox customerIdBox = new TextBox
            {
                Location = new Point(0, startY + 35),
                Size = new Size(100, 25),
                Text = "Customer ID"
            };
            contentPanel.Controls.Add(customerIdBox);

            TextBox newEmailBox = new TextBox
            {
                Location = new Point(110, startY + 35),
                Size = new Size(200, 25),
                Text = "New email address"
            };
            contentPanel.Controls.Add(newEmailBox);

            Button updateBtn = new Button
            {
                Text = "✏️ Update Customer",
                Location = new Point(320, startY + 33),
                Size = new Size(130, 29),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            updateBtn.FlatAppearance.BorderSize = 0;
            updateBtn.Click += (s, e) => {
                // DA1 - SQL injection, DA5 - Missing authorization
                string result = VulnerabilityManager.UpdateCustomer(customerIdBox.Text, newEmailBox.Text);
                MessageBox.Show(result, "Customer Update");
            };
            contentPanel.Controls.Add(updateBtn);

            // Customer deletion (very dangerous)
            Button deleteBtn = new Button
            {
                Text = "🗑️ Delete Customer",
                Location = new Point(460, startY + 33),
                Size = new Size(130, 29),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            deleteBtn.FlatAppearance.BorderSize = 0;
            deleteBtn.Click += (s, e) => {
                // DA1 - SQL injection, DA5 - No authorization check
                string result = VulnerabilityManager.DeleteCustomer(customerIdBox.Text);
                MessageBox.Show(result, "Customer Deletion");
            };
            contentPanel.Controls.Add(deleteBtn);
        }

        private void ShowCustomerResults(string results)
        {
            // Remove existing results
            var existingResults = contentPanel.Controls.OfType<TextBox>().Where(c => c.Name == "customerResults").ToList();
            foreach (var ctrl in existingResults)
                contentPanel.Controls.Remove(ctrl);

            TextBox customerResults = new TextBox
            {
                Name = "customerResults",
                Location = new Point(0, 200),
                Size = new Size(700, 250),
                Font = new Font("Consolas", 9),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Text = results,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White
            };
            contentPanel.Controls.Add(customerResults);
        }

        private void ShowInventoryManager()
        {
            contentPanel.Controls.Clear();

            Label inventoryLabel = new Label
            {
                Text = "📋 Inventory Management System",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 10),
                Size = new Size(400, 30)
            };
            contentPanel.Controls.Add(inventoryLabel);

            // File upload for inventory (vulnerable)
            CreateInventoryUploadForm(60);

            // Inventory query interface
            CreateInventoryQueryForm(180);
        }

        private void CreateInventoryUploadForm(int startY)
        {
            Label uploadLabel = new Label
            {
                Text = "Upload Inventory File:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, startY),
                Size = new Size(200, 25)
            };
            contentPanel.Controls.Add(uploadLabel);

            TextBox filePathBox = new TextBox
            {
                Location = new Point(0, startY + 35),
                Size = new Size(300, 25),
                Text = "Path to inventory file (CSV, XML)"
            };
            contentPanel.Controls.Add(filePathBox);

            Button uploadBtn = new Button
            {
                Text = "📤 Upload & Process",
                Location = new Point(320, startY + 33),
                Size = new Size(140, 29),
                BackColor = Color.Blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            uploadBtn.FlatAppearance.BorderSize = 0;
            uploadBtn.Click += (s, e) => {
                // DA1 - XXE, CSV injection, DA8 - File upload vulnerabilities
                string result = VulnerabilityManager.ProcessInventoryFile(filePathBox.Text);
                MessageBox.Show(result, "File Processing");
            };
            contentPanel.Controls.Add(uploadBtn);
        }

        private void CreateInventoryQueryForm(int startY)
        {
            Label queryLabel = new Label
            {
                Text = "Inventory Search & Management:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, startY),
                Size = new Size(300, 25)
            };
            contentPanel.Controls.Add(queryLabel);

            TextBox productSearchBox = new TextBox
            {
                Location = new Point(0, startY + 35),
                Size = new Size(250, 25),
                Text = "Product name or category"
            };
            contentPanel.Controls.Add(productSearchBox);

            Button searchProductBtn = new Button
            {
                Text = "🔍 Search Products",
                Location = new Point(260, startY + 33),
                Size = new Size(120, 29),
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            searchProductBtn.FlatAppearance.BorderSize = 0;
            searchProductBtn.Click += (s, e) => {
                // DA1 - SQL injection in product search
                string result = VulnerabilityManager.SearchInventory(productSearchBox.Text);
                ShowInventoryResults(result);
            };
            contentPanel.Controls.Add(searchProductBtn);

            // Inventory modification
            Label modifyLabel = new Label
            {
                Text = "Quick Inventory Update:",
                Font = new Font("Segoe UI", 11),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, startY + 80),
                Size = new Size(200, 20)
            };
            contentPanel.Controls.Add(modifyLabel);

            TextBox productIdBox = new TextBox
            {
                Location = new Point(0, startY + 105),
                Size = new Size(80, 25),
                Text = "Product ID"
            };
            contentPanel.Controls.Add(productIdBox);

            TextBox quantityBox = new TextBox
            {
                Location = new Point(90, startY + 105),
                Size = new Size(80, 25),
                Text = "New Qty"
            };
            contentPanel.Controls.Add(quantityBox);

            Button updateInventoryBtn = new Button
            {
                Text = "📝 Update Stock",
                Location = new Point(180, startY + 103),
                Size = new Size(100, 29),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            updateInventoryBtn.FlatAppearance.BorderSize = 0;
            updateInventoryBtn.Click += (s, e) => {
                // DA1 - SQL injection, DA5 - Authorization bypass
                string result = VulnerabilityManager.UpdateInventory(productIdBox.Text, quantityBox.Text);
                MessageBox.Show(result, "Inventory Update");
            };
            contentPanel.Controls.Add(updateInventoryBtn);
        }

        private void ShowInventoryResults(string results)
        {
            // Remove existing results
            var existingResults = contentPanel.Controls.OfType<TextBox>().Where(c => c.Name == "inventoryResults").ToList();
            foreach (var ctrl in existingResults)
                contentPanel.Controls.Remove(ctrl);

            TextBox inventoryResults = new TextBox
            {
                Name = "inventoryResults",
                Location = new Point(0, 350),
                Size = new Size(700, 200),
                Font = new Font("Consolas", 9),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Text = results,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White
            };
            contentPanel.Controls.Add(inventoryResults);
        }

        private void ShowSecurityLab()
        {
            contentPanel.Controls.Clear();

            Label securityLabel = new Label
            {
                Text = "🔒 Security Testing Lab",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                Location = new Point(0, 10),
                Size = new Size(400, 30)
            };
            contentPanel.Controls.Add(securityLabel);

            Label infoLabel = new Label
            {
                Text = "Test these vulnerabilities in a safe environment:",
                Font = new Font("Segoe UI", 12),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(0, 50),
                Size = new Size(450, 25)
            };
            contentPanel.Controls.Add(infoLabel);

            int buttonY = 90;
            int buttonHeight = 40;
            int buttonSpacing = 10;

            // DA1 - SQL Injection Test
            CreateVulnTestButton("💉 DA1: SQL Injection", buttonY, Color.Red, () => {
                TestSQLInjectionDemo();
            });
            buttonY += buttonHeight + buttonSpacing;

            // DA2 - Registry Session Storage Test
            CreateVulnTestButton("🗝️ DA2: Insecure Session Storage", buttonY, Color.Orange, () => {
                TestRegistryStorageDemo();
            });
            buttonY += buttonHeight + buttonSpacing;

            // DA4 - Command Injection Test
            CreateVulnTestButton("⚡ DA4: Command Injection", buttonY, Color.Purple, () => {
                TestCommandInjectionDemo();
            });
            buttonY += buttonHeight + buttonSpacing;

            // Show Registry Contents Button
            CreateVulnTestButton("🔍 View Registry Session Data", buttonY, Color.Blue, () => {
                ShowRegistryContents();
            });
            buttonY += buttonHeight + buttonSpacing;
        }

        private void CreateVulnTestButton(string text, int y, Color color, Action onClick)
        {
            Button button = new Button
            {
                Text = text,
                Location = new Point(0, y),
                Size = new Size(400, 35),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(color.R + 20, color.G + 20, color.B + 20);
            button.Click += (s, e) => onClick();
            contentPanel.Controls.Add(button);
        }

        private void TestSQLInjectionDemo()
        {
            string testInput = "' OR '1'='1' --";
            string result = VulnerabilityManager.TestSQLInjection(testInput);
            MessageBox.Show($"SQL Injection Test:\n\nInput: {testInput}\n\nResult: {result}", 
                          "DA1 - SQL Injection Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TestRegistryStorageDemo()
        {
            // Demonstrate registry storage vulnerability
            VulnerabilityManager.DA2_BrokenAuthentication.StoreSessionInRegistry("testuser", "plaintext_password123", false);
            string sessionInfo = VulnerabilityManager.DA2_BrokenAuthentication.GetSessionInfo();
            MessageBox.Show($"Registry Storage Vulnerability:\n\n{sessionInfo}\n\n⚠️ Passwords stored in plain text!", 
                          "DA2 - Insecure Session Storage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void TestCommandInjectionDemo()
        {
            string testCommand = "test & calc";
            string result = VulnerabilityManager.DA1_Injections.ExecuteSystemCommand(testCommand);
            MessageBox.Show($"Command Injection Test:\n\nInput: {testCommand}\n\nResult: {result}\n\n⚠️ Calculator should open!", 
                          "DA4 - Command Injection Demo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void ShowRegistryContents()
        {
            try
            {
                string sessionInfo = VulnerabilityManager.DA2_BrokenAuthentication.GetSessionInfo();
                MessageBox.Show($"Current Registry Session Data:\n\n{sessionInfo}", 
                              "Registry Contents", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading registry: {ex.Message}", 
                              "Registry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserLogin_Click(object sender, EventArgs e)
        {
            UserLoginForm userLoginForm = new UserLoginForm();
            userLoginForm.ShowDialog();
        }

        private void AdminLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = Interaction.InputBox("Enter admin username:", "Admin Login", "admin");
                if (string.IsNullOrEmpty(username)) return;
                
                string password = Interaction.InputBox("Enter admin password:", "Admin Login", "");
                if (string.IsNullOrEmpty(password)) return;
                
                if (VulnerabilityManager.AuthenticateAdmin(username, password))
                {
                    MessageBox.Show("✅ Admin access granted! Opening admin dashboard...", "Success", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    AdminDashboardForm adminForm = new AdminDashboardForm();
                    adminForm.WindowState = FormWindowState.Normal;
                    adminForm.StartPosition = FormStartPosition.CenterScreen;
                    adminForm.Show();
                    adminForm.BringToFront();
                }
                else
                {
                    MessageBox.Show("❌ Invalid credentials!", "Access Denied", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in admin login: {ex.Message}", "Login Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handlers for compatibility
        private void btnLogin_Click(object sender, EventArgs e)
        {
            AdminLogin_Click(sender, e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ApplyProfessionalTheme();
        }

        // Modern e-commerce UI methods
        private void CreateModernContent(TableLayoutPanel mainLayout)
        {
            // Content container with sidebar and main content
            TableLayoutPanel contentLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.FromArgb(248, 248, 248),
                Margin = new Padding(0)
            };
            
            // Sidebar (250px) and main content (expand)
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            mainLayout.Controls.Add(contentLayout, 0, 1);

            // Create modern sidebar
            CreateModernSidebar(contentLayout);
            
            // Create main content area
            CreateMainContentArea(contentLayout);
        }

        private void CreateModernSidebar(TableLayoutPanel contentLayout)
        {
            sidebarPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 1, 0),
                AutoScroll = true
            };
            contentLayout.Controls.Add(sidebarPanel, 0, 0);

            // Add border
            sidebarPanel.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, sidebarPanel.ClientRectangle,
                    Color.FromArgb(230, 230, 230), ButtonBorderStyle.Solid);
            };

            CreateModernSidebarContent();
        }

        private void CreateModernSidebarContent()
        {
            int yPos = 20;

            // Categories section
            CreateSidebarSection("Shop by Category", yPos);
            yPos += 40;

            string[] categories = { "Electronics", "Clothing & Fashion", "Books & Media", "Home & Kitchen", 
                                  "Sports & Outdoors", "Beauty & Health", "Toys & Games", "Automotive" };

            foreach (string category in categories)
            {
                CreateModernSidebarButton(category, yPos, () => ShowCategory(category));
                yPos += 35;
            }

            yPos += 20;

            // Brands section
            CreateSidebarSection("Top Brands", yPos);
            yPos += 40;

            string[] brands = { "Apple", "Samsung", "Nike", "Adidas", "Amazon Basics", "Sony" };

            foreach (string brand in brands)
            {
                CreateModernSidebarButton(brand, yPos, () => ShowBrandProducts(brand));
                yPos += 35;
            }

            yPos += 20;

            // Offers section
            CreateSidebarSection("Special Offers", yPos);
            yPos += 40;

            CreateModernSidebarButton("🔥 Today's Deals", yPos, () => ShowTodaysDeals());
            yPos += 35;
            CreateModernSidebarButton("💰 Flash Sale", yPos, () => ShowFlashSale());
            yPos += 35;
            CreateModernSidebarButton("🎁 Gift Cards", yPos, () => ShowGiftCards());
            yPos += 50;

            // Security Testing section (for demonstration purposes)
            CreateSidebarSection("🔐 Security Testing", yPos);
            yPos += 40;

            CreateModernSidebarButton("🚨 UAC Test", yPos, () => ShowUACTest());
            yPos += 35;
            CreateModernSidebarButton("📁 File Access", yPos, () => ShowFileAccessTest());
            yPos += 35;
            CreateModernSidebarButton("🔑 Privilege Check", yPos, () => ShowPrivilegeTest());
        }

        private void CreateSidebarSection(string title, int y)
        {
            Label sectionLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(15, y),
                Size = new Size(200, 25)
            };
            sidebarPanel.Controls.Add(sectionLabel);
        }

        private void CreateModernSidebarButton(string text, int y, Action onClick)
        {
            Button button = new Button
            {
                Text = text,
                Location = new Point(15, y),
                Size = new Size(220, 30),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(70, 70, 70),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
            button.Click += (s, e) => onClick();
            sidebarPanel.Controls.Add(button);
        }

        private void CreateMainContentArea(TableLayoutPanel contentLayout)
        {
            contentMainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(0),
                AutoScroll = true,
                Padding = new Padding(20)
            };
            contentLayout.Controls.Add(contentMainPanel, 1, 0);
        }

        private void CreateModernFooter(TableLayoutPanel mainLayout)
        {
            Panel footerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(35, 47, 62),
                Margin = new Padding(0)
            };
            mainLayout.Controls.Add(footerPanel, 0, 2);

            Label footerLabel = new Label
            {
                Text = "© 2024 ShopVault - Your Premium Shopping Destination | Privacy Policy | Terms of Service",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            footerPanel.Controls.Add(footerLabel);
        }

        // Modern homepage
        private void ShowModernHomePage()
        {
            contentMainPanel.Controls.Clear();

            // Hero banner
            CreateHeroBanner();

            // Product categories
            CreateCategoriesGrid(200);

            // Featured products
            CreateFeaturedProductsGrid(400);

            // Special offers banner
            CreateOffersSection(700);
        }

        private void CreateHeroBanner()
        {
            Panel heroBanner = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(contentMainPanel.Width - 40, 180),
                BackColor = Color.FromArgb(255, 159, 28),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            contentMainPanel.Controls.Add(heroBanner);

            Label heroTitle = new Label
            {
                Text = "🎉 Mega Sale Festival",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(40, 30),
                Size = new Size(500, 50)
            };
            heroBanner.Controls.Add(heroTitle);

            Label heroSubtitle = new Label
            {
                Text = "Up to 70% OFF on Electronics, Fashion & More!",
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.White,
                Location = new Point(40, 85),
                Size = new Size(500, 30)
            };
            heroBanner.Controls.Add(heroSubtitle);

            Button shopNowBtn = new Button
            {
                Text = "Shop Now",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 159, 28),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 45),
                Location = new Point(40, 125),
                Cursor = Cursors.Hand
            };
            shopNowBtn.FlatAppearance.BorderSize = 0;
            shopNowBtn.Click += (s, e) => ShowTodaysDeals();
            heroBanner.Controls.Add(shopNowBtn);
        }

        private void CreateCategoriesGrid(int startY)
        {
            Label categoriesTitle = new Label
            {
                Text = "Shop by Category",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(0, startY),
                Size = new Size(300, 40)
            };
            contentMainPanel.Controls.Add(categoriesTitle);

            string[] categories = { "📱 Electronics", "👕 Fashion", "📚 Books", "🏠 Home", "⚽ Sports", "💄 Beauty" };
            
            for (int i = 0; i < categories.Length; i++)
            {
                int x = (i % 3) * 250;
                int y = startY + 50 + (i / 3) * 120;
                
                CreateCategoryCard(categories[i], x, y);
            }
        }

        private void CreateCategoryCard(string category, int x, int y)
        {
            Panel categoryCard = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(230, 100),
                BackColor = Color.FromArgb(250, 250, 250),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };
            categoryCard.Click += (s, e) => ShowCategory(category.Substring(2)); // Remove emoji
            contentMainPanel.Controls.Add(categoryCard);

            Label categoryLabel = new Label
            {
                Text = category,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 70, 70),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            categoryCard.Controls.Add(categoryLabel);

            categoryCard.MouseEnter += (s, e) => categoryCard.BackColor = Color.FromArgb(240, 240, 240);
            categoryCard.MouseLeave += (s, e) => categoryCard.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void CreateFeaturedProductsGrid(int startY)
        {
            Label featuredTitle = new Label
            {
                Text = "Featured Products",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(0, startY),
                Size = new Size(300, 40)
            };
            contentMainPanel.Controls.Add(featuredTitle);

            string[,] products = {
                { "iPhone 15 Pro", "$999", "📱" },
                { "MacBook Air", "$1199", "💻" },
                { "AirPods Pro", "$249", "🎧" },
                { "Gaming Console", "$499", "🎮" }
            };

            for (int i = 0; i < 4; i++)
            {
                int x = i * 250;
                int y = startY + 50;
                
                CreateModernProductCard(products[i,0], products[i,1], products[i,2], x, y, i);
            }
        }

        private void CreateModernProductCard(string name, string price, string emoji, int x, int y, int productId)
        {
            Panel productCard = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(230, 220),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };
            contentMainPanel.Controls.Add(productCard);

            Label emojiLabel = new Label
            {
                Text = emoji,
                Font = new Font("Segoe UI", 48),
                Location = new Point(80, 20),
                Size = new Size(70, 70),
                TextAlign = ContentAlignment.MiddleCenter
            };
            productCard.Controls.Add(emojiLabel);

            Label nameLabel = new Label
            {
                Text = name,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(10, 100),
                Size = new Size(210, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            productCard.Controls.Add(nameLabel);

            Label priceLabel = new Label
            {
                Text = price,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 159, 28),
                Location = new Point(10, 130),
                Size = new Size(210, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            productCard.Controls.Add(priceLabel);

            Button addToCartBtn = new Button
            {
                Text = "Add to Cart",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(255, 159, 28),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(180, 35),
                Location = new Point(25, 170),
                Cursor = Cursors.Hand
            };
            addToCartBtn.FlatAppearance.BorderSize = 0;
            addToCartBtn.Click += (s, e) => {
                // DA2 - Weak authentication, DA5 - Authorization bypass
                string result = VulnerabilityManager.AddToCart(productId, 1);
                MessageBox.Show(result, "Cart Update");
            };
            productCard.Controls.Add(addToCartBtn);

            productCard.MouseEnter += (s, e) => productCard.BackColor = Color.FromArgb(248, 248, 248);
            productCard.MouseLeave += (s, e) => productCard.BackColor = Color.White;
        }

        private void CreateOffersSection(int startY)
        {
            Panel offersPanel = new Panel
            {
                Location = new Point(0, startY),
                Size = new Size(contentMainPanel.Width - 40, 150),
                BackColor = Color.FromArgb(46, 125, 50),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            contentMainPanel.Controls.Add(offersPanel);

            Label offersTitle = new Label
            {
                Text = "🎁 Special Offers Just for You!",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 30),
                Size = new Size(500, 40)
            };
            offersPanel.Controls.Add(offersTitle);

            Label offersDesc = new Label
            {
                Text = "Get exclusive deals on your favorite brands",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.White,
                Location = new Point(30, 75),
                Size = new Size(400, 25)
            };
            offersPanel.Controls.Add(offersDesc);

            Button viewOffersBtn = new Button
            {
                Text = "View All Offers",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 125, 50),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 40),
                Location = new Point(30, 100),
                Cursor = Cursors.Hand
            };
            viewOffersBtn.FlatAppearance.BorderSize = 0;
            viewOffersBtn.Click += (s, e) => ShowFlashSale();
            offersPanel.Controls.Add(viewOffersBtn);
        }

        // Login/Signup functionality
        private void ShowLoginSignupPanel(bool showSignup = false)
        {
            contentMainPanel.Controls.Clear();

            // Create login/signup container
            Panel authPanel = new Panel
            {
                Size = new Size(500, 600),
                Location = new Point((contentMainPanel.Width - 500) / 2, 50),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top
            };
            contentMainPanel.Controls.Add(authPanel);

            // Add shadow effect
            authPanel.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, authPanel.ClientRectangle,
                    Color.FromArgb(200, 200, 200), ButtonBorderStyle.Solid);
            };

            CreateAuthTabs(authPanel, showSignup);
        }

        private void CreateAuthTabs(Panel authPanel, bool showSignup)
        {
            // Tab buttons
            Button loginTab = new Button
            {
                Text = "Sign In",
                Location = new Point(0, 0),
                Size = new Size(250, 50),
                BackColor = showSignup ? Color.FromArgb(245, 245, 245) : Color.FromArgb(255, 159, 28),
                ForeColor = showSignup ? Color.Black : Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            loginTab.FlatAppearance.BorderSize = 0;
            loginTab.Click += (s, e) => CreateAuthTabs(authPanel, false);

            Button signupTab = new Button
            {
                Text = "Sign Up",
                Location = new Point(250, 0),
                Size = new Size(250, 50),
                BackColor = !showSignup ? Color.FromArgb(245, 245, 245) : Color.FromArgb(255, 159, 28),
                ForeColor = !showSignup ? Color.Black : Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            signupTab.FlatAppearance.BorderSize = 0;
            signupTab.Click += (s, e) => CreateAuthTabs(authPanel, true);

            authPanel.Controls.Clear();
            authPanel.Controls.Add(loginTab);
            authPanel.Controls.Add(signupTab);

            if (showSignup)
            {
                CreateSignupForm(authPanel);
            }
            else
            {
                CreateLoginForm(authPanel);
            }
        }

        private void CreateLoginForm(Panel authPanel)
        {
            Label titleLabel = new Label
            {
                Text = "Welcome Back!",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(50, 80),
                Size = new Size(400, 30)
            };
            authPanel.Controls.Add(titleLabel);

            Label subtitleLabel = new Label
            {
                Text = "Sign in to your account to continue shopping",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(50, 115),
                Size = new Size(400, 25)
            };
            authPanel.Controls.Add(subtitleLabel);

            // Email field
            Label emailLabel = new Label
            {
                Text = "Email Address",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(50, 160),
                Size = new Size(120, 20)
            };
            authPanel.Controls.Add(emailLabel);

            TextBox emailBox = new TextBox
            {
                Location = new Point(50, 185),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle
            };
            authPanel.Controls.Add(emailBox);

            // Password field
            Label passwordLabel = new Label
            {
                Text = "Password",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(50, 230),
                Size = new Size(120, 20)
            };
            authPanel.Controls.Add(passwordLabel);

            TextBox passwordBox = new TextBox
            {
                Location = new Point(50, 255),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '●'
            };
            authPanel.Controls.Add(passwordBox);

            // Remember me checkbox
            CheckBox rememberBox = new CheckBox
            {
                Text = "Remember me",
                Location = new Point(50, 300),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(70, 70, 70)
            };
            authPanel.Controls.Add(rememberBox);

            // Forgot password link
            Label forgotLabel = new Label
            {
                Text = "Forgot Password?",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(255, 159, 28),
                Location = new Point(300, 300),
                Size = new Size(150, 25),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleRight
            };
            forgotLabel.Click += (s, e) => ShowForgotPassword();
            authPanel.Controls.Add(forgotLabel);

            // Login button
            Button loginButton = new Button
            {
                Text = "Sign In",
                Location = new Point(50, 350),
                Size = new Size(400, 45),
                BackColor = Color.FromArgb(255, 159, 28),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.Click += (s, e) => {
                // DA2 - Weak authentication check
                bool loginSuccess = VulnerabilityManager.AuthenticateUser(emailBox.Text, passwordBox.Text);
                if (loginSuccess)
                {
                    LoginUser(emailBox.Text);
                    ShowModernHomePage();
                }
                else
                {
                    MessageBox.Show("Invalid email or password. Try admin@shopvault.com / admin123", "Login Failed");
                }
            };
            authPanel.Controls.Add(loginButton);

            // Demo credentials info
            Label demoLabel = new Label
            {
                Text = "Demo: admin@shopvault.com / admin123",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(150, 150, 150),
                Location = new Point(50, 410),
                Size = new Size(400, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            authPanel.Controls.Add(demoLabel);
        }

        private void CreateSignupForm(Panel authPanel)
        {
            Label titleLabel = new Label
            {
                Text = "Join ShopVault!",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(50, 80),
                Size = new Size(400, 30)
            };
            authPanel.Controls.Add(titleLabel);

            Label subtitleLabel = new Label
            {
                Text = "Create your account and start shopping",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(50, 115),
                Size = new Size(400, 25)
            };
            authPanel.Controls.Add(subtitleLabel);

            // Full Name field
            Label nameLabel = new Label
            {
                Text = "Full Name",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(50, 160),
                Size = new Size(120, 20)
            };
            authPanel.Controls.Add(nameLabel);

            TextBox nameBox = new TextBox
            {
                Location = new Point(50, 185),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle
            };
            authPanel.Controls.Add(nameBox);

            // Email field
            Label emailLabel = new Label
            {
                Text = "Email Address",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(50, 230),
                Size = new Size(120, 20)
            };
            authPanel.Controls.Add(emailLabel);

            TextBox emailBox = new TextBox
            {
                Location = new Point(50, 255),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle
            };
            authPanel.Controls.Add(emailBox);

            // Password field
            Label passwordLabel = new Label
            {
                Text = "Password",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(50, 300),
                Size = new Size(120, 20)
            };
            authPanel.Controls.Add(passwordLabel);

            TextBox passwordBox = new TextBox
            {
                Location = new Point(50, 325),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '●'
            };
            authPanel.Controls.Add(passwordBox);

            // Terms checkbox
            CheckBox termsBox = new CheckBox
            {
                Text = "I agree to the Terms of Service and Privacy Policy",
                Location = new Point(50, 375),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(70, 70, 70)
            };
            authPanel.Controls.Add(termsBox);

            // Signup button
            Button signupButton = new Button
            {
                Text = "Create Account",
                Location = new Point(50, 420),
                Size = new Size(400, 45),
                BackColor = Color.FromArgb(255, 159, 28),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            signupButton.FlatAppearance.BorderSize = 0;
            signupButton.Click += (s, e) => {
                if (!termsBox.Checked)
                {
                    MessageBox.Show("Please accept the Terms of Service to continue.", "Terms Required");
                    return;
                }
                
                // DA1 - SQL injection in user registration, DA3 - Data exposure
                string result = VulnerabilityManager.RegisterUser(nameBox.Text, emailBox.Text, passwordBox.Text);
                if (result.Contains("Success"))
                {
                    MessageBox.Show("Account created successfully! You can now sign in.", "Registration Success");
                    CreateAuthTabs(authPanel, false);
                }
                else
                {
                    MessageBox.Show(result, "Registration Failed");
                }
            };
            authPanel.Controls.Add(signupButton);
        }

        private void LoginUser(string email)
        {
            isUserLoggedIn = true;
            currentUser = email.Split('@')[0];
            
            // Refresh header to show logged in state
            CreateAccountSection((TableLayoutPanel)headerPanel.Controls[0]);
        }

        private void LogoutUser()
        {
            isUserLoggedIn = false;
            currentUser = "";
            
            // Refresh header to show login state
            CreateAccountSection((TableLayoutPanel)headerPanel.Controls[0]);
            ShowModernHomePage();
        }

        private void ShowForgotPassword()
        {
            MessageBox.Show("Password reset functionality - Demo purposes only", "Forgot Password");
        }

        private void ShowAccountDropdown()
        {
            MessageBox.Show("Account dropdown - My Orders, Wishlist, Account Settings", "Account Menu");
        }

        // Placeholder methods for sidebar actions
        private void ShowBrandProducts(string brand)
        {
            MessageBox.Show($"Showing products from {brand} - Feature in development", "Brand Products");
        }

        private void ShowTodaysDeals()
        {
            MessageBox.Show("Today's Deals - Up to 70% OFF on selected items!", "Today's Deals");
        }

        private void ShowFlashSale()
        {
            MessageBox.Show("Flash Sale - Limited time offers ending soon!", "Flash Sale");
        }

        private void ShowGiftCards()
        {
            MessageBox.Show("Gift Cards - Perfect for any occasion!", "Gift Cards");
        }

        // Security testing methods for UAC demonstration
        private void ShowUACTest()
        {
            contentMainPanel.Controls.Clear();

            // UAC Test Interface
            Label titleLabel = new Label
            {
                Text = "🚨 UAC & Privilege Escalation Test",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69), // Bootstrap danger color
                Location = new Point(0, 20),
                Size = new Size(800, 40)
            };
            contentMainPanel.Controls.Add(titleLabel);

            Label warningLabel = new Label
            {
                Text = "⚠️ WARNING: This application demonstrates security vulnerabilities\nfor educational and testing purposes only.",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(133, 77, 14), // Bootstrap warning color
                Location = new Point(0, 70),
                Size = new Size(800, 50),
                BackColor = Color.FromArgb(255, 243, 205) // Warning background
            };
            contentMainPanel.Controls.Add(warningLabel);

            Button uacTestBtn = new Button
            {
                Text = "🔐 Test UAC Bypass (DA6)",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(0, 150),
                Size = new Size(250, 45),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            uacTestBtn.FlatAppearance.BorderSize = 0;
            uacTestBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestUACBypass();
                ShowSecurityTestResult(result);
            };
            contentMainPanel.Controls.Add(uacTestBtn);

            Button registryTestBtn = new Button
            {
                Text = "📋 Test Registry Access (DA4)",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(270, 150),
                Size = new Size(250, 45),
                BackColor = Color.FromArgb(255, 193, 7),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            registryTestBtn.FlatAppearance.BorderSize = 0;
            registryTestBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestRegistryAccess();
                ShowSecurityTestResult(result);
            };
            contentMainPanel.Controls.Add(registryTestBtn);

            // Information panel
            Panel infoPanel = new Panel
            {
                Location = new Point(0, 220),
                Size = new Size(800, 200),
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle
            };
            contentMainPanel.Controls.Add(infoPanel);

            Label infoTitle = new Label
            {
                Text = "🔍 What This Tests:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(15, 15),
                Size = new Size(200, 25),
                ForeColor = Color.FromArgb(33, 37, 41)
            };
            infoPanel.Controls.Add(infoTitle);

            Label infoText = new Label
            {
                Text = "• DA6 - Security Misconfiguration (UAC bypass attempts)\n" +
                       "• DA4 - Improper Cryptography (Registry secret storage)\n" +
                       "• DA5 - Improper Authorization (Privilege escalation)\n" +
                       "• DA3 - Sensitive Data Exposure (Admin credential access)\n\n" +
                       "This application runs with elevated privileges and demonstrates\n" +
                       "how malicious software could abuse administrator access.",
                Font = new Font("Segoe UI", 11),
                Location = new Point(15, 50),
                Size = new Size(770, 130),
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            infoPanel.Controls.Add(infoText);
        }

        private void ShowFileAccessTest()
        {
            contentMainPanel.Controls.Clear();

            Label titleLabel = new Label
            {
                Text = "📁 File System Access Test",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(0, 20),
                Size = new Size(600, 40)
            };
            contentMainPanel.Controls.Add(titleLabel);

            Button fileTestBtn = new Button
            {
                Text = "🗂️ Test File System Access",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(0, 80),
                Size = new Size(250, 45),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            fileTestBtn.FlatAppearance.BorderSize = 0;
            fileTestBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestFileSystemAccess();
                ShowSecurityTestResult(result);
            };
            contentMainPanel.Controls.Add(fileTestBtn);

            Label descLabel = new Label
            {
                Text = "Tests DA4 (Insecure Data Storage) by creating files with sensitive data\n" +
                       "in temporary directories without proper permissions.",
                Font = new Font("Segoe UI", 11),
                Location = new Point(0, 140),
                Size = new Size(600, 50),
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            contentMainPanel.Controls.Add(descLabel);
        }

        private void ShowPrivilegeTest()
        {
            contentMainPanel.Controls.Clear();

            Label titleLabel = new Label
            {
                Text = "🔑 Privilege Escalation Test",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(0, 20),
                Size = new Size(600, 40)
            };
            contentMainPanel.Controls.Add(titleLabel);

            Button privilegeTestBtn = new Button
            {
                Text = "⚡ Test Privilege Escalation",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(0, 80),
                Size = new Size(250, 45),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            privilegeTestBtn.FlatAppearance.BorderSize = 0;
            privilegeTestBtn.Click += (s, e) => {
                string result = VulnerabilityManager.TestPrivilegeEscalation();
                ShowSecurityTestResult(result);
            };
            contentMainPanel.Controls.Add(privilegeTestBtn);

            Label descLabel = new Label
            {
                Text = "Tests DA5 (Improper Authorization) by checking current user privileges\n" +
                       "and demonstrating unnecessary administrative access.",
                Font = new Font("Segoe UI", 11),
                Location = new Point(0, 140),
                Size = new Size(600, 50),
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            contentMainPanel.Controls.Add(descLabel);
        }

        private void ShowSecurityTestResult(string result)
        {
            // Create a detailed result window
            Form resultForm = new Form
            {
                Text = "Security Test Results",
                Size = new Size(800, 600),
                StartPosition = FormStartPosition.CenterParent,
                Icon = this.Icon,
                MaximizeBox = true,
                MinimizeBox = false
            };

            TextBox resultTextBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 10),
                Text = result,
                BackColor = Color.FromArgb(33, 37, 41),
                ForeColor = Color.FromArgb(248, 249, 250),
                Margin = new Padding(10)
            };

            Panel buttonPanel = new Panel
            {
                Height = 50,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(248, 249, 250)
            };

            Button copyButton = new Button
            {
                Text = "📋 Copy Results",
                Size = new Size(120, 30),
                Location = new Point(10, 10),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            copyButton.FlatAppearance.BorderSize = 0;
            copyButton.Click += (s, e) => {
                Clipboard.SetText(result);
                MessageBox.Show("Results copied to clipboard!", "Copied");
            };

            Button closeButton = new Button
            {
                Text = "Close",
                Size = new Size(80, 30),
                Location = new Point(140, 10),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (s, e) => resultForm.Close();

            buttonPanel.Controls.Add(copyButton);
            buttonPanel.Controls.Add(closeButton);
            resultForm.Controls.Add(resultTextBox);
            resultForm.Controls.Add(buttonPanel);

            resultForm.ShowDialog(this);
        }

        private void ShowCategoryProducts(string category)
        {
            contentMainPanel.Controls.Clear();

            // Category header
            Label categoryTitle = new Label
            {
                Text = $"🏷️ {category} Products",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(0, 20),
                Size = new Size(500, 40)
            };
            contentMainPanel.Controls.Add(categoryTitle);

            // Category description
            Label categoryDesc = new Label
            {
                Text = GetCategoryDescription(category),
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(0, 70),
                Size = new Size(800, 30)
            };
            contentMainPanel.Controls.Add(categoryDesc);

            // Products grid
            CreateCategoryProductGrid(category, 120);
        }

        private string GetCategoryDescription(string category)
        {
            switch (category.ToLower())
            {
                case "electronics":
                    return "Discover the latest smartphones, laptops, tablets, and tech accessories";
                case "clothing":
                    return "Fashion for men, women, and kids - from casual wear to formal attire";
                case "books":
                    return "Bestsellers, textbooks, fiction, non-fiction, and digital books";
                case "home & garden":
                    return "Everything for your home - furniture, décor, kitchen, and garden essentials";
                case "gaming":
                    return "Latest games, consoles, accessories, and gaming gear";
                case "sports":
                    return "Sports equipment, fitness gear, outdoor activities, and athletic wear";
                case "beauty":
                    return "Skincare, makeup, fragrances, and personal care products";
                default:
                    return "Browse our wide selection of quality products";
            }
        }

        private void CreateCategoryProductGrid(string category, int startY)
        {
            // Sample products for each category
            string[,] products = GetCategoryProducts(category);
            int productsCount = products.GetLength(0);

            for (int i = 0; i < productsCount; i++)
            {
                int row = i / 4;
                int col = i % 4;
                int x = col * 250;
                int y = startY + row * 250;

                CreateModernProductCard(products[i, 0], products[i, 1], products[i, 2], x, y, i + 100);
            }
        }

        private string[,] GetCategoryProducts(string category)
        {
            switch (category.ToLower())
            {
                case "electronics":
                    return new string[,] {
                        { "iPhone 15 Pro", "$999", "📱" },
                        { "MacBook Air", "$1199", "💻" },
                        { "Samsung Galaxy", "$899", "📱" },
                        { "iPad Pro", "$799", "📱" },
                        { "Sony Headphones", "$299", "🎧" },
                        { "Nintendo Switch", "$349", "🎮" },
                        { "Apple Watch", "$399", "⌚" },
                        { "Dell Monitor", "$249", "🖥️" }
                    };
                case "clothing":
                    return new string[,] {
                        { "Nike Air Max", "$129", "👟" },
                        { "Levi's Jeans", "$79", "👖" },
                        { "Adidas Hoodie", "$89", "👕" },
                        { "Calvin Klein Dress", "$149", "👗" },
                        { "Ray-Ban Sunglasses", "$199", "🕶️" },
                        { "Polo Shirt", "$59", "👕" },
                        { "Winter Jacket", "$199", "🧥" },
                        { "Designer Handbag", "$299", "👜" }
                    };
                case "books":
                    return new string[,] {
                        { "Programming Guide", "$49", "📚" },
                        { "Fiction Bestseller", "$19", "📖" },
                        { "Science Textbook", "$89", "📘" },
                        { "Art & Design", "$39", "🎨" },
                        { "Cooking Recipes", "$29", "👨‍🍳" },
                        { "Travel Guide", "$24", "🌍" },
                        { "Self-Help Book", "$16", "💡" },
                        { "History Novel", "$22", "📜" }
                    };
                case "home & garden":
                    return new string[,] {
                        { "Coffee Maker", "$79", "☕" },
                        { "Garden Tools Set", "$49", "🌱" },
                        { "LED Lamp", "$39", "💡" },
                        { "Dining Chair", "$89", "🪑" },
                        { "Kitchen Blender", "$59", "🥤" },
                        { "Flower Pot", "$19", "🌺" },
                        { "Wall Clock", "$29", "🕐" },
                        { "Storage Box", "$24", "📦" }
                    };
                case "gaming":
                    return new string[,] {
                        { "Gaming Keyboard", "$129", "⌨️" },
                        { "Gaming Mouse", "$79", "🖱️" },
                        { "VR Headset", "$399", "🥽" },
                        { "Game Controller", "$59", "🎮" },
                        { "Gaming Chair", "$299", "🪑" },
                        { "Graphics Card", "$699", "🔧" },
                        { "Gaming Monitor", "$349", "🖥️" },
                        { "Streaming Mic", "$149", "🎤" }
                    };
                default:
                    return new string[,] {
                        { "Popular Item 1", "$99", "⭐" },
                        { "Popular Item 2", "$79", "⭐" },
                        { "Popular Item 3", "$149", "⭐" },
                        { "Popular Item 4", "$59", "⭐" }
                    };
            }
        }
    }
}
