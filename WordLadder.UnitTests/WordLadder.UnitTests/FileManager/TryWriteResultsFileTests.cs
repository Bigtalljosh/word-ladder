using WordLadder.IO;
using Xunit;

namespace WordLadder.UnitTests
{
    public class TryWriteResultsFileTests
    {
        private readonly string _testFileName = "results.txt";
        private readonly string _resultsWordLadder = "This Is A Test String";

        [Fact]
        public void ShouldReturnTrue_WhenFileWritesSuccessfully()
        {
            var fileManager = new FileManager();
            var result = fileManager.TryWriteResultsFile(_testFileName, _resultsWordLadder);
            Assert.True(result.Success);
        }

        [Theory]
        [InlineData("?")]
        [InlineData("test.png")]
        [InlineData("test/.txt")]
        [InlineData("results?.txt")]
        public void ShouldReturnFalseAndReason_WhenFileContainsInvalidCharacters(string fileName)
        {
            var fileManager = new FileManager();
            var result = fileManager.TryWriteResultsFile(fileName, _resultsWordLadder);
            Assert.False(result.Success);
        }
    }
}

