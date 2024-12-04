using System.Text.RegularExpressions;

namespace aoc2024.Day4;
public class Day4
{
    public static void Part1()
    {
        const string word = "XMAS"; 
        string[] wordSearch = ReadInput();
        int total = HorizontalMatches(wordSearch) + FindVerticalAndDiagonalMatches(wordSearch, word);
        System.Console.WriteLine("Answer Day4 - part 1: {0}", total);
    }
    public static void Part2()
    {

    }


    private static int HorizontalMatches(string[] wordSearch)
    {
        int matches = 0;
        string pattern = @"(?=(XMAS))|(?=(SAMX))";
        foreach (var line in wordSearch)
        {
            matches += Regex.Matches(line, pattern, RegexOptions.Compiled).Count;
        }
        return matches;
    }

    private static int FindVerticalAndDiagonalMatches(string[] wordSearch, string word)
    {
        int matches = 0;

        for (int x = 0; x < wordSearch.Length; x++)
        {
            for (int y = 0; y < wordSearch[x].Length; y++)
            {
                int minLettersToMatch = word.Length - 1;
                int maxX = wordSearch.Length - minLettersToMatch;
                int maxY = wordSearch[x].Length - minLettersToMatch;
                int minX = minLettersToMatch - 1;
                int minY = minLettersToMatch - 1;
                char letter = wordSearch[x][y];

                if(letter == word.First())
                {
                    for(int direction = 0; direction <= (int)Directions.DownLeft; direction++)
                    {
                        bool match = false;

                        switch ((Directions)direction)
                        {
                            case Directions.Up:
                                if (x > minX)
                                {
                                    match = IsMatch(x, y, word, Directions.Up, wordSearch);
                                }
                                break;
                            case Directions.Down:
                                if (x < maxX)
                                {
                                    match = IsMatch(x, y, word, Directions.Down, wordSearch);
                                }
                                break;
                            case Directions.UpRight:
                                if (x > minX && y < maxY)
                                {
                                    match = IsMatch(x, y, word, Directions.UpRight, wordSearch);
                                }
                                break;
                            case Directions.DownRight:
                                if (x < maxX && y < maxY)
                                {
                                    match = IsMatch(x, y, word, Directions.DownRight, wordSearch);
                                }
                                break;
                            case Directions.UpLeft:
                                if (x > minX && y > minY)
                                {
                                    match = IsMatch(x, y, word, Directions.UpLeft, wordSearch);
                                }
                                break;
                            case Directions.DownLeft:
                                if (x < maxX && y > minY)
                                {
                                    match = IsMatch(x, y, word, Directions.DownLeft, wordSearch);
                                }
                                break;
                        }
                        
                        if(match)
                        {
                            matches++;
                        }
                    }
                }
            }
        }
        return matches;
    }

    private static bool IsMatch(int startX, int startY, string word, Directions direction, string[] wordSearch)
    {
        bool isMatch = false;
        int x = startX;
        int y = startY;
        for (int letterToMatch = 1; letterToMatch < word.Length; letterToMatch++)
        {
            switch(direction)
            {
                case Directions.Up :
                    x--;
                    break;
                case Directions.Down :
                    x++;
                    break;
                case Directions.UpRight :
                    x--;
                    y++;
                    break;
                case Directions.DownRight :
                    x++;
                    y++;
                    break;
                case Directions.UpLeft :
                    x--;
                    y--;
                    break;
                case Directions.DownLeft :
                    x++;
                    y--;
                    break;
            }
            if(wordSearch[x][y] != word[letterToMatch])
            { 
                return false;
            } 
            else
            {
                isMatch = true;
            }
        }
        return isMatch;
    }

    private static string[] ReadInput()
    {
        return File.ReadAllLines(Path.Combine("Day4", "input"));
    } 

    enum Directions
    {
        Up,
        Down,
        UpRight,
        DownRight,
        UpLeft,
        DownLeft
    }

}