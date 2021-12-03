using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class ExceptionExtensionTests
    {

        [Fact]
        public void GetInnerExceptions()
        {
            var innerException = new Exception(Faker.Lorem.Sentence());
            var exception = new Exception(Faker.Lorem.Sentence(), innerException);

            Assert.Collection(exception.GetInnerExceptions(),
                item => Assert.Equal(item.Message, exception.Message),
                item => Assert.Equal(item.Message, innerException.Message));            
        }
    }
}
