using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Components
{
    [ViewComponent(Name = "Rating")]
    public class RatingComponent:ViewComponent
    {
        public RatingComponent()
        {
           
        }
        private decimal _movieRating;
        public async Task<IViewComponentResult> InvokeAsync(decimal rating)
        {
            

            ViewBag.FullStars = (int)rating;
            ViewBag.HasHalfStar = rating % 1!=0;
            ViewBag.MaximumStars = 5;

            return View();
        }
    }
}
