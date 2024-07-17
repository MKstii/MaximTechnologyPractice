using MaximPractice.Controllers;
using MaximPractice.Models;
using MaximPractice.Services;
using MaximPractice.Services.StringConverter;
using MaximPractice.src.Settings;
using MaximPractice.src.Sorts;
using Microsoft.AspNetCore.Mvc;

namespace MaximPractice.Tests
{
    public class ControllerShould
    {
        private ExerciseService _exerciseService;
        private Ex1Controller _ex1Controller;
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
            _ex1Controller = new Ex1Controller(_exerciseService);
        }

        [TestCase("a", "aa")]
        [TestCase("abcdef", "cbafed")]
        [TestCase("abcde", "edcbaabcde")]
        public void StringConvertingTestEx1(string input, string expected)
        {
            // Act
            var actual = _ex1Controller.Ex1(input, SortType.TreeSort);

            // Assert
            if (actual is OkObjectResult okRequest)
            {
                Assert.That(okRequest.Value, Is.Not.Null);
                Assert.That(okRequest.Value, Is.TypeOf<ExResponse>());
                var response = (ExResponse)okRequest.Value;
                Assert.That(response.ConvertedString, Is.Not.Null);
                Assert.That(response.ConvertedString, Is.EqualTo(expected));
            }
            else
            {
                Assert.Fail($"Expected OkObjectResult but {actual.GetType()}");
            }
        }

        [TestCase("AaA", "A, A")]
        [TestCase("РУС", "Р, У, С")]
        [TestCase("123p", "1, 2, 3")]
        public void LowerRegistryOnlyExceptionEx2(string input, string expected)
        {
            // Act
            var actual = _ex1Controller.Ex1(input, SortType.TreeSort);

            // Assert
            if (actual is BadRequestObjectResult badRequest)
            {
                Assert.That(badRequest.Value, Is.Not.Null);
                StringAssert.Contains(expected, badRequest.Value.ToString());
            }
            else
            {
                Assert.Fail($"Expected BadRequestObjectResult but {actual.GetType()}");
            }
        }

        [TestCaseSource(nameof(CymbolCountTestControllerEx3Source))]
        public void CymbolCountTestEx3(string input, Dictionary<char, int> expected)
        {
            // Act
            var actual = _ex1Controller.Ex1(input, SortType.TreeSort);

            // Assert
            if (actual is OkObjectResult okRequest)
            {
                Assert.That(okRequest.Value, Is.Not.Null);
                Assert.That(okRequest.Value, Is.TypeOf<ExResponse>());
                var response = (ExResponse)okRequest.Value;
                Assert.That(response.SymbolCount, Is.Not.Null);
                CollectionAssert.AreEquivalent(expected, response.SymbolCount);
            }
            else
            {
                Assert.Fail($"Expected OkObjectResult but {actual.GetType()}");
            }
        }

        #region[Данные для теста задания 3 для контроллера]
        public static IEnumerable<object[]> CymbolCountTestControllerEx3Source
        {
            get
            {
                yield return new object[] 
                {
                    "a",
                    new Dictionary<char, int>() 
                    { 
                        ['a'] = 2 
                    } 
                };

                yield return new object[] 
                { 
                    "abcdef",
                    new Dictionary<char, int>() 
                    { 
                        ['c'] = 1,
                        ['b'] = 1,
                        ['a'] = 1,
                        ['f'] = 1,
                        ['e'] = 1,
                        ['d'] = 1
                    } 
                };

                yield return new object[] 
                {
                    "abcde",
                    new Dictionary<char, int>() 
                    { 
                        ['e'] = 2,
                        ['d'] = 2,
                        ['c'] = 2,
                        ['b'] = 2,
                        ['a'] = 2
                    } 
                };
            }
        }
        #endregion
        

        [TestCase("a", "aa")]
        [TestCase("abcdef", "afe")]
        [TestCase("abcde", "edcbaabcde")]
        public void LongestSubstringTestEx4(string input, string expected)
        {
            // Act
            var actual = _ex1Controller.Ex1(input, SortType.TreeSort);

            // Assert
            if (actual is OkObjectResult okRequest)
            {
                Assert.That(okRequest.Value, Is.Not.Null);
                Assert.That(okRequest.Value, Is.TypeOf<ExResponse>());
                var response = (ExResponse)okRequest.Value;
                Assert.That(response.LongestSubstring, Is.Not.Null);
                Assert.That(response.LongestSubstring, Is.EqualTo(expected));
            }
            else
            {
                Assert.Fail($"Expected OkObjectResult but {actual.GetType()}");
            }
        }

        [TestCase("a", SortType.TreeSort, "aa")]
        [TestCase("a", SortType.Quicksort, "aa")]
        [TestCase("abcdef", SortType.TreeSort, "abcdef")]
        [TestCase("abcdef", SortType.Quicksort, "abcdef")]
        [TestCase("abcde", SortType.TreeSort, "aabbccddee")]
        [TestCase("abcde", SortType.Quicksort, "aabbccddee")]
        public void SortTestEx5(string input, SortType sortType, string expected)
        {
            // Act
            var actual = _ex1Controller.Ex1(input, sortType);

            // Assert
            if (actual is OkObjectResult okRequest)
            {
                Assert.That(okRequest.Value, Is.Not.Null);
                Assert.That(okRequest.Value, Is.TypeOf<ExResponse>());
                var response = (ExResponse)okRequest.Value;
                Assert.That(response.SortedString, Is.Not.Null);
                Assert.That(response.SortedString, Is.EqualTo(expected));
            }
            else
            {
                Assert.Fail($"Expected OkObjectResult but {actual.GetType()}");
            }
        }
    }
}