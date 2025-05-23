﻿using Microsoft.AspNetCore.Mvc;
using BibliotheekApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace BibliotheekApp.Controllers
{
    [Route("Boek")]
    public class BoekController : Controller
    {
        private readonly BibliotheekContext _context;

        public BoekController(BibliotheekContext context)
        {
            _context = context;
        }

        // Iedereen kan de boekenlijst zien
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var boeken = _context.Boeken
                                 .Include(b => b.Auteur)
                                 .Where(b => !b.IsDeleted)
                                 .ToList();
            return View(boeken);
        }

        // AJAX-zoekfunctionaliteit
        [HttpGet("Search")]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                // Geef alle boeken terug als er geen zoekopdracht is
                var allBoeken = _context.Boeken
                                        .Include(b => b.Auteur)
                                        .Where(b => !b.IsDeleted)
                                        .ToList();
                return PartialView("_BoekSearchResults", allBoeken);
            }

            // Zoek boeken op titel of auteur
            var filteredBoeken = _context.Boeken
                                         .Include(b => b.Auteur)
                                         .Where(b => !b.IsDeleted &&
                                                     (b.Titel.Contains(query) || b.Auteur.Naam.Contains(query)))
                                         .ToList();

            return PartialView("_BoekSearchResults", filteredBoeken);
        }

        // Alleen admin mag een boek toevoegen
        [Authorize(Roles = "Admin")]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Boek boek)
        {
            if (ModelState.IsValid)
            {
                _context.Boeken.Add(boek);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Boek succesvol toegevoegd!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(boek);
        }

        // Alleen admin mag een boek bewerken
        [Authorize(Roles = "Admin")]
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(string id)
        {
            var boek = _context.Boeken.Include(b => b.Auteur).FirstOrDefault(b => b.ISBN == id);
            if (boek == null)
            {
                return NotFound();
            }

            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(boek);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Boek boek)
        {
            if (id != boek.ISBN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(boek);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(boek);
        }

        // Alleen admin mag een boek verwijderen
        [Authorize(Roles = "Admin")]
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var boek = _context.Boeken.FirstOrDefault(b => b.ISBN == id);
            if (boek == null)
            {
                return NotFound();
            }

            return View(boek);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("DeleteConfirmed/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var boek = _context.Boeken.FirstOrDefault(b => b.ISBN == id);
            if (boek != null)
            {
                boek.IsDeleted = true;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
