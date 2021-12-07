using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.Caching
{
    public class MemoryCacheTests
    {
        private readonly SqlDatabaseContext _context;
        private readonly IPersonService _service;

        public MemoryCacheTests(
            IPersonService service,
            SqlDatabaseContext context)
        {
            _context = context;
            _service = service;
        }

        [Fact]
        public void Options()
        {            
            Assert.Throws<ArgumentNullException>(() => OltMemoryCacheServiceCollectionExtensions.AddOltAddMemoryCache(null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(15)));
            Assert.Throws<ArgumentNullException>(() => OltMemoryCacheServiceCollectionExtensions.AddOltAddMemoryCache(null, opt => new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTimeOffset.Now.AddSeconds(15))));
        }

        private PersonDto Clone(PersonDto personDto)
        {
            return new PersonDto
            {
                Email = personDto.Email,
                First = personDto.First,
                Middle = personDto.Middle,
                Last = personDto.Last,
            };
        }

        [Fact]
        public void GetAndRemove()
        {
            var personDto = UnitTestHelper.CreatePersonDto();
            

            var services = new ServiceCollection();
            services.AddOltAddMemoryCache(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(15));

            var provider = services.BuildServiceProvider();
            var memoryCache = provider.GetRequiredService<IMemoryCache>();
            var oltMemoryCache = provider.GetRequiredService<IOltMemoryCache>();

            Assert.Throws<ArgumentNullException>(() => oltMemoryCache.Get(null, () => Clone(personDto)));
            Assert.Throws<ArgumentException>(() => oltMemoryCache.Get("", () => Clone(personDto)));


            var cacheKey = $"cache-person-{Guid.NewGuid()}";
            oltMemoryCache.Get(cacheKey, () => Clone(personDto)).Should().BeEquivalentTo(personDto, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));
            memoryCache.Get<PersonDto>(cacheKey).Should().BeEquivalentTo(personDto, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));

            oltMemoryCache.Remove(cacheKey);
            oltMemoryCache.Get(cacheKey, () => Clone(personDto)).Should().BeEquivalentTo(personDto, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));
            oltMemoryCache.Get(cacheKey, () => UnitTestHelper.CreatePersonDto()).Should().BeEquivalentTo(personDto, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));
            oltMemoryCache.Remove(cacheKey);
            oltMemoryCache.Get(cacheKey, () => UnitTestHelper.CreatePersonDto()).Should().NotBeEquivalentTo(personDto, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));

            cacheKey = $"cache-person-{Guid.NewGuid()}";
            oltMemoryCache.Remove(cacheKey);
            personDto = UnitTestHelper.CreatePersonDto();
            oltMemoryCache.Get(cacheKey, () => Clone(personDto), TimeSpan.FromMilliseconds(1)).Should().BeEquivalentTo(personDto, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));
            Assert.False(new ManualResetEvent(false).WaitOne(500));
            memoryCache.Get<PersonDto>(cacheKey).Should().BeNull();
            oltMemoryCache.Get(cacheKey, () => UnitTestHelper.CreatePersonDto()).Should().NotBeEquivalentTo(personDto, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));
            memoryCache.Get<PersonDto>(cacheKey).Should().NotBeNull();

            cacheKey = $"cache-person-{Guid.NewGuid()}";
            personDto = UnitTestHelper.CreatePersonDto();

            oltMemoryCache.Get(cacheKey, () => Clone(personDto), null, TimeSpan.FromMilliseconds(1)).Should().BeEquivalentTo(personDto, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));
            Assert.False(new ManualResetEvent(false).WaitOne(500));
            memoryCache.Get<PersonDto>(cacheKey).Should().BeNull();
            oltMemoryCache.Get(cacheKey, () => UnitTestHelper.CreatePersonDto()).Should().NotBeEquivalentTo(personDto, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));
            memoryCache.Get<PersonDto>(cacheKey).Should().NotBeNull();
        }


        [Fact]
        public async void GetAndRemoveAsync()
        {

            var entity1 = UnitTestHelper.AddPerson(_context);
            var entity2 = UnitTestHelper.AddPerson(_context);
            await _context.SaveChangesAsync();

            var personDto1 = await _service.GetAsync<PersonDto>(entity1.Id);
            var personDto2 = await _service.GetAsync<PersonDto>(entity2.Id);


            var services = new ServiceCollection();
            services.AddOltAddMemoryCache(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(15));

            var provider = services.BuildServiceProvider();
            var memoryCache = provider.GetRequiredService<IMemoryCache>();
            var oltMemoryCache = provider.GetRequiredService<IOltMemoryCache>();

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await oltMemoryCache.GetAsync(null, async () => await _service.GetAsync<PersonDto>(entity1.Id)));
            await Assert.ThrowsAsync<ArgumentException>(async () => await oltMemoryCache.GetAsync("", async () => await _service.GetAsync<PersonDto>(entity1.Id)));
            

            var cacheKey = $"cache-person-{Guid.NewGuid()}";
            (await oltMemoryCache.GetAsync(cacheKey, async () => await _service.GetAsync<PersonDto>(entity1.Id))).Should().BeEquivalentTo(personDto1, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));
            oltMemoryCache.Remove(cacheKey);

            cacheKey = $"cache-person-{Guid.NewGuid()}";
            (await oltMemoryCache.GetAsync(cacheKey, async () => await _service.GetAsync<PersonDto>(entity2.Id))).Should().BeEquivalentTo(personDto2, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));
            oltMemoryCache.Remove(cacheKey);
            (await oltMemoryCache.GetAsync(cacheKey, async () => await _service.GetAsync<PersonDto>(entity1.Id))).Should().NotBeEquivalentTo(personDto2, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));


            cacheKey = $"cache-person-{Guid.NewGuid()}";
            (await oltMemoryCache.GetAsync(cacheKey, async () => await _service.GetAsync<PersonDto>(entity2.Id), TimeSpan.FromMilliseconds(1), null)).Should().BeEquivalentTo(personDto2, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));
            Assert.False(new ManualResetEvent(false).WaitOne(500));
            (await oltMemoryCache.GetAsync(cacheKey, async () => await _service.GetAsync<PersonDto>(entity1.Id), null, TimeSpan.FromMilliseconds(1))).Should().NotBeEquivalentTo(personDto2, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));
            Assert.False(new ManualResetEvent(false).WaitOne(500));
            (await oltMemoryCache.GetAsync(cacheKey, async () => await _service.GetAsync<PersonDto>(entity2.Id), null, TimeSpan.FromMilliseconds(1))).Should().NotBeEquivalentTo(personDto1, opt => opt.Excluding(t => t.PersonId).Excluding(t => t.UniqueId));
        }
    }
}
