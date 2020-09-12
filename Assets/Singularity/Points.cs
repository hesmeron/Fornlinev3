using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Points 
{
    public static int points = 4;

    public static bool BuyForPoint()
    {
        if(points > 0)
        {
            points--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
