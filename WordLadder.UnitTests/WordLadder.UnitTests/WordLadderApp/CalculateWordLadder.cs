using System.Collections.Generic;
using Xunit;

namespace WordLadder.UnitTests
{
    public class CalculateWordLadder
    {
        [Theory]
        [InlineData("Spin", "Span")]
        [InlineData("Spin", "Spot")]
        public void ShouldReturnTrueAndWordLadder_WhenAbleToCalculateWordLadder(string startWord, string endWord)
        {
            List<string> wordList = new() { "Spin", "Spit", "Spat", "Spot", "Span" };
            var wordLadder = new WordLadderApp(wordList);

            var ladderResult = wordLadder.CalculateWordLadder(startWord, endWord);
            Assert.True(ladderResult.Success);
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
            List<string> wordList = new() { "Spin", "Spit", "Spat", "Spot", "Span" };
            var wordLadder = new WordLadderApp(wordList);

            var ladderResult = wordLadder.CalculateWordLadder(startWord, endWord);
            Assert.False(ladderResult.Success);
        }

        [Theory]
        [InlineData("Spin", "Cave")]
        [InlineData("Ball", "Dogs")]
        public void ShouldReturnFalseAndReason_WhenLadderCannotBeCreated(string startWord, string endWord)
        {
            List<string> wordList = new() { "Spin", "Spit", "Spat", "Spot", "Span" };
            var wordLadder = new WordLadderApp(wordList);

            var ladderResult = wordLadder.CalculateWordLadder(startWord, endWord);
            Assert.False(ladderResult.Success);
        }
    }
}
