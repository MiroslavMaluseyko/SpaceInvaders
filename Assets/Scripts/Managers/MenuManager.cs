using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Singleton pattern fo all managers
    public static MenuManager Instance;

    //prefabs of all pop-up menus
    public PauseUI pausePrefab;
    public GameOverUI gameOverPrefab;
    public SettingsUI settingsPrefab;
    private void Awake()
    { 
        //destroy unwanted copies for Singleton
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MenuManager");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        
        if (Instance == null) Instance = this;
        
        DontDestroyOnLoad(this);
    }

    //restart option
    //change pause state, make time move, load gameplay scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.gamePaused = false;
        Time.timeScale = 1;
    }
    
    //pause option
    //change pause state, show pause menu, stop time
    public void Pause()
    {
        if (PauseUI.Instance == null)
        {
            Instantiate(pausePrefab);
        }
        
        if (GameManager.Instance.gamePaused) return;
        PauseUI.Instance.gameObject.SetActive(true);
        GameManager.Instance.gamePaused = true;
        Time.timeScale = 0;
    }
    
    //unpause option
    //change pause state, hide pause menu, make time move
    public void Unpause()
    {
        
        if (!GameManager.Instance.gamePaused) return;
        if (PauseUI.Instance == null)
        {
            Instantiate(pausePrefab);
        }
        
        PauseUI.Instance.gameObject.SetActive(false);
        GameManager.Instance.gamePaused = false;
        Time.timeScale = 1;
    }

    //game over option
    //stop time, show game over menu
    public void GameOver()
    {
        if (GameOverUI.Instance == null)
        {
            Instantiate(gameOverPrefab);
        }
        Time.timeScale = 0;
        GameOverUI.Instance.gameObject.SetActive(true);
    }

    //to main menu option
    //make time move, load main menu
    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    //start game option
    //change pause state, load gameplay, make time move
    public void StartGame()
    {
        
        SceneManager.LoadScene("Gameplay");
        GameManager.Instance.gamePaused = false;
        Time.timeScale = 1;
    }

    //quit option
    public void Quit()
    {
        Application.Quit();
    }

    //open settings menu option
    public void SettingsOn()
    {
        if (SettingsUI.Instance == null)
        {
            Instantiate(settingsPrefab);
        }
        
        SettingsUI.Instance.gameObject.SetActive(true);
    }
    
    //close settings menu option
    public void SettingsOff()
    {
        if (SettingsUI.Instance == null)
        {
            Instantiate(settingsPrefab);
        }
        
        SettingsUI.Instance.gameObject.SetActive(false);
    }
}
