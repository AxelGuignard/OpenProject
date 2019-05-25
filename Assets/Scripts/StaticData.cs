using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    private static string level = "AU CLAIR DE LA LUNE";
    private static string difficulty = "NORMAL";

    public static string Level { get => level; set => level = value; }
    public static string Difficulty { get => difficulty; set => difficulty = value; }
}
