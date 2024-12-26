namespace aoc2024.Day2;
public static class Day2
{
    private const int MAX_DIFFERENCE = 3;
    private const int MIN_DIFFERENCE = 1;
    public static void Part1()
    {
        Stopwatch sw = new();
        sw.Start();
        string[] input = File.ReadAllLines(Path.Combine("Day2", "input"));
        int totalSafeReports = 0;

        foreach (var report in input)
        {
            List<int> numbers = report.Split(" ").Select(x => int.Parse(x)).ToList();
            bool isSafe = IsSafe(numbers);
            if (isSafe)
            {
                totalSafeReports++;
            }
        }
        sw.Stop();
        System.Console.WriteLine("\nAnswer Day2 - part 1: {0}. Time Taken: {1}ms", totalSafeReports, sw.ElapsedMilliseconds);
    }

    public static void Part2()
    {
        Stopwatch sw = new();
        sw.Start();
        string[] input = File.ReadAllLines(Path.Combine("Day2", "input"));
        int totalSafeReports = 0;

        foreach (var report in input)
        {
            List<int> numbers = report.Split(" ").Select(x => int.Parse(x)).ToList();
            bool isSafe = IsSafe(numbers);
            if (isSafe)
            {
                totalSafeReports++;
            }
            else
            {
                for(int j = 0; j < numbers.Count; j++)
                {
                    int temp = numbers[j];
                    numbers.RemoveAt(j);

                    if(IsSafe(numbers))
                    {
                        totalSafeReports++;
                        break;
                    }
                    numbers.Insert(j, temp);
                }
            }
        }
        sw.Stop();
        System.Console.WriteLine("Answer Day2 - part 2: {0}. Time Taken: {1} Microseconds", totalSafeReports, sw.Elapsed.TotalMicroseconds);
    }
    private static bool IsSafe(List<int> numbers)
    {
        if(numbers[0] == numbers[1])
        {
            return false;
        }

        bool isIncreasing = numbers[0] < numbers[1];

        for(int i = 0; i < numbers.Count - 1; i++)
        {
            int diff = numbers[i + 1] - numbers[i];
            if(isIncreasing && (diff > MAX_DIFFERENCE || diff < MIN_DIFFERENCE))
            {
                return false;
            }
            else if(!isIncreasing && (diff < -MAX_DIFFERENCE || diff > -MIN_DIFFERENCE))
            {
                return false;
            }
        }

        return true;
    }
}