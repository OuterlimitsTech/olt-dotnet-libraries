using System;

namespace OLT.Libraries.UnitTest.Assets.Models
{
    public class UserTempDto
    {
        public int? UserId { get; set; }
        public Guid UserGuid { get; set; }
        public NameAutoMapperModel Name { get; set; } = new NameAutoMapperModel();
    }
}