using System.ComponentModel.DataAnnotations;

namespace SmartHealth.Core.Domain.Dtos
{
    public class UserDto
    {
        public virtual int Id { get; set; }

        [Display(Name = "User name")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me(up to 30 days)")]
        public bool RememberMe { get; set; }
    }
}
