using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task2.Client.Models;

namespace Task2.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderDetails(decimal orderId)
        {
            ViewData["Id"] = orderId;
            return View();
        }

        [HttpGet]
        public IActionResult UpdateItem(decimal orderId, decimal itemId)
        {
            ViewData["orderId"] = orderId;
            ViewData["itemId"] = itemId;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateItem()
        {            
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult AddItems(decimal orderId)
        {
            ViewData["orderId"] = orderId;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CreateAccount()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
