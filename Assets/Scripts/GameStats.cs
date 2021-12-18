using UnityEngine;
using System.Collections;

public class GameStats
{
    #region SingletonSetup
    static GameStats instance;
    public static GameStats Instance
    {
        get
        {
            if (instance == null)
            {
                return instance = new GameStats();
            }
            return instance;
        }
    }

    private GameStats()
    {

    }

    public static void Reset()
    {
        instance = null;
    }
    #endregion

    END_STATE endState;
    public END_STATE EndState
    {
        get
        {
            return endState;
        }
        set
        {
            endState = value;

            Util.LoadSceneWithLoading(SCENE.END);
        }
    }
}