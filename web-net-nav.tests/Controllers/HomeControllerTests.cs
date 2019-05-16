using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using web_net_nav.Controllers;

namespace web_net_nav.tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IndexShouldReturnIndexView()
        {
            // Arrange
            var HomeController = new HomeController();

            // Act
            var result = HomeController.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}