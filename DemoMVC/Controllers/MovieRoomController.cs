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
            var Rs = DC.MovieRoom_Row.OrderBy(q => q.SortNo);
            return View(Rs);
        }
        //[HttpPost]
        public ActionResult MovieRoom_SellAdd(long ID)
        {
            ViewBag.Msg = "";
            var S = DC.NovieRoom_Sell.FirstOrDefault(q => q.MRCID == ID);
            if (S != null)
                ViewBag.Msg = "此位置已有人購買";
            else
            {
                NovieRoom_Sell N = new NovieRoom_Sell();
                N.MRCID = ID;
                N.CreDate = DateTime.Now;
                DC.NovieRoom_Sell.InsertOnSubmit(N);
                DC.SubmitChanges();
                ViewBag.Msg = "購買成功";
            }
                
            MovieRoom_List();
            return View("MovieRoom_List");
        }

        public ActionResult MovieRoom_SellRemove(long ID)
        {
            ViewBag.Msg = "";
            var S = DC.NovieRoom_Sell.FirstOrDefault(q => q.MRCID == ID);
            if (S != null)
            {
                DC.NovieRoom_Sell.DeleteOnSubmit(S);
                DC.SubmitChanges();
                ViewBag.Msg = "此位置已釋出";
            }
                

            MovieRoom_List();
            return View("MovieRoom_List");
        }
    }
}