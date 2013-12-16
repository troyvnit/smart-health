using System;
using System.ComponentModel.DataAnnotations;
using SmartHealth.Core.Domain.Models;

namespace SmartHealth.Core.Domain.Dtos
{
    public class UserDto
    {
        public virtual int Id { get; set; }

        [Display(Name = "User name")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "Remember me(up to 30 days)")]
        public bool RememberMe { get; set; }

        public virtual string DisplayName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Location { get; set; }
        public virtual string Phone { get; set; }
        public virtual DateTime? DOB { get; set; }
        public virtual Gender? Gender { get; set; }
    }
}
