using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    //image to move
    [SerializeField] private RawImage img;
    //in what side will image move
    [SerializeField] private Vector2 direction;

    private void Update()
    {
        //using UV rect to move sprite in [direction] direction
        img.uvRect = new Rect(img.uvRect.position + direction * Time.deltaTime, img.uvRect.size);
    }
}
