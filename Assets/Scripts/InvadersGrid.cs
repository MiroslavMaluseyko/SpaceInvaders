using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class InvadersGrid : MonoBehaviour
{
    //reduced Singleton
    static public InvadersGrid Instance;
    
    //invaders to spawn
    [SerializeField] private Invader[] prefabs;

    //start rows and columns count
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 6;

    //settings of invaders spawn
    [SerializeField] private float yOffset = 60;

    //list of spawned invaders
    private List<Invader> _invaders;
    
    //count of spawned lines
    public int LinesSpawned { get; private set; }
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        foreach (var invader in prefabs)
        {
            invader.size = invader.GetComponent<SpriteRenderer>().size.x * invader.transform.localScale.x;
        }
    }

    //get reference to invaders
    //spawn first block of invaders
    private void Start()
    {
        _invaders = EnemyManager.Instance.invaders;
        SpawnBlock(rows);
    }

    public void SpawnBlock(int height)
    {
        
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Invader invaderPrefab = prefabs[Random.Range(0, prefabs.Length)];
                //calculating height to spawn invader
                Debug.Log(invaderPrefab.size);
                float h = yOffset - invaderPrefab.size * i;
                Vector3 pos = new Vector3( 0,h,0);
                //create invader
                Invader invader = Instantiate(invaderPrefab, pos, Quaternion.identity,transform);
                //set invader row number
                invader.rowNumber = LinesSpawned + height - i - 1;
                //add invader to list
                _invaders.Add(invader);
            }
            CenterRow(LinesSpawned + height - i - 1);
        }
        //increase count of lines
        LinesSpawned += height;
    }

    //make row number [row] to change direction
    public void TurnRow(int row)
    {
        foreach (var invader in _invaders.FindAll(inv => inv.rowNumber == row))
        {
            invader.ChangeDirection();
        }

    }
    //centre row number [row]
    public void CenterRow(int row)
    {
        //screen width
        float leftBord = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        float length = Camera.main.ViewportToWorldPoint(Vector3.one).x - leftBord;
        float invadersWidth = 0;
        int invadersCount = 0;
        //all invaders in row
        List<Invader> invadersRow = _invaders.FindAll(inv => inv.rowNumber == row);
        
        //calculating offset
        foreach (var invader in invadersRow)
        {
            invadersWidth += invader.size;
            invadersCount++;
        }

        float offset = (length - invadersWidth) / (invadersCount + 1);

        //change positions
        invadersWidth = 0;
        invadersCount = 0;
        
        foreach (var invader in invadersRow)
        {
            invader.gameObject.transform.position = new Vector3(leftBord + invadersWidth + offset * invadersCount + invader.size/2, invader.transform.position.y, 0);
            invadersWidth += invader.size;
            invadersCount++;
        }
    }
}
