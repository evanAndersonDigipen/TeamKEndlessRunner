//------------------------------------------------------------------------------
//
// File Name:	ScrollHorizontal.cs
// Author(s):	Jeremy Kings (j.kings) - Unity Project
//              Nathan Mueller - original Zero Engine project
// Project:		Endless Runner
// Course:		WANIC VGP
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollHorizontal : MonoBehaviour
{
    public bool FlipDirection = false;
    public float MoveSpeed = 10.0f;
    public float WrapZoneLeft = -18.0f;
    public float WrapZoneRight = 56.0f;
    
    public float randomBumpX = 0;
    public float randomBumpY = 0;

    public GameObject secondPlatform;
    public Collider2D secondPlatformCol;
    //56
    public GameObject[] prefabs;


    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        secondPlatformCol = secondPlatform.GetComponent<Collider2D>();

        if(transform.position.y < secondPlatform.transform.position.y)
        {
            VParallax.nextPanel = this.gameObject;
        }
        else if(transform.position.y == secondPlatform.transform.position.y)
        {
            VParallax.nextPanel = this.gameObject;
        }

        // Store current position
        Vector3 position = transform.position;
        

        // Left --> Right, Reset
        if(FlipDirection)
        {
            if (transform.position.x >= WrapZoneRight)
            {
                position.x = WrapZoneLeft;
            }
        }
        // Left <-- Right, Reset
        else
        {
            if (transform.position.x <= WrapZoneLeft)
            {
                position.x = secondPlatform.transform.position.x+(secondPlatformCol.bounds.size.x/2)+ Random.Range(3.0f, 10.0f); //WrapZoneRight +randomBumpX;
                position.y = secondPlatform.transform.position.y + Random.Range(-10, 7f);

               

                randomBumpX = secondPlatform.transform.position.x + Random.Range(3, 15);
                

                Random.InitState(System.DateTime.Now.Second);

            }
        }

        // Move
        if(FlipDirection)
        {
            position.x += MoveSpeed * Time.deltaTime;
        }
        else
        {
            position.x -= MoveSpeed * Time.deltaTime;
        }

        // Set new position
        transform.position = position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
