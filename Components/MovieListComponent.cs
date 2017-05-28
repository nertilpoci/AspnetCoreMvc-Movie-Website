using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Components
{
    [ViewComponent(Name = "MovieList")]

    public class MovieListComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<Movie> movies)
        {

            return View(movies);
        }
    }
}
