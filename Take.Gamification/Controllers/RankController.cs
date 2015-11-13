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
            var merits = (from u in _context.Users
                         join um in _context.UserMerits on u.Id equals um.TargetUserId into query
                         from q in query.DefaultIfEmpty()
                         group q by u.Name into grouped
                         select new GroupedUserMerits { TargetName = grouped.Key, Score = grouped.Sum(x => (decimal?)x.Value ?? 0) }
                         ).ToList().OrderByDescending(m => m.Score);
                                                     

            return View(merits);
        }
    }

    public class GroupedUserMerits
    {
        public string TargetName { get; set; }
        public decimal Score { get; set; }
    }
}