using System.Collections.Generic;
using WordLadder.IO;
using Xunit;

namespace WordLadder.UnitTests
{
    public class TryLoadDictionaryFileTests
    {
        private readonly string _testFileName = "test.txt";

        [Fact]
        public void ShouldReturnTrue_WhenFileLoadsSuccessfully()
        {
            var fileManager = new FileManager();
            var wordList = new List<string>();
            var result = fileManager.TryLoadDictionaryFile(_testFileName, ref wordList);
            Assert.True(result.Success);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("test.png")]
        [InlineData("test?.txt")]
        public void ShouldReturnFalseAndReason_WhenFileIsNotValid(string fileName)
        {
            var fileManager = new FileManager();
            var wordList = new List<string>();
            var result = fileManager.TryLoadDictionaryFile(fileName, ref wordList);
            Assert.False(result.Success);
        }
    }
}