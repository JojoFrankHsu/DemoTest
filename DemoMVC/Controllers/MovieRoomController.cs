using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoMVC.Controllers
{
    public class MovieRoomController : Controller
    {
        private DataClassesDataContext DC = new DataClassesDataContext();
        // GET: MovieRoom
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MovieRoom_List()
        {
            ViewBag.Title = "Hi";
            var Rs = DC.MovieRoom_Row.OrderBy(q => q.SortNo);
            return View(Rs);
        }


    }
}