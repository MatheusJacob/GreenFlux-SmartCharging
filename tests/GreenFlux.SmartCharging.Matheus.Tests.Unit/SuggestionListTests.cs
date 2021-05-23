using GreenFlux.SmartCharging.Matheus.Data;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Tests.Unit
{
    public class SuggestionListTests : BaseTest
    {
        public SuggestionListTests() : base(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "SmartChargingMatheusTest")
            .Options)
        {

        }

        [SetUp]
        public void Setup()
        {
            Seed();
        }

        [Test]
        public void Should_init_suggestion_list_properly()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                SuggestionList suggestionList = new SuggestionList(10);
                Assert.AreEqual(10, suggestionList.TotalSum);

                Assert.DoesNotThrow(() => new SuggestionList());
            }
        }

        [Test]
        public void Should_merge_2_suggestion_lists()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                SuggestionList suggestionList = new SuggestionList(5);
                suggestionList.Add(new Suggestion(Guid.NewGuid(), 1, 5));

                SuggestionList suggestionList2 = new SuggestionList(10);
                suggestionList2.Add(new Suggestion(Guid.NewGuid(), 1, 5));
                suggestionList2.Add(new Suggestion(Guid.NewGuid(), 2, 5));

                Assert.Less(0, suggestionList2.CompareTo(suggestionList));

                SuggestionList suggestionList3 = new SuggestionList(suggestionList, suggestionList2);

                Assert.AreEqual(15, suggestionList3.TotalSum);
                Assert.AreEqual(3, suggestionList3.Count);                

                Assert.DoesNotThrow(() => new SuggestionList());
            }
        }

        [Test]
        public void Should_merge_1_suggestion_list_with_connector()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                SuggestionList suggestionList = new SuggestionList(5);
                suggestionList.Add(new Suggestion(Guid.NewGuid(), 1, 5));

                Connector connector = new Connector(1,5f);

                SuggestionList suggestionList3 = new SuggestionList(suggestionList, connector);

                Assert.AreEqual(10, suggestionList3.TotalSum);
                Assert.AreEqual(2, suggestionList3.Count);
            }
        }

        [Test]
        public void Should_throw_comparing_to_null()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                SuggestionList suggestionList = new SuggestionList(5);
                Assert.AreEqual(1, suggestionList.CompareTo(null));

                Assert.Throws<ArgumentException>(() => suggestionList.CompareTo(new Connector(5)));
            }
        }
    }
}
