using UnityEngine;
using UnityEditor;
using System;
using System.Threading;

public class BakedTimerWindow : EditorWindow
{
    public bool isBakeStart = false;
    public bool isBakeUpadate = false;
    public bool isBakeCompleted = false;

    public System.DateTime bakeStartTime;
    public System.DateTime bakeCompletedTime;
    public void Awake()
    {
        Lightmapping.bakeStarted += OnBakeStarted;
        Lightmapping.lightingDataUpdated += OnLightingDataUpdated;
        Lightmapping.bakeCompleted += OnBakeCompleted;
    }

    public void OnDestroy()
    {
        Lightmapping.bakeStarted -= OnBakeStarted;
        Lightmapping.lightingDataUpdated -= OnLightingDataUpdated;
        Lightmapping.bakeCompleted -= OnBakeCompleted;
    }

    public void OnGUI()
    {
        if (isBakeStart)
        {
            GUILayout.Label("开始时间: " +bakeStartTime, EditorStyles.boldLabel);
        }

        if (isBakeCompleted)
        {
            GUILayout.Label("--------------------------------------------------------");
            GUILayout.Label("开始时间: " + bakeStartTime, EditorStyles.boldLabel);
            GUILayout.Label("结束时间: " + bakeCompletedTime.ToString(), EditorStyles.boldLabel);

            TimeSpan time = bakeCompletedTime - bakeStartTime;
            GUILayout.Label("花费时间: " + time.ToString(), EditorStyles.boldLabel);
        }

        if (isBakeUpadate && Lightmapping.isRunning)
        {
            TimeSpan time = DateTime.Now - bakeStartTime;
            GUILayout.Label("花费时间: " +  time.ToString(), EditorStyles.boldLabel);
        }
    }

    public void OnInspectorUpdate()
    {
        Repaint();
    }

    [MenuItem("Timor/Window/BakedTimer")]
    public static void ShowWindow()
    {
        BakedTimerWindow window = GetWindow<BakedTimerWindow>("烘焙计时");
    }
    public void OnBakeStarted()
    {
        isBakeStart = true;
        bakeStartTime = DateTime.Now;
        isBakeCompleted = false;
    }
    public void OnLightingDataUpdated()
    {
        isBakeUpadate = true;
    }
    public void OnBakeCompleted()
    {
        isBakeCompleted = true;
        bakeCompletedTime = DateTime.Now;

        isBakeStart = false;
        isBakeUpadate = false;
    }
}
