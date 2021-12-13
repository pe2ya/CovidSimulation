using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Covid
{
    class Simulation
    {
        private List<Person> people = new List<Person>();
        private int i;
        static Random rnd = new Random();

        public void Generator(int square, int PersonsNumber)
        {
            for (i = 0; i < PersonsNumber; i++)
            {
                Person p = new Person();
                p.HaveCovid = false;
                p.Home = new Coordinates(rnd.Next(square), rnd.Next(square));
                p.Work = new Coordinates(rnd.Next(square), rnd.Next(square));
                p.AtWork = rnd.Next(0, 4) == 0;

                people.Add(p);
            }
            people[rnd.Next(PersonsNumber)].HaveCovid = true;
        }

        public int CountCovidCases()
        {
            int result = 0;
            foreach (var x in people)
            {
                if (x.HaveCovid == true)
                {
                    result++;
                }
            }

            return result;
        }

        public IEnumerable<Person> CovidCase()
        {
            foreach (var x in people)
            {
                if (x.HaveCovid == true)
                {
                    yield return x;
                }
            }
        }

        int WorkCovidX = 0;
        int WorkCovidY = 0;
        int HomeCovidX = 0;
        int HomeCovidY = 0;
        int PersonAndCovidWorkX = 0;
        int PersonAndCovidWorkY = 0;
        int PersonAndCovidHomeX = 0;
        int PersonAndCovidHomeY = 0;

        public void CloseToCovidCase()
        {
            foreach (var x in CovidCase())
            {
                Coordinates covidlocation = x.Work;

                WorkCovidX = x.Work.X;
                WorkCovidY = x.Work.Y;
                HomeCovidX = x.Home.X;
                HomeCovidY = x.Home.Y;

                foreach (var p in people)
                {
                    p.AtWork = rnd.Next(0, 4) == 0; 

                    PersonAndCovidWorkX = Math.Abs(WorkCovidX - p.Work.X);
                    PersonAndCovidWorkY = Math.Abs(WorkCovidY - p.Work.Y);
                    PersonAndCovidHomeX = Math.Abs(HomeCovidX - p.Home.X);
                    PersonAndCovidHomeY = Math.Abs(HomeCovidY - p.Home.Y);

                    if ((PersonAndCovidWorkX + PersonAndCovidWorkY) <= 2)
                    {
                        if (p.AtWork == true)
                        {
                            p.HaveCovid = true;
                        }
                    }
                    
                    if((PersonAndCovidHomeX + PersonAndCovidHomeY) > 500)
                    {
                        if (p.AtWork == false)
                        { 
                            p.HaveCovid = false;
                        }
                    }
                    

                }

            }


        }

        public void Sim()
        {
            int vaccine = 0;
            for (i = 1; i > 0; i++)
            {
                CloseToCovidCase();
                Console.WriteLine(CountCovidCases());

                if (i > 5 && vaccine == 0)
                {

                    foreach (var x in CovidCase())
                    {
                        x.HaveCovid = rnd.Next(0, 25) == 1;
                    }
                }

                if (i == 14)
                {
                    vaccine = rnd.Next(0, 2);
                }

                if (i > 14 && i % 5 == 0 && vaccine == 0)
                {
                    vaccine = rnd.Next(0, 2);
                }

                if (vaccine == 1)
                {
                    foreach (var x in CovidCase())
                    {
                        x.HaveCovid = rnd.Next(0, 100) == 1;
                    }
                }


                if (CountCovidCases() == people.Count() || CountCovidCases() == 0)
                {
                    break;
                }
            }
            
        }

        public override string ToString()
        {
            string result = "";

                foreach (var x in people)
                {
                    result += x.ToString() + "\n";
                }

                return result;
            
        }
    }
}
