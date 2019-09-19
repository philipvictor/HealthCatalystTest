using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthCatalystUserSearchAPI.Context
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public ICollection<UserToInterest> MyInterests { get; set; }

        [Required]
        public Addresses MyAddress { get; set; }
    }
}
