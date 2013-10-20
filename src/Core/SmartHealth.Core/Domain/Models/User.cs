using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Infrastructure.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartHealth.Core.Domain.Models
{
    [Serializable]
    public class User : Entity
    {
        public User()
        {
            CreatedTime = DateTime.UtcNow;
            UserType = UserType.Admin;
            HasAvatar = false;
        }


        [StringLength(101)]
        public virtual string Username { get; set; }

        [StringLength(50)]
        [Required]
        public virtual string Password { get; set; }

        [StringLength(101)]
        public virtual string DisplayName { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public virtual string LastName { get; set; }

        [StringLength(100)]
        [Required]
        public virtual string Email { get; set; }


        public virtual DateTime CreatedTime { get; set; }

        public virtual DateTime? ModifiedTime { get; set; }

        public virtual DateTime? LastLoginTime { get; set; }


        public virtual UserType UserType { get; set; }

        public virtual string FullName
        {
            get { return String.Concat(FirstName, " ", LastName); }
        }
        public virtual DateTime? DOB { get; set; }

        public virtual Gender? Gender { get; set; }

        public virtual UserStatus Status { get; set; }

        public virtual string Location { get; set; }
        public virtual float Longtitude { get; set; }
        public virtual float Lattitude { get; set; }


        [StringLength(4001)]
        public virtual string AboutMe { get; set; }


        public virtual bool HasAvatar { get; set; }

        
        public virtual int MyPhysicianId { get; set; }
        
        public virtual string DisplayGenderOwner
        {
            get
            {
                if (this.Gender.HasValue == false)
                    return "his/her";

                return this.Gender.Value == SmartHealth.Core.Domain.Models.Gender.Female ? "her" : "his";
            }
        }


        public virtual string SuffixKey
        {
            get
            {
                if (this.Gender.HasValue == false)
                    return "HisHer";

                return this.Gender.Value == SmartHealth.Core.Domain.Models.Gender.Female ? "Her" : "His";
            }
        }
        public virtual Language Language { get; set; }

    }

    public enum Gender
    {
        Female,
        Male
    }

    public enum Civility
    {
        [Description("Mr")]
        Mr,
        [Description("Mrs")]
        Mrs,
        [Description("Ms")]
        Ms
    }

    public enum UserStatus
    {
        Active,
        Inactived,
        IsDeleted,
        Blocked
    }

    public enum UserType
    {
        Physician,
        Patient,
        Admin,
        Secretary
    }

}
