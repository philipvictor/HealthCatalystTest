using System;

namespace HealthCatalystUserSearchAPI.Models
{
    public class UserToInterestDto
    {
        public Guid UserId { get; set; }
        public UserDto User { get; set; }

        public Guid InterestId { get; set; }
        public InterestDto Interest { get; set; }
    }
}
