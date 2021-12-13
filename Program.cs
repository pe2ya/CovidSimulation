using System;

namespace Covid
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulation sim = new Simulation();
            sim.Generator(70, 10000);

            Console.WriteLine(sim);

            Console.WriteLine(sim.CountCovidCases());
            /*
            foreach (var x in sim.CovidCase())
            {
                Console.WriteLine(x);
            }
            
            sim.CloseToCovidCase();
            Console.WriteLine(sim.CountCovidCases());

            sim.CloseToCovidCase();
            Console.WriteLine(sim.CountCovidCases());
            */


            sim.Sim();
        }
    }
}
