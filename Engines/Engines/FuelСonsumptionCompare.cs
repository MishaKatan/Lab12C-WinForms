using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engines
{
    public class FuelСonsumptionCompare : IComparer<Engine>
    {
        public int Compare(Engine fC1, Engine fC2)
        {
            if(fC1.FuelСonsumption < fC2.FuelСonsumption)
                return -1;
            if (fC1.FuelСonsumption > fC2.FuelСonsumption)
                return 1;
            else
                return 0;
        }
    }
}
