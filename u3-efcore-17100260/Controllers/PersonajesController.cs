using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using u3_efcore_17100260.Models;

namespace u3_efcore_17100260.Controllers
{
    public class PersonajesController : Controller
    {
        private readonly CaricaturasContext db;

        public PersonajesController(CaricaturasContext context)
        {
            db = context;
        }

        public IActionResult VerRegistro(int id)
        {
            Personaje personaje = db.Personajes.Where(e => e.IdPersonaje == id).FirstOrDefault();
            return View(personaje);
        }

        public IActionResult ListadoRegistros()
        {
            var listadoPersonajes = db.Personajes.Include(e => e.IdAnimeNavigation).ToList();
            return View(listadoPersonajes);
        }

        public IActionResult AgregarRegistro()
        {
            ViewData["AnimeId"] = new SelectList(db.Animes, "IdAnime", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarRegistro([Bind("IdPersonaje,Nombre,IdAnime")] Personaje personaje)
        {
            if (ModelState.IsValid)
            {
                db.Add(personaje);
                await db.SaveChangesAsync();
                TempData["AlertMessage"] = "El registro se creó correctamente! :)";
                return View("VerRegistro", personaje);
            }
            ViewData["AnimeId"] = new SelectList(db.Animes, "IdAnime", "Nombre");
            return View(personaje);
        }

        public IActionResult CargarRegistros()
        {
            for (int i = 0; i < 5; i++)
            {
                Personaje personaje = new Personaje();
                personaje.Nombre = "Ejemplo" + (i + 1).ToString();
                personaje.IdAnime = i + 1;
                db.Add(personaje);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(ListadoRegistros));
        }

        //GET: EditarRegistro
        public async Task<IActionResult> EditarRegistro(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaje = await db.Personajes.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }
            ViewData["AnimeId"] = new SelectList(db.Animes, "IdAnime", "Nombre");
            return View(personaje);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarRegistro(int id, [Bind("IdPersonaje,Nombre,IdAnime")] Personaje personaje)
        {
            if (id != personaje.IdPersonaje)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(personaje);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonajeExists(personaje.IdPersonaje))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["AlertMessage"] = "El registro se actualizó correctamente! :)";
                return RedirectToAction(nameof(ListadoRegistros));
            }
            ViewData["AnimeId"] = new SelectList(db.Animes, "IdAnime", "Nombre");
            return View(personaje);
        }

        // POST: Employees/Delete/5

        public IActionResult EliminarRegistro(int id)
        {
            var personaje = db.Personajes.Find(id);
            db.Personajes.Remove(personaje);
            db.SaveChanges();
            TempData["AlertMessage"] = "El registro se eliminó correctamente! :)";
            return RedirectToAction(nameof(ListadoRegistros));
        }

        private bool PersonajeExists(int id)
        {
            return db.Personajes.Any(e => e.IdPersonaje == id);
        }

    }
}
