using System.Collections.Generic;

namespace HealthCatalystUserSearchAPI.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<InterestDto> MyInterests { get; set; }

        public AddressDto MyAddress { get; set; }
    }
}
