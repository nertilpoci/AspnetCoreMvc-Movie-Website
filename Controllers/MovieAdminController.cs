using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.DAL;
using Movies.Data;
using Movies.Helpers;
using Movies.Impl;
using Movies.Models;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Controllers
{[Authorize]
    public class MovieAdminController : BaseController
    {
        private IUnitOfWork _work;
        private IHostingEnvironment _environment;
        private string uploadsFolder;

        public MovieAdminController(IUnitOfWork work, IHostingEnvironment environment, IHostingEnvironment env) : base(work, env)
        {
            _work = work;
            _environment = environment;
             uploadsFolder = Path.Combine(_environment.WebRootPath, "MoviePosters");
        }
       

        // GET: Movies
        public async Task<ActionResult> Index(string name = "", int page = 1)
        {

            var pageSize = 10;

            ViewData["name"] = name;
           
            var filterpredicate = PredicateBuilder.True<Movie>();


            if (!string.IsNullOrEmpty(name)) filterpredicate = filterpredicate.And(p => p.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase));
            



            return View((await _work.Movies.FindAsync(filterpredicate)).ToPagedList(pageSize, page));
           
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Movie movie = _work.Movies.Get((int)id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            SetGenreList();
            return View();
        }
        private void SetGenreList()
        {
            var genres = _work.Genres.GetAll().ToList();
            genres.Insert(0, new Genre { Name = "Select Genre" });
            ViewBag.MovieGenres = genres;
        }
        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormFile portrait, IFormFile landscape, [Bind("Name,ReleaseDate,Description,YoutubeVideoId,Rating,GenreId")] Movie movie)
        {

            if (null == portrait) ModelState.AddModelError("", "Please select a file for Portrait poster");
            if (null == landscape) ModelState.AddModelError("", "Please select a file for Landscape poster");
            if (movie.GenreId == 0) ModelState.AddModelError("", "Please select a Genre");
            if (ModelState.IsValid)
            {
                var portraitFile = $"{Guid.NewGuid().ToString()}.{ portrait.FileName.Split('.').Last()}";
                var landscapeFile = $"{Guid.NewGuid().ToString()}.{ landscape.FileName.Split('.').Last()}";

                using (var fileStream = new FileStream(Path.Combine(uploadsFolder, portraitFile), FileMode.Create))
                {
                    await portrait.CopyToAsync(fileStream);
                }
                using (var fileStream = new FileStream(Path.Combine(uploadsFolder, landscapeFile), FileMode.Create))
                {
                    await landscape.CopyToAsync(fileStream);
                }
                movie.PosterPortrait = portraitFile;
                movie.PosterLandscape = landscapeFile;
                _work.Movies.Add(movie);
                _work.Persist();
                return RedirectToAction("Index");
            }
            SetGenreList();
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {



            if (id == null)
            {
                return BadRequest();
            }
            Movie movie = _work.Movies.Get((int)id);
            if (movie == null)
            {
                return NotFound();
            }
            SetGenreList();
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormFile portrait, IFormFile landscape, [Bind("Id,Name,ReleaseDate,YoutubeVideoId,Rating,GenreId")]  Movie movie)
        {


            if (ModelState.IsValid)
            {
                if (portrait != null)
                {
                    var portraitFile = $"{Guid.NewGuid().ToString()}.{ portrait.FileName.Split('.').Last()}";

                    using (var fileStream = new FileStream(Path.Combine(uploadsFolder, portraitFile), FileMode.Create))
                    {
                        await portrait.CopyToAsync(fileStream);
                    }
                    System.IO.File.Delete(Path.Combine(uploadsFolder, movie.PosterPortrait));

                    movie.PosterPortrait = portraitFile;
                }
                if (landscape != null)
                {
                    var landscapeFile = $"{Guid.NewGuid().ToString()}.{ landscape.FileName.Split('.').Last()}";


                    using (var fileStream = new FileStream(Path.Combine(uploadsFolder, landscapeFile), FileMode.Create))
                    {
                        await landscape.CopyToAsync(fileStream);
                    }
                    var mv = _work.Movies.Get(id);
                    System.IO.File.Delete(Path.Combine(uploadsFolder, mv.PosterLandscape));
                    movie.PosterLandscape = landscapeFile;
                }





                var old = _work.Movies.Get(id);
                old = movie;
                _work.Persist();
                return RedirectToAction("Index");
            }
            SetGenreList();
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Movie movie = _work.Movies.Get((int)id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = _work.Movies.Get(id);
            _work.Movies.Remove(movie);
            System.IO.File.Delete(Path.Combine(uploadsFolder, movie.PosterLandscape));
            System.IO.File.Delete(Path.Combine(uploadsFolder, movie.PosterPortrait));
            _work.Persist();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _work.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
