using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Take.Gamification.Models
{
    public class UserMerit
    {
        public int Id { get; set; }
        public virtual UserAccount OwnerUser { get; set; }
        public int? OwnerUserId { get; set; }
        public virtual UserAccount TargetUser { get; set; }
        public int TargetUserId { get; set; }
        public int MeritId { get; set; }
        public virtual Merit Merit { get; set; }
        public int Value { get; set; }
    }
}