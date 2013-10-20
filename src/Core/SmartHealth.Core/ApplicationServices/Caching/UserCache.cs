using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Core.Domain.Models;

namespace SmartHealth.Core.ApplicationServices.Caching
{
    [Serializable]
    public class UserCache
    {
        public UserCache(User user)
        {
            Id = user.Id;
            Username = user.Username;
            DisplayName = user.DisplayName;
            FullName = user.FullName;
            Email = user.Email;
            UserType = user.UserType;
            FirstName = user.FirstName;
            LastName = user.LastName;
            HasAvatar = user.HasAvatar;
            Location = user.Location;
            LangId = (user.Language != null ? user.Language.Id : 1);
            LangCultureInfo = (user.Language != null ? user.Language.CultureInfo : "en-us");
            Longtitude = user.Longtitude;
            Lattitude = user.Lattitude;
            AboutMe = user.AboutMe;
            CreatedTime = user.CreatedTime;
            Gender = user.Gender.HasValue ? user.Gender.Value.ToString() : string.Empty;            

            MyPhysicianId = user.MyPhysicianId;         
        }

        public static UserCache Create(User user)
        {
            if (user != null)
                return new UserCache(user);

            return null;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string Location { get; set; }
        public string LangCultureInfo { get; set; }
        public int LangId { get; set; }

        

        
       

        public int MyPhysicianId { get; set; }
        public float Longtitude { get; set; }
        public float Lattitude { get; set; }

        public string Gender { get; set; }
        public bool HasAvatar { get; set; }

        public string AboutMe { get; set; }
        public string StateAbbreviation { get; set; }
        public DateTime CreatedTime { get; set; }        
        public bool IsSelfRegister { get; set; }
    }
}
