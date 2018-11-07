#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingFixerUpper {
    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        Debug.Log("Fixing lighting");
        SceneManager.sceneLoaded += (s, m) => DynamicGI.UpdateEnvironment();
    }
}
#endif