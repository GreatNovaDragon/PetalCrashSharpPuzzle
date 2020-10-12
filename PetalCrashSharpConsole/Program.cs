using PetalCrashSharpLib;
using System;

namespace PetalCrashSharpConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var seed =
                "UGxheWVyAHRlc3QgYgAEACAAFAGJAACJgRAfAAc=";
            var a = new Field(true, seed);
            Display_Field(a);
            Console.WriteLine("----");




        }


        static void Display_Field(Field field)
        {
            System.Threading.Thread.Sleep(50);

            string blocks = "";
            string spawners = "";
            for (int y_loop = 0; y_loop <= field.y_size; y_loop++)
            {
                for (int x_loop = 0; x_loop <= field.x_size; x_loop++)
                {

                    blocks = blocks + "|" + field.FieldData.BlockArray[x_loop, y_loop];
                    spawners = spawners + "|" + field.FieldData.SpawnerArray[x_loop, y_loop];

                }
                Console.WriteLine(blocks);
                Console.WriteLine(spawners);
                Console.WriteLine("________________________________");
                blocks = "";
                spawners = "";

            }
        }

    }

}