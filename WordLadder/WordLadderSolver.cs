using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WordLadder
{
    public class WordLadderSolver
    {
        private List<string> _wordList;

        public WordLadderSolver(List<string> wordList)
        {
            _wordList = wordList;
            SanitiseList();
        }

        public (bool Success, string Result) CalculateWordLadder(string startWord, string endWord)
        {
            var isStartWordValid = IsWordValid(startWord);
            var isEndWordValid = IsWordValid(endWord);
            string result = string.Empty;

            if (isStartWordValid && isEndWordValid)
            {
                // do things
                result = "ta da two";
            }
            else if (!isStartWordValid)
            {
                return (false, "Start word is not valid");
            }
            else if (!isEndWordValid)
            {
                return (false, "End word is not valid");
            }

            return (true, result);
        }

        private void SanitiseList()
        {
            Console.WriteLine($"Sanitising Word List Current Length is: {_wordList.Count}");
            SanitiseWordList();
            Console.WriteLine($"Sanitised Word List Length is now: {_wordList.Count}");
        }

        private void SanitiseWordList()
        {
            var newList = new List<string>();

            foreach (var word in _wordList)
            {
                if (IsWordValid(word))
                    newList.Add(word);
            }

            _wordList = newList;
        }

        private static bool IsWordValid(string word)
        {
            Regex rgx = new("^[a-zA-Z]*$");

            if (word.Trim().Length != 4)
                return false;

            if (!rgx.IsMatch(word))
                return false;

            return true;
        }
    }
}
