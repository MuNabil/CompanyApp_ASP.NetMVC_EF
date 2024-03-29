﻿using CompanyApp.Data;
using CompanyApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CompanyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Company_DbContext dbContext;

        public HomeController(ILogger<HomeController> logger,
            Company_DbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            TempData["logging"] = "yes";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(User model)
        {
            if (ModelState.IsValid)
            {
                dbContext.Users.Add(model);
                dbContext.SaveChanges();

                return RedirectToAction("Login");
            }

            TempData["logging"] = "yes";
            return View();
        }

        public IActionResult Login()
        {
            TempData["logging"] = "yes";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string emailAdr, string pass)
        {
            if (ModelState.IsValid)
            {
                var user = dbContext.Users.SingleOrDefault(u => u.UserEmail == emailAdr && u.UserPassword == pass);

                if (user == null)
                {
                    ViewBag.Massege = "Wrong Email or Password.";

                    TempData["logging"] = "yes";
                    return View();
                }

                return RedirectToAction("Index");
            }

            TempData["logging"] = "yes";
            return View();
        }

        public IActionResult Search()
        {
            TempData["what"] = "What are you searching for in this blank page..!";
            TempData["what2"] = "To search somewhere else..";

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
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