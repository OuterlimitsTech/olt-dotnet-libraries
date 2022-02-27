////using System;
////using System.Collections.Generic;
////using System.Linq;
////using FluentAssertions;
////using OLT.Core;
////using OLT.Libraries.UnitTest.Abstract;
////using OLT.Libraries.UnitTest.Assets.Entity;
////using OLT.Libraries.UnitTest.Assets.Entity.Models;
////using OLT.Libraries.UnitTest.Assets.LocalServices;
////using OLT.Libraries.UnitTest.Assets.Models;
////using OLT.Libraries.UnitTest.Assets.Rules;
////using Xunit;
////using Xunit.Abstractions;

////namespace OLT.Libraries.UnitTest.OLT.Shared
////{
////    public class ExpressionTests : BaseTest
////    {
////        private readonly IPersonService _service;
////        private readonly SqlDatabaseContext _context;

////        public ExpressionTests(
////            SqlDatabaseContext context,
////            IPersonService service,
////            ITestOutputHelper output) : base(output)
////        {
////            _context = context;
////            _service = service;
////        }


////        //[Fact]
////        //public void Contains()
////        //{
////        //    var list = new List<int>();
////        //    for (var idx = 0; idx < 5; idx++)
////        //    {
////        //        var dto = UnitTestHelper.AddPerson(_service, UnitTestHelper.CreateTestAutoMapperModel());
////        //        list.Add(dto.PersonId.Value);
////        //    }

////        //    var expression = new OltEntityExpressionContains<PersonEntity>(person => person.Id)
////        //    {
////        //        Value = list
////        //    };
////        //    var results = expression
////        //        .BuildQueryable(_context.InitializeQueryable<PersonEntity>())
////        //        .Select(s => s.Id)
////        //        .ToList();

////        //    results.Should().Equal(list);
////        //}

////        //[Fact]
////        //public void Int()
////        //{
            
////        //    var dto = UnitTestHelper.AddPerson(_service, UnitTestHelper.CreateTestAutoMapperModel());

////        //    var expression = new OltEntityExpressionInt<PersonEntity>(person => person.Id)
////        //    {
////        //        Value = dto.PersonId.Value
////        //    };
////        //    var results = expression
////        //        .BuildQueryable(_context.InitializeQueryable<PersonEntity>())
////        //        .Select(s => s.Id)
////        //        .ToList();

////        //    results.Should().Equal(dto.PersonId.Value);
////        //}


////        //[Fact]
////        //public void IntNullable()
////        //{

////        //    var dto = UnitTestHelper.AddPerson(_service, UnitTestHelper.CreateTestAutoMapperModel());

////        //    var expression = new OltEntityExpressionIntNullable<PersonEntity>(person => person.Id)
////        //    {
////        //        Value = dto.PersonId
////        //    };

////        //    var results = expression
////        //        .BuildQueryable(_context.InitializeQueryable<PersonEntity>())
////        //        .Select(s => s.Id)
////        //        .ToList();

////        //    results.Should().Equal(dto.PersonId.Value);
////        //}

////        //[Fact]
////        //public void StringContains()
////        //{
////        //    var saveDto = UnitTestHelper.CreateTestAutoMapperModel();
////        //    saveDto.Name.First = $"{saveDto.Name.First}_{Guid.NewGuid().ToString().Left(5)}";
////        //    var dto = UnitTestHelper.AddPerson(_service, saveDto);

////        //    var expression = new OltEntityExpressionStringContains<PersonEntity>(person => person.NameFirst)
////        //    {
////        //        Value = dto.Name.First
////        //    };

////        //    var results = expression
////        //        .BuildQueryable(_context.InitializeQueryable<PersonEntity>())
////        //        .Select(s => s.NameFirst)
////        //        .ToList();

////        //    results.Should().Equal(dto.Name.First);
////        //}

////        //[Fact]
////        //public void StringStartsWith()
////        //{
////        //    var prefix = Guid.NewGuid().ToString().Left(5);
////        //    var saveDto = UnitTestHelper.CreateTestAutoMapperModel();
////        //    saveDto.Name.Last = $"{prefix}{saveDto.Name.Last}";
////        //    var dto = UnitTestHelper.AddPerson(_service, saveDto);

////        //    var expression = new OltEntityExpressionStringStartsWith<PersonEntity>(person => person.NameLast)
////        //    {
////        //        Value = prefix
////        //    };

////        //    var results = expression
////        //        .BuildQueryable(_context.InitializeQueryable<PersonEntity>())
////        //        .Select(s => s.NameLast)
////        //        .ToList();

////        //    results.Should().Equal(dto.Name.Last);
////        //}

////        //[Fact]
////        //public void StringEquals()
////        //{
////        //    var saveDto = UnitTestHelper.CreateTestAutoMapperModel();
////        //    saveDto.Name.Last = $"{Guid.NewGuid().ToString().Left(5)}{saveDto.Name.Last}";
////        //    var dto = UnitTestHelper.AddPerson(_service, saveDto);

////        //    var expression = new OltEntityExpressionStringEquals<PersonEntity>(person => person.NameLast)
////        //    {
////        //        Value = saveDto.Name.Last
////        //    };

////        //    var results = expression
////        //        .BuildQueryable(_context.InitializeQueryable<PersonEntity>())
////        //        .Select(s => s.NameLast)
////        //        .ToList();

////        //    results.Should().Equal(dto.Name.Last);
////        //}
////    }
////}