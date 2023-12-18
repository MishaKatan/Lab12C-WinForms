using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Engines
{
    public class Diesel : Engine
    {
        protected double volume;
        public Diesel() { }
        public Diesel(double power, double fuelСonsumption, string modelName, string fuel, double volume) : base(power, fuelСonsumption, modelName, fuel)
        {
            Volume = volume;
        }       
        public double Volume
        {
            get => volume;
            set
            {
                if (value < 0) volume = 0;
                else volume = value;
            }
        }
        public override void Show()
        {
            base.Show(); Console.WriteLine($"Объем двигателя: {Volume} литров");
        }
        public override string ToString() => base.ToString() + $", Объем двигателя: {Volume} литров";
        public override void Init()
        {
            Console.WriteLine($"\nВведите мощность двигателя:");
            Power = Input.Int();
            Console.WriteLine($"Введите количество потребляемого дизельного топлива:");
            FuelСonsumption = Input.Double();
            Console.WriteLine($"Введите название модели двигателя:");
            ModelName = Input.String();
            Fuel = "1";
            Console.WriteLine($"Введите обьем двигателя:");
            Volume = Input.Double();
        }
        public override void RandomInit()
        {
            base.RandomInit();
            Fuel = "1";
            Volume = Round(random.NextDouble() * random.Next(10),1);
        }
        public override bool Equals(object obj)
        {
            if (obj is Diesel motor)
                return this.Power == motor.Power && this.FuelСonsumption == motor.FuelСonsumption && this.ModelName == motor.ModelName && this.Fuel == motor.Fuel && this.Volume == motor.Volume;
            else
                return false;
        }
        public override void Request()
        {
            Console.WriteLine("Запрос для дизеля:");
            if (Volume >=8 && Power >= 1500)
                Console.Write($"Этот двигатель возможно поставить на танк m1a2 abrams");
            else
                Console.Write($"Этот двигатель невозможно поставить на танк m1a2 abrams \n \n");
        }
        public object Clone() => new Diesel(Power = this.Power, FuelСonsumption = this.FuelСonsumption, ModelName = this.ModelName, Fuel = this.Fuel, Volume = this.Volume);
    }
}
