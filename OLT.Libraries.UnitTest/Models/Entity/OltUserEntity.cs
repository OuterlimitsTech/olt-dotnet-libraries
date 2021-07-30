using System;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Models.Entity
{
    public class OltUserEntity : OltEntityId, IOltEntityUniqueId
    {
        public Guid UniqueId { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameSuffix { get; set; }
        
    }
}