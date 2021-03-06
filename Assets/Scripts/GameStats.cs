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

    uint coins;
    public uint Coins
    {
        get
        {
            return coins;
        }
        set
        {
            coins = value;
            if (UIManager.InstanceAvailable(out UIManager uiManager))
            {
                uiManager.SetCoins(coins);
            }
        }
    }

    uint collectedSpeedBursts = 0;
    public uint CollectedSpeedBursts
    {
        get
        {
            return collectedSpeedBursts;
        }
    }

    uint speedBursts;
    public uint SpeedBursts
    {
        get
        {
            return speedBursts;
        }
        set
        {
            speedBursts = value;
            if (UIManager.InstanceAvailable(out UIManager uIManager))
            {
                uIManager.SetSpeedBursts(speedBursts);
            }
        }
    }
    
    /// <summary>
    /// The total quantity of speed bursts in the level.
    /// </summary>
    public uint BurstQuantity { get; set; }

    END_STATE endState = END_STATE.LOSE;
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

    public void IncrementCollectedSpeedBursts()
    {
        collectedSpeedBursts++;
    }
}