using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Take.Gamification.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public virtual ICollection<UserMerit> Owners { get; set; }
        public virtual ICollection<UserMerit> Targets { get; set; }

    }
}