using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ApplicationQuit : MonoBehaviour
{
    public void OnQuit()
    {
#if UNITY_EDITOR
        if (Application.isPlaying && Application.isEditor)
        {
            EditorApplication.isPlaying = false;
        }
#endif

        Application.Quit();
    }
}
