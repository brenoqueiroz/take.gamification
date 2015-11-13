using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
            }

            var loggedUser = _context.Users.First(x => x.Mail == User.Identity.Name);

            PrimeiroAcesso(loggedUser);

            PrimeiroAcessoDia(loggedUser);

            CadastroFoto(loggedUser);

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

        private void PrimeiroAcesso(UserAccount loggedUser)
        {
            var primeiroAcesso = _context.UserMerits.FirstOrDefault(x => x.TargetUserId == loggedUser.Id && x.Merit.Name.Equals(MeritsConst.PrimeiroAcesso));
            if (primeiroAcesso == null)
            {
                var merit = _context.Merits.First(x => x.Name.Equals(MeritsConst.PrimeiroAcesso));

                _context.UserMerits.Add(new UserMerit
                {
                    MeritId = merit.Id,
                    Value = merit.Value,
                    TargetUserId = loggedUser.Id
                });
                _context.SaveChanges();
            }
        }

        private void CadastroFoto(UserAccount loggedUser)
        {
            var cadastroFoto = _context.UserMerits.FirstOrDefault(x => x.TargetUserId == loggedUser.Id && x.Merit.Name.Equals(MeritsConst.CadastrarFoto));
            if (cadastroFoto == null)
            {
                var hash = HashEmailForGravatar(User.Identity.Name);
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = client.GetByteArrayAsync("https://secure.gravatar.com/avatar/" + hash + "?d=404").Result;
                        var merit = _context.Merits.First(x => x.Name.Equals(MeritsConst.CadastrarFoto));

                        _context.UserMerits.Add(new UserMerit
                        {
                            MeritId = merit.Id,
                            Value = merit.Value,
                            TargetUserId = loggedUser.Id
                        });
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        private void PrimeiroAcessoDia(UserAccount loggedUser)
        {
            var primeiroAcessoDia = _context.UserMerits.FirstOrDefault(x => x.TargetUserId == loggedUser.Id
                            && x.Merit.Name.Equals(MeritsConst.PrimeiroAcessoDia)
                            && x.Created.Day == DateTime.Now.Day
                            && x.Created.Month == DateTime.Now.Month
                            && x.Created.Year == DateTime.Now.Year);

            if (primeiroAcessoDia == null)
            {
                var merit = _context.Merits.First(x => x.Name.Equals(MeritsConst.PrimeiroAcessoDia));

                _context.UserMerits.Add(new UserMerit
                {
                    MeritId = merit.Id,
                    Value = merit.Value,
                    TargetUserId = loggedUser.Id
                });
                _context.SaveChanges();
            }
        }

        private string HashEmailForGravatar(string email)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();  // Return the hexadecimal string. 
        }
    }
}