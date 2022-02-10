using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //player speed
    public float speed;

    private Rigidbody2D _rb;

    private float playerSize;
    
    private void Awake()
    {
        playerSize = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        _rb = GetComponent<Rigidbody2D>();
    }

    //move players rigidbody depends on input
    private void FixedUpdate()
    {
        bool moveLeft = false;
        bool moveRight = false;
        //touchscreen input
        for(int i = 0;i < Input.touchCount; i++)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            if (touchPos.x < Camera.main.transform.position.x) 
                moveLeft = true;
            else 
                moveRight = true;
        }

        //keyboard input
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveRight = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveLeft = true;
        }

        float border = Camera.main.ViewportToWorldPoint(Vector3.right).x - playerSize/2;
        if(moveRight && _rb.position.x < border)
        {
            //border.x += InvadersGrid.Instance.InvaderSize/2;
            _rb.MovePosition(_rb.position + Vector2.right*Time.fixedDeltaTime*speed);
        }
        border = Camera.main.ViewportToWorldPoint(Vector3.zero).x + playerSize/2;
        if(moveLeft && border < _rb.position.x)
        {
            //border.x -= InvadersGrid.Instance.InvaderSize/2;
            _rb.MovePosition(_rb.position + Vector2.left*Time.fixedDeltaTime*speed);
        }
    }
}
