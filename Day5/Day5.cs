namespace aoc2024.Day5;

public class Day5
{
    public static void Part1()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        (List<int[]> rules, List<int[]> updates) = ParseInput();
        int total = ProcessUpdates(rules, updates, Part.Part1);
        sw.Stop();
        System.Console.WriteLine("\nAnswer Day5 - part 1: {0}. Time Taken: {1}ms", total, sw.ElapsedMilliseconds);
    }
    public static void Part2()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        (List<int[]> rules, List<int[]> updates) = ParseInput();
        int total = ProcessUpdates(rules, updates, Part.Part2);
        sw.Stop();
        System.Console.WriteLine("Answer Day5 - part 2: {0}. Time Taken: {1}ms", total, sw.ElapsedMilliseconds);
    }

    private static (List<int[]> rules, List<int[]> updates) ParseInput()
    {
        string[] input = File.ReadAllLines(Path.Combine("Day5", "input"));
        List<int[]> rules = new List<int[]>();
        List<int[]> updates = new List<int[]>();

        foreach (var line in input)
        {
            if (line.Contains('|'))
            {
                rules.Add(Array.ConvertAll(line.Split('|'), int.Parse));
            }
            else if (line.Contains(','))
            {
                updates.Add(Array.ConvertAll(line.Split(','), int.Parse));
            }
        }

        return (rules, updates);
    }

    private static int ProcessUpdates(List<int[]> rules, List<int[]> updates, Part part)
    {
        int totalCorrectUpdates = 0;
        foreach (var line in updates)
        {
            if(IsCorrectUpdate(line, rules) && part == Part.Part1)
            {
                totalCorrectUpdates += line[line.Length / 2];
            }
            else if(!IsCorrectUpdate(line, rules) && part == Part.Part2)
            {
                totalCorrectUpdates += CorrectBadUpdateAndGetMiddlePage(line, rules);
            }
        }

        return totalCorrectUpdates;
    }

    private static bool IsCorrectUpdate(int[] update, List<int[]> rules)
    {
        foreach (int num in update)
        {
            foreach (var rule in rules.Where(r => r.Contains(num)))
            {
                int otherPage = rule.First(p => p != num);
                int numRuleIndex = Array.IndexOf(rule, num);
                int numUpdateIndex = Array.IndexOf(update, num);
                int otherPageUpdateIndex = Array.IndexOf(update, otherPage);

                if (update.Contains(otherPage))
                {
                    if (numRuleIndex < 1 && numUpdateIndex > otherPageUpdateIndex)
                    {
                        return false;
                    }
                    else if (numRuleIndex > 0 && numUpdateIndex < otherPageUpdateIndex)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private static int CorrectBadUpdateAndGetMiddlePage(int[] update, List<int[]> rules)
    {
        int[] tempUpdate = update;
        while (!IsCorrectUpdate(tempUpdate, rules))
        {
            for (int i = 0; i < tempUpdate.Length; i++)
            {
                foreach (var rule in rules.Where(r => r.Contains(tempUpdate[i])))
                {
                    int otherPage = rule.First(p => p != tempUpdate[i]);
                    if (tempUpdate.Contains(otherPage))
                    {
                        int numRuleIndex = Array.IndexOf(rule, tempUpdate[i]);
                        int otherPageUpdateIndex = Array.IndexOf(tempUpdate, otherPage);
                        bool pageShouldBeBefore = numRuleIndex < 1;
                        bool pageIsBefore = i < otherPageUpdateIndex;

                        if ((pageShouldBeBefore && !pageIsBefore) || (!pageShouldBeBefore && pageIsBefore))
                        {
                            int tempPage = otherPage;
                            tempUpdate[otherPageUpdateIndex] = tempUpdate[i];
                            tempUpdate[i] = tempPage;
                        }
                    }
                }
            }
        }
        return tempUpdate[tempUpdate.Length / 2];
    }
    private enum Part
    {
        Part1,
        Part2
    }
}
