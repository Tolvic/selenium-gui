using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_net_nav.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace web_net_nav.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult RunReceipe(string url)
        {
            var driver = new ChromeDriver(@"C:\Projects\web-net-nav\drivers");

            driver.Navigate().GoToUrl(url);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
