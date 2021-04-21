# Word Ladder

## The Challenge

Write a console application in C#.NET that takes the following command-line arguments:

* DictionaryFile - the file name of a text file containing four letter words (included in the test pack)
* StartWord - a four letter word (that you can assume is found in the DictionaryFile file)
* EndWord - a four letter word (that you can assume is found in the DictionaryFile file) 
* ResultFile - the file name of a text file that will contain the result

Your program should calculate the shortest list of four letter words
Starting with StartWord, and ending with EndWord, with a number of intermediate words that must appear in the DictionaryFile file where each word differs from the previous word by one letter only. 
The result should be written to the destination specified by the ResultFile argument.

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

## Usage

In order to run the application from the CLI - Build the solution, navigate to the generated files and from the command line type:
`.\WordLadder.exe --DictionaryFileName=words-english.txt --StartWord=spin --EndWord=spot --ResultsFileName=results.txt`

You should be able to change the params as you see fit

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

## Steps taken

First I crafted a quick console app, I've used a library called FluentCommandLineParser as this is a neat package to let you quickly express what arguments you're expecting and even throw up a helpful dialogue for the user.
This also nicely maps my arguments to an object for me to pass around if needs be.

Next I started writing some of the test cases listed above, and a class to handle file IO. 

After that was compelte I started writing the WordLadderApp class which will handle the bulk of the application logic.
In here contains the validation logic for the words in the word list. I strip out all the digits, special characters, and words longer than 4 letters.
Covered this class in tests to cover various scenarios that would prevent us being able to generate a word ladder due to bad params.

Now I moved onto the meaty part which was writing the algorithm to actually generate the word ladder.
I had a very rough idea how to tackle this, but it did require me to do some research.
During my research I discovered that a binary tree was not the correct approach I had initially thought and instead generating a graph and performing a breadth first search was the better option here. 
Neither of these things are things I write very often so I took some time to relearn the concepts and after a few iterations my algorithm was producing the expected output.
Writing the tests with expected data really helped me here as I was tweakind and tidying the code I was able to ensure I didn't break anything

Once I was happy with the funcationality of the solution, I spent some time to tidy and document the code a little.
I'm not a fan of covering code in comments, code should be self documenting and generally quite readable without the aid of comments but I felt for the solver algorithm specifically
That could do with some comments just to ease the understanding of what's happening and why.

Another time where the tests served me well is when I refactored the application to use a hosted service worker and use dotnet dependency injection.
I was able to restructre the code with the confidence that the logic of the application remained working.
I've done this in order to show off some newer framework features.

I did intend to add some benchmarks into the application to prove if sorting the dictionary or removing duplicates notibly affected performance but I've ran out of time.