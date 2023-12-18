using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace Engines
{
    public class TestCollections
    {
        Random random = new Random();
        public Stack<Engine> stackEngines;
        public Stack<string> stackNames;
        public Dictionary<Engine, Diesel> dictionaryDiesel;
        public Dictionary<string, Diesel> dictionaryNames;

        public TestCollections()
        {
            stackEngines = new Stack<Engine>();
            stackNames = new Stack<string>();
            dictionaryDiesel = new Dictionary<Engine, Diesel>();
            dictionaryNames = new Dictionary<string, Diesel>();
            for (int i = 0; i < 1000; i++) // генерация случайных объектов Engine и Diesel
            {
                Diesel motorD = new Diesel();
                motorD.RandomInit();
                string str = motorD.ModelName;
                motorD.ModelName = "№" + i + "-" + str;
                Engine motorE = new Engine();
                motorE = motorD.GetBaseEngine();
                stackEngines.Push(motorE);                 // добавление элементов в коллекции
                stackNames.Push(motorE.ModelName);
                dictionaryDiesel.Add(motorE, motorD);
                dictionaryNames.Add(motorE.ModelName, motorD);
            }
        }
        public static void ShowStackEngines(TestCollections collections)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\n\nКоллекция Stack :");
            foreach (var item in collections.stackEngines)
                Console.WriteLine($"{item}");
        }
        public static void ShowQueueNames(TestCollections collections)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\nКоллекция Stack названия двигателей:");
            foreach (var item in collections.stackNames)
                Console.WriteLine($"{item}");
        }
        public static void ShowDictionaryEngines(TestCollections collections)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n\nКоллекция Dictionary (ключ - двигатель, значение - дизель):");
            foreach (var item in collections.dictionaryDiesel)
            {
                Console.WriteLine($"Ключ: {item.Key}");
                Console.WriteLine($"Значение: {item.Value}\n");
            }
        }
        public static void ShowDictionaryEnginesNames(TestCollections collections)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\n\nКоллекция Dictionary (ключ - название двигателя, значение - дизель):");
            foreach (var item in collections.dictionaryNames)
                Console.WriteLine($"Ключ: {item.Key} Значение: {item.Value}");
        }
        public static void TimeFindInstackEngines(TestCollections collections)
        {
            // Нахождение элементов для применения метода Contains()  
            Engine firstObj = collections.stackEngines.Peek();
            Engine[] engines = collections.stackEngines.ToArray();
            Engine centralObj = engines[500];
            Engine lastObj = engines[999];
            Engine excessObj = new Engine();

            Timer(collections, firstObj, "1");
            Timer(collections, centralObj, "2");
            Timer(collections, lastObj, "3");
            Timer(collections, excessObj, "4");
        }
        public static void TimeFindInstackNames(TestCollections collections)
        {
            string firstObj = collections.stackNames.Peek();
            string[] engines = collections.stackNames.ToArray();
            string centralObj = engines[500];
            string lastObj = engines[999];
            string excessObj = "";

            Timer(collections, firstObj, "1");
            Timer(collections, centralObj, "2");
            Timer(collections, lastObj, "3");
            Timer(collections, excessObj, "4");
        }
        public static void TimeFindIndictionaryDiesel(TestCollections collections)
        {
            var enumerator = collections.dictionaryDiesel.GetEnumerator();
            enumerator.MoveNext();
            Engine firstObj = enumerator.Current.Key;

            var enumerator2 = collections.dictionaryDiesel.GetEnumerator();
            for (int i = 0; i < 501; i++)
                enumerator2.MoveNext();
            Engine centralObj = enumerator2.Current.Key;

            var enumerator3 = collections.dictionaryDiesel.GetEnumerator();
            for (int i = 0; i < 1000; i++)
                enumerator3.MoveNext();
            Engine lastObj = enumerator3.Current.Key;

            Engine excessObj = new Engine();

            Timerd(collections, firstObj, "1");
            Timerd(collections, centralObj, "2");
            Timerd(collections, lastObj, "3");
            Timerd(collections, excessObj, "4");
        }
        public static void TimeFindIndictionaryNames(TestCollections collections)
        {
            var enumerator = collections.dictionaryNames.GetEnumerator();
            enumerator.MoveNext();
            string firstObj = enumerator.Current.Key;

            var enumerator2 = collections.dictionaryNames.GetEnumerator();
            for (int i = 0; i < 500; i++)
                enumerator2.MoveNext();
            string centralObj = enumerator2.Current.Key;

            var enumerator3 = collections.dictionaryNames.GetEnumerator();
            for (int i = 0; i < 1000; i++)
                enumerator3.MoveNext();
            string lastObj = enumerator3.Current.Key;

            string excessObj = "";

            Timerd(collections, firstObj, "1");
            Timerd(collections, centralObj, "2");
            Timerd(collections, lastObj, "3");
            Timerd(collections, excessObj, "4");
        }
        public static void TimeFindIndictionaryDieselByValue(TestCollections collections)
        {
            var enumerator = collections.dictionaryDiesel.GetEnumerator();
            enumerator.MoveNext();
            Diesel firstObj = enumerator.Current.Value;

            var enumerator2 = collections.dictionaryDiesel.GetEnumerator();
            for (int i = 0; i < 500; i++)
                enumerator2.MoveNext();
            Diesel centralObj = enumerator2.Current.Value;

            var enumerator3 = collections.dictionaryDiesel.GetEnumerator();
            for (int i = 0; i < 1000; i++)
                enumerator3.MoveNext();
            Diesel lastObj = enumerator3.Current.Value;

            Diesel excessObj = new Diesel();

            Timerd(collections, firstObj, "1");
            Timerd(collections, centralObj, "2");
            Timerd(collections, lastObj, "3");
            Timerd(collections, excessObj, "4");
        }
        public static void Timer(TestCollections collections, Engine desiredObj, string operationNum)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            collections.stackEngines.Contains(desiredObj);
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения {operationNum}: {stopwatch.ElapsedTicks} тик.");
        }
        public static void Timer(TestCollections collections, string desiredObj, string operationNum)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            collections.stackNames.Contains(desiredObj);
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения {operationNum}: {stopwatch.ElapsedTicks} тик.");
        }
        public static void Timerd(TestCollections collections, Engine desiredObj, string operationNum)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            collections.dictionaryDiesel.ContainsKey(desiredObj);
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения {operationNum}: {stopwatch.ElapsedTicks} тик.");
        }
        public static void Timerd(TestCollections collections, string desiredObj, string operationNum)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            collections.dictionaryNames.ContainsKey(desiredObj);
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения {operationNum}: {stopwatch.ElapsedTicks} тик.");
        }
        public static void Timerd(TestCollections collections, Diesel desiredObj, string operationNum)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            collections.dictionaryDiesel.ContainsValue(desiredObj);
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения {operationNum}: {stopwatch.ElapsedTicks} тик.");
        }
    }
}
