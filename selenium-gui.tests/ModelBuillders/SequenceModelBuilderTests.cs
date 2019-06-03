using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NUnit.Framework;
using selenium_gui.ModelBuilders;
using selenium_gui.Models;
using FluentAssertions;
using Moq;
using selenium_gui.Interfaces;

namespace selenium_gui.tests.ModelBuillders
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class SequenceModelBuilderTests
    {
        private Mock<IDriverModelBuilder> _driverModelBuilderMock;
        private SequenceModelBuilder _sequenceModelBuilder;

        [SetUp]
        public void Setup()
        {
            _driverModelBuilderMock = new Mock<IDriverModelBuilder>();
            _sequenceModelBuilder = new SequenceModelBuilder(_driverModelBuilderMock.Object);

            _driverModelBuilderMock.Setup(x => x.Build());
        }

        [Test]
        public void ThrowsExceptionWhenPassedEmptyString()
        {
            // Arrange
            var sequenceData = string.Empty;
            

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => _sequenceModelBuilder.Build(sequenceData));
        }

        [Test]
        public void ThrowsExceptionWhenPassedNull()
        {
            // Arrange

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => _sequenceModelBuilder.Build(null));
        }

        [Test]
        public void BuildsSequence()
        {
            // Arrange
            var sequenceData = "[\"{\\\"Type\\\":\\\"Go To URL\\\",\\\"Parameters\\\":[\\\"https://google.com\\\"]}\"]";
            Sequence expectedResult = new Sequence()
            {
                Steps = new List<Step>() {
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
            var result = _sequenceModelBuilder.Build(sequenceData);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

    }
}
