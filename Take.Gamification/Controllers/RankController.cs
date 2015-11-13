using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Take.Gamification.Models;

namespace Take.Gamification.Controllers
{
    public class RankController : Controller
    {
        GameContext _context = new GameContext();

        // GET: Rank
        public ActionResult Index()
        {
            var merits = _context.UserMerits
                            .GroupBy(u => u.TargetUserId)
                            .Select(g => new GroupedUserMerits()
                             {
                                 TargetName = g.FirstOrDefault().TargetUser.Name,
                                 Score = g.Sum(u => u.Value)
                             })
                             .OrderByDescending(g => g.Score)
                             .ToList();
            return View(merits);
        }
    }

    public class GroupedUserMerits
    {
        public string TargetName { get; set; }
        public int Score { get; set; }
    }
}