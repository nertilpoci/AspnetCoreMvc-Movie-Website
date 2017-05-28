using Microsoft.AspNetCore.Mvc;
using Movies.DAL;
using Movies.Data;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Components
{
    [ViewComponent(Name = "FeaturedMovies")]

    public class FeaturedMovies : ViewComponent
    {
        private IUnitOfWork _work;
        public FeaturedMovies(IUnitOfWork work)
        {
            _work = work;
        }
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<Movie> movies)
        {


           
            ViewBag.OwlId = Guid.NewGuid().ToString();

            return View(await _work.Movies.GetFeaturedMoviesAsync(20));
        }
    }
}
