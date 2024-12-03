using System.Text.RegularExpressions;

namespace aoc2024.Day3;
public static class Day3
{
    public static void Part1()
    {
        List<string> instructions = ParseInstructions(parseOnlyMulitply: true);
        int total = instructions.Sum( instruction => {
            int[] numbers = ParseMultiplyinstruction(instruction);
            return numbers[0] * numbers[1];
        });

        System.Console.WriteLine("Answer Day3 - part 1: {0}", total);
    }

    public static void Part2()
    {
        List<string> instructions = ParseInstructions(parseOnlyMulitply: false);
        int total = ProcessInstructionsWithSwitch(instructions);
        System.Console.WriteLine("Answer Day3 - part 2: {0}", total);
    }

    private static int[] ParseMultiplyinstruction(string instruction)
    {
        const int prefixLength = 4; // "mul(" length
        return instruction
            .TrimEnd(')')
            .Substring(prefixLength)
            .Split(',')
            .Select(int.Parse)
            .ToArray();

    }

    private static List<string> ParseInstructions(bool parseOnlyMulitply)
    {
        string input = File.ReadAllText(Path.Combine("Day3", "input"));
        string pattern = parseOnlyMulitply
            ? @"[m][u][l][(]\d{1,4}[,]\d{1,4}[)]"
            : @"(mul[(]\d{1,4}[,]\d{1,4}[)])|(don't[(][)])|(do[(][)])";

        List<string> matches = Regex.Matches(input, pattern, RegexOptions.Compiled)
            .Select(x => x.Value)
            .ToList();

        return matches;
    }

    private static int ProcessInstructionsWithSwitch(List<string> instructions)
    {
        const string dontCommand = "don't()";
        const string doCommand = "do()";

        int total = 0;
        bool shouldMultiply = true;

        foreach (string instruction in instructions)
        {
            switch (instruction)
            {
                case dontCommand : 
                    shouldMultiply = false;
                    break;
                case doCommand:
                    shouldMultiply = true;
                    break;
                default:
                    if (shouldMultiply)
                    {
                        int[] numbers = ParseMultiplyinstruction(instruction);
                        total += numbers[0] * numbers[1];
                    }
                    break;
            }
        }
        return total;
    }
}