using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivationQuit : MonoBehaviour
{

    [Header("Event Settings")]
    public UnityEvent StartEvents;
    public UnityEvent UpdateEvents;

    // Start is called before the first frame update
    void Start()
    {
        StartEvents?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEvents?.Invoke();
    }

    public void QuitApplication()
    {
        //mematikan game saat dalam editor
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        //mematikan game setelah di build
        Application.Quit();
    }
}
