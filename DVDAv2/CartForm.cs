using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DVDAv2
{
    public partial class CartForm : Form
    {
        private List<string> cartItems;

        public CartForm(List<string> items)
        {
            InitializeComponent();
            cartItems = items;
            listBoxCart.Items.AddRange(cartItems.ToArray());
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Checkout successful!");
            cartItems.Clear();
            listBoxCart.Items.Clear();
        }

        public CartForm()
        {
            InitializeComponent();
        }
    }
}
