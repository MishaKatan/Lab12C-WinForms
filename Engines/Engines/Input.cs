using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engines
{
    public class Input
    {
        public static int Int()
        {
            bool isParse = int.TryParse(Console.ReadLine(), out int number);
            while (!isParse || number < 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Ошибка ввода. Введите целое положительное число:");
                isParse = int.TryParse(Console.ReadLine(), out number);
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            return number;
        }
        public static double Double()
        {
            bool isParse = double.TryParse(Console.ReadLine(), out double number);
            while (!isParse || number<0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Ошибка ввода. Введите положительное число:");
                isParse = double.TryParse(Console.ReadLine(), out number);
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            return number;
        }
        public static string String()
        {
            string line = Console.ReadLine();
            while (line == null || line.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Введите строку:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                line = Console.ReadLine();
            }
            return line;
        }
    }
}
