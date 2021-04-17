using Fclp;
using System;
using System.Text.Json;
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
                // Run the app
                Console.WriteLine(JsonSerializer.Serialize(parser.Object));
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
