using System.Text;

namespace MaximPractice.src.Sorts.TreeSort
{
    public static class TreeSort
    {
        public static string Sort(string str)
        {
            var treeNode = new TreeNode(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                treeNode.AddNode(str[i]);
            }
            return treeNode.ConvertToString();
        }
    }
}
