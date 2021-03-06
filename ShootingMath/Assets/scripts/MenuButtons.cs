﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
