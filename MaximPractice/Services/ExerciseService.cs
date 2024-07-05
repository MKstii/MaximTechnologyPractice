using MaximPractice.Services.StringConverter;
using MaximPractice.src.Settings;
using MaximPractice.src.Sorts;
using MaximPractice.src.Sorts.TreeSort;
using System.Text;

namespace MaximPractice.Services
{
    public class ExerciseService
    {
        private AppSettings _appSettings;
        private StringConverterService _stringConverterService;
        public ExerciseService(AppSettings appSetting, StringConverterService stringConverterService) 
        {
            _appSettings = appSetting;
            _stringConverterService = stringConverterService;
        } 
        public StringConvertResult ConvertString(string str)
        {
            return _stringConverterService.Convert(str);
        }
        public string GetLongestSubstring(string str, string alphabet)
        {
            string res = "";
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (alphabet.Contains(str[i]))
                {
                    for (int j = 1; j < str.Length - i + 1; j++)
                    {
                        var substring = str.Substring(i, j);
                        if (alphabet.Contains(substring[substring.Length - 1]))
                        {
                            if (substring.Length > res.Length)
                            {
                                res = substring;
                            }
                        }
                    }
                }
            }
            return res;
        }
        public Dictionary<char, int> GetSymbolCount(string str)
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
        public string SortString(string str, SortType sortType)
        {
            switch (sortType) 
            {
                case SortType.Quicksort: 
                    return QuicksortString.Sort(str);
                case SortType.TreeSort:
                    return TreeSort.Sort(str);
                default:
                    throw new Exception("Недопустимый тип сортировки");
            }
        }

        public string DeleteRandomSymbol(string str)
        {
            var symbolId = GetRandomNumber(str.Length).Result;

            var sb = new StringBuilder();
            sb.Append(str.Substring(0,symbolId));
            sb.Append(str.Substring(symbolId+1, str.Length - (symbolId+1)));

            return sb.ToString();
        }

        private async Task<int> GetRandomNumber(int maxNumber)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_appSettings.RandomApi);
            var response = await client.GetAsync($"/api/v1.0/random?min=0&max={maxNumber}");
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<int[]>();
                return res[0];
            }
            else
            {
                return GetRandomNumberLocal(maxNumber);
            }
        }

        private int GetRandomNumberLocal(int maxNumber)
        {
            Random rnd = new Random();
            return rnd.Next(maxNumber);
        }
    }
}
