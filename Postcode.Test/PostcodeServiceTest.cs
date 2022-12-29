using System;
using Autofac;
using NUnit.Framework;
using Postcode.Services;

namespace Postcode.Tests
{
    [TestFixture]
    public class PostcodeServiceTest
    {
        private PostcodeService postcodeService;

        protected ILifetimeScope Scope { get; set; }

        public IUrlFactoryService UrlFactory { get; set; }

        [SetUp]
        public void Setup()
        {
            postcodeService = new PostcodeService();
            var builder = new ContainerBuilder();
            builder.RegisterType<UrlFactoryService>().As<IUrlFactoryService>().InstancePerLifetimeScope();
            var container = builder.Build();
            // Now we create our scope.
            Scope = container.BeginLifetimeScope("httpRequest");

        }

        [TearDown]
        public void TearDown()
        {
            // Cleanup the scope at the end of the test run.
            Scope.Dispose();
        }

        [Test]
        [TestCase(typeof(IUrlFactoryService))]
        public void Test_If_Controller_Resolves(Type serviceTyoe)
        {
            // We create the given type using the IOC scope.
            var controller = Scope.Resolve(serviceTyoe);
            // Assert it isn't null, although if your registrations are wrong, 
            // the above line will thrown an exception.
            Assert.IsNotNull(serviceTyoe);
        }
    }
}
