using System;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Models
{
    public class UserDto : OltPersonName
    {
        public int? UserId { get; set; }
        public Guid UserGuid { get; set; }
    }

    public class UserNoAdapterDto : OltPersonName
    {
        public int? UserId { get; set; }
        public Guid UserGuid { get; set; }
    }
}