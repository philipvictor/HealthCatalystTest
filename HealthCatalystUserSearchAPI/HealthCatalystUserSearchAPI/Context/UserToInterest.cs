using System;

namespace HealthCatalystUserSearchAPI.Context
{
    public class UserToInterest
    {
        public Guid UserId { get; set; }
        public Users User { get; set; }

        public Guid InterestId { get; set; }
        public Interests Interest { get; set; }
    }
}
