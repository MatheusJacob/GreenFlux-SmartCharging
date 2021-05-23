using GreenFlux.SmartCharging.Matheus.Data;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Tests.Unit
{
    public class RemoveSuggestionsTests : BaseTest
    {
        public RemoveSuggestionsTests() : base(new DbContextOptionsBuilder<ApplicationDbContext>()
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
        public void Should_generate_suggestions_from_examples_1()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                List<Connector> connectors = new List<Connector>();
                connectors.Add(new Connector(1, 10));
                connectors.Add(new Connector(2, 10));
                connectors.Add(new Connector(3, 10));
                connectors.Add(new Connector(4, 10));
                connectors.Add(new Connector(5, 10));
                connectors.Add(new Connector(6, 20));
                connectors.Add(new Connector(7, 20));


                RemoveSuggestions removeSuggestion = new RemoveSuggestions();
                Assert.DoesNotThrow(() => removeSuggestion.GenerateAllSuggestions(connectors, 5f));

                Assert.AreEqual(5, removeSuggestion.Count);
                Assert.AreEqual(1, removeSuggestion[0].Count);
                Assert.AreEqual(1, removeSuggestion[1].Count);
                Assert.AreEqual(1, removeSuggestion[2].Count);
                Assert.AreEqual(1, removeSuggestion[3].Count);
                Assert.AreEqual(1, removeSuggestion[4].Count);
            }
        }

        [Test]
        public void Should_generate_suggestions_from_examples_2()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                List<Connector> connectors = new List<Connector>();
                connectors.Add(new Connector(1, 25));
                connectors.Add(new Connector(2, 25));
                connectors.Add(new Connector(3, 25));
                connectors.Add(new Connector(4, 15));


                RemoveSuggestions removeSuggestion = new RemoveSuggestions();
                Assert.DoesNotThrow(() => removeSuggestion.GenerateAllSuggestions(connectors, 10f));

                Assert.AreEqual(1, removeSuggestion.Count);
                Assert.AreEqual(1, removeSuggestion[0].Count);
            }
        }

        [Test]
        public void Should_generate_suggestions_from_examples_3()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                List<Connector> connectors = new List<Connector>();
                connectors.Add(new Connector(1, 19));
                connectors.Add(new Connector(2, 27));
                connectors.Add(new Connector(3, 27));
                connectors.Add(new Connector(4, 27));


                RemoveSuggestions removeSuggestion = new RemoveSuggestions();
                Assert.DoesNotThrow(() => removeSuggestion.GenerateAllSuggestions(connectors, 30f));

                Assert.AreEqual(3, removeSuggestion.Count);
                Assert.AreEqual(2, removeSuggestion[0].Count);
                Assert.AreEqual(2, removeSuggestion[1].Count);
                Assert.AreEqual(2, removeSuggestion[2].Count);

            }
        }

        [Test]
        public void Should_generate_suggestions_with_exact_amount()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                List<Connector> connectors = new List<Connector>();
                connectors.Add(new Connector(1, 20));
                connectors.Add(new Connector(2, 30));
                connectors.Add(new Connector(3, 30));
                connectors.Add(new Connector(4, 30));


                RemoveSuggestions removeSuggestion = new RemoveSuggestions();
                Assert.DoesNotThrow(() => removeSuggestion.GenerateAllSuggestions(connectors, 20f));

                Assert.AreEqual(1, removeSuggestion.Count);
                Assert.AreEqual(1, removeSuggestion[0].Count);
            }
        }

        [Test]
        public void Should_calculate_binary_Search_second_set()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                List<Connector> connectors = new List<Connector>();
                connectors.Add(new Connector(1, 10));
                connectors.Add(new Connector(2, 19));
                connectors.Add(new Connector(3, 30));
                connectors.Add(new Connector(4, 40));



                RemoveSuggestions removeSuggestion = new RemoveSuggestions();
                Assert.DoesNotThrow(() => removeSuggestion.GenerateAllSuggestions(connectors, 50f));

                Assert.AreEqual(1, removeSuggestion.Count);
                Assert.AreEqual(2, removeSuggestion[0].Count);
            }
        }
    }
}
