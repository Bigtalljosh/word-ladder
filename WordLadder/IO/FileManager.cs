using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordLadder.IO
{
    public class FileManager : IFileManager
    {
        public FileManager()
        {

        }

        public (bool Success, string Reason) TryLoadDictionaryFile(string fileName, ref List<string> wordList)
        {
            try
            {
                var isFileNameValid = ValidateFilename(fileName);

                if (!isFileNameValid.IsValid)
                    return (false, isFileNameValid.Reason);

                wordList = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), fileName)).ToList();

                return (true, string.Empty);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return (false, ex.Message);
            }
        }

        public (bool Success, string Reason) TryWriteResultsFile(string fileName, string wordLadder)
        {
            var isFileNameValid = ValidateFilename(fileName);

            if (!isFileNameValid.IsValid)
                return (false, isFileNameValid.Reason);

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            var wordList = wordLadder.Split(' ');

            File.WriteAllLines(fullPath, wordList, Encoding.UTF8);
            return (true, string.Empty);
        }

        private (bool IsValid, string Reason) ValidateFilename(string input)
        {
            var invalidChars = Path.GetInvalidFileNameChars();

            if (!Path.GetExtension(input).Equals(".txt"))
                return (false, "Filename must end with .txt");

            if (input.Where(x => invalidChars.Contains(x)).ToArray().Length > 0)
                return (false, "Filename contains illegal characters");

            return (true, string.Empty);
        }
    }
}
