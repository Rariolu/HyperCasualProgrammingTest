using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DemoLevel
{
    public delegate void DirectionButtonPressed(DIR dir);
    [System.Obsolete("This class was used in the demo level and is not to be used outside of that scene.")]
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
            lblCoins.text = string.Format("Coins: {0}", coins);
        }

        public void SetSpeedBursts(uint speedBursts)
        {
            lblSpeedBursts.text = string.Format("Speed Bursts: {0};", speedBursts);
        }

        // Start is called before the first frame update
        void Start()
        {
            Player player;
            if (Player.InstanceAvailable(out player))
            {
                if (btnBurst != null)
                {
                    btnBurst.onClick.AddListener(() => { player.UseBurst(); });
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}