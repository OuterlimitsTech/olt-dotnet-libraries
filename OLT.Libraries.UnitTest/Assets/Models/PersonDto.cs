using OLT.Core;
using System;

namespace OLT.Libraries.UnitTest.Assets.Models
{
    // ReSharper disable once InconsistentNaming
    public class PersonDto : OltPersonName
    {
        public Guid? UniqueId { get; set; }
        public int? PersonId { get; set; }
        public string Email { get; set; }
    }    
}