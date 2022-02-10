using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

//this class responsible for player health tracking and damage taking
public class Health : MonoBehaviour
{
    //player`s current and maximum health
    public int health;
    public int maxHealth;
    //invulnerability time after taking damage 
    public float invulnerableInSeconds;
    
    //how fast player will flashing during invulnerability
    public float flashingSpeedSeconds;
    
    //hearts in UI
    public Image[] hearts;
    
    //is player invulnerable now
    private bool isInvulnerable = false;

    //player sprite renderer for flash animation
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //set hearts count in UI according to health
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < health;
        }
    }


    //taking damage if not invulnerability time
    public void TakeDamage(int damage)
    {
        //healing
        if (damage < 0)
        {
            health -= damage;
            if (health > maxHealth) health = maxHealth;
            AudioManager.Instance.Play("Heal");
        }
        else
        if (!isInvulnerable)
        {
            health -= damage;
            //if health ends - player dead
            if(health <= 0) GameManager.Instance.GameOver();
            //invulnerability coroutine
            StartCoroutine(Invulnerability());
            //plays taking damage sound
            AudioManager.Instance.Play("Damage");
        }
    }

    private IEnumerator Invulnerability()
    {
        //ons invulnerability and offs it after time
        isInvulnerable = true;
        for (int i = 0; i < invulnerableInSeconds / flashingSpeedSeconds; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashingSpeedSeconds/2);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashingSpeedSeconds/2);
        }
        isInvulnerable = false;
    }
    
}
