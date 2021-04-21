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
            Console.Title = "Word Ladder";

            var parser = SetupFluentCommandLineParser();
            var parsedResults = parser.Parse(args);

            if (parsedResults.HasErrors is false)
            {
                List<string> wordList = new();
                var fileManagerReadResult = FileManager.TryLoadDictionaryFile(parser.Object.DictionaryFileName, ref wordList);

                if (!fileManagerReadResult.Success)
                {
                    Console.Error.WriteLine($"Unable to load Dictionary File: {fileManagerReadResult.Reason}");
                }

                var wordLadderSolver = new WordLadderApp(wordList);
                var wordLadderResult = wordLadderSolver.CalculateWordLadder(parser.Object.StartWord, parser.Object.EndWord);

                if (!wordLadderResult.Success)
                {
                    Console.Error.WriteLine($"Unable to calculate word ladder for startWord: {parser.Object.StartWord} and endWord: {parser.Object.EndWord}.{Environment.NewLine}Reason: {wordLadderResult.Result} ");
                }

#if (DEBUG)
                Console.WriteLine("Result is :");
                foreach (var word in wordLadderResult.Result.Split(" "))
                {
                    Console.WriteLine(word);
                }
#endif

                var fileManagerWriteResult = FileManager.TryWriteResultsFile(parser.Object.ResultsFileName, wordLadderResult.Result);

                if (!fileManagerWriteResult.Success)
                {
                    Console.Error.WriteLine($"Unable to write results file, Reason: {fileManagerWriteResult.Reason} ");
                }
            }
            else
            {
                Console.Error.WriteLine(parsedResults.ErrorText);
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
             .SetDefault("words-english.txt")
             ;//.Required();

            parser.Setup(arg => arg.ResultsFileName)
             .As('r', "result")
             .WithDescription("The filename of the results .txt file.")
             .SetDefault("resultsfile.txt")
             ;//.Required();

            parser.Setup(arg => arg.StartWord)
             .As('s', "start")
             .WithDescription("The Word to start the word ladder with.")
             .SetDefault("spin")
             ;//.Required();

            parser.Setup(arg => arg.EndWord)
             .As('e', "end")
             .WithDescription("The Word to end the word ladder at.")
             .SetDefault("spot")
             ;//.Required();

            return parser;
        }
    }
}
