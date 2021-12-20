using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// A script to manage tutorials when opening the main game scene.
/// Will only load them if they haven't previously been loaded.
/// </summary>
public class TutorialScript : MonoBehaviour
{
    static bool tutorialHasPlayed = false;

    [SerializeField]
    Button btnNext;

    [SerializeField]
    GameObject levelRoot;

    [SerializeField]
    GameObject[] tutorialPanels;

    int panelIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (!tutorialHasPlayed)
        {
            btnNext.onClick.AddListener(BtnNextClick);
            BtnNextClick();
        }
        else
        {
            levelRoot.SetActive(true);
            Destroy(gameObject);
        }
    }

    void BtnNextClick()
    {
        StaticSoundManager.PlaySoundAsync(SOUND.BUTTON_CLICK);
        if (panelIndex > -1)
        {
            tutorialPanels[panelIndex].SetActive(false);
        }

        panelIndex++;
        if (panelIndex < tutorialPanels.Length)
        {
            tutorialPanels[panelIndex].SetActive(true);
        }
        else
        {
            tutorialHasPlayed = true;
            levelRoot.SetActive(true);
            Destroy(gameObject);
        }
    }
}
