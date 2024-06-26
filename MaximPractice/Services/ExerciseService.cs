﻿using MaximPractice.src.Sorts;
using MaximPractice.src.Sorts.TreeSort;
using System.Text;

namespace MaximPractice.Services
{
    public class ExerciseService
    {
        public ExerciseService() { } 
        public string ConverString(string str)
        {
            return str.Length % 2 == 0 ?
                ConvertEvenString(str) : ConvertOddString(str);
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

        public List<char> GetInvalidChars(string str, string alphabet)
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
    }
}
