using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using Movies.Config;
using Movies.DAL;
using Movies.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Impl
{
    public abstract class BaseController:Controller
    {

        private IHostingEnvironment environment;
        private IUnitOfWork _work;
        public  BaseController(IUnitOfWork work, IHostingEnvironment env)
        {
            _work = work;
            environment = env;
           
        }
        public override ViewResult View(string view, object model)
        {
            ViewBag.Genres = _work.Genres.GetAll();
            ViewBag.PosterFolder = "~/MoviePosters/";
            return base.View(view, model);
        }
       //public override void OnActionExecuting(ActionExecutingContext filterContext)
       // {
       //     ViewBag.Genres = db.Genres.ToList();
       //     base.OnActionExecuting(filterContext);
       // }
        
    }
}
