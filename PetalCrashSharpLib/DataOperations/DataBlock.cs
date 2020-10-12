using System;
using System.Collections.Generic;

public class DataBlock
{
    public class Three
    {
        public bool[] data = new bool[3];
        public Three()
        {
            data = new bool[3];
        }
        public Three(bool first, bool second, bool third)
        {

            data[0] = first;
            data[1] = second;
            data[2] = third;

        }
        public Three(bool[] array)
        {
            if (array.Length != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(array), array, "array must be of size 3");
            }

            data = array;
        }

        public int ToInt()
        {
            int i = 0;
            if (data[0])
            {
                i += 4;
            }

            if (data[1])
            {
                i += 2;
            }

            if (data[2])
            {
                i += 1;
            }

            return i;
        }

    }

    public class Six
    {
        public bool[] data;
        public Six()
        {
            data = new bool[6];
        }

        public Six(Three one, Three two)
        {
            data = new bool[6];
            data[0] = one.data[0];
            data[1] = one.data[1];
            data[2] = one.data[2];
            data[3] = two.data[0];
            data[4] = two.data[1];
            data[5] = two.data[2];
        }
        public Six(bool[] array)
        {
            if (array.Length != 6)
            {
                throw new ArgumentOutOfRangeException(nameof(array), array, "array must be of size 6");
            }
            data = array;
        }

        public int ToInt()
        {
            int i = 0;
            if (data[0])
            {
                i += 32;
            }

            if (data[1])
            {
                i += 16;
            }

            if (data[2])
            {
                i += 8;
            }

            if (data[3])
            {
                i += 4;
            }

            if (data[4])
            {
                i += 2;
            }

            if (data[5])
            {
                i += 1;
            }

            return i;
        }


    }
    public class ThreeList
    {
        public List<Three> data = new List<Three>();
        public ThreeList()
        {
            data.Add(new Three());
        }
        public ThreeList(bool[] boolArray)
        {
            int caseing = 0;
            int shiftmake = 0;

            foreach (var bol in boolArray)
            {
                switch (caseing)
                {

                    case 0:
                        data.Add(new Three(bol, false, false));
                        caseing++;
                        break;

                    case 1:
                        data[shiftmake].data[caseing] = bol;
                        caseing++;
                        break;
                    case 2:
                        data[shiftmake].data[caseing] = bol;
                        caseing = 0;
                        shiftmake++;
                        break;
                }
            }
        }

        public int NormalInt()
        {
            int ret = data[0].ToInt();
            data.RemoveAt(0);
            return ret;
        }
        public bool[] Normal()
        {
            bool[] bol = data[0].data;
            data.RemoveAt(0);
            return bol;
        }


    }


}
