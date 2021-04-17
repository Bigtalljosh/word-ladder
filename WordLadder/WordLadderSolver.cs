using System;
using System.IO;

namespace WordLadder
{
    public class WordLadderSolver
    {
        private string[] _wordList;

        public WordLadderSolver(ApplicationArguments arguments)
        {
            LoadDictionaryFile(arguments.DictionaryFileName);
            foreach(var word in _wordList)
            {
                Console.WriteLine(word + "\r\n");
            }
        }

        private void LoadDictionaryFile(string filename)
        {
            try
            {
                _wordList = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), filename));
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
