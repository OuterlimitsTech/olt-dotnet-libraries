using System;

namespace OLT.Libraries.UnitTest.Assests.Models
{
    public class OltUserTestModel
    {
        public int? UserId { get; set; }
        public Guid UserGuid { get; set; }
        public OltNameTestModel Name { get; set; } = new OltNameTestModel();
    }
}