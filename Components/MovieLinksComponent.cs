using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Components
{
    [ViewComponent(Name = "MovieLinks")]
    public class MovieLinksComponent: ViewComponent
    {
        ApplicationDbContext db;
        public MovieLinksComponent(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(int movieId,bool isAdmin=false)
        {

            ViewBag.IsAdmin = isAdmin;
            return View(db.MovieLinks.Where(m=>m.MovieId==movieId));
        }
    }
}
