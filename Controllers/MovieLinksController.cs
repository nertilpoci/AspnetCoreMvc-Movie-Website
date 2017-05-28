using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using Movies.DAL;
using Microsoft.AspNetCore.Authorization;

namespace Movies.Controllers
{
    [Authorize]
    public class MovieLinksController : Controller
    {
        private readonly IUnitOfWork _work;

        public MovieLinksController(IUnitOfWork work)
        {
            _work = work;
        }

       

        // GET: MovieLinks/Create
        public IActionResult Create(int id)
        {

            ViewData["MovieId"] = id;
            return View();
        }

        // POST: MovieLinks/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Source,MovieId")] MovieLinks movieLinks)
        {
            if (ModelState.IsValid)
            {
                _work.Links.Add(movieLinks);
                await _work.PersistAsync();
                return  RedirectToAction("Details", "MovieAdmin", new { id = movieLinks.MovieId });
            }
            ViewData["MovieId"] = movieLinks.MovieId;
            return View(movieLinks);
        }

        // GET: MovieLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieLinks = await _work.Links.SingleOrDefaultAsync(m => m.Id == id);
            if (movieLinks == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = movieLinks.MovieId;
            return View(movieLinks);
        }

        // POST: MovieLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Source,MovieId")] MovieLinks movieLinks)
        {
            if (id != movieLinks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existing = _work.Links.Get(id);
                    existing = movieLinks;
                    await _work.PersistAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieLinksExists(movieLinks.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return  RedirectToAction("Details", "MovieAdmin", new { id = movieLinks.MovieId });
            }
            ViewData["MovieId"] =  movieLinks.MovieId;
            return View(movieLinks);
        }

        // GET: MovieLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieLinks = await _work.Links.GetAsync((int)id);
            if (movieLinks == null)
            {
                return NotFound();
            }

            return View(movieLinks);
        }

        // POST: MovieLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieLinks = await _work.Links.SingleOrDefaultAsync(m => m.Id == id);
            _work.Links.Remove(movieLinks);
            await _work.PersistAsync();
            return RedirectToAction("Details","MovieAdmin",new {id=movieLinks.MovieId });
        }

        private bool MovieLinksExists(int id)
        {
            return _work.Links.Any(e => e.Id == id);
        }
    }
}
