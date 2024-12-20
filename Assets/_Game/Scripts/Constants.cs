using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Constants : MonoBehaviour
{
    //Anim triger
    public const string IS_IDLE = "IsIdle";
    public const string IS_DEAD = "IsDead";
    public const string IS_ATTACK = "IsAttack";
    public const string IS_WIN = "IsWin";
    public const string IS_DANCE = "IsDance";
    public const string IS_ULTI = "IsUlti";

    public const string BOT_TAG = "Bot";
    public const string PLAYER_TAG = "Player";
    public const string OBSTACLE_TAG = "Obstacle";

    public const string EQUIP_OPTION = "Equip";
    public const string EQUIPPED_OPTION = "Equipped";

    public static string[] NAME_LIST = {
        "Alice", "Bob", "Charlie", "Diana", "Ethan", "Fiona",
        "George", "Hannah", "Isaac", "Julia", "Kevin", "Laura",
        "Michael", "Nina", "Oliver", "Paula", "Quentin", "Rachel",
        "Samuel", "Tina", "Victor", "Wendy", "Xavier", "Yvonne", "Zach"
    };

}
