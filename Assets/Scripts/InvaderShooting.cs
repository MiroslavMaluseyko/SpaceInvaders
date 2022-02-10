using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class InvaderShooting : MonoBehaviour
{
    //invaders chance to shoot
    [SerializeField, Range(0,1)] private float shootChance;
    //time in seconds between shooting tries 
    [SerializeField] private float reloadSeconds;
    //prefab of invaders ammo
    [SerializeField] private Projectile missilePrefab;
    //point from invaders shoot
    [SerializeField] private Transform gunPoint;

    private Coroutine shootingCoroutine;
    private void Start()
    {
        shootingCoroutine = StartCoroutine(Shooting());
    }

    //invader tries to shoot every [reloadSeconds] seconds
    private IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(reloadSeconds);
            if (Random.value < shootChance) Shoot();
        }
    }

    //spawn missile and play shot sound
    public void Shoot()
    {
        AudioManager.Instance.Play("InvaderShot");
        Instantiate(missilePrefab, gunPoint.position, Quaternion.identity);
    }

    //increases chance to shoot
    public void IncShootChance(float chance)
    {
        shootChance += chance;
    }
}
