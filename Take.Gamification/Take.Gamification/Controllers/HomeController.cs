using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Take.Gamification.Models;

namespace Take.Gamification.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        GameContext _context = new GameContext();

        public ActionResult Index()
        {
            if (!_context.UserSet.Any(x => x.Mail.Equals(User.Identity.Name)))
            {
                ClaimsIdentity user = (ClaimsIdentity)User.Identity;
                _context.UserSet.Add(new UserAccount() { Mail = user.Name, Name = user.Claims.First(x => x.Type == "name").Value });
                _context.SaveChanges();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}