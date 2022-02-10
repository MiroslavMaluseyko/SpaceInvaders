using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    //reduced Singleton
    public static GameOverUI Instance;

    [SerializeField] private TextMeshProUGUI score;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    //when menu activated show this try score
    private void OnEnable()
    {
        score.SetText(GameManager.Instance.invedersKilled.ToString());
    }

    public void Restart()
    {
        MenuManager.Instance.Restart();
    }

    public void ToMenu()
    {   
        MenuManager.Instance.ToMenu();
    }

}
