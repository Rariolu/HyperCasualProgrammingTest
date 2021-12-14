using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class Util
{
    public static void LoadScene(SCENE scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene((int)scene, mode);
    }
}