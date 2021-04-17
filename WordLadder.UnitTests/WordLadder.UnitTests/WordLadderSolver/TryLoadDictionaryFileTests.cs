using Xunit;

namespace WordLadder.UnitTests
{
    public class TryLoadDictionaryFileTests
    {
        private readonly string _testFileName = "test.txt";

        [Fact]
        public void ShouldReturnTrue_WhenFileLoadsSuccessfully()
        {
            var solver = new WordLadderSolver();
            var result = solver.TryLoadDictionaryFile(_testFileName);
            Assert.True(result.Success);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("test.png")]
        [InlineData("test?.txt")]
        [InlineData(".txt")]
        public void ShouldReturnFalseAndReason_WhenFileIsNotValid(string fileName)
        {
            var solver = new WordLadderSolver();
            var result = solver.TryLoadDictionaryFile(fileName);
            Assert.False(result.Success);
        }
    }
}