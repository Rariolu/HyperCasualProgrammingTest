using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;


/// <summary>
/// A static class full of generally useful utility functions.
/// </summary>
public static class Util
{
    static System.Random rand = new System.Random();
    public static DIR Negate(this DIR dir)
    {
        return (DIR)(-(int)dir);
    }

    /// <summary>
    /// Check if the gameobject has the given tag.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static bool GameObjectIs(this GameObject gameObject, TAG tag)
    {
        return gameObject.tag.NormaliseString() == tag.NormaliseString();
    }

    /// <summary>
    /// Return a random element from the given array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="arr"></param>
    /// <returns></returns>
    public static T GetRandomElement<T>(this T[] arr)
    {
        if (arr.Length < 1)
        {
            return default(T);
        }
        int index = rand.Next(arr.Length);
        Debug.LogFormat("Random index: {0}", index);
        return arr[index];
    }

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
    /// Converts the given object into a string and "normalises" it (i.e. trims trailing whitespace and makes it lowercase).
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string NormaliseString(this object obj)
    {
        return obj.ToString().Trim().ToLower();
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