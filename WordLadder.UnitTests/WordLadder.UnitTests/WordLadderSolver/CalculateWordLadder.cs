using Xunit;

namespace WordLadder.UnitTests.WordLadderSolver
{
    public class CalculateWordLadder
    {

        [Theory]
        [InlineData("", "")]
        public void ShouldReturnTrueAndWordLadder_WhenAbleToCalculateWordLadder(string startWord, string endWord)
        {

        }

        [Theory]
        [InlineData("A£A", "BBB")]
        [InlineData("AAA", "B£B")]
        [InlineData("AAA", "BBB")]
        [InlineData("AAAA", "BBB!")]
        [InlineData("AAA!", "BBBB")]
        [InlineData("AAA1", "BBBB")]
        [InlineData("AAAA", "BBB1")]
        public void ShouldReturnFalseAndReason_WhenStartOrEndWordsAreInvalid(string startWord, string endWord)
        {

        }
    }
}
