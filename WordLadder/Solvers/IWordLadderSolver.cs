using System.Collections.Generic;

namespace WordLadder
{
    public interface IWordLadderSolver
    {
        List<string> FindShortestLadder(string startWord, string endWord, List<string> wordList);
    }
}