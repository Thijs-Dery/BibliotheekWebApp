using Microsoft.AspNetCore.Mvc;
using BibliotheekApp.Models;
using System.Linq;

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

        public IActionResult LedenLijst()
        {
            var leden = _context.Leden.ToList();
            return View(leden);
        }

        public IActionResult BoekenLijst()
        {
            var boeken = _context.Boeken.ToList();
            return View(boeken);
        }

        public IActionResult AuteursLijst()
        {
            var auteurs = _context.Auteurs.ToList();
            return View(auteurs);
        }
    }
}


