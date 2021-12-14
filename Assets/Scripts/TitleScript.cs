using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    [SerializeField]
    Button btnPlay;

    [SerializeField]
    Button btnQuit;

    [SerializeField]
    //The amount of seconds to wait between the quit button is pressed
    //and actually exiting the program.
    float quitDelay = 0.25f;
    
    void Start()
    {
        if (btnPlay != null)
        {
            //Set "PlayLevel" to run when "btnPlay" is clicked.
            btnPlay.onClick.AddListener(() => { StartCoroutine(PlayLevel()); });
        }
        else
        {
            Debug.LogError("btnPlay hasn't been set.");
        }

        if (btnQuit != null)
        {
            //Set "Quit" to run when "btnQuit" is clicked.
            btnQuit.onClick.AddListener(() => { StartCoroutine(Quit()); });
        }
        else
        {
            Debug.LogError("btnQuit hasn't been set.");
        }
    }

    IEnumerator PlayLevel()
    {
        yield return 0;
        Util.LoadSceneWithLoading(SCENE.GAME);
    }

    /// <summary>
    /// Wait a predetermined length of time before quitting.
    /// </summary>
    /// <returns></returns>
    IEnumerator Quit()
    {
        yield return new WaitForSeconds(quitDelay);
        Util.Quit();
    }
}