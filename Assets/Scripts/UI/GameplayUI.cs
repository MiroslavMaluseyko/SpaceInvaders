using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    //reduced Singleton
    public static GameplayUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

}
