using Fclp;
using System;
using System.Collections.Generic;
using WordLadder;

namespace Wordladder
{
    class Program
    {
        public static void Main(string[] args)
        {
            var parser = SetupFluentCommandLineParser();
            var parsedResults = parser.Parse(args);

            if (parsedResults.HasErrors is false)
            {
                List<string> wordList = new();
                var fileLoadResult = FileManager.TryLoadDictionaryFile(parser.Object.DictionaryFileName, ref wordList);
                if (!fileLoadResult.Success)
                {
                    Console.WriteLine($"Unable to load Dictionary File: {fileLoadResult.Reason}");
                }

                var wordLadder = new WordLadderSolver(wordList);
                var ladderResult = wordLadder.CalculateWordLadder(parser.Object.StartWord, parser.Object.EndWord);

                if (!ladderResult.Success)
                {
                    Console.WriteLine($"Unable to calculate word ladder for startWord: {parser.Object.StartWord} and endWord: {parser.Object.EndWord}.\r\nReason: {ladderResult.Result} ");
                }

                var resultsFileWriteResult = FileManager.TryWriteResultsFile(parser.Object.ResultsFileName, ladderResult.Result);

                if (!resultsFileWriteResult.Success)
                {
                    Console.WriteLine($"Unable to write results file, Reason: {resultsFileWriteResult.Reason} ");
                }
            }
            else
            {
                Console.WriteLine(parsedResults.ErrorText);
            }
        }

        private static FluentCommandLineParser<ApplicationArguments> SetupFluentCommandLineParser()
        {
            var parser = new FluentCommandLineParser<ApplicationArguments>();
            parser.SetupHelp("?", "help")
                  .Callback(text => Console.WriteLine(text));

            parser.Setup(arg => arg.DictionaryFileName)
             .As('d', "dictionary")
             .WithDescription("The filename of the Dictionary File to use. Currently we only support .txt files.")
             .Required();

            parser.Setup(arg => arg.ResultsFileName)
             .As('r', "result")
             .WithDescription("The filename of the results .txt file.")
             .Required();

            parser.Setup(arg => arg.StartWord)
             .As('s', "start")
             .WithDescription("The Word to start the word ladder with.")
             .Required();

            parser.Setup(arg => arg.EndWord)
             .As('e', "end")
             .WithDescription("The Word to end the word ladder at.")
             .Required();

            return parser;
        }
    }
}
