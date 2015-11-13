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
            if (!_context.Users.Any(x => x.Mail.Equals(User.Identity.Name)))
            {
                var user = (ClaimsIdentity)User.Identity;
                var userAccount = new UserAccount() { Mail = user.Name, Name = user.Claims.First(x => x.Type == "name").Value.Replace("(Takenet)", "") };
                _context.Users.Add(userAccount);
                _context.SaveChanges();

                var merit = _context.Merits.First(x => x.Name.Contains("Primeiro acesso"));

                _context.UserMerits.Add(new UserMerit
                {
                    MeritId = merit.Id,
                    Value = merit.Value,
                    TargetUserId = userAccount.Id
                });
                _context.SaveChanges();
            }

            var query = _context.UserMerits.Where(x => x.TargetUser.Mail == User.Identity.Name);
            ViewBag.Score = query.Sum(x => (decimal?)x.Value) ?? 0;
            ViewBag.MeritsCount = query.Count();
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