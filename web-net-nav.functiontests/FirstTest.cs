using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace web_net_nav.FunctionalTests
{
    [TestFixture]
    class MetaDataTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Init()
        {
            _driver = new ChromeDriver(@"C:\Projects\web-net-nav\drivers");
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