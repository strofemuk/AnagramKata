using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnagramKata
{
    class Program
    {
        /// <summary>
        /// KATA06: Anagrams.  Apart from having some fun with words, this kata should make you think somewhat about algorithms. 
        /// The simplest algorithms to find all the anagram combinations may take inordinate amounts of time to do the job. 
        /// Working though alternatives should help bring the time down by orders of magnitude. To give you a possible point of comparison, 
        /// I hacked a solution together in 25 lines of Ruby. It runs on this wordlist in 1.8s on a 1.7GHz i7. It’s also an interesting exercise 
        /// in testing: can you write unit tests to verify that your code is working correctly before setting it to work on the full dictionary.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Anagrams anagrams = new Anagrams("wordlist.txt");

            List<string> eatAnagrams = anagrams.AnagramsFor("eat");
            Console.WriteLine("There are {0} anagrams in this wordlist for the word EAT", eatAnagrams.Count());
            eatAnagrams.ForEach(t => Console.Write("{0} ", t));
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Anagrams of more than 15 words in this list are:");
            anagrams.PrintAll(15);


            Console.ReadKey();

        }
    }

    public class Anagrams : Dictionary<string, List<string>>
    {
        /// <summary>
        /// Constructor for the Anagrams class that takes a filename and tries to load the word list from a file.
        /// The file must contain one word per line.
        /// </summary>
        /// <param name="fileName"></param>
        public Anagrams(string fileName)
        {
            try
            {
                ProcessWordList(File.ReadAllLines(fileName));
            }
            catch
            {
                Console.WriteLine("There is a problem reading the word list file.");
                throw;
            }
        }

        /// <summary>
        /// Constructor that takes a string array for the word list.
        /// </summary>
        /// <param name="wordList"></param>
        public Anagrams(string[] wordList)
        {
            ProcessWordList(wordList);
        }

        public List<string> AnagramsFor(string word)
        {
            List<string> returnList = new List<string>();
            string key = CreateKey(word);
            if (this.ContainsKey(key))
            {
                returnList = this[key];
            }
            return returnList;
        }

        /// <summary>
        /// Prints out the list of anagrams with coorsponding keys.  
        /// </summary>
        /// <param name="limit">The limit on the number in the set of anagrams.  2 will return all records that contain two or more anagrams.</param>
        public void PrintAll(int limit = 2)
        {
            this.Where(g => g.Value.Count() >= limit)
                .ToList()
                .ForEach(value => 
                {
                    Console.Write("{0}: ", value.Key);
                    value.Value.ForEach(word =>
                    {
                        Console.Write("{0} ", word);
                    });
                    Console.WriteLine();
                });
        }

        private string CreateKey(string word)
        {
            return new Regex("[^A-Za-z]")
                .Replace(string.Concat(word.OrderBy(c => c)), "")
                .ToUpper();
        }
        
        /// <summary>
        /// Processes the string array into the dicitionary inherited by the Anagrams class.
        /// </summary>
        /// <param name="wordList"></param>
        private void ProcessWordList(string[] wordList)
        {
            try
            {
                wordList
                    .GroupBy(key => CreateKey(key))
                    .Where(wordGroup => wordGroup.Count() > 1)
                    .ToList()
                    .ForEach( anagrams => this.Add(anagrams.Key,anagrams.ToList()));
            }
            catch
            {
                Console.WriteLine("There is a problem creating the Anagrams collection.");
                throw;
            }

        }
    }
}

