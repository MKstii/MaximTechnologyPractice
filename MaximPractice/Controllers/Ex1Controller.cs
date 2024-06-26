using MaximPractice.Services;
using MaximPractice.src.Sorts;
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
        private ExerciseService _exerciseService;

        public Ex1Controller(ILogger<Ex1Controller> logger)
        {
            _logger = logger;
            _exerciseService = new ExerciseService();
        }

        [HttpGet]
        public IActionResult Ex1(string str, SortType sortType)
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var errorChars = _exerciseService.GetInvalidChars(str, alphabet);

            if (errorChars.Count == 0)
            {
                string res = _exerciseService.ConverString(str);

                var cymbolCount = _exerciseService.GetSymbolCount(str);

                var vowelAlphabet = "aeiouy";
                var longestSubstring = _exerciseService.GetLongestSubstring(res, vowelAlphabet);

                var sortedString = _exerciseService.SortString(res, sortType);

                var withDeletedRandomSymbol = _exerciseService.DeleteRandomSymbol(res);

                var answer = new
                {
                    result = res,
                    cymbolCount = cymbolCount,
                    longestSubstring = longestSubstring,
                    sortedString = sortedString,
                    withDeletedRandomSymbol = withDeletedRandomSymbol
                };
                return Ok(answer);
            }
            else
            {
                return BadRequest($"Invalid symbols: {String.Join(", ", errorChars)}");
            }
        }
    }
}
