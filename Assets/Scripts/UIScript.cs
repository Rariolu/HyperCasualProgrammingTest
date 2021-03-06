using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    Button btnPlay;

    [SerializeField]
    Button btnQuit;

    protected virtual void Start()
    {
        if (btnPlay != null)
        {
            //Set "PlayLevel" to run when "btnPlay" is clicked.
            btnPlay.onClick.AddListener(() =>
            {
                GameStats.Reset();
                StartCoroutine(PlayLevel());
            });
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
        yield return StaticSoundManager.PlaySound(SOUND.BUTTON_CLICK);
        Util.LoadSceneWithLoading(SCENE.GAME);
    }

    /// <summary>
    /// Wait a predetermined length of time before quitting.
    /// </summary>
    /// <returns></returns>
    IEnumerator Quit()
    {

        yield return StaticSoundManager.PlaySound(SOUND.BUTTON_CLICK);
        Util.Quit();
    }
}
