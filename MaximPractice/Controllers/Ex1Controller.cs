using MaximPractice.Services;
using MaximPractice.Models;
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
                var response = new ExResponse();

                string res = _exerciseService.ConverString(str);

                response.ConvertedString = res;
                response.SymbolCount = _exerciseService.GetSymbolCount(str);

                var vowelAlphabet = "aeiouy";
                response.LongestSubstring = _exerciseService.GetLongestSubstring(res, vowelAlphabet);

                response.SortedString = _exerciseService.SortString(res, sortType);

                response.WithRandomDeletedSymbol = _exerciseService.DeleteRandomSymbol(res);

                return Ok(response);
            }
            else
            {
                return BadRequest($"Invalid symbols: {String.Join(", ", errorChars)}");
            }
        }
    }
}
