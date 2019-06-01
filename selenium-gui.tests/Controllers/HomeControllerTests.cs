using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using selenium_gui.Controllers;

namespace selenium_gui.tests.Controllers
{
    [ExcludeFromCodeCoverage]
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
            var homeController = new HomeController();

            // Act
            var result = homeController.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}