using MaximPractice.src.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MaximPractice.Services.StringConverter
{
    public class StringConverterService
    {
        private AppSettings _appSettings;
        public StringConverterService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public StringConvertResult Convert(string str)
        {
            // Поиск слова в черном списке
            if (InBlacklist(str))
            {
                var errorText = "Word in blacklist";
                return new StringConvertResult(string.Empty, false, errorText);
            }

            // Проверка только на английские символы нижнего регистра
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var errorChars = GetInvalidChars(str, alphabet);
            if (errorChars.Count != 0)
            {
                var errorText = $"Invalid symbols: {string.Join(", ", errorChars)}";
                return new StringConvertResult(string.Empty, false, errorText);
            }

            var result = str.Length % 2 == 0 ?
                ConvertEvenString(str) : ConvertOddString(str);
            return new StringConvertResult(result, true, string.Empty);
        }
        private bool InBlacklist(string str)
        {
            return _appSettings.Settings.BlackList.Contains(str);
        }
        private string ConvertEvenString(string str)
        {
            var strBuilder = new StringBuilder();

            var firstHalf = ReverseString(str, 0, (str.Length - 1) / 2);
            var secondHalf = ReverseString(str, str.Length / 2, str.Length - 1);

            strBuilder.Append(firstHalf);
            strBuilder.Append(secondHalf);

            return strBuilder.ToString();
        }
        private string ConvertOddString(string str)
        {
            var strBuilder = new StringBuilder();

            var firstHalf = ReverseString(str, 0, str.Length - 1);

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
    }
}
