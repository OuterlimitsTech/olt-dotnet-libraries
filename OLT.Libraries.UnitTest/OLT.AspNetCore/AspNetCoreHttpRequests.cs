using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using OLT.Core;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore
{
    public class AspNetCoreHttpRequests
    {
        [Fact]
        public void ToOltGenericParameter()
        {
            var username = Faker.Internet.UserName();
            var email = Faker.Internet.Email();
            var userId = Faker.RandomNumber.Next();

            var formRequest = new Dictionary<string, StringValues>
            {
                { "email", email }
            };
            var queryRequest = new Dictionary<string, StringValues>
            {
                { "username", username }
            };

            var formCollection = new FormCollection(formRequest);
            var form = new FormFeature(formCollection);
            var queryCollection = new QueryCollection(queryRequest);
            var query = new QueryFeature(queryCollection);
            var routeValues = new RouteValuesFeature();
            routeValues.RouteValues.Add("userId", userId.ToString());

            var features = new FeatureCollection();
            features.Set<IQueryFeature>(query);
            features.Set<IFormFeature>(form);
            features.Set<IRouteValuesFeature>(routeValues);
            var context = new DefaultHttpContext(features);
            context.Response.Body = new MemoryStream();

            var results = context.Request.ToOltGenericParameter();

            results.Values.Should().ContainValues(username, email, userId.ToString());
        }

        [Fact]
        public void ToOltGenericParameterDuplicate()
        {
            var username = Faker.Internet.UserName();
            var email = Faker.Internet.Email();
            var userId = Faker.RandomNumber.Next();

            var formRequest = new Dictionary<string, StringValues>
            {
                { "email", email }
            };
            var queryRequest = new Dictionary<string, StringValues>
            {
                { "username", username }
            };

            var formCollection = new FormCollection(formRequest);
            var form = new FormFeature(formCollection);
            var queryCollection = new QueryCollection(queryRequest);
            var query = new QueryFeature(queryCollection);
            var routeValues = new RouteValuesFeature();
            routeValues.RouteValues.Add("username", userId.ToString());

            var features = new FeatureCollection();
            features.Set<IQueryFeature>(query);
            features.Set<IFormFeature>(form);
            features.Set<IRouteValuesFeature>(routeValues);
            var context = new DefaultHttpContext(features);
            context.Response.Body = new MemoryStream();

            var results = context.Request.ToOltGenericParameter();
            
            results.Values.Should().ContainValues($"{username},{userId}" , email);
        }


        [Fact]
        public void ToOltGenericParameterQuery()
        {
            var username = Faker.Internet.UserName();
            var email = Faker.Internet.Email();
            var userId = Faker.RandomNumber.Next();
            var dictionary = new Dictionary<string, StringValues>
            {
                { "username", username },
                { "email", email },
                { "userId", userId.ToString() }
            };

            var queryCollection = new QueryCollection(dictionary);
            var query = new QueryFeature(queryCollection);

            var features = new FeatureCollection();
            features.Set<IQueryFeature>(query);
            var context = new DefaultHttpContext(features);
            context.Response.Body = new MemoryStream();

            var results = context.Request.Query.ToOltGenericParameter();

            results.Values.Should().ContainValues(username, email, userId.ToString());
        }


        [Fact]
        public void ToOltGenericParameterRouteValues()
        {
            var username = Faker.Internet.UserName();
            var email = Faker.Internet.Email();
            var userId = Faker.RandomNumber.Next();
            var dictionary = new Dictionary<string, StringValues>
            {
                { "username", username },
                { "email", email }
            };

            var queryCollection = new QueryCollection(dictionary);
            var query = new QueryFeature(queryCollection);
            var routeValues = new RouteValuesFeature();
            routeValues.RouteValues.Add("userId", userId.ToString());


            var features = new FeatureCollection();
            features.Set<IQueryFeature>(query);
            features.Set<IRouteValuesFeature>(routeValues);
            var context = new DefaultHttpContext(features);
            context.Response.Body = new MemoryStream();

            var results = context.Request.RouteValues.ToOltGenericParameter();

            results.Values.Should().ContainValues(userId.ToString());
        }


        [Fact]
        public void ToOltGenericParameterForm()
        {
            var username = Faker.Internet.UserName();
            var email = Faker.Internet.Email();
            var userId = Faker.RandomNumber.Next();
            var dictionary = new Dictionary<string, StringValues>
            {
                { "username", username },
                { "email", email },
                { "userId", userId.ToString() }
            };

            var formCollection = new FormCollection(dictionary);
            var form = new FormFeature(formCollection);


            var features = new FeatureCollection();
            features.Set<IFormFeature>(form);
            var context = new DefaultHttpContext(features);
            context.Response.Body = new MemoryStream();

            var results = context.Request.Form.ToOltGenericParameter();

            results.Values.Should().ContainValues(username, email, userId.ToString());
        }


        [Fact]
        public async Task GetRawBodyStringAsync()
        {
            var dto = UnitTestHelper.CreatePersonDto();
            var json = JsonConvert.SerializeObject(dto);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var httpContext = new DefaultHttpContext()
            {
                Request = { Body = stream, ContentLength = stream.Length }
            };

            var result = await httpContext.Request.GetRawBodyStringAsync();
            Assert.True(result.Equals(json));
        }


        [Fact]
        public async Task GetRawBodyBytesAsync()
        {
            var dto = UnitTestHelper.CreatePersonDto();
            var json = JsonConvert.SerializeObject(dto);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var httpContext = new DefaultHttpContext()
            {
                Request = { Body = stream, ContentLength = stream.Length }
            };

            var result = await httpContext.Request.GetRawBodyBytesAsync();
            
            Assert.True(result.Length.Equals(json.ToASCIIBytes().Length));
        }


        [Fact]
        public void OltGenericParameterGetValue()
        {
            var username = Faker.Internet.UserName();
            var email = Faker.Internet.Email();
            var dictionary = new Dictionary<string, StringValues>
            {
                { "username", username },
                { "email", email }
            };
            var queryCollection = new QueryCollection(dictionary);
            var query = new QueryFeature(queryCollection);
            var features = new FeatureCollection();
            features.Set<IQueryFeature>(query);
            var context = new DefaultHttpContext(features);
            context.Response.Body = new MemoryStream();

            var results = context.Request.ToOltGenericParameter();
            var value = results.GetValue("username");

            Assert.Equal(value?.ToString(), username);
        }


        [Fact]
        public void OltGenericParameterGetValueUsingGeneric()
        {
            var username = Faker.Internet.UserName();
            var email = Faker.Internet.Email();
            var dictionary = new Dictionary<string, StringValues>
            {
                { "username", username },
                { "email", email }
            };
            var queryCollection = new QueryCollection(dictionary);
            var query = new QueryFeature(queryCollection);
            var features = new FeatureCollection();
            features.Set<IQueryFeature>(query);
            var context = new DefaultHttpContext(features);
            context.Response.Body = new MemoryStream();

            var results = context.Request.ToOltGenericParameter();

            Assert.Equal(results.GetValue<string>("email"), email);
        }

        [Fact]
        public void OltGenericParameterGetValueUsingGenericNull()
        {
            var username = Faker.Internet.UserName();
            var email = Faker.Internet.Email();
            var dictionary = new Dictionary<string, StringValues>
            {
                { "username", username },
                { "email", email }
            };
            var queryCollection = new QueryCollection(dictionary);
            var query = new QueryFeature(queryCollection);
            var features = new FeatureCollection();
            features.Set<IQueryFeature>(query);
            var context = new DefaultHttpContext(features);
            context.Response.Body = new MemoryStream();

            var results = context.Request.ToOltGenericParameter();

            Assert.True(results.GetValue<string>("foobar") == null);
        }

        [Fact]
        public void OltGenericParameterGetValueUsingGenericDefault()
        {
            var username = Faker.Internet.UserName();
            var email = Faker.Internet.Email();
            var defaultEmail = Faker.Internet.Email();
            var dictionary = new Dictionary<string, StringValues>
            {
                { "username", username },
                { "email", email }
            };
            var queryCollection = new QueryCollection(dictionary);
            var query = new QueryFeature(queryCollection);
            var features = new FeatureCollection();
            features.Set<IQueryFeature>(query);
            var context = new DefaultHttpContext(features);
            context.Response.Body = new MemoryStream();

            var results = context.Request.ToOltGenericParameter();

            Assert.Equal(results.GetValue<string>("foobar", defaultEmail), defaultEmail);
        }

        [Fact]
        public void OltGenericParameterEmpty()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var results = context.Request.ToOltGenericParameter();
            Assert.False(results.Values.Any());
        }
    }
}