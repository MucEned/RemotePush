using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDefine : ScriptableObject
{
    public static Color White = Color.white;
    public static Color Green = Color.green;
    public static Color Blue = Color.cyan;
    public static Color Yellow = Color.yellow;
    public static Color Red = Color.red;
    public static Color Purple = Color.magenta;
    public static Color Gray = Color.gray;
    public static Color DarkBlue = Color.blue;
    public static Color[] ColorSet = new Color[] {Red, Yellow, Blue, Green, Purple, DarkBlue, Gray, White};
}
