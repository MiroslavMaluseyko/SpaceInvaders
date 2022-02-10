using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    //reduced Singleton
    public static PauseUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
