using MaximPractice.Controllers;
using MaximPractice.Services.StringConverter;
using MaximPractice.Services;
using MaximPractice.src.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaximPractice.Models;
using MaximPractice.src.Sorts;
using Microsoft.AspNetCore.Mvc;
using MaximPractice.src.Sorts.TreeSort;

namespace MaximPractice.Tests
{
    public class ServicesShould
    {
        private ExerciseService _exerciseService;
        [SetUp]
        public void Setup()
        {
            // Arrange
            var appSettings = new AppSettings
            {
                RandomApi = "https://www.randomnumberapi.com",
                Settings = new Settings
                {
                    BlackList = new string[] { "abc", "ab", "edf" },
                    ParallelLimit = 5
                }
            };
            var stringConverter = new StringConverterService(appSettings);
            _exerciseService = new ExerciseService(appSettings, stringConverter);
        }

        [TestCase("a", "aa")]
        [TestCase("abcdef", "cbafed")]
        [TestCase("abcde", "edcbaabcde")]
        public void StringConvertingTestEx1(string input, string expected)
        {
            // Act
            var actual = _exerciseService.ConvertString(input);
            // Assert
            Assert.That(actual.Value, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(CymbolCountTestServiceEx3Source))]
        public void CymbolCountTestEx3(string input, Dictionary<char, int> expected)
        {
            // Act
            var actual = _exerciseService.GetSymbolCount(input);

            // Assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        #region[Данные для теста задания 3]
        public static IEnumerable<object[]> CymbolCountTestServiceEx3Source
        {
            get
            {
                yield return new object[]
                {
                    "a",
                    new Dictionary<char, int>()
                    {
                        ['a'] = 1
                    }
                };

                yield return new object[]
                {
                    "abcdef",
                    new Dictionary<char, int>()
                    {
                        ['a'] = 1,
                        ['b'] = 1,
                        ['c'] = 1,
                        ['d'] = 1,
                        ['e'] = 1,
                        ['f'] = 1
                    }
                };

                yield return new object[]
                {
                    "abcde",
                    new Dictionary<char, int>()
                    {
                        ['a'] = 1,
                        ['b'] = 1,
                        ['c'] = 1,
                        ['d'] = 1,
                        ['e'] = 1
                    }
                };
            }
        }
        #endregion

        [TestCase("aa", "aa")]
        [TestCase("cbafed", "afe")]
        [TestCase("edcbaabcde", "edcbaabcde")]
        public void LongestSubstringTestEx4(string input, string expected)
        {
            // Act
            var actual = _exerciseService.GetLongestSubstring(input, "aeiouy");

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("aa", "aa")]
        [TestCase("cbafed", "abcdef")]
        [TestCase("edcbaabcde", "aabbccddee")]
        public void QuickSortTestEx5(string input, string expected)
        {
            // Act
            var actual = QuicksortString.Sort(input);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("aa", "aa")]
        [TestCase("cbafed", "abcdef")]
        [TestCase("edcbaabcde", "aabbccddee")]
        public void TreeSortTestEx5(string input, string expected)
        {
            // Act
            var actual = TreeSort.Sort(input);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
