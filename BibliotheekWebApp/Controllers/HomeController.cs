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

        public IActionResult BoekenLijst()
        {
            var boeken = _context.Boeken.Where(b => !b.IsDeleted).ToList();
            return View(boeken);
        }

        public IActionResult AuteursLijst()
        {
            var auteurs = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(auteurs);
        }

        public IActionResult LedenLijst()
        {
            var leden = _context.Leden.Where(l => !l.IsDeleted).ToList();
            return View(leden);
        }
    }
}




