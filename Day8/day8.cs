using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace aoc2024.Day8;
public static class Day8
{
    public static void Part1()
    {
        Stopwatch sw =  new();
        sw.Start();

        string[] input = File.ReadAllLines(Path.Combine("Day8", "input"));
        int total = FindUniqueLocations(input);

        sw.Stop();
        System.Console.WriteLine("\nAnswer Day8 - part 1: total: {0}. Time taken: {1}ms", total, sw.ElapsedMilliseconds);
    }

    public static void Part2()
    {
        Stopwatch sw =  new();
        sw.Start();

        int total = 0;
        sw.Stop();
        System.Console.WriteLine("Answer Day8 - part 2: total: {0}. Time taken: {1}ms", total, sw.ElapsedMilliseconds);
    }

    private static int FindUniqueLocations(string[] input)
    {
        HashSet<Coordinate> coordinates =  [];
        int maxY = input.Length;
        int maxX = input[0].Length;

        Dictionary<char, List<Coordinate>> antennas = LocateAntennas(input);

        foreach (var locations in antennas.Values)
        {
            AddAntinodesForLocations(locations, coordinates, maxX, maxY);
        }

        return coordinates.Count;
    }

    private static void AddAntinodesForLocations(List<Coordinate> locations, HashSet<Coordinate> coordinates, int maxX, int maxY)
    {
        for (int i = 0; i < locations.Count - 1; i++)
        {
            for (int j = i + 1; j < locations.Count; j++)
            {
                (Coordinate before, Coordinate after) = locations[i].GetAntinodes(locations[j]);
                if (IsWithinBounds(before, maxX, maxY))
                {
                    coordinates.Add(before);
                }
                if (IsWithinBounds(after, maxX, maxY))
                {
                    coordinates.Add(after);
                }
            }
        }
    }

    private static bool IsWithinBounds(Coordinate coordinate, int maxX, int maxY)
    {
        return coordinate.X >= 0 && coordinate.X < maxX && coordinate.Y >= 0 && coordinate.Y < maxY;
    }

    private static Dictionary<char, List<Coordinate>> LocateAntennas(string[] input)
    {
        Dictionary<char, List<Coordinate>> antennas = [];

        for (int y = 0; y < input.Length; y++)
        {
            string line = input[y];
            for(int x = 0; x < line.Length; x++)
            {
                Coordinate coordinate = new() { X = x, Y = y };
                if(line[x] != '.')
                {
                    if (!antennas.TryGetValue(line[x], out List<Coordinate>? list))
                    {
                        antennas[line[x]] = list = [];
                    }
                    list.Add(coordinate);
                }
            }
        }

        List<char> toRemove = antennas
            .Where(kvp => kvp.Value.Count < 2)
            .Select(kvp => kvp.Key)
            .ToList();
        foreach(char key in toRemove)
        {
            antennas.Remove(key);
        }
        
        return antennas;
    }

    private struct Coordinate
    {
        public int X {get; set;}
        public int Y {get; set;}

        public (Coordinate before, Coordinate beyond) GetAntinodes(Coordinate other)
        {
            int dx = other.X - X;
            int dy = other.Y - Y;

            Coordinate beyond = new Coordinate
            {
                X = other.X + dx,
                Y = other.Y + dy
            };

            Coordinate before = new Coordinate
            {
                X = X - dx,
                Y = Y - dy
            };

            return (before, beyond);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if(obj is Coordinate other)
            {
                return Equals(other);
            }
            return false;
        }

        public bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return left.Equals(right);
        }

        public override readonly string ToString()
        {
            return $"({X},{Y})";
        }
    }
}