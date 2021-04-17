using Xunit;

namespace WordLadder.UnitTests
{
    public class TryWriteResultsFile
    {
        private readonly string _testFileName = "results.txt";
        private readonly string _resultsWordLadder = "This Is A Test String";

        [Fact]
        public void ShouldReturnTrue_WhenFileWritesSuccessfully()
        {
            var result = FileManager.TryWriteResultsFile(_testFileName, _resultsWordLadder);
            Assert.True(result.Success);
        }

        [Theory]
        [InlineData("?")]
        [InlineData("test.png")]
        [InlineData("results?.txt")]
        public void ShouldReturnFalseAndReason_WhenFileContainsInvalidCharacters(string fileName)
        {
            var result = FileManager.TryWriteResultsFile(fileName, _resultsWordLadder);
            Assert.False(result.Success);
        }
    }
}

