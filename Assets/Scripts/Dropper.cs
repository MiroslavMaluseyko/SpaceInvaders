using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] private Drop[] objects;

    //drop random things depends on their chances
    public void DropRandom()
    {
        if (objects.Length == 0) return;

        foreach (var drop in objects)
        {
            if(Random.value < drop.chance)
                Instantiate(drop._object, transform.position, Quaternion.identity);
        }
    }
}
