using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Take.Gamification.Models;

namespace Take.Gamification.Controllers
{
    public class MeritsController : Controller
    {
        GameContext _context = new GameContext();

        // GET: Merits
        public ActionResult Index()
        {
            var merits = _context.UserMerits.Where(x => x.TargetUser.Mail == User.Identity.Name).OrderByDescending(x => x.Created);
            return View(merits);
        }

        public ActionResult LastMerits()
        {
            var merits = _context.UserMerits.OrderByDescending(x => x.Created).Take(5);
            return PartialView("_LastMerits", merits);
        }

        public ActionResult MyLastMerits()
        {
            var merits = _context.UserMerits.Where(x => x.TargetUser.Mail == User.Identity.Name).OrderByDescending(x => x.Created).Take(5);
            return PartialView("_LastMerits", merits);
        }
    }
}