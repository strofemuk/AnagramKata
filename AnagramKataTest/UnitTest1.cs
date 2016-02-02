using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnagramKata;

namespace AnagramKataTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AbesBase()
        {
            string[] wordList = { "abe's", "base","race","car","care"};

            Anagrams anagrams = new Anagrams(wordList);

            Assert.IsTrue(anagrams["ABES"].Count == 2);
        }
    }
}
