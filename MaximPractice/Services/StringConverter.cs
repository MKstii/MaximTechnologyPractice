using MaximPractice.src.Settings;
using Microsoft.AspNetCore.Mvc;

namespace MaximPractice.Services
{
    public class StringConverter
    {
        private AppSettings _appSettings;
        public StringConverter(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public StringConvertResult Convert(string str)
        {
            // Поиск слова в черном списке
            var exService = new ExerciseService(_appSettings);
            if (exService.InBlacklist(str))
            {
                var errorText = "Word in blacklist";
                return new StringConvertResult(string.Empty, false, errorText);
            }

            // Проверка только на английские символы нижнего регистра
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var errorChars = exService.GetInvalidChars(str, alphabet);
            if (errorChars.Count != 0)
            {
                var errorText = $"Invalid symbols: {String.Join(", ", errorChars)}";
                return new StringConvertResult(string.Empty, false, errorText);
            }
            
            var result = str.Length % 2 == 0 ?
                exService.ConvertEvenString(str) : exService.ConvertOddString(str);
            return new StringConvertResult(result, true, string.Empty);
        }
    }

    public class StringConvertResult
    {
        public string Value { get; }
        public bool IsSuccesed { get; }
        public string ErrorText { get; }
        public StringConvertResult(string value, bool isSuccesed, string errorText) 
        {
            Value = value;
            IsSuccesed = isSuccesed;
            ErrorText = errorText;
        }
    }
}
