using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using selenium_gui.Models;

namespace selenium_gui.tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class SequenceTests
    {
        private Mock<INavigation> _driverNavigationMock;
        private Mock<IWebDriver> _driverMock;

        [SetUp]
        public void Setup()
        {
            _driverNavigationMock = new Mock<INavigation>();
            _driverMock = new Mock<IWebDriver>();
            _driverMock.Setup(x => x.Navigate()).Returns(_driverNavigationMock.Object);
        }

        [Test]
        public void RunCallsDriverNavigateMethodOnEachStep()
        {
            // Arrange
            var sequence = new Sequence()
            {
                Driver = _driverMock.Object,
                Steps = new List<Step>() {
                    new Step()
                    {
                    Type = "Go To URL",
                    Parameters = new List<string>()
                        {
                            "https://google.com"
                        }

                    },
                    new Step()
                    {
                        Type = "Go To URL",
                        Parameters = new List<string>()
                        {
                            "https://google.com"
                        }

                    }
                }
            };

            // Act
            sequence.Run();

            // Assert
            _driverMock.Verify(x => x.Navigate(), Times.Exactly(2));
        }
    }
}
