namespace DVDAv2
{
    partial class CartForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBoxCart;
        private System.Windows.Forms.Button btnCheckout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.listBoxCart = new System.Windows.Forms.ListBox();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxCart
            // 
            this.listBoxCart.FormattingEnabled = true;
            this.listBoxCart.ItemHeight = 16;
            this.listBoxCart.Location = new System.Drawing.Point(12, 12);
            this.listBoxCart.Name = "listBoxCart";
            this.listBoxCart.Size = new System.Drawing.Size(360, 180);
            this.listBoxCart.TabIndex = 0;
            // 
            // btnCheckout
            // 
            this.btnCheckout.Location = new System.Drawing.Point(12, 200);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(360, 30);
            this.btnCheckout.TabIndex = 1;
            this.btnCheckout.Text = "Checkout";
            this.btnCheckout.UseVisualStyleBackColor = true;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // CartForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 251);
            this.Controls.Add(this.btnCheckout);
            this.Controls.Add(this.listBoxCart);
            this.Name = "CartForm";
            this.Text = "Cart";
            this.ResumeLayout(false);

            this.SuspendLayout();
            // 
            // CartForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Name = "CartForm";
            this.Text = "Your Cart";
            this.ResumeLayout(false);
        }
    }
}
