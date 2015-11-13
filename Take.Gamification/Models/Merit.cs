using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Take.Gamification.Models
{
    public class Merit
    {
        public int Id { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Valor")]
        public int Value { get; set; }
        public bool IsVisible { get; set; }
        public virtual ICollection<UserMerit> UserMerits { get; set; }
    }
}