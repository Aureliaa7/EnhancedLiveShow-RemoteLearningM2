using System.ComponentModel.DataAnnotations;

namespace LiveShow.Services.Models.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Please enter the username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}




