using System.Collections.Generic;
using System.Text;

namespace WordLadder.Solvers
{
    public class WordLadderSolver : IWordLadderSolver
    {
        private const char _wildcardCharacter = '*';

        /* This is our graph of words
         * The key will be the word itself with the _wildcardCharacter replaced in each position
         * The Hashset contains the whole words that can be made by replacing the wildcard character */
        private static Dictionary<string, HashSet<string>> _graph;

        // This Dictionary is to store the possible paths
        private static Dictionary<string, List<List<string>>> _paths;

        // This is a collection of words we have already visited in the graph 
        private static HashSet<string> _visited;

        public WordLadderSolver()
        {
            _graph = new();
            _paths = new();
            _visited = new();
        }

        /// <summary>
        /// Finds the Shortest possible ladder from the startWord to the endWord in a given wordList.
        /// </summary>
        /// <param name="startWord">The word to start the ladder from</param>
        /// <param name="endWord">The word to end the ladder at</param>
        /// <param name="wordList">The word list to traverse to produce the ladder</param>
        /// <returns></returns>
        public List<string> FindShortestLadder(string startWord, string endWord, List<string> wordList)
        {
            InitialiseGraph(startWord, wordList);

            Queue<string> queue = new();
            queue.Enqueue(startWord);

            _paths[startWord] = new() { new() { startWord } };

            while (queue.Count > 0)
            {
                var processingWord = queue.Dequeue();

                if (processingWord.Equals(endWord))
                {
                    return _paths[endWord][0];
                }

                _visited.Add(processingWord);

                // Transform word to intermidiate words and find matches in the mappings we created for each word
                for (int i = 0; i < processingWord.Length; i++)
                {
                    StringBuilder sb = new(processingWord);
                    sb[i] = _wildcardCharacter;
                    var transformedWord = sb.ToString();

                    foreach (var word in _graph[transformedWord])
                    {
                        if (!_visited.Contains(word))
                        {
                            GenerateAdjacentPaths(processingWord, word);
                            // Place the next word on the queue to repeat the process for the next word in the ladder
                            queue.Enqueue(word);
                        }
                    }
                }
            }

            return new List<string> { "No Ladder Can Be Found" };
        }

        /// <summary>
        /// Initialise our graph of words
        /// </summary>
        /// <param name="startWord"></param>
        /// <param name="wordList"></param>
        private void InitialiseGraph(string startWord, List<string> wordList)
        {
            AddWordMappingToGraph(startWord, _graph);

            foreach (var word in wordList)
                AddWordMappingToGraph(word, _graph);
        }

        /// <summary>
        /// This method genereates a mapping from each word replacing each character with a wildcard so we can create
        /// a graph of words that could link from the input word. 
        /// For example if we take 'Cat' we could have a mapping of: *at, c*t, ca*
        /// We store all these mappings in a dictionary where the key is the input word
        /// </summary>
        /// <param name="word">The input word to map</param>
        /// <param name="graph">The graph to store the mappings in</param>
        private void AddWordMappingToGraph(string word, Dictionary<string, HashSet<string>> graph)
        {
            for (int i = 0; i < word.Length; i++)
            {
                StringBuilder sb = new(word);
                sb[i] = _wildcardCharacter;

                if (graph.ContainsKey(sb.ToString()))
                {
                    graph[sb.ToString()].Add(word);
                }
                else
                {
                    HashSet<string> set = new() { word };
                    graph[sb.ToString()] = set;
                }
            }
        }

        /// <summary>
        /// Generates the paths to adjacent words from our processing word to our transformed word
        /// </summary>
        /// <param name="processingWord"></param>
        /// <param name="word"></param>
        private void GenerateAdjacentPaths(string processingWord, string word)
        {
            foreach (var path in _paths[processingWord])
            {
                var newPath = new List<string>(path) { word };

                if (!_paths.ContainsKey(word))
                {
                    _paths[word] = new() { newPath };
                } // We are only interested in the shortest path
                else if (_paths[word][0].Count >= newPath.Count)
                {
                    _paths[word].Add(newPath);
                }
            }
        }
    }
}
