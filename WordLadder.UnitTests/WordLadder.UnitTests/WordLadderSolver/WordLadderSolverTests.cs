using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WordLadder.UnitTests.WordLadderSolver
{
    public class WordLadderSolverTests
    {
        [Theory]
        [InlineData("spin","spot")]
        public void ShouldReturnAWordLadder_WhenALadderCanBeFound(string startWord, string endWord)
        {

        }

        [Theory]
        [InlineData("spin", "spot")]
        public void ShouldReturnTheShortestLadder_WhenMultipleLaddersCanBeFound(string startWord, string endWord)
        {

        }

        [Theory]
        [InlineData("aaaa", "bbbb")]
        public void ShouldReturnNoLadderCanBeFound_WhenALadderCannotBeFound(string startWord, string endWord)
        {

        }
    }
}
