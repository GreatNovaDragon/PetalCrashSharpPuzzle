using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetalCrashSharpLib
{
    public class Puzzle
    {
        /*
    puzzle format it is 
    [Authorname][NULLbyte][Puzzlename][NULLbyte][Version][available moves][clear conditions][solid blocks][spawner colour][spawner timer][zero padding][checksum]
        
     Authorname: Maximum 12 chars
     Puzzlename: Max 16 chars
     Version of puzzle format: 3-bit, currently 0
     available moves: wildcard, up, right, down, left (3bit each)
     clear conditions: 6 bit flags red, blue, orange, green, purple, garbage
     colors are: 0 (empty), 1 (wall), 2 (red), 3 (blue), 4 (orange), 5 (green), 6 (purple), 7 (garbage)
     spawner progress is a number 0-7 representing how many ticks are filled on the spawner

     repeat is aa[amounts of a-2 in 6bit]

     [solid blocks][spawner colour][spawner timer] repeat separately

     zero padding occurs before the checksum, so that the amount of numbers is mod 8 = 0

     checksum is just [Version][available moves][clear conditions][solid blocks][spawner colour][spawner timer][zero padding] and ISBN-13 it 
     Sum up odd-positioned values, sum up even-positioned values times three, add together, mod by 10, subtract that from 10 unless it's 0 then it's just 0
     */


        public static SPuzzle Decode(string puzzle)
        {
            byte[] data = Convert.FromBase64String(puzzle);

            string author = Encoding.Default.GetString(data).Split("\0")[0];
            string name = Encoding.Default.GetString(data).Split("\0")[1];
            int lengthNames = author.Length + name.Length + 2;

            bool[] puzzleArray = ArrayOps.GetBoolArrayFromByteArray(data.Skip(lengthNames).ToArray());

            DataBlock.ThreeList puzzleBitArray = new DataBlock.ThreeList(puzzleArray);

            // Copy 3bits for Puzzle Version 
            int puzzleVersion = puzzleBitArray.NormalInt();
            Console.WriteLine("Ver:" + puzzleVersion);

            int[] moves = new int[5];
            // get the amount of available moves
            for (int i = 0; i <= 4; i++)
            {
                moves[i] = puzzleBitArray.NormalInt();
            }

            //Copy 6bits for clear conditions
            bool[] clearCondition = new bool[6];

            bool[] clearone = puzzleBitArray.Normal();
            clearCondition[0] = clearone[0];
            clearCondition[1] = clearone[1];
            clearCondition[2] = clearone[2];
            bool[] cleartwo = puzzleBitArray.Normal();
            clearCondition[3] = cleartwo[0];
            clearCondition[4] = cleartwo[1];
            clearCondition[5] = cleartwo[2];

            var intList = new List<int>();

            int index = 0;

            while (puzzleBitArray.data.Count != 0)
            {
                int v = puzzleBitArray.NormalInt();
                intList.Add(v);
                index++;
                if (puzzleBitArray.data.Count != 0 && v == puzzleBitArray.data[0].ToInt() && (index == 0 || index % 64 != 0))
                {
                    puzzleBitArray.Normal();
                    int count = puzzleBitArray.NormalInt();
                    count = puzzleBitArray.NormalInt() + count * 8 + 1;
                    for (int i = 0; i < count; i++)
                    {
                        intList.Add(v);
                        index++;
                    }
                }
            }
            //first 64 ints will be Blocks, next 64 are spawner locations. if the spawner location is not filled with 0, save that position in extra array
            Block[,] blockArray = new Block[8, 8];
            Spawner[,] spawnerArray = new Spawner[8, 8];
            var spawnerLocations = new List<int[]>();


            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    blockArray[x, y] = new Block(intList[0]);
                    intList.RemoveAt(0);
                }
            }

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    int i = intList[0];
                    spawnerArray[x, y] = new Spawner(i);
                    intList.RemoveAt(0);
                    if (i != 0) spawnerLocations.Add(new int[] { x, y });
                }
            }
            while (spawnerLocations.Count != 0)
            {
                int x, y;
                x = spawnerLocations[0][0];
                y = spawnerLocations[0][1];
                spawnerLocations.RemoveAt(0);
                spawnerArray[x, y].SetProgress(intList[0]);
                intList.RemoveAt(0);

            }
            return new SPuzzle(author, name, puzzleVersion, clearCondition, moves,
                new Field.CField(spawnerArray, blockArray));
        }

        public struct SPuzzle
        {
            public SPuzzle(string authorName,
                string name,
                int version,
                bool[] clearConditions,
                int[] availableMoves,
                Field.CField field)
            {
                AuthorName = authorName;
                Name = name;
                Version = version;
                ClearConditions = clearConditions;
                AvailableMoves = availableMoves;
                Field = field;
            }

            public string AuthorName, Name;
            public int Version;
            public bool[] ClearConditions;
            public int[] AvailableMoves;
            public Field.CField Field;
        }
    }
}