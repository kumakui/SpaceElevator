using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorUtil
{
    public static Color GetColor(String colorName)
    {
        if (colorName == "Black")
        {
            return Color.black;
        }else if (colorName == "Red")
        {
            return Color.red;
        }

        return Color.black;
    }
}
