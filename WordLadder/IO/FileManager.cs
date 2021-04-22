using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordLadder.IO
{
    public class FileManager : IFileManager
    {
        private readonly ILogger _logger;
        public FileManager(ILogger<FileManager> logger)
        {
            _logger = logger;
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
                _logger.LogError(ex.Message);
                return (false, ex.Message);
            }
        }

        public (bool Success, string Reason) TryWriteResultsFile(string fileName, string wordLadder)
        {
            var isFileNameValid = ValidateFilename(fileName);

            if (!isFileNameValid.IsValid)
                return (false, isFileNameValid.Reason);

            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                var wordList = wordLadder.Split(' ');

                File.WriteAllLines(fullPath, wordList, Encoding.UTF8);
            }
            catch (IOException ex)
            {
                _logger.LogError(ex.Message);
                return (false, ex.Message);
            }

            return (true, string.Empty);
        }

        private static (bool IsValid, string Reason) ValidateFilename(string input)
        {
            if (!Path.GetExtension(input).Equals(".txt"))
                return (false, "Filename must end with .txt");

            return (true, string.Empty);
        }
    }
}
