using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRacing
{
    internal class Horse
    {
        public string Name { get; private set; }
        public int Distance { get; private set; }
        public int Progress { get; private set; }
        public DateTime FinishTime { get; private set; }
        public Action<int> ReportProgress { get; set; }
        public int HorseId { get; private set; }

        public static int HorsesCount { get; private set; } = 0;

        private static object lockObject = new object();

        public Horse(string name)
        {
            Name = name;
            Distance = 0;
            HorseId = ++HorsesCount;
        }

        public void Run()
        {
            int raceDistance = 50;
            Random rnd = new Random();

            while (Distance < raceDistance)
            {
                Thread.Sleep(rnd.Next(100, 500));
                Distance++;
                Progress = (int)((double)Distance / raceDistance * 100);
                ReportProgress?.Invoke(Progress);
            }

            FinishTime = DateTime.Now;

            lock (lockObject)
            {
                Console.SetCursorPosition(0, HorsesCount + HorseId + 1);
                Console.WriteLine($"{Name} finished the race at {FinishTime}!");
            }
        }
    }
}
