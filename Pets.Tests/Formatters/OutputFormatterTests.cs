using System;
using Pets.Formatters;
using Xunit;

namespace Pets.Tests.Formatters
{
    public class OutputFormatterTests
    {
        private OutputFormatter _sut;

        public OutputFormatterTests()
        {
            _sut = new OutputFormatter();
        }


        [Fact]
        public void FormatAsHeaderAndSubPoints_WhenNoGroupingSpecified_ShouldThrowException()
        {
        }

        [Fact]
        public void FormatAsHeaderAndSubPoints_WhenGroupingSpecifiedButGroupIsNullOrEmpty_ShouldThrowException()
        {
        }

        [Fact]
        public void FormatAsHeaderAndSubPoints_WhenGroupingSpecifiedWithValidData_ShouldReturnFormattedText()
        {
        }
    }
}
