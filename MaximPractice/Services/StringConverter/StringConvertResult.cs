namespace MaximPractice.Services.StringConverter
{
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
