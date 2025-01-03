namespace aoc2024.Day6;

public static class Day6
{
    public static void Part1()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        string[] input = ParseInput();
        int total =  GetXs(input);
        sw.Stop();
        System.Console.WriteLine("\nAnswer Day6 - part 1: {0}. Time taken: {1}ms", total, sw.ElapsedMilliseconds);
    }

    public static void Part2()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        string[] input = ParseInput();
        int total = GetLoops(input);
        sw.Stop();
        System.Console.WriteLine("Answer Day6 - part 2: {0}. Time taken: {1}ms", total, sw.ElapsedMilliseconds);
    }

    private static int GetLoops(string[] map)
    {
        int loops = 0;
        int[] intitialCoordinates = GetInitialCoordinates(map); 
        int[] currentLocation = intitialCoordinates;
        List<int[]> visited = [];
        Direction currentDirection = Direction.Up;

        do
        {
            if(!visited.Any(l => l.SequenceEqual(currentLocation)))
            {
                visited.Add(currentLocation);
            }
            if(NotTurning(GetNextLocation(currentLocation, currentDirection), map))
            {
                if
                (
                    !GetNextLocation(currentLocation, currentDirection).SequenceEqual(intitialCoordinates) &&
                    !visited.Any(l => l.SequenceEqual(GetNextLocation(currentLocation, currentDirection))) &&
                    CanCreateLoop(map, currentLocation, currentDirection))
                {
                    loops++;
                }
                currentLocation = GetNextLocation(currentLocation, currentDirection);
            }
            else 
            {
                currentDirection = GetNewDirection(currentDirection);
            }
        } while (!IsFinished(GetNextLocation(currentLocation, currentDirection), map));

        return loops;
    }

    private static bool CanCreateLoop(string[] map, int[] currentLocation, Direction currentDirection)
    {
        string[] tempMap = (string[]) map.Clone();
        string[] tempMapWithObsticale = AddObstacle(tempMap, currentLocation, currentDirection);
        if(StuckInLoop(tempMapWithObsticale, currentLocation, currentDirection))
        {
            return true;
        }
        return false;
    }

    private static bool StuckInLoop(string[] map, int[] location, Direction direction)
    {
        Direction currentDirection = direction;
        int[] currentLocation = (int[])location.Clone();
        var visited = new HashSet<(int Y, int X, Direction dir)>();

        do
        {
            var locationHash = (currentLocation[0], currentLocation[1], currentDirection);

            if(visited.Contains(locationHash))
            {
                return true;
            }

            visited.Add(locationHash);

            if(NotTurning(GetNextLocation(currentLocation, currentDirection), map))
            {
                currentLocation = GetNextLocation(currentLocation, currentDirection);
            }
            else 
            {
                currentDirection = GetNewDirection(currentDirection);
            }
        } while (!IsFinished(GetNextLocation(currentLocation, currentDirection), map));

        return false;
    }

    private static string[] AddObstacle(string[] map, int[] location, Direction direction)
    {
        string[] tempMap = (string[])map.Clone();
        int[] nextLocation = GetNextLocation(location, direction);

        int Y = nextLocation[0];
        int X = nextLocation[1];

        tempMap[Y] = $"{tempMap[Y][..X]}#{tempMap[Y][(X + 1)..]}";
        return tempMap;
    }
    
    private static int GetXs(string[] map)
    {
        List<int[]> visitedCoordinates = [];
        int[] intitialCoordinates = GetInitialCoordinates(map); 
        visitedCoordinates.Add(intitialCoordinates);
        int[] currentLocation = intitialCoordinates;
        Direction currentDirection = Direction.Up;

        do
        {
            if(NotTurning(GetNextLocation(currentLocation, currentDirection), map))
            {
                currentLocation = GetNextLocation(currentLocation, currentDirection);
                if(!visitedCoordinates.Any(coordinate => coordinate.SequenceEqual(currentLocation)))
                {
                    visitedCoordinates.Add(currentLocation);
                }
            }
            else 
            {
                currentDirection = GetNewDirection(currentDirection);
            }
        } while (!IsFinished(GetNextLocation(currentLocation, currentDirection), map));

        return visitedCoordinates.Count;
    }

    private static Direction GetNewDirection(Direction currentDirection)
    {
        int newDirection = (int)currentDirection + 1;
        if( newDirection > 3 )
        {
            newDirection = 0;
        }

        return (Direction)newDirection;
    }
    private static bool NotTurning(int[] location, string[] map)
    {
        return !map[location[0]][location[1]].Equals('#');
    }

    private static bool IsFinished(int[] nextLocation, string[]map)
    {
        if(nextLocation[0] < 0 || nextLocation[0] > map.Length - 1)
        { 
            return true;
        }

        if(nextLocation[1] < 0 || nextLocation[1] > map[0].Length - 1)
        {
            return true;
        }
        return false;
    }

    private static int[] GetNextLocation(int[] currentLocation, Direction direction)
    {
        int[] nextLocation = new int[2];
        switch (direction)
        {
            case Direction.Up :
            nextLocation = [(currentLocation[0] - 1), currentLocation[1]]; 
            break;
            
            case Direction.Right:
            nextLocation = [(currentLocation[0]), currentLocation[1] + 1]; 
            break;

            case Direction.Down:
            nextLocation = [(currentLocation[0] + 1), currentLocation[1]]; 
            break;

            case Direction.Left:
            nextLocation = [(currentLocation[0]), currentLocation[1] - 1]; 
            break;
        }
        return nextLocation;
    }

    private static string[] ParseInput()
    {
        return File.ReadAllLines(Path.Combine("Day6", "input"));
    } 

    private static int[] GetInitialCoordinates(string[] map)
    {
        int initialY = Array.FindIndex(map, line => line.Contains('^'));
        int initialX = map[initialY].LastIndexOf('^');

        return [initialY, initialX];
    }

    private enum Direction
    { 
        Up,
        Right,
        Down,
        Left
    }
}