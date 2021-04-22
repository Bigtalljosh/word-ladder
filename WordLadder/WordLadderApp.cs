﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WordLadder.IO;
using WordLadder.Solvers;

namespace WordLadder
{
    public class WordLadderApp
    {
        private const int _maxWordLength = 4;
        private readonly List<string> _wordList;
        private readonly IWordLadderSolver _wordLadderSolver;

        public WordLadderApp(IFileManager fileManager, IWordLadderSolver solver, IOptions<ApplicationArguments> options)
        {
            _wordList = new();
            _wordLadderSolver = solver;
            fileManager.TryLoadDictionaryFile(options.Value.DictionaryFileName, ref _wordList);
            _wordList = SanitiseWordList(_wordList);
        }

        public (bool Success, string Result) CalculateWordLadder(string startWord, string endWord)
        {
            var areParamsValid = AreParamsValid(startWord, endWord);
            if (!areParamsValid.Success)
                return (false, string.Join(Environment.NewLine, areParamsValid.Errors));

            var ladder = _wordLadderSolver.FindShortestLadder(startWord.ToLower(), endWord.ToLower(), _wordList);
            var result = string.Join(" ", ladder);

            return (true, result);
        }

        private (bool Success, List<string> Errors) AreParamsValid(string startWord, string endWord)
        {
            (bool Success, List<string> Errors) response = new(true, new());

            if (startWord.Equals(endWord))
            {
                response.Success = false;
                response.Errors.Add("Start Word and End Word must be different");
            }

            if (!_wordList.Contains(startWord.ToLower()) ||
                !_wordList.Contains(endWord.ToLower()))
            {
                response.Success = false;
                response.Errors.Add("Both words must exist in the dictionary");
            }

            if (!IsWordValid(startWord))
            {
                response.Success = false;
                response.Errors.Add($"Start Word is not a valid word. Needs to be {_maxWordLength} letters. No special characters or numbers.");
            }

            if (!IsWordValid(endWord))
            {
                response.Success = false;
                response.Errors.Add($"End Word is not a valid word. Needs to be {_maxWordLength} letters. No special characters or numbers.");
            }

            return response;
        }

        private static List<string> SanitiseWordList(List<string> wordList) =>
            wordList
                .Where(IsWordValid)
                .Select(w => w.ToLower())
                .ToList();


        private static bool IsWordValid(string word)
        {
            Regex rgx = new("^[a-zA-Z]*$");

            if (word.Trim().Length != _maxWordLength)
                return false;

            if (!rgx.IsMatch(word))
                return false;

            return true;
        }
    }
}
