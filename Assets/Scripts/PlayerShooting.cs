using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //reload time in seconds
    [SerializeField] private float reloadingSeconds;
    //players ammo
    [SerializeField] private Projectile projectilePrefab;
    //point from what shot appears
    [SerializeField] private Transform gunPoint;
    
    private Coroutine shootingCoroutine;
    
    private void Start()
    {
        shootingCoroutine = StartCoroutine(Shooting());
    }

    //coroutine that shoots every [reloadingSeconds] seconds
    private IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(reloadingSeconds);
            Shoot();
        }
    }

    //Plays shot sound and creates laser
    private void Shoot()
    {
        AudioManager.Instance.Play("Laser");
        Instantiate(projectilePrefab, gunPoint.position, Quaternion.identity);
    }
}
