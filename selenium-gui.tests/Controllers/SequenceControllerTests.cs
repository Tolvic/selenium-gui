using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NUnit.Framework;
using Moq;
using selenium_gui.Interfaces;
using selenium_gui.Models;

namespace selenium_gui.tests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class SequenceControllerTests
    {

        private Mock<ISequenceModelBuilder> _sequenceModelBuilderMock;

        [SetUp]
        public void Setup()
        {
            _sequenceModelBuilderMock = new Mock<ISequenceModelBuilder>();
            _sequenceModelBuilderMock.Setup(x => x.Build(It.IsAny<string>())).Returns(new Sequence());
        }

        [Test]
        public void SequenceTest()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
