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
    [ViewComponent(Name = "LatestMovies")]

    public class LatestMovies : ViewComponent
    {
        private IUnitOfWork _work;
        public LatestMovies(IUnitOfWork work)
        {
            _work = work;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {


            return View(await _work.Movies.GetLatestMoviesForGendersAsync(8) );
        }
    }
}
