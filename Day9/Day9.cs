using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace aoc2024.Day9;
public static class Day9
{
    public static void Part1()
    {
        Stopwatch sw =  new();
        sw.Start();

        double total = GetChecksum();
        sw.Stop();
        System.Console.WriteLine("\nAnswer Day9 - part 1: total: {0}. Time taken: {1}ms", total, sw.ElapsedMilliseconds);
    }

    public static void Part2()
    {
        Stopwatch sw =  new();
        sw.Start();

        double total = GetChecksum(true);
        sw.Stop();
        System.Console.WriteLine("Answer Day9 - part 2: total: {0}. Time taken: {1}ms", total, sw.ElapsedMilliseconds);
    }

    private static double GetChecksum(bool moveWholeFiles = false)
    {
        List<string> compactedDisk = CompactFiles(ParseInput(), moveWholeFiles);
        double checksum = 0;

        for (int i = 0; i < compactedDisk.Count; i++)
        {
            if(compactedDisk[i] != ".")
            {
                checksum += i * double.Parse(compactedDisk[i]);
            }
        }
        return checksum;
    }

    private static List<string> CompactFiles(List<string> disk, bool moveWholeFiles = false)
    {
        List<string> tempDisk = disk;

        if(moveWholeFiles)
        {
            MoveWholeFiles(tempDisk);
        }
        else
        {
            MoveIndividualBlocks(tempDisk);
        }
        return tempDisk;
    }

    private static void MoveWholeFiles(List<string> tempDisk)
    {
        int index = tempDisk.Count - 1;

        while(index >= 0)
        {
            string fileId = tempDisk[index];
            if(fileId != ".")
            {
                int blockCount = GetBlockCount(tempDisk, index, fileId);
                (int emptyIndex, bool emptyExists) = FindEmptySpace(tempDisk, blockCount, index);
                if (emptyExists)
                {
                    for (int i = 0; i < blockCount; i++)
                    {
                        tempDisk[emptyIndex + i] = fileId;
                        tempDisk[index - i] = ".";
                    }
                }
                index -= blockCount - 1;
            }

            index--;
        }
    }

    private static (int emptyIndex, bool emptyExists) FindEmptySpace(List<string> disk, int blockCount, int fileLocation)
    {
        int index = 0;

        while(index < fileLocation)
        {
            int emptyBlocks = 1;
            if(disk[index] == ".")
            {
                int innerIndex = index + 1;
                while (disk[innerIndex] == ".")
                {
                    emptyBlocks++;
                    innerIndex++;
                }
                if (emptyBlocks >= blockCount)
                {
                    return (index, true);
                }

                index += emptyBlocks - 1;
            }

            index++;
        }
        return (0, false);
    }

    private static int GetBlockCount(List<string> disk, int i, string id)
    {
        int index = i - 1;
        int blockCount = 1;

        while (index >= 0 && disk[index] == id)
        {
            blockCount++;
            index--;
        }

        return blockCount;
    }
    private static void MoveIndividualBlocks(List<string> tempDisk)
    {
        for (int i = tempDisk.Count - 1; i >= 0; i--)
        {
            string tempFile = tempDisk[i];
            int firstEmptyBlock = tempDisk.IndexOf(".");
            if(firstEmptyBlock > i)
            {
                break;
            }
            tempDisk[firstEmptyBlock] = tempFile;
            tempDisk[i] = ".";
        }
    }

    private static List<string> ParseInput()
    {
        List<string> disk = []; 
        string input = File.ReadAllText(Path.Combine("Day9", "input"));
        int fileCount = 0;
        for(int i = 0; i < input.Length; i++)
        {
            bool isFile = i % 2 == 0;
            int blocks = input[i] - '0';
            for(int j = 0; j < blocks; j++)
            {
                if(isFile)
                {
                    disk.Add(fileCount.ToString());
                }
                else
                {
                    disk.Add(".");
                }
            }

            if(isFile)
            {
                fileCount++;
            }
        }

        return disk;

    }

}