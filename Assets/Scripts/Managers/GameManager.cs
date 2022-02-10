using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton pattern fo all managers
    public static GameManager Instance;

    //count of invaders killed current session
    public int invedersKilled;

    //game pause state
    public bool gamePaused = false;
    
    //false if game just started
    public bool gameStarted = false;
    private void Awake()
    { 
        //destroy unwanted copies for Singleton
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (!gameStarted) StartCoroutine(StartGame());
    }

    //starts game after 3 seconds
    private IEnumerator StartGame()
    {
        gameStarted = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }
    
    //Player lost game
    public void GameOver()
    {
        //saving best score
        if(PlayerPrefs.GetInt("Score") < invedersKilled)
            PlayerPrefs.SetInt("Score", invedersKilled);
        
        //play sound effect of losing
        AudioManager.Instance.Play("Explosion");
        //stop gameplay
        Time.timeScale = 0;
        //show game over menu
        MenuManager.Instance.GameOver();
    }


    
}
