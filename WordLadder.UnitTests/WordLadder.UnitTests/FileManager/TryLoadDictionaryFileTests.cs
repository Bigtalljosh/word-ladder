using System.Collections.Generic;
using Xunit;

namespace WordLadder.UnitTests
{
    public class TryLoadDictionaryFileTests
    {
        private readonly string _testFileName = "test.txt";

        [Fact]
        public void ShouldReturnTrue_WhenFileLoadsSuccessfully()
        {
            var wordList = new List<string>();
            var result = FileManager.TryLoadDictionaryFile(_testFileName, ref wordList);
            Assert.True(result.Success);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("test.png")]
        [InlineData("test/.txt")]
        [InlineData("test?.txt")]
        public void ShouldReturnFalseAndReason_WhenFileIsNotValid(string fileName)
        {
            var wordList = new List<string>();
            var result = FileManager.TryLoadDictionaryFile(fileName, ref wordList);
            Assert.False(result.Success);
        }
    }
}