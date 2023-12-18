using System.Xml.Linq;
using static System.Console;
using static System.Math;
using Newtonsoft.Json;


namespace Engines
{
    public class Engine : IInit, IComparable, IComparable<Engine>
    {
        //Fields
        public static Random random = new Random();
        protected double power;
        protected double fuelСonsumption;
        protected string modelName;
        protected string fuel;
        protected readonly string[] modelNames = new string[] { "Axes11g", "DisOM602", "BMW 330D", "Toyta 3S-FE", "TEsC20XE", "Lan2JZ-GE", "VANOS143" };
        protected readonly string[] fuelTypes = new string[] { "1", "2", "3", "4", "5" };
        public Engine() { }
        public Engine(double power, double fuelСonsumption, string modelName, string fuel)
        {
            Power = power;
            FuelСonsumption = fuelСonsumption;
            ModelName = modelName;
            Fuel = fuel;
        }
        public double Power
        {
            get => power;
            set
            {
                if (value < 0) power = 0;
                else power = value;
            }
        }
        public double FuelСonsumption
        {
            get => fuelСonsumption;
            set
            {
                if (fuelСonsumption < 0) fuelСonsumption = 0;
                else fuelСonsumption = value;
            }
        }
        public string ModelName
        {
            get => modelName;
            set => modelName = value;
        }
        public string Fuel
        {
            get => fuel;
            set
            {
                if (value == "1" || value ==  "Дизель") fuel = "Дизель";
                else if (value == "2" || value == "Бензин") fuel = "Бензин";
                else if (value == "3" || value == "Электричество") fuel = "Электричество";
                else if (value == "4" || value == "Ядерное") fuel = "Ядерное";
                else if (value == "5" || value == "Керосин") fuel = "Керосин";
                else fuel = "Неизвестно";
            }
        }
        public virtual void Show() => WriteLine(ToString());
        public override string ToString() => $"Мощность: {Power} л.с, Потребление топлива: {FuelСonsumption} л/100км, Название модели: {ModelName}, Тип топлива: {Fuel}";
        public virtual void Init()
        {
            WriteLine($"\nВведите мощность двигателя:");
            Power = Input.Int();
            WriteLine($"Введите количество потребляемого топлива:");
            FuelСonsumption = Input.Double();
            WriteLine($"Введите название модели двигателя:");
            ModelName = Input.String();
            WriteLine($"Введите тип топлива: [1] Дизель, [2] Бензин, [3] Электричество, [4] Ядерное, [5] Керосин:");
            Fuel = Input.String();
        }
        public virtual void RandomInit()
        {
            Power = random.Next(2000);
            FuelСonsumption = Round(random.NextDouble() * random.Next(5, 50), 1);
            modelName = modelNames[random.Next(0, modelNames.Length - 1)];
            Fuel = fuelTypes[random.Next(0, fuelTypes.Length - 1)];
        }
        public override bool Equals(object obj)
        {
            if (obj is Engine motor)
                return this.Power == motor.Power && this.FuelСonsumption == motor.FuelСonsumption && this.ModelName == motor.ModelName && this.Fuel == motor.Fuel;
            else
                return false;
        }
        public virtual void Request()
        {
            int distance;
            WriteLine("Базовый запрос:");
            Write($"Для расчета количества требуемого топлива введите растояние в км: ");
            distance = Input.Int();
            if (Fuel == "3" || Fuel == "Электричество")
                WriteLine($"{Round(FuelСonsumption / 100 * distance, 2)} кВт электричества нужно на {distance} км \n");
            else
                WriteLine($"{Round(FuelСonsumption / 100 * distance, 2)} литра топлива нужно на {distance} км \n");
        }
        public int CompareTo(object obj)//реализация интерфейса
        {
            Engine motor = (Engine)obj;//приведение к типу SimpleFraction
            if (motor != null)
            {
                if (this.Power < motor.Power)
                    return -1;
                else if (this.Power > motor.Power)
                    return 1;
                else
                    return 0;
            }
            else
                throw new Exception("Параметр должен быть типа Engine");
        }
        public int CompareTo(Engine other)
        {
            if (other == null)
                return 1;

            return this.Power.CompareTo(other.Power);
        }
        public object ShallowCopy() => MemberwiseClone();
        public object Clone() => new Engine(Power = this.Power, FuelСonsumption = this.FuelСonsumption, ModelName = this.ModelName, Fuel = this.Fuel);
        public Engine GetBaseEngine() => new Engine(Power, FuelСonsumption, ModelName, Fuel);
    }
}
