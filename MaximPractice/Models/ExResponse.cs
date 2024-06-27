namespace MaximPractice.Models
{
    public class ExResponse
    {
        public string? ConvertedString { get; set; }
        public Dictionary<char, int>? SymbolCount { get; set; }
        public string? LongestSubstring { get; set; }
        public string? SortedString { get; set; }
        public string? WithRandomDeletedSymbol { get; set; }

        public ExResponse() { }
    }
}
