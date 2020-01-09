using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Webshop18AO.Data;
using Webshop18AO.Models;

namespace Webshop18AO.Controllers
{
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            CheckoutViewModel cvm = new CheckoutViewModel();

            List<CartItem> cart = new List<CartItem>();

            string cartString = HttpContext.Session.GetString("cart");
            if (cartString != null)
                cart = JsonConvert.DeserializeObject<List<CartItem>>(cartString);


            List<CartItemViewModel> cartvm = new List<CartItemViewModel>();

            double totalPrice = 0;

            foreach (CartItem ci in cart)
            {
                CartItemViewModel civm = new CartItemViewModel();

                civm.ProductId = ci.ProductId;
                civm.Amount = ci.Amount;

                Product p = _context.Product.Find(ci.ProductId);

                civm.Name = p.Name;
                civm.Price = p.Price;
                civm.ImageUrl = p.ImageUrl;

                totalPrice += ci.Amount * p.Price;

                cartvm.Add(civm);
            }

            cvm.CartItems = cartvm;
            cvm.TotalPrice = totalPrice;

            return View(cvm);
        }
    }
}