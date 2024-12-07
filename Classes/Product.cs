using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce__Project.Classes
{
    public class Product
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public decimal Price { get; set; }
    }
}