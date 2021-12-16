using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DemoLevel
{
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

        [SerializeField]
        Button btnDown;

        [SerializeField]
        Button btnLeft;

        [SerializeField]
        Button btnRight;

        [SerializeField]
        Button btnUp;

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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}