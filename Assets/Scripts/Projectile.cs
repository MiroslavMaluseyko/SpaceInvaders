using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //projectile`s speed
    [SerializeField] private float speed;
    //damage (or heal, if damage below zero)
    [SerializeField] private float damage;
    //projectile will be destroyed after [lifeTimeSeconds] seconds
    [SerializeField] private float lifeTimeSeconds;
    //distance to Raycast
    [SerializeField] private float distance;
    //effect when projectile destroyed
    [SerializeField] private ParticleSystem destroyEffect;
    //what objects will projectile interact
    //it is better to use layers, but tags are enough for our task
    [SerializeField] private String tagToShoot;
    [SerializeField] private String tagToIgnore;
    
    private Coroutine destroyCoroutine;

    private void Start()
    {
        StartCoroutine(DestroyProjectileAfterTime());
    }

    private void Update()
    {
        //raycast to know wen projectile shoot something
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag(tagToShoot))
            {
                if(hitInfo.collider.CompareTag("Invader"))
                    hitInfo.collider.GetComponent<Invader>().DestroyInvader();
                if(hitInfo.collider.CompareTag("Player"))
                    hitInfo.collider.GetComponent<Health>().TakeDamage((int)damage);
            }

            if (! hitInfo.collider.CompareTag(tagToIgnore))
                DestroyProjectile();
        }
        //projectile moving
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    //projectile will be destroyed after [lifeTimeSeconds] seconds
    private IEnumerator DestroyProjectileAfterTime()
    {
        yield return new WaitForSeconds(lifeTimeSeconds);
        DestroyProjectile();
    }
    
    //destroy projectile and play effects
    private void DestroyProjectile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
