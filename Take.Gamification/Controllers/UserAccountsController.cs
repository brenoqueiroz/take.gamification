using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Take.Gamification.Models;

namespace Take.Gamification.Controllers
{
    public class UserAccountsController : Controller
    {
        GameContext _context = new GameContext();

        // GET: UserAccounts
        public ActionResult Index()
        {
            var users = _context.Users.Where(x => x.Mail != User.Identity.Name).OrderBy(x => x.Name);
            var merits = _context.Merits.Where(x => x.IsVisible).OrderBy(x => x.Name);

            ViewBag.Merits = merits;
            return View(users);
        }

        public ActionResult Details(int id)
        {
            var user = _context.Users.First(x => x.Id == id);
            return View(user);
        }

        public ContentResult DoMerit(int meritId, int userId)
        {
            try
            {
                var owner = _context.Users.First(x => x.Mail == User.Identity.Name);
                var merit = _context.Merits.First(x => x.Id == meritId);
                var user = _context.Users.First(x => x.Id == userId);

                var userMerit = new UserMerit
                {
                    MeritId = merit.Id,
                    OwnerUserId = owner.Id,
                    TargetUserId = user.Id,
                    Value = merit.Value
                };

                _context.UserMerits.Add(userMerit);
                _context.SaveChanges();

                GiveMedal(user);

                var concederMerit = _context.Merits.First(x => x.Name.Equals(MeritsConst.ConcederMerito));
                var concederMeritUser = new UserMerit
                {
                    MeritId = concederMerit.Id,
                    Value = concederMerit.Value,
                    TargetUserId = _context.Users.First(x => x.Mail == User.Identity.Name).Id
                };

                _context.UserMerits.Add(concederMeritUser);
                _context.SaveChanges();

                GiveMedal(owner);

                SendEmail.SendMerito(user.Mail, new Dictionary<string, string>
                {
                    ["{Name}"] = user.Name,
                    ["{Owner}"] = owner.Name,
                    ["{Merit}"] = merit.Name,
                    ["{Value}"] = merit.Value.ToString(),
                    ["{url}"] = $"{Request.Url.Host}/Merits"
                });

                Thread.Sleep(TimeSpan.FromSeconds(2));
                return Content("Mérito adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        private void GiveMedal(UserAccount user)
        {
            var query = _context.UserMerits.Where(x => x.TargetUserId == user.Id);
            var targetUserScore = query.Sum(x => (decimal?)x.Value) ?? 0;

            var medals = _context.Medals.Where(x => x.Value <= targetUserScore).ToList();

            foreach (var medal in medals)
            {
                if (!_context.UserMedals.Any(x => x.UserId == user.Id && x.MedalId == medal.Id))
                {
                    var userMedal = new UserMedal
                    {
                        MedalId = medal.Id,
                        UserId = user.Id
                    };

                    _context.UserMedals.Add(userMedal);
                    _context.SaveChanges();
                }
            }
        }
    }
}