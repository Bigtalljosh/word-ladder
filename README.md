# Word Ladder

## The Challenge

Write a console application in C#.NET that takes the following command-line arguments:

* DictionaryFile - the file name of a text file containing four letter words (included in the test pack)
* StartWord - a four letter word (that you can assume is found in the DictionaryFile file)
* EndWord - a four letter word (that you can assume is found in the DictionaryFile file) 
* ResultFile - the file name of a text file that will contain the result

Your program should calculate the shortest list of four letter words, starting with StartWord, and ending with EndWord, with a number of intermediate words that must appear in the DictionaryFile file where each word differs from the previous word by one letter only. The result should be written to the destination specified by the ResultFile argument.

For example, if StartWord = Spin, EndWord = Spot and DictionaryFile file contains:
* Spin
* Spit
* Spat
* Spot
* Span

Then ResultFile should contain:
* Spin
* Spit
* Spot

Two examples of incorrect results:
* Spin, Span, Spat, Spot (incorrect as it takes 3 changes rather than 2)
* Spin, Spon, Spot (incorrect as spon is not a word)

Your solution should deal with the case where the dictionary file is not in alphabetical order.

Please explain the thought/development process you went through to achieve your solution.  
Your solution should be both elegant and maintainable.  
Extra points awarded for solutions that have an emphasis on how the code will be tested, and performance implications.

---

## Thought Process

My initial reaction here was some kind of binary tree (I've been watching a lot of YouTube videos for traditional CS challenges recently)
However after researching this a bit more, it seems the preferred aproach is a breadth-first search(BFS).

Taking a look at the provided Dictionary File it's obvious that I'm going to need to sanitise this input.
The challenge states that we're only using 4 letter words, so I'm thinking a regular expression to strip all special characters
and numbers out, then any words greater than 4 in length.

Looking at the params we're expecting we're gonna need a little validation around those.
Check that we have provided a dictionaryFile param, check that file actually exists. 
Check that the startWord and the endWord are exactly 4 characters long, check those characters are all letters (no special characters or numbers)
Check that the startWord and the endWord are not the same.
Check that we pass a resultsFile name in, check that the filename is valid, as in no forbidden characters that the OS doesn't let you save, and we could also make sure we cover different OS's

Reading a little deeper some key points that should be covered in tests:

* I have to use the shortest path
I'm hoping the provided example is one I can rely on to craft a unit test. 
I'll check all those words are in the dictionary.

* Solution should deal with the case that the dictionary file may not be alphabetical
I'm going to try my solution on a sorted and unsorted word list, comparing results and performance