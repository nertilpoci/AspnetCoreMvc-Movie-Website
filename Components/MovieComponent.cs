using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Components
{
    [ViewComponent(Name = "Movie")]
    public class MovieComponent:ViewComponent
    {
        public MovieComponent()
        {
           
        }
        public async Task<IViewComponentResult> InvokeAsync(Movie movie,string posterFolder="~/",string ribben="", string customCssClasses="")
        {


            ViewBag.Ribben = ribben;
            ViewBag.CustomContainerClasses = customCssClasses;
            ViewBag.PosterFolder = posterFolder;
            return View(movie);
        }
    }
}
