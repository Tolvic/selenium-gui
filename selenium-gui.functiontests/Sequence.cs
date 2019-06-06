using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace selenium_gui.functiontests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class SequenceFunctionalTests
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
        public void Clicking_Add_Step_Adds_A_Step()
        {
            // Arrange
            var url = "https://localhost:44321/";

            // Act
            _driver.Navigate().GoToUrl(url);
            var addStepButton = _driver.FindElement(By.Id("add-step"));
            
            addStepButton.Click();
            System.Threading.Thread.Sleep(1000);
            var steps = _driver.FindElements(By.ClassName("step-li"));
            var result = steps.Count;

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Clicking_X_On_A_Step_Deletes_That_Step()
        {
            // Arrange
            var url = "https://localhost:44321/";

            // Act
            _driver.Navigate().GoToUrl(url);
            var stepDeleteButton = _driver.FindElement(By.ClassName("delete-step"));

            stepDeleteButton.Click();
            var steps = _driver.FindElements(By.ClassName("step-li"));
            var result = steps.Count;

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Clicking_X_On_An_added_Step_Deletes_That_Step()
        {
            // Arrange
            var url = "https://localhost:44321/";

            // Act
            _driver.Navigate().GoToUrl(url);
            var addStepButton = _driver.FindElement(By.Id("add-step"));

            addStepButton.Click();
            System.Threading.Thread.Sleep(1000);

            var stepDeleteButton = _driver.FindElements(By.ClassName("delete-step"));

            stepDeleteButton[1].Click();
            var steps = _driver.FindElements(By.ClassName("step-li"));
            var result = steps.Count;

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }
    }
}