using System.ComponentModel.DataAnnotations;

namespace LiveShow.Services.Models.User
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public UserTypeEnum Type { get; set; }
        public string Salt {get; set; }
    }
}
