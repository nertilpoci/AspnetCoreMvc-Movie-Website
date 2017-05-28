using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL;
using Movies.Data;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Components
{
    [ViewComponent(Name = "MovieYouMightLike")]
    public class MovieYouMightLikeComponent : ViewComponent
    {
        private readonly IUnitOfWork _work;
        public MovieYouMightLikeComponent(IUnitOfWork work)
        {
            _work = work;
        }
        public async Task<IViewComponentResult> InvokeAsync(int genreId)
        {


            ViewBag.Genre = await _work.Genres.GetAsync(genreId);
          

            return View(await _work.Movies.GetMoviesYouMightLike(genreId,15));
        }
    }
}
