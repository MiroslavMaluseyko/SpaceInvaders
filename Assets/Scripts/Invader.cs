using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class Invader : MonoBehaviour
{
    //speed of invader moving
    public float speed;
    //direction of invader moving
    public Vector2 direction;
    //effect after invader dies
    public ParticleSystem destroyEffect;

    //invaders sprite width
    [HideInInspector]
    public float size;
    //rigidbody for moving
    private Rigidbody2D _rb;

    //number of row in witch invader stays
    public int rowNumber;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + Time.fixedDeltaTime*speed*direction);
    }
    
    public void ChangeDirection()
    {
        direction.x *= -1;
    }
    

    //death 
    //increase score, play all effects, remove invader from list
    public void DestroyInvader()
    {
        GameManager.Instance.invedersKilled++;

        //drop object like medicals 
        Dropper dropper = GetComponent<Dropper>();
        if(dropper != null)dropper.DropRandom();
        
        AudioManager.Instance.Play("Splat");
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EnemyManager.Instance.invaders.Remove(this);
    }
}
