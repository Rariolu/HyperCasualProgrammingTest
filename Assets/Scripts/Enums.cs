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
    ENEMY,
    TREE
}

public enum END_STATE
{
    WIN,
    LOSE
}

public enum SOUND
{
    BUTTON_CLICK
}

public enum SOUND_TYPE
{
    SFX,
    MUSIC
}

public enum DIR
{
    DOWN = -1,
    LEFT = -2,
    RIGHT = 2,
    UP = 1
}