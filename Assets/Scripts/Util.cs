using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;

public static class Util
{
    /// <summary>
    /// Show the "Loading" scene as a buffer while the desired scene loads.
    /// </summary>
    /// <param name="scene"></param>
    public static void LoadSceneWithLoading(SCENE scene)
    {
        LoadScene(SCENE.LOADING, LoadSceneMode.Additive);
        LoadScene(scene);
    }

    /// <summary>
    /// Load a given scene either to replace the one that's currently loaded (LoadSceneMode.Single) or co-exist with it (LoadSceneMode.Additive).
    /// </summary>
    /// <param name="scene">An enum representing the build index of the chosen scene.</param>
    /// <param name="mode">The mode in which to load the scene.</param>
    public static void LoadScene(SCENE scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene((int)scene, mode);
    }

    /// <summary>
    /// Close the application if it is a build or stop playing the game if it is running inside the editor.
    /// </summary>
    public static void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}