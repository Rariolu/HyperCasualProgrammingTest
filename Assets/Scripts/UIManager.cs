using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void DirectionButtonPressed(DIR dir);
public class UIManager : MonoBehaviour
{
    #region SingletonSetup
    static UIManager instance;
    public static bool InstanceAvailable(out UIManager uiManager)
    {
        uiManager = instance;
        return instance != null;
    }

    private void Awake()
    {
        instance = this;
        SetDirectionButtonClick(btnDown, DIR.DOWN);
        SetDirectionButtonClick(btnLeft, DIR.LEFT);
        SetDirectionButtonClick(btnRight, DIR.RIGHT);
        SetDirectionButtonClick(btnUp, DIR.UP);
    }
    #endregion
    
    #region UnityEngineFields
    [SerializeField]
    Button btnBurst;

    [SerializeField]
    Button btnDown;

    [SerializeField]
    Button btnLeft;

    [SerializeField]
    Button btnRight;

    [SerializeField]
    Button btnUp;

    [SerializeField]
    Text lblCoins;

    [SerializeField]
    Text lblSpeedBursts;

    [SerializeField]
    Image pbSpeedBursts;

    #endregion

    public DirectionButtonPressed DirectionButtonPressed;

    void SetDirectionButtonClick(Button button, DIR dir)
    {
        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                DirectionButtonPressed(dir);
            });
        }
        else
        {
            Debug.LogFormat("Button for direction {0} is null.", dir);
        }
    }

    public void SetCoins(uint coins)
    {
        lblCoins.text = coins.ToString();//string.Format("Coins: {0}", coins);
    }

    public void SetSpeedBursts(uint speedBursts)
    {
        if (speedBursts > 0)
        {
            pbSpeedBursts.gameObject.SetActive(true);
            lblSpeedBursts.text = speedBursts.ToString();
        }
        else
        {
            pbSpeedBursts.gameObject.SetActive(false);
            lblSpeedBursts.text = "";
        }
        //lblSpeedBursts.text = string.Format("Speed Bursts: {0};", speedBursts);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Player.InstanceAvailable(out Player player))
        {
            if (btnBurst != null)
            {
                btnBurst.onClick.AddListener(() => { player.UseBurst(); });
            }
        }

        SetSpeedBursts(0);
    }
}
