using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthCatalystUserSearchAPI.Context
{
    public class Addresses
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Street1 { get; set; }
        public string Street2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string ZipCode { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
