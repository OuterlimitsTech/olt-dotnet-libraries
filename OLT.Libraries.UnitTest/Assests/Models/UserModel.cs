using System;

namespace OLT.Libraries.UnitTest.Assests.Models
{
    // ReSharper disable once InconsistentNaming
    public class UserModel
    {
        public int? UserId { get; set; }
        public Guid UserGuid { get; set; }
        public NameAutoMapperModel Name { get; set; } = new NameAutoMapperModel();
    }
}