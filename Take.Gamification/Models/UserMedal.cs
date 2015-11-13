using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Take.Gamification.Models
{
    public class UserMedal
    {
        public UserMedal()
        {
            Created = DateTimeOffset.Now;
        }

        public int Id { get; set; }
        public virtual UserAccount User { get; set; }
        public int UserId { get; set; }
        public virtual Medal Medal { get; set; }
        public int MedalId { get; set; }
        [DisplayName("Data de criação")]
        public DateTimeOffset Created { get; set; }
    }
}