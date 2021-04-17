using System;
using System.IO;
using System.Linq;

namespace WordLadder
{
    public class WordLadderSolver
    {
        private string[] _wordList;

        public WordLadderSolver()
        {

        }

        public void TestListLoaded()
        {
            foreach (var word in _wordList)
                Console.WriteLine(word);
        }

        public (bool Success, string Reason) TryLoadDictionaryFile(string fileName)
        {
            try
            {
                var isFileNameValid = ValidateFilename(fileName);

                if (!isFileNameValid.IsValid)
                    return (false, isFileNameValid.Reason);

                _wordList = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), fileName));

                return (true, string.Empty);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return (false, ex.Message);
            }
        }

        private static (bool IsValid, string Reason) ValidateFilename(string input)
        {
            var invalidChars = Path.GetInvalidFileNameChars();

            if(!Path.GetExtension(input).Equals(".txt")) 
                return (false, "Filename must end with .txt");

            if (input.Where(x => invalidChars.Contains(x)).ToArray().Length > 0)
                return (false, "Filename contains illegal characters");

            return (true, string.Empty);
        }
    }
}
