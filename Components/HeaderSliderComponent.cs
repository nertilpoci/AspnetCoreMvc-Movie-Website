using Microsoft.AspNetCore.Mvc;
using Movies.DAL;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Components
{
    [ViewComponent(Name = "HeaderSlider")]

    public class HeaderSlider : ViewComponent
    {
        private readonly IUnitOfWork _work;
        public HeaderSlider(IUnitOfWork work)
        {
            _work = work;
        }
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<Movie> movies)
        {

            return View( await _work.Movies.GetHeaderSliderMovies(15));
        }
    }
}
