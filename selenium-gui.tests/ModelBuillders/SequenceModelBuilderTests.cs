using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NUnit.Framework;
using selenium_gui.ModelBuilders;

namespace selenium_gui.tests.ModelBuillders
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class SequenceModelBuilderTests
    {
        private SequenceModelBuilder _sequenceModelBuilder;

        [SetUp]
        public void Setup()
        {
            _sequenceModelBuilder = new SequenceModelBuilder();
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


    }
}
