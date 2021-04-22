using System.Collections.Generic;

namespace WordLadder.IO
{
    public interface IFileManager
    {
        (bool Success, string Reason) TryLoadDictionaryFile(string fileName, ref List<string> wordList);
        (bool Success, string Reason) TryWriteResultsFile(string fileName, string wordLadder);
    }
}