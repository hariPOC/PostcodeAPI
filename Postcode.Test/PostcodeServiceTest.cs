using System;
using Autofac;
using Autofac.Core;
using Autofac.Extras.Moq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Postcode.Models;
using Postcode.Services;

namespace Postcode.Tests
{
    [TestFixture]
    public class PostcodeServiceTest
    {
        private PostcodeService postcodeService;

        [SetUp]
        public void Setup()
        {

            Environment.SetEnvironmentVariable("PostcodeUrl", "http://api.postcodes.io/postcodes/");
            
            Action<ContainerBuilder> containerBuilderAction = delegate (ContainerBuilder cb)
            {
                cb.RegisterType<UrlFactoryService>().As<IUrlFactoryService>();
                cb.RegisterType<PostcodeService>().PropertiesAutowired(); //The autofac will go to every single property and try to resolve it.
            };

            var mock = AutoMock.GetLoose(containerBuilderAction);

            postcodeService = mock.Create<PostcodeService>();
        }

       
        [Test]
        [TestCase("S10 1AE")]
        public void IsLookupSucess(string postcode)
        {
            PostcodeInfo expectedResult = new PostcodeInfo
            {
                Postcode = "S10 1AE",
                Country = "England",
                latitude = 53.381165,
                Region = "Yorkshire and The Humber",
                AdminDistrict = "Sheffield",
                ParliamentaryConstituency = "Sheffield Central",
                Area = "North"
            };

            PostcodeInfo actualResult = postcodeService.Lookup(postcode).Result;

            //Fluent Assertion.
            actualResult.Should().BeEquivalentTo(expectedResult);
            
        }

        [Test]
        [TestCase("S")]
        public void IsAutocompleteSucess(string postcode)
        {

            var expectedResult = "S10 1AE";
            var actualResult = postcodeService.Autocomplete(postcode).Result;

            //Fluent Assertion.
            Assert.Contains(expectedResult, (System.Collections.ICollection)actualResult);

        }

        [Test]
        [TestCase("S10 1AE")]
        public void IsLookupFailure(string postcode)
        {
            PostcodeInfo expectedResult = new PostcodeInfo
            {
                Postcode = "S10 1AG",
                Country = "England",
                latitude = 53.379204,
                Region = "Yorkshire and The Humber",
                AdminDistrict = "Sheffield",
                ParliamentaryConstituency = "Sheffield, Hallam",
                Area = "North"
            };

            var actualResult = postcodeService.Lookup(postcode).Result;

            //Fluent Assertion
            actualResult.Should().NotBeEquivalentTo(expectedResult);

        }


    }
}
