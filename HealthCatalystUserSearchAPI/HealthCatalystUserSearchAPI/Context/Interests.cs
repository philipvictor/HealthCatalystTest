using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthCatalystUserSearchAPI.Context
{
    public class Interests
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string InterestName { get; set; }

        public string InterestType { get; set; }

        public ICollection<UserToInterest> Users { get; set; }
    }
}
