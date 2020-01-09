using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop18AO.Models
{
    public class CheckoutViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public double TotalPrice { get; set; }

        [required]
        public string Name { get; set; }
        [required]
        public string Address { get; set; }
        [required]
        public string City { get; set; }
    }
}
