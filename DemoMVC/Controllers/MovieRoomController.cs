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
        public ActionResult MovieRoom_R_List()
        {
            var Rs = DC.MovieRoom_Row.OrderBy(q => q.SortNo);
            return View(Rs);
        }
        public ActionResult MovieRoom_R_Remove(long ID)
        {
            var R = DC.MovieRoom_Row.FirstOrDefault(q => q.MRRID == ID);
            if(R!=null)
            {
                var Cs = DC.MovieRoom_Cell.Where(q => q.MRRID == R.MRRID);
                var Ss = from q in Cs
                         join p in DC.NovieRoom_Sell
                         on q.MRCID equals p.MRCID
                         select p;

                DC.NovieRoom_Sell.DeleteAllOnSubmit(Ss);
                DC.SubmitChanges();
                DC.MovieRoom_Cell.DeleteAllOnSubmit(Cs);
                DC.SubmitChanges();
                DC.MovieRoom_Row.DeleteOnSubmit(R);
                DC.SubmitChanges();
            }
            MovieRoom_R_List();
            return View("MovieRoom_R_List");
        }

        public ActionResult MovieRoom_R_Edit(long ID)
        {
            var R = DC.MovieRoom_Row.FirstOrDefault(q => q.MRRID == ID);
            if(R!= null)
            {
                R = new MovieRoom_Row();
            }
            return View(R);
        }
        [HttpPost]
        public ActionResult MovieRoom_R_Edit(long MRRID,string RowName,int SortNo)
        {
            var R = DC.MovieRoom_Row.FirstOrDefault(q => q.MRRID == MRRID);
            if (R != null)
            
                R = new MovieRoom_Row();

            R.RowName = RowName;
            R.SortNo = SortNo;
            R.UpdDate = DateTime.Now;
            if(MRRID == 0)
            {
                R.CreDate = R.UpdDate;
                DC.MovieRoom_Row.InsertOnSubmit(R);
            }
            DC.SubmitChanges();

            return View("MovieRoom_R_List");
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