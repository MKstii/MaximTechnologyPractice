using MaximPractice.Services;
using MaximPractice.Models;
using MaximPractice.src.Sorts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MaximPractice.src.Settings;
using Microsoft.Extensions.Options;

namespace MaximPractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Ex1Controller : ControllerBase
    {
        private ILogger<Ex1Controller> _logger;
        private ExerciseService _exerciseService;
        private AppSettings _appSettings;

        public Ex1Controller(ILogger<Ex1Controller> logger, AppSettings appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
            _exerciseService = new ExerciseService(appSettings);
        }

        [HttpGet]
        public IActionResult Ex1(string str, SortType sortType)
        {
            var convertResult = _exerciseService.ConverString(str);
            if (convertResult.IsSuccesed)
            {
                var response = new ExResponse();

                string res = convertResult.Value;

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
                return BadRequest(convertResult.ErrorText);
            }
        }
    }
}
