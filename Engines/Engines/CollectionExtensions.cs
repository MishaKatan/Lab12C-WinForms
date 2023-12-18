using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Engines
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> GetData<T>(this IEnumerable<T> source, Func<T, bool> condition) // ищет по условию 
        {
            if (source == null || condition == null) 
                throw new ArgumentNullException();

            foreach (var item in source) 
                if (condition(item)) 
                    yield return item;
        }

        public static double GetAmount<T>(this IEnumerable<T> source, Func<T, double> addFunction)
        {
            if (source == null || addFunction == null)
                throw new ArgumentNullException();

            double sum = default;

            foreach (var item in source)
                sum += addFunction(item);

            return sum;
        }

        public static double GetMax<T>(this IEnumerable<T> source, Func<T, double> compareFunction)
        {
            if (source == null || compareFunction == null)
                throw new ArgumentNullException();

            double max = int.MinValue;

            foreach (var item in source)
                if (compareFunction(item) > max)
                    max = compareFunction(item);
            return max;
        }

        public static double GetMin<T>(this IEnumerable<T> source, Func<T, double> compareFunction)
        {
            if (source == null || compareFunction == null)
                throw new ArgumentNullException();

            double min = int.MaxValue;

            foreach (var item in source)
                if (compareFunction(item) < min)
                    min = compareFunction(item);
            return min;
        }

        public static double GetAverage<T>(this IEnumerable<T> source, Func<T, double> convertFunction)
        {
            if (source == null || convertFunction == null)
                throw new ArgumentNullException();

            double sum = 0;
            int count = 0;

            foreach (var item in source)
            {
                sum += convertFunction(item);
                count++;
            }

            if (count == 0)
                throw new InvalidOperationException("Коллекция пустая");

            return sum / count;
        }

        public static IEnumerable<T> SortBinarySearchTree<T>(this BinarySearchTree<T> source, Func<T, double> comparison) where T : IComparable<T>
        {
            if (source == null || comparison == null)
                throw new ArgumentNullException();

            List<T> list = new List<T>();
            foreach (var item in source)
                list.Add(item);
            list.Sort();
            source.Root = Rebalance(list, 0, list.Count - 1);
            static TreeNode<T> Rebalance(List<T> values, int start, int end)
            {
                if (start > end)
                    return null;

                int mid = (start + end) / 2;
                TreeNode<T> node = new TreeNode<T>(values[mid]);
                node.Left = Rebalance(values, start, mid - 1);
                node.Right = Rebalance(values, mid + 1, end);

                return node;
            }
            return source;
        }
    }
}

