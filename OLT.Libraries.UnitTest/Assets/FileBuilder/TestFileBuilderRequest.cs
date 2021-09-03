using System.Collections.Generic;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.FileBuilder
{
    public class TestFileBuilderRequest : IOltRequest
    {
        public List<PersonDto> Data { get; set; } = new List<PersonDto>();
    }
}