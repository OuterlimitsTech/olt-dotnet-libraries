using System.ComponentModel.DataAnnotations;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models
{
    public class AddressEntity : OltEntityIdDeletable
    {
        [StringLength(50)]
        public string Street { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(20)]
        public string State { get; set; }



        public static AddressEntity FakerEntity()
        {
            return new AddressEntity
            {
                Street = Faker.Address.StreetAddress(),
                City = Faker.Address.City(),
                State = Faker.Address.UsState(),
            };
        }
    }
}