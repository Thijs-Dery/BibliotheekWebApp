using Microsoft.AspNetCore.Mvc;
using BibliotheekApp.Models;

namespace BibliotheekApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly BibliotheekContext _context;

        public HomeController(BibliotheekContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BoekenLijst()
        {
            var boeken = _context.Boeken.ToList();
            return View(boeken);
        }
    }
}

