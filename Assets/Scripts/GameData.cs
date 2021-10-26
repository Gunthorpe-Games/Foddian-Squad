using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int dioHP;
    public static int pogoHP;
    public static int kingHP;
    public enum EnemyType
    {
        Null,
        Slime
    }
    public static EnemyType enemy1;
    public static EnemyType enemy2;
    public static EnemyType enemy3;

    public static Vector2 worldPos;

    public static int lastBattle;
    public static bool inBattle;
}
