using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using WordLadder.IO;
using Xunit;

namespace WordLadder.UnitTests
{
    public class TryLoadDictionaryFileTests
    {
        private readonly string _testFileName = "test.txt";
        private readonly ILogger<FileManager> _fileManagerLogger;

        public TryLoadDictionaryFileTests()
        {
            _fileManagerLogger = Substitute.For<ILogger<FileManager>>();
        }

        [Fact]
        public void ShouldReturnTrue_WhenFileLoadsSuccessfully()
        {
            var fileManager = new FileManager(_fileManagerLogger);
            var wordList = new List<string>();
            var result = fileManager.TryLoadDictionaryFile(_testFileName, ref wordList);
            Assert.True(result.Success);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("test.png")]
        [InlineData("test/.txt")]
        [InlineData("test?.txt")]
        public void ShouldReturnFalseAndReason_WhenFileIsNotValid(string fileName)
        {
            var fileManager = new FileManager(_fileManagerLogger);
            var wordList = new List<string>();
            var result = fileManager.TryLoadDictionaryFile(fileName, ref wordList);
            Assert.False(result.Success);
        }
    }
}