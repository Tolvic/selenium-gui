using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace selenium_gui.functiontests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class MetaDataTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Init()
        {
            _driver = new ChromeDriver(@"C:\Projects\selenium-gui\drivers");
        }

        [TearDown]
        public void Cleanup()
        {
            _driver.Close();
        }

        [Test]
        public void IndexHasTitle()
        {
            // Arrange

            var url = "https://localhost:44321/";

            // Act
            _driver.Navigate().GoToUrl(url);
            var result = _driver.Title;


            // Assert
            Assert.That(result, Does.Contain("home page").IgnoreCase);

        }
    }
}