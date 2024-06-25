using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MaximPractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Ex1Controller : ControllerBase
    {
        private ILogger<Ex1Controller> _logger;

        public Ex1Controller(ILogger<Ex1Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Ex1(string str)
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var errorChars = GetInvalidChars(str, alphabet);

            if (errorChars.Count == 0)
            {
                string res = str.Length % 2 == 0 ?
                ConvertEvenString(str) : ConvertOddString(str);

                var cymbolCount = GetSymbolCount(str);

                var vowelAlphabet = "aeiouy";
                var longestSubstring = GetLongestSubstring(res, vowelAlphabet);

                var answer = new
                {
                    result = res,
                    cymbolCount = cymbolCount,
                    longestSubstring = longestSubstring
                };
                return Ok(answer);
            }
            else
            {
                return BadRequest($"Invalid symbols: {String.Join(", ", errorChars)}");
            }
        }

        private string GetLongestSubstring(string str, string alphabet)
        {
            string res = "";
            for (int i = 0; i < str.Length-1; i++)
            {
                if (alphabet.Contains(str[i]))
                {
                    for (int j = 1; j < str.Length - i+1; j++)
                    {
                        var substring = str.Substring(i,j);
                        if (alphabet.Contains(substring[substring.Length - 1]))
                        {
                            if(substring.Length > res.Length)
                            {
                                res = substring;
                            }
                        }
                    }
                }
            }
            return res;
        }

        private Dictionary<char, int> GetSymbolCount(string str)
        {
            var res = new Dictionary<char, int>();
            foreach (var chr in str)
            {
                if (res.ContainsKey(chr))
                {
                    res[chr] = res[chr] + 1;
                }
                else
                {
                    res.Add(chr, 1);
                }
            }
            return res;
        }

        private List<char> GetInvalidChars(string str, string alphabet)
        {
            var errorChars = new List<char>();
            foreach (var chr in str)
            {
                if (!alphabet.Contains(chr))
                {
                    errorChars.Add(chr);
                }
            }
            return errorChars;
        }

        private string ConvertEvenString(string str)
        {
            var strBuilder = new StringBuilder();

            var firstHalf = ReverseString(str, 0, (str.Length - 1) / 2);
            var secondHalf = ReverseString(str, str.Length / 2, str.Length-1);

            strBuilder.Append(firstHalf);
            strBuilder.Append(secondHalf);

            return strBuilder.ToString();
        }

        private string ConvertOddString(string str)
        {
            var strBuilder = new StringBuilder();

            var firstHalf = ReverseString(str, 0, str.Length-1);

            strBuilder.Append(firstHalf);
            strBuilder.Append(str);

            return strBuilder.ToString();
        }

        private string ReverseString(string str, int start, int end)
        {
            var strBuilder = new StringBuilder();
            for (int i = end; i >= start; i--)
            {
                strBuilder.Append(str[i]);
            }
            return strBuilder.ToString();
        }
    }
}
