using System.Collections.Generic;
using WordLadder.Solvers;
using Xunit;

namespace WordLadder.UnitTests
{
    public class WordLadderSolverTests
    {
        private static readonly List<string> _wordList = new() { "spin", "spit", "spat", "spot", "span", "spot" };

        [Fact]
        public void ShouldReturnAWordLadder_WhenALadderCanBeFound()
        {
            var wordLadderSolver = new WordLadderSolver();
            var startWord = "spin";
            var endWord = "spot";
            var ladder = wordLadderSolver.FindShortestLadder(startWord, endWord, _wordList);

            Assert.Contains("spin", ladder);
            Assert.Contains("spit", ladder);
            Assert.Contains("spot", ladder);
        }

        [Theory]
        [InlineData("spin", "spot", 3)]
        [InlineData("spin", "spit", 2)]
        public void ShouldReturnTheShortestLadder_WhenMultipleLaddersCanBeFound(string startWord, string endWord, int expectedLength)
        {
            var wordLadderSolver = new WordLadderSolver();
            var ladder = wordLadderSolver.FindShortestLadder(startWord, endWord, _wordList);
            Assert.Equal(expectedLength, ladder.Count);
        }

        [Theory]
        [InlineData("aaaa", "bbbb")]
        public void ShouldReturnNoLadderCanBeFound_WhenALadderCannotBeFound(string startWord, string endWord)
        {
            var wordLadderSolver = new WordLadderSolver();
            var ladder = wordLadderSolver.FindShortestLadder(startWord, endWord, _wordList);

            Assert.Single(ladder);
            Assert.Equal("No Ladder Can Be Found", ladder[0]);
        }
    }
}
