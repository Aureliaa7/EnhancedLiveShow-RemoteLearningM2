using System;
using System.ComponentModel.DataAnnotations;

namespace LiveShow.Services.Models.User
{
    public class UserChangePasswordDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        [Display(Name = "Current password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
