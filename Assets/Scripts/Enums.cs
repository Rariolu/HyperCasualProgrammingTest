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

public enum SOUND
{
    TEMP
}

public enum SOUND_TYPE
{
    SFX,
    MUSIC
}

namespace DemoLevel
{
    public enum DIR
    {
        DOWN = -1,
        LEFT = -2,
        RIGHT = 2,
        UP = 1
    }

    public static class DirTemp
    {
        public static DIR Negate(this DIR dir)
        {
            return (DIR)(-(int)dir);
        }
    }
}