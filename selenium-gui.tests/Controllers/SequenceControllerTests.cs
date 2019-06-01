using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NUnit.Framework;
using Moq;
using selenium_gui.Controllers;
using selenium_gui.Interfaces;
using selenium_gui.Models;
using Microsoft.AspNetCore.Mvc;

namespace selenium_gui.tests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class SequenceControllerTests
    {

        private Mock<ISequence> _sequenceMock;
        private Mock<ISequenceModelBuilder> _sequenceModelBuilderMock;

        [SetUp]
        public void Setup()
        {
            _sequenceMock = new Mock<ISequence>();
            _sequenceModelBuilderMock = new Mock<ISequenceModelBuilder>();

            _sequenceMock.Setup(x => x.Run());
            _sequenceModelBuilderMock.Setup(x => x.Build(It.IsAny<string>())).Returns(_sequenceMock.Object);
            
        }

        [Test]
        public void RunReturnsOKStatus()
        {
            // Arrange
            var sequenceController = new SequenceController(_sequenceModelBuilderMock.Object);


            // Act
            var result = sequenceController.Run("test string");

            // Assert
            Assert.IsInstanceOf<OkResult>(result);

        }
    }
}
