using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SCENE
{
    TITLE = 0,
    LOADING = 1,
    GAME = 2,
    END = 3
}

public enum TAG
{
    PLAYER,
    WALL,
    NODE,
    PICKUP,
    ENEMY
}

public enum END_STATE
{
    WIN,
    LOSE
}

namespace DemoLevel
{
    public enum DIR
    {
        DOWN,
        LEFT,
        RIGHT,
        UP
    }
}