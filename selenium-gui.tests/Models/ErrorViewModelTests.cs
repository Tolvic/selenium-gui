using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using selenium_gui.Models;

namespace seleniumgui.tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ErrorViewModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShowRequestIdShouldReturnFalseWhenRequestIdIsSetToAnEmptyString()
        {
            // Arrange
            var ErrorViewModel = new ErrorViewModel();
            ErrorViewModel.RequestId = "";

            // Act
            var result = ErrorViewModel.ShowRequestId;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ShowRequestIdShouldReturnFalseWhenRequestIdIsSetToNull()
        {
            // Arrange
            var ErrorViewModel = new ErrorViewModel();
            ErrorViewModel.RequestId = null;

            // Act
            var result = ErrorViewModel.ShowRequestId;

            // Assert
            Assert.That(result, Is.False);
        }
    }
}