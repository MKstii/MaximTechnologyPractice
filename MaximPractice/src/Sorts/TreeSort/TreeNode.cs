using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MaximPractice.src.Sorts.TreeSort
{
    public class TreeNode
    {
        public char Value { get; }
        private TreeNode? leftChild;
        private TreeNode? rightChild;
        public TreeNode(char chr) 
        {
           Value = chr;
        }

        public void AddNode(char chr)
        {
            if(chr <= Value)
            {
                if(leftChild == null)
                {
                    leftChild = new TreeNode(chr);
                }
                else
                {
                    leftChild.AddNode(chr);
                }
            }
            else
            {
                if(rightChild == null)
                {
                    rightChild = new TreeNode(chr);
                }
                else
                {
                    rightChild.AddNode(chr);
                }
            }
        }

        public string ConvertToString()
        {
            var sb = new StringBuilder();

            if(leftChild != null)
            {
                sb.Append(leftChild.ConvertToString());
            }

            sb.Append(Value);

            if(rightChild != null)
            {
                sb.Append(rightChild.ConvertToString());
            }

            return sb.ToString();
        }
    }
}
