using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engines
{
    public class Electric : Engine
    {
        protected string currentType;
        protected readonly string[] currentTypes = new string[] { "1", "2"};
        public Electric() { }
        public Electric(double power, double fuelСonsumption, string modelName, string fuel, string currentType) : base(power, fuelСonsumption, modelName, fuel)
        {
            CurrentType = currentType;
        } 
        public string CurrentType
        {
            get => currentType;
            set
            {
                if (value == "1" || value == "Переменный")
                    currentType = "Переменный";
                else if (value == "2" || value == "Постоянный")
                    currentType = "Постоянный";
                else 
                    currentType = "Неизвестно";
            }
        }
        public override void Show() => Console.WriteLine(ToString());
        public override string ToString() => $"Мощность: {Power} кВт, Потребление топлива: {FuelСonsumption} кВ/ч, Название модели: {ModelName}, Тип топлива: {Fuel}, Тип тока: {CurrentType}"; 
        public override void Init()
        {
            Console.WriteLine($"\nВведите мощность двигателя:");
            Power = Input.Int();
            Console.WriteLine($"Введите количество потребляемого электричества:");
            FuelСonsumption = Input.Double();
            Console.WriteLine($"Введите название модели двигателя:");
            ModelName = Input.String();
            Fuel = "3";
            Console.WriteLine($"Введите тип тока: [1] Переменный, [2] Постоянный:");
            CurrentType = Input.String();
        }
        public override void RandomInit()
        {
            base.RandomInit();
            Fuel = "3";
            CurrentType = currentTypes[random.Next(0, currentTypes.Length - 1)];
        }
        public override bool Equals(object obj)
        {
            if (obj is Electric motor)
                return this.Power == motor.Power && this.FuelСonsumption == motor.FuelСonsumption && this.ModelName == motor.ModelName && this.Fuel == motor.Fuel && this.CurrentType == motor.CurrentType;
            else
                return false;
        }
        public override void Request()
        {
            Console.WriteLine("Запрос для электрического двигателя:");
            if (CurrentType == "1")
                Console.Write($"Этот двигатель возможно поставить на тесла \n \n");
            else
                Console.Write($"Этот двигатель невозможно поставить на тесла\n \n");
        }
        public object Clone() => new Electric(Power = this.Power, FuelСonsumption = this.FuelСonsumption, ModelName = this.ModelName, Fuel = this.Fuel, CurrentType=this.CurrentType);
    }
}
