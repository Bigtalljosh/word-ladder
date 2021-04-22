using Microsoft.Extensions.Logging;
using NSubstitute;
using WordLadder.IO;
using Xunit;

namespace WordLadder.UnitTests
{
    public class TryWriteResultsFileTests
    {
        private readonly string _testFileName = "results.txt";
        private readonly string _resultsWordLadder = "This Is A Test String";
        private readonly ILogger<FileManager> _fileManagerLogger;

        public TryWriteResultsFileTests()
        {
            _fileManagerLogger = Substitute.For<ILogger<FileManager>>();
        }

        [Fact]
        public void ShouldReturnTrue_WhenFileWritesSuccessfully()
        {
            var fileManager = new FileManager(_fileManagerLogger);
            var result = fileManager.TryWriteResultsFile(_testFileName, _resultsWordLadder);
            Assert.True(result.Success);
        }

        [Theory]
        [InlineData("?")]
        [InlineData("test.png")]
        public void ShouldReturnFalseAndReason_WhenFileContainsInvalidCharacters(string fileName)
        {
            var fileManager = new FileManager(_fileManagerLogger);
            var result = fileManager.TryWriteResultsFile(fileName, _resultsWordLadder);
            Assert.False(result.Success);
        }
    }
}

