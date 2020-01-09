using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop18AO.Models
{
    public class Order
    {
        public int Id { get; set; }
        [required]
        public string Name { get; set; }
        [required]
        public string Address { get; set; }
        [required]
        public string City { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderLine> OrderLines { get; set; }

        public IdentityUser User { get; set; }
        public string UserId { get; set; }

        public Order()
        {
            OrderDate = DateTime.Now;
        }
    }
}
