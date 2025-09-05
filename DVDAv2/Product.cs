using System;
using System.Windows.Forms;

namespace DVDAv2
{
    namespace DVDAv2
    {
        public class Product
        {
            public string Name { get; set; }
            public string ImagePath { get; set; }

            public Product() { }

            public Product(string name, string imagePath)
            {
                Name = name;
                ImagePath = imagePath;
            }
        }
    }

}
