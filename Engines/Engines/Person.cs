using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Console;

namespace Engines
{
    public class Id
    {
        public int id;
        public Id(int id) => this.id = id;
        public void Show() => WriteLine($"Id: {id}");
    }
    public class Person : IInit, ICloneable
    {
        public Id id;
        public static Random random = new Random();
        protected string name;
        protected int age;
        protected int height;
        protected readonly string[] names = new string[] { "Vanya", "Victor", "Luther", "Diego", "Klaus", "Ben", "Number Five" };
        public Person() { }
        public Person(string name, int age, int height)
        {
            Name = name;
            Age = age;
            Height = height;
        }
        public Person(string name, int age, int height, Id id)
        {
            Name = name;
            Age = age;
            Height = height;
            this.id = id;
        }
        string Name
        {
            get => name;
            set => name = value;
        }
        int Age
        {
            get => age;
            set => age = value;
        }
        public int Height
        {
            get => height;
            set
            {
                if (value < 150)
                    height = 150;
                else
                    height = value;
            }
        }
        public virtual void Init()
        {
            WriteLine($"\nВведите имя человека:");
            Name = Input.String();
            WriteLine($"Введите возраст человека:");
            Age = Input.Int();
            WriteLine($"Введите рост человека:");
            Height = Input.Int();
        }
        public virtual void RandomInit()
        {
            Name = names[random.Next(0, names.Length - 1)];
            Age = random.Next(0, 100);
            Height = random.Next(150, 230);
        }
        public virtual void Show() => WriteLine(ToString());
        public virtual string ToString() => $"Имя: {Name}, Возраст: {Age} лет, Рост: {Height} см";
        public object ShallowCopy() => MemberwiseClone();
        public object Clone() => new Person(Name = this.Name, Age = this.Age, Height = this.Height, new Id(this.id.id));
    }
}
