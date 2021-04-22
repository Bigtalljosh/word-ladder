using System.ComponentModel.DataAnnotations;

namespace WordLadder
{
    public class ApplicationArguments
    {
        /// <summary>
        /// The filename of the Dictionary File to use. Currently we only support .txt files.
        /// </summary>
        [Required]
        public string DictionaryFileName { get; set; }

        /// <summary>
        /// The filename of the results .txt file.
        /// </summary>
        [Required]
        public string ResultsFileName { get; set; }

        /// <summary>
        /// The Word to start the word ladder with.
        /// </summary>
        [Required]
        public string StartWord { get; set; }

        /// <summary>
        /// The Word to end the word ladder at.
        /// </summary>
        [Required]
        public string EndWord { get; set; }
    }
}
