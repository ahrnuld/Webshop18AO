using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Webshop18AO.Data;
using Webshop18AO.Models;

namespace Webshop18AO.Controllers
{
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public CheckoutController(ApplicationDbContext context,
            UserManager<IdentityUser> userMan)
        {
            _context = context;
            _userManager = userMan;
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutViewModel model)
        {
            Order order = new Order();
            order.Address = model.Address;
            order.City = model.City;
            order.Name = model.Name;

            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);

            order.User = user;

            order.OrderLines = new List<OrderLine>();

            List<CartItem> cart = new List<CartItem>();

            string cartString = HttpContext.Session.GetString("cart");
            if (cartString != null)
                cart = JsonConvert.DeserializeObject<List<CartItem>>(cartString);

            foreach (CartItem cartItem in cart)
            {
                Product product = _context.Product.Find(cartItem.ProductId);

                OrderLine orderLine = new OrderLine();
                orderLine.Amount = cartItem.Amount;
                orderLine.Price = product.Price;
                orderLine.ProductId = cartItem.ProductId;

                order.OrderLines.Add(orderLine);
            }

            try
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

            return View("Confirm");
        }
    }
}