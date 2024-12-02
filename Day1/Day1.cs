using System.Numerics;

namespace aoc2024.Day1;
public static class Day1
{
    private static (List<int> left, List<int> right) ParseInput()
    {
        string[] input = File.ReadAllLines(Path.Combine("Day1", "input"));
        List<int> left = [];
        List<int> right = [];

        foreach (var line in input)
        {
            string leftNum = string.Empty;
            string rightNum = string.Empty;
            for (int i = 0; i < line.Length; i++)
            {
                if (i < 5)
                {
                    leftNum += line[i];
                }
                else if (i > 7)
                {
                    rightNum += line[i];
                }
            }

            left.Add(int.Parse(leftNum));
            right.Add(int.Parse(rightNum));
        }

        return (left, right);
    }

    public static void Part1()
    {
        (List<int> left, List<int> right) = ParseInput();
        left.Sort();
        right.Sort();

        BigInteger totalDistance = 0;

        for (int i = 0; i < left.Count; i++)
        {
            totalDistance += Math.Abs(left[i] - right[i]);
        }

        System.Console.WriteLine("Answer Day1 - part 1: {0}", totalDistance);
    }

    public static void Part2()
    {
        (List<int> left, List<int> right) = ParseInput();

        int similarityScore = 0;
        foreach (int number in left)
        {
            int noOfOccurences = right.FindAll(x => x.Equals(number)).Count;
            similarityScore += number * noOfOccurences;
        }

        System.Console.WriteLine("Answer Day1 - part 2: {0}", similarityScore);
    }
}