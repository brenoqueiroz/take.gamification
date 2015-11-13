using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Take.Gamification.Models
{
    public class Medal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public MedalType Type { get; set; }
        public virtual ICollection<UserMedal> UserMedals { get; set; }
    }

    public enum MedalType
    {
        Bronze,
        Silver,
        Gold
    }
}