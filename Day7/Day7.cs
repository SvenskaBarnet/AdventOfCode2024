using System.Numerics;
using System.Transactions;

namespace aoc2024.Day7;
public static class Day7
{
    public static void Part1()
    {
        Stopwatch sw =  new();
        sw.Start();

        Dictionary<double[], double> parsedInput = ParseInput();
        double total = FindCalibrationResults(parsedInput, 2);
        sw.Stop();
        System.Console.WriteLine("\nAnswer Day7 - part 1: total: {0}. Time taken: {1}ms", total, sw.ElapsedMilliseconds);
    }

    public static void Part2()
    {
        Stopwatch sw =  new();
        sw.Start();

        Dictionary<double[], double> parsedInput = ParseInput();
        double total = FindCalibrationResults(parsedInput, 3);
        sw.Stop();
        System.Console.WriteLine("Answer Day7 - part 2: total: {0}. Time taken: {1}ms", total, sw.ElapsedMilliseconds);
    }

    private static double FindCalibrationResults(Dictionary<double[], double> input, int operatorsCount)
    {
        double result = 0;

        foreach (var kvp in input)
        {
            if(IsEquationTrue(kvp, operatorsCount))
            {
                result += kvp.Value;
            }
        }
        return result;
    }

    private static bool IsEquationTrue(KeyValuePair<double[], double> equation, int operatorsCount)
    {
        int baseValue = operatorsCount;
        int digits = equation.Key.Length - 1;
        int totalCombinations = (int)Math.Pow(baseValue, digits);

        for (int i = 0; i < totalCombinations; i++)
        {
            int[] combination = new int[digits];
            int current = i;

            for (int j = 0; j < digits; j++)
            {
                int currentIndex = digits - j - 1;
                combination[currentIndex] = current % baseValue;
                current /= baseValue;
            }

            if(CombinationIsTrue(equation, combination))
            {
                return true;
            }
        }

        return false;
    }

    private static bool CombinationIsTrue(KeyValuePair<double[], double> equation, int[] combination)
    {
        double testValue = equation.Value;
        double[] equationParts = equation.Key;
        double result = equationParts[0];

        for (int i = 0; i < equationParts.Length - 1; i++)
        {
            switch(combination[i])
            {
                case 0 :
                    result += equationParts[i + 1];
                    break;
                case 1 :
                    result *= equationParts[i + 1];
                    break;
                case 2 :
                    result = double.Parse(result.ToString() + equationParts[i + 1].ToString());
                    break;
            }
            if(result > testValue)
            {
                return false;
            }
        }

        if(result.Equals(testValue))
        {
            return true;
        }

        return false;
    }

    private static Dictionary<double[], double> ParseInput()
    {
        string[] input = File.ReadAllLines(Path.Combine("Day7", "input"));
        Dictionary<double[], double> parsedInput = [];

        foreach(var line in input)
        {
            string[] splitLine = line.Split(':');
            double value = double.Parse(splitLine[0]);
            double[] key = Array.ConvertAll(splitLine[1].Trim().Split(' '), double.Parse);

            parsedInput.Add(key, value);
        }

        return parsedInput;
    }
}