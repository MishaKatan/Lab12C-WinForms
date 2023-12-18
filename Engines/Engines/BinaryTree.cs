using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Engines
{
    public class BinaryTree<T>
    {
        private TreeNode<T>? root = null; // корневой элемент
        private TreeNode<double>? intRoot; // корневой элемент для работы с деревом чисел
        private List<string> modelNames = new List<string>();
        public List<double> Powers = new List<double>();
        public int Count { get; private set; } // количество элементов
        public TreeNode<T>? Root => root;
        public TreeNode<double>? IntRoot => intRoot;
        public BinaryTree() // пустая коллекция 
        {
            root = null;
        }
        public BinaryTree(int count, List<double> Powers)
        {
            Count = count;
            intRoot = CreateBalancedTree(Powers);
        }
        public BinaryTree(int count) // создание с заданной емкостью 
        {
            Count = count;
            root = Gen(count, root);
        }

        public TreeNode<T> Gen(int count, TreeNode<T>? root) // генерация дерева
        {
            if (count == 0) // условие выхода из рекурсии
                return root;

            int left = count / 2; // производим балансировку дерева, расчитываем количество левых узлов (нод)
            int right = count - left - 1; // не учитываем корень дерева (рут) - вычитаем единицу

            Engine motor1 = new Engine();
            motor1.RandomInit(); // генерируем рандомный элемент
            modelNames.Add(motor1.ModelName);
            Powers.Add(motor1.Power);
            root = new TreeNode<T>((T)(object)motor1); // корень дерева

            root.Left = Gen(left, root.Left); // рекурсивно вызываем метод для генерации левой ветки
            root.Right = Gen(right, root.Right); // генерируем правую ветку
            return root;
        }
        public void ShowByLevels() // вывод дерева по уровням
        {
            if (root != null)
                ShowByLevelsHelper(root, 3);
            else
                Console.WriteLine("Дерево не содержит элементов");
        }
        public void ShowByLevelsForBallanceTree() // вывод дерева по уровням
        {
            if (intRoot != null)
                ShowByLevelsHelper(intRoot, 3);
            else
                Console.WriteLine("Дерево не содержит элементов");
        }
        private void ShowByLevelsHelper(TreeNode<T> node, int l) // вспомогательный рекурсивный метод для отображения дерева по уровням
        {
            if (node != null)
            {
                ShowByLevelsHelper(node.Left, l + 3);
                for (int i = 0; i < l; i++)
                    Console.Write("  ");

                Engine motor = (Engine)(object)node.Data;
                if (motor is not null)
                    Console.WriteLine(motor.ModelName);
                ShowByLevelsHelper(node.Right, l + 3);
            }
        }
        private void ShowByLevelsHelper(TreeNode<double> node, int l) // перегруженный вспомогательный рекурсивный метод для отображения дерева по уровням
        {
            if (node != null)
            {
                ShowByLevelsHelper(node.Left, l + 3);
                for (int i = 0; i < l; i++)
                    Console.Write("  ");

                var motor = node.Data;
                //if (motor is not null)
                Console.WriteLine(motor);
                ShowByLevelsHelper(node.Right, l + 3);
            }
        }
        public void Clear() // удаление дерева 
        {
            if (root == null)
                Console.WriteLine("Дерево не содержит элементов");
            else
            {
                root = null;
                Count = 0;
                GC.Collect();
                Console.WriteLine("Дерево успешно удалено");
            }
        }
        public string RemoveByLetter(char letter)
        {
            int Count1 = 0;
            foreach (var item in modelNames)
                if (item[0] == letter)
                    Count1++;
            return (Count1.ToString() + $" двигателей с названием с буквы {letter}");
        }// название не поменял, думал удалять надо, а оказывается просто посчитать
        public TreeNode<double> CreateBalancedTree(List<double> Powers)
        {
            Powers.Sort();
            if (Powers == null || Powers.Count == 0)
                return null;

            return CreateBalancedTree(Powers, 0, Powers.Count - 1);
        }
        private static TreeNode<double> CreateBalancedTree(List<double> values, int start, int end)
        {
            if (start > end)
                return null;

            int mid = (start + end) / 2;
            TreeNode<double> node = new TreeNode<double>(values[mid]);
            node.Right = CreateBalancedTree(values, start, mid - 1);
            node.Left = CreateBalancedTree(values, mid + 1, end);

            return node;
        }
    }
}
