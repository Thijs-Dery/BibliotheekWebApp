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

        // Index - Welkomstpagina
        public IActionResult Index()
        {
            return View();
        }

        // Boekenlijst
        public IActionResult BoekenLijst()
        {
            var boeken = _context.Boeken.ToList();
            return View(boeken);
        }

        // Auteurslijst
        public IActionResult AuteursLijst()
        {
            var auteurs = _context.Auteurs.ToList();
            return View(auteurs);
        }

        // Ledenlijst
        public IActionResult LedenLijst()
        {
            var leden = _context.Leden.ToList();
            return View(leden);
        }

        // Privacy
        public IActionResult Privacy()
        {
            return View();
        }
    }
}



