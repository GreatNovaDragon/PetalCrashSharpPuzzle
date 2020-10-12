using System;
using System.Collections.Generic;


public class ArrayOps
{

    public static bool IsBitSet(byte value, int bitNumber)
    {
        if ((bitNumber < 0) || (bitNumber > 7))
        {
            throw new ArgumentOutOfRangeException(nameof(bitNumber), bitNumber, "bitNumber must be 0..7");
        }

        return ((value & (1 << bitNumber)) != 0);
    }

    public static bool[] GetBoolArrayFromByteArray(byte[] byteArray)
    {
        List<bool> boolList = new List<bool>();

        foreach (var bollvar in byteArray)
        {
            for (int i = 7; i >= 0; i -= 1)
            {
                boolList.Add(IsBitSet(bollvar, i));
            }
        }

        return boolList.ToArray();
    }
}