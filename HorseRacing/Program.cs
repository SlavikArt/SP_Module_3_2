using HorseRacing;
using System.Text;

class Program
{
    private static object lockObject = new object();

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        int horseCount = GetHorsesCount();

        Horse[] horses = CreateHorses(horseCount);

        StartRace(horses);

        DisplayRaceResults(horses);
    }

    private static int GetHorsesCount()
    {
        Console.WriteLine("Enter the number of horses in the race:");

        int horseCount;
        while (!int.TryParse(Console.ReadLine(), out horseCount) || horseCount <= 0)
        {
            Console.WriteLine("Please enter a valid positive number.");
        }
        return horseCount;
    }

    private static Horse[] CreateHorses(int horseCount)
    {
        Horse[] horses = new Horse[horseCount];
        for (int i = 0; i < horseCount; i++)
        {
            horses[i] = new Horse($"Horse {i + 1}");
        }
        return horses;
    }

    private static void StartRace(Horse[] horses)
    {
        Console.WriteLine("Press any button to start the Horse Race");
        Console.ReadLine();
        Console.Clear();

        // Displaying Finish of each horse
        for (int i = 0; i < horses.Length; i++)
        {
            Console.SetCursorPosition(horses[i].Name.Length + 104, i);
            Console.Write($"<- Finish.");
        }

        Task[] tasks = new Task[horses.Length];
        for (int i = 0; i < horses.Length; i++)
        {
            int index = i;
            horses[index].ReportProgress = progress =>
            {
                lock (lockObject)
                {
                    Console.SetCursorPosition(0, index);
                    Console.Write($"{horses[index].Name}: {new string('-', progress) 
                        + "H" + (index + 1).ToString()}");
                }
            };
            tasks[index] = Task.Run(() => horses[index].Run());
        }
        Task.WaitAll(tasks);
    }

    private static void DisplayRaceResults(Horse[] horses)
    {
        Console.SetCursorPosition(0, horses.Length * 2 + 2);
        Console.WriteLine("\nRace finished!\n\n" +
            "Race Results (TOP 3 Horses):\n");

        var topThreeHorses = horses
            .OrderBy(h => h.FinishTime)
            .Take(3)
            .ToList();

        foreach (var horse in topThreeHorses)
            Console.WriteLine($"{horse.Name}: finished at {horse.FinishTime}");
    }
}
