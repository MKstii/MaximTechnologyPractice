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
                return Ok(res);
            }
            else
            {
                return BadRequest($"Invalid symbols: {String.Join(", ", errorChars)}");
            }
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
