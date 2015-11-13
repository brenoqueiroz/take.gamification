﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Take.Gamification.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Email")]
        public string Mail { get; set; }
        public virtual ICollection<UserMerit> Owners { get; set; }
        public virtual ICollection<UserMerit> Targets { get; set; }

        public virtual ICollection<UserMedal> Medals { get; set; }
    }
}