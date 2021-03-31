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
    float BasicMoveSpeed = 10.0f;
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
        Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
        transform.GetChild(0).localPosition = new Vector3(0, 0 - transform.GetChild(0).GetComponent<Collider2D>().bounds.size.y);
    }
    // Update is called once per frame
    void Update()
    {
        secondPlatformCol = secondPlatform.transform.GetChild(0).GetComponent<Collider2D>();

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
                position.x = secondPlatform.transform.position.x+(secondPlatformCol.bounds.size.x)+ Random.Range(3.0f, 10.0f); //WrapZoneRight +randomBumpX;
                position.y = secondPlatform.transform.position.y-(secondPlatformCol.bounds.size.y/2) + Random.Range(-5, 10f);
                Destroy(transform.GetChild(0).gameObject);
                Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);

                transform.GetChild(0).localPosition = new Vector3(0, 0-transform.GetChild(0).GetComponent<Collider2D>().bounds.size.y);

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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            MoveSpeed = 0;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    private void OnDrawGizmos()
    {
        Vector3 Size = new Vector3(1, 1);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Size);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector3(WrapZoneLeft, 0), Size);
    }

}
