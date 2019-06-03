using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using selenium_gui.ModelBuilders;

namespace selenium_gui.tests.ModelBuillders
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class DriverModelBuilderTests
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void CleanUp()
        {
            _webDriver.Close();
        }

        [Test]
        public void BuildReturnsChromeDirver()
        {
            // Arrange
            var driverModelBuilder = new DriverModelBuilder();

            // Act
            _webDriver = driverModelBuilder.Build();

            // Assert
            Assert.IsInstanceOf<ChromeDriver>(_webDriver);
        }

    }
}
