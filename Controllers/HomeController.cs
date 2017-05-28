using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Microsoft.EntityFrameworkCore;
using Movies.Impl;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Sakura.AspNetCore;
using Movies.DAL;
using Movies.Helpers;
using Movies.Models;

namespace Movies.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUnitOfWork _work;
        private readonly IHostingEnvironment environment;
        public  HomeController(IUnitOfWork work, IHostingEnvironment env) : base(work, env)
        {
            _work = work;
            environment = env;
        }
        

      
        public async Task<IActionResult> Index()
        {

            ViewBag.ShowMainCorousel = true;
            return View();
        }
        public IActionResult Details(int id)
        {
            var movie = _work.Movies.Get(id);
          

            if (movie == null) return BadRequest();
          
            return View(movie);
        }
       
        public async Task<IActionResult> AZ(string character="A",int page= 1)
        {
           
            var pageSize = 10;

            ViewBag.Letters = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray();
            
            ViewBag.Character = character;
            var data = await _work.Movies.FindAsync(m => m.Name.StartsWith(character, StringComparison.CurrentCultureIgnoreCase));

           
            return View(data.ToPagedList(pageSize, page));
        }
        public async Task<IActionResult> Genre(int genreId , int page = 1)
        {

            var pageSize = 10;
            var genre = await _work.Genres.GetAsync(genreId);
            if (null == genre) return BadRequest();
            ViewBag.Genre = genre;
            return View((await _work.Movies.FindAsync(m=>m.GenreId==genreId)).ToPagedList(pageSize, page));
        }
        public async Task<IActionResult> Search(string name= "", int page = 1,int? genreId=null, int? year=null)
        {

            var pageSize = 10;

            ViewData["name"] = name;
            ViewData["genreId"] = genreId;
            ViewData["year"] = year;
            var filterpredicate = PredicateBuilder.False<Movie>();

           
            if (!string.IsNullOrEmpty(name)) filterpredicate = filterpredicate.And(p => p.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase));
            if (genreId!=null)  filterpredicate = filterpredicate.And(p => p.GenreId==genreId);
            if (year!=null) filterpredicate = filterpredicate.And(p => p.ReleaseDate.Year==year);

           

            return View((await _work.Movies.FindAsync(filterpredicate)).ToPagedList(pageSize, page));
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
