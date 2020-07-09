﻿using System;
using System.ComponentModel.DataAnnotations;

namespace LiveShow.Services.Models.User
{
    public class UserEditDto
    {
        public Guid Id { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
    }
}
