using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ActivationLoadScene : MonoBehaviour
{
    [Header("Event Settings")]
    public UnityEvent StartEvents;
    public UnityEvent UpdateEvents;

    [Header("Delay Settings")]
    public bool usingDelay;
    public float Delay;
    public UnityEvent DelayEvents;

    // Start is called before the first frame update
    void Start()
    {
        StartEvents?.Invoke();
        if (usingDelay)
        {
            Invoke("LoadScene", Delay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEvents?.Invoke();
    }

    void LoadScene()
    {
        DelayEvents?.Invoke();
    }

    public void LoadScene(string aValue)
    {
        //Melakukan perpindahan antar scene. Catatan: Scene yang dipanggil sudah didaftarkan di Build Setting
        SceneManager.LoadScene(aValue);
    }

    public void RestartScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }

}
