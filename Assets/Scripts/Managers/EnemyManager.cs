using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Singleton pattern fo all managers
    static public EnemyManager Instance;

    public float spawnDelaySeconds;
    public float chanceIncPerRow;
    
    private Coroutine spawningCoroutine;
    
    [HideInInspector]
    public List<Invader> invaders;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        
    }

    private void Start()
    {
        //Start spawning enemies
        spawningCoroutine = StartCoroutine(SpawnInvaders());
    }

    private void Update()
    {
        //get left and right side of camera 
        Vector3 borderLeft = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 borderRight = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (var invader in invaders)
        {
            //turn all row of invaders if edge invader crosses border
            if(invader.transform.position.x <= borderLeft.x + invader.size/2 && invader.direction.x < 0)
                InvadersGrid.Instance.TurnRow(invader.rowNumber);
            if(invader.transform.position.x >= borderRight.x - invader.size/2 && invader.direction.x > 0)
                InvadersGrid.Instance.TurnRow(invader.rowNumber);
        }
    }

    //spawning coroutine, that spawn 1 row of invader every [spawnDelaySeconds] seconds
    //and increase invaders chance to shoot every row
    private IEnumerator SpawnInvaders()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelaySeconds);
            InvadersGrid.Instance.SpawnBlock(1);
            IncRowShootChance(InvadersGrid.Instance.LinesSpawned-1);
        }
    }
    
    //adding [chanceIncPerRow] to row`s number [row] shoot chance 
    private void IncRowShootChance(int row)
    {
        foreach (var invader in invaders.FindAll(inv => inv.rowNumber == row))
        {
            invader.GetComponent<InvaderShooting>().IncShootChance(chanceIncPerRow);
        }
    }

}
