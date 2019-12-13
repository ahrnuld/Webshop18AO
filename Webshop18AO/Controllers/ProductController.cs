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
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Product.ToList());
        }

        public IActionResult AddToCart(int id)
        {
            List<CartItem> cart = new List<CartItem>();

            string cartString = HttpContext.Session.GetString("cart");
            if (cartString != null)
                cart = JsonConvert.DeserializeObject<List<CartItem>>(cartString);

            CartItem item = new CartItem
            {
                Amount = 1,
                ProductId = id
            };
            cart.Add(item);

            cartString = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("cart", cartString);

            return RedirectToAction("index");
        }

        public IActionResult Cart()
        {
            List<CartItem> cart = new List<CartItem>();

            string cartString = HttpContext.Session.GetString("cart");
            if (cartString != null)
                cart = JsonConvert.DeserializeObject<List<CartItem>>(cartString);


            List<CartItemViewModel> cartvm = new List<CartItemViewModel>();

            foreach(CartItem ci in cart)
            {
                CartItemViewModel civm = new CartItemViewModel();

                civm.ProductId = ci.ProductId;
                civm.Amount = ci.Amount;

                Product p = _context.Product.Find(ci.ProductId);

                civm.Name = p.Name;
                civm.Price = p.Price;
                civm.ImageUrl = p.ImageUrl;

                cartvm.Add(civm);
            }


            return View(cartvm);
        }
    }
}