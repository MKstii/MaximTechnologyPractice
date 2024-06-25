using System.Collections.Concurrent;
using System.Text;

namespace MaximPractice.src.Sorts
{
    public static class QuicksortString
    {

        public static string Sort(string str)
        {
            var strBuilder = new StringBuilder(str);
            Quicksort(strBuilder, 0, strBuilder.Length - 1);
            return strBuilder.ToString();
        }

        private static void Quicksort(StringBuilder strBuilder, int low, int high)
        {
            if (low < high)
            {
                var pivot = Partition(strBuilder, low, high);
                Quicksort(strBuilder, low, pivot - 1);
                Quicksort(strBuilder, pivot, high);
            }
        }

        private static int Partition(StringBuilder strBuilder, int low, int high)
        {
            var pivot = strBuilder[high];
            var i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (strBuilder[j] <= pivot)
                {
                    i++;
                    if (i != j)
                    {
                        Swap(strBuilder, i, j);
                    }
                }
            }
            Swap(strBuilder, i + 1, high);
            return i + 1;
        }

        private static void Swap(StringBuilder strBuilder, int first, int second)
        {
            var temp = strBuilder[first];
            strBuilder[first] = strBuilder[second];
            strBuilder[second] = temp;
        }
    }
}
