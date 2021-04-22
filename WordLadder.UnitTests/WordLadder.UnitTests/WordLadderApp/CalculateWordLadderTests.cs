using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Collections.Generic;
using WordLadder.IO;
using WordLadder.Solvers;
using Xunit;

namespace WordLadder.UnitTests
{
    public class CalculateWordLadderTests
    {
        private readonly IFileManager _fileManager;
        private readonly IWordLadderSolver _wordLadderSolver;
        private readonly IOptions<ApplicationArguments> _options;

        public CalculateWordLadderTests()
        {
            var fileManagerLogger = Substitute.For<ILogger<FileManager>>();
            _fileManager = new FileManager(fileManagerLogger);
            _wordLadderSolver = Substitute.For<IWordLadderSolver>();
            _options = Options.Create(new ApplicationArguments 
            {
                DictionaryFileName = "test.txt",
                StartWord = "spin",
                EndWord = "spot",
                ResultsFileName = "testresults.txt"
            });
        }

        [Fact]
        public void ShouldReturnTrueAndWordLadder_WhenAbleToCalculateWordLadder()
        {
            var wordLadder = new WordLadderApp(_fileManager, _wordLadderSolver, _options);
            var response = new List<string> { "spin", "spat", "spot" };
            _wordLadderSolver.FindShortestLadder(default, default, default).ReturnsForAnyArgs(response);

            var ladderResult = wordLadder.CalculateWordLadder("spin", "spot");
            Assert.True(ladderResult.Success);
            Assert.Equal("spin spat spot", ladderResult.Result);
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
            var wordLadder = new WordLadderApp(_fileManager, _wordLadderSolver, _options);

            var ladderResult = wordLadder.CalculateWordLadder(startWord, endWord);
            Assert.False(ladderResult.Success);
        }

        [Theory]
        [InlineData("Spin", "Cave")]
        [InlineData("Ball", "Dogs")]
        public void ShouldReturnFalseAndReason_WhenLadderCannotBeCreated(string startWord, string endWord)
        {
            var wordLadder = new WordLadderApp(_fileManager, _wordLadderSolver, _options);

            var ladderResult = wordLadder.CalculateWordLadder(startWord, endWord);
            Assert.False(ladderResult.Success);
        }
    }
}
