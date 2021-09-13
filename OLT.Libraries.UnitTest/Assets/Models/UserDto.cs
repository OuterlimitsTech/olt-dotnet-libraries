using System;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Models
{
    public class UserDto : OltPersonName
    {
        public int? UserId { get; set; }
        public Guid UserGuid { get; set; }
    }
}