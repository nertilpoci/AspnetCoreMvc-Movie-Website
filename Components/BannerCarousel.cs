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
    [ViewComponent(Name = "BannerCarousel")]

    public class BannerCarousel : ViewComponent
    {
        private IUnitOfWork _work;
        public BannerCarousel(IUnitOfWork work)
        {
            _work = work;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {



            ViewBag.OwlId = Guid.NewGuid().ToString();

            return View(await _work.Movies.GetAllAsync(20));
        }

    }
}
