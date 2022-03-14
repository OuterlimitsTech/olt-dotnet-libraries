using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;
using OLT.Libraries.UnitTest.Assets.Rules;
using Xunit;

namespace OLT.Libraries.UnitTest.GeneralTests
{
    public class ExceptionTests
    {

        private const string DefaultMessage = "Test Error";

        private T ToSerialize<T>(T obj)
        {
            using Stream s = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(s, obj);
            s.Position = 0; // Reset stream position
            return (T)formatter.Deserialize(s);
        }

        //[Fact]
        //public void ExceptionTest()
        //{
        //    var ex = new OltException(DefaultMessage);
        //    var result = ToSerialize(ex);
        //    Assert.Equal(ex.Message, result.Message);
        //}

        //[Fact]
        //public void InnerExceptionTest()
        //{
        //    var innerException = new Exception($"Inner {DefaultMessage}");
        //    var ex = new OltException(DefaultMessage, innerException);
        //    var result = ToSerialize(ex);
        //    Assert.Equal(ex.Message, result.Message);
        //}

        //[Fact]
        //public void BadRequestExceptionTest()
        //{
        //    var ex = new OltBadRequestException(DefaultMessage);
        //    var result = ToSerialize(ex);
        //    Assert.Equal(ex.Message, result.Message);
        //}

        //[Fact]
        //public void AdapterNotFoundExceptionTest()
        //{
        //    var ex = new OltAdapterNotFoundException(DefaultMessage);
        //    var result = ToSerialize(ex);
        //    Assert.Equal(ex.Message, result.Message);
        //}

        //[Fact]
        //public void AdapterNotFoundExceptionTestTyped()
        //{
        //    var ex = new OltAdapterNotFoundException<PersonEntity, PersonDto>();
        //    var result = ToSerialize(ex);
        //    Assert.Equal(ex.Message, result.Message);
        //}

        //[Fact]
        //public void RecordNotFoundException()
        //{
        //    var ex = new OltRecordNotFoundException(DefaultMessage);
        //    var result = ToSerialize(ex);
        //    Assert.Equal(ex.Message, result.Message);
        //}

        //[Fact]
        //public void RecordNotFoundExceptionEnum()
        //{
        //    var ex = new OltRecordNotFoundException<RecordNotFound>(RecordNotFound.Person);
        //    var result = ToSerialize(ex);
        //    Assert.Equal(ex.Message, result.Message);
        //}

        [Fact]
        public void RuleNotFoundExceptionTest()
        {
            var ex = new OltRuleNotFoundException(DefaultMessage);
            var result = ToSerialize(ex);
            Assert.Equal(ex.Message, result.Message);
        }

        [Fact]
        public void RuleNotFoundExceptionTestType()
        {
            var ex = new OltRuleNotFoundException(typeof(INotValidRule));
            var result = ToSerialize(ex);
            Assert.Equal(ex.Message, result.Message);
        }

        [Fact]
        public void RuleExceptionTest()
        {
            var ex = new OltRuleException(DefaultMessage);
            var result = ToSerialize(ex);
            Assert.Equal(ex.Message, result.Message);
        }

        //[Fact]
        //public void ValidationExceptionTest()
        //{
        //    var list = new List<OltValidationError> { new OltValidationError(DefaultMessage) };
        //    var ex = new OltValidationException(list);
        //    var result = ToSerialize(ex);
        //    Assert.Equal(ex.Message, result.Message);
        //}

        //[Fact]
        //public void AutoMapperExceptionTest()
        //{
        //    var innerException = new AutoMapperMappingException(DefaultMessage);
        //    var ex = new OltAutoMapperException<PersonEntity, PersonDto>(innerException);
        //    var expected =
        //        $"AutoMapper Exception while using map {nameof(IOltAdapterMap<PersonEntity, PersonDto>)}: {typeof(PersonEntity).FullName} -> {typeof(PersonDto).FullName} {Environment.NewLine}{innerException.Message}";
        //    Assert.Equal(expected, ex.Message);
        //}


        //[Fact]
        //public void AutoMapperInnerExceptionTest()
        //{
        //    var innerException = new Exception(DefaultMessage);
        //    var ex = new OltAutoMapperException<PersonEntity, PersonDto>(innerException);
        //    var expected =
        //        $"AutoMapper Exception while using map {nameof(IOltAdapterMap<PersonEntity, PersonDto>)}: {typeof(PersonEntity).FullName} -> {typeof(PersonDto).FullName}";
        //    Assert.Equal(expected, ex.Message);
        //}
    }
}