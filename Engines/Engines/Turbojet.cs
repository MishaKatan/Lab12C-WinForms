using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engines
{
    public class Turbojet : Engine
    {
        protected int engineThrust;
        public Turbojet() { }
        public Turbojet(double power, double fuelСonsumption, string modelName, string fuel, int engineThrust) : base(power, fuelСonsumption, modelName, fuel)
        {
            EngineThrust = engineThrust;
        }
        public int EngineThrust
        {
            get => engineThrust;
            set
            {
                if (value < 0) engineThrust = 0;
                else engineThrust = value;
            }
        }
        public override void Show()
        {
            base.Show(); Console.WriteLine($"Тяга двигателя: {EngineThrust} Н");
        }
        public override string ToString() => base.ToString() +  $", Тяга двигателя: {EngineThrust} Н";
        public override void Init()
        {
            base.Init();
            Console.WriteLine($"Введите силу тяги:");
            EngineThrust = Input.Int();
        }
        public override void RandomInit()
        {
            base.RandomInit();
            while (Fuel == "Электричество") Fuel = fuelTypes[random.Next(0, fuelTypes.Length - 1)];
            EngineThrust = random.Next(1000000);
        }
        public override bool Equals(object obj)
        {
            if (obj is Turbojet motor)
                return this.Power == motor.Power && this.FuelСonsumption == motor.FuelСonsumption && this.ModelName == motor.ModelName && this.Fuel == motor.Fuel && this.EngineThrust == motor.EngineThrust;
            else
                return false;
        }
        public override void Request()
        {
            Console.WriteLine("Запрос для турбореактивного:");
            if (EngineThrust >= 475000)
                Console.Write($"Тяги хватит для взлета боинга 777 \n ");
            else
                Console.Write($"Не хватает {475000 - EngineThrust} H тяги для взлета боинга 777 \n ");
        }
        public object Clone() => new Turbojet(Power = this.Power, FuelСonsumption = this.FuelСonsumption, ModelName = this.ModelName, Fuel = this.Fuel, EngineThrust = this.EngineThrust);
    }
}
