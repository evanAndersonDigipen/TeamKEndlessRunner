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
using System.Threading.Tasks;
using UnityEngine;

public class ScrollHorizontal : MonoBehaviour
{
    public bool FlipDirection = false;
    public float MoveSpeed = 10.0f;
    [HideInInspector]
    public float BasicMoveSpeed = 10.0f;
    public static float ActiveMoveSpeed;
    public float WrapZoneLeft = -18.0f;
    public float WrapZoneRight = 56.0f;

    public float AccellerationAmount = 0.001f;

    public float randomBumpX = 0;
    public float randomBumpY = 0;

    public GameObject linkedPlatform;
    public Collider2D secondPlatformCol;
    //56
    public GameObject[] prefabs;
    public GameObject arrow;


    private void Start()
    {
        ActiveMoveSpeed = MoveSpeed;
        BasicMoveSpeed = MoveSpeed;
        if (!FlipDirection)
        {
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
            transform.GetChild(0).localPosition = new Vector3(0, 0 - transform.GetChild(0).GetComponent<Collider2D>().bounds.size.y);
            //secondPlatformCol = linkedPlatform.transform.GetChild(0).GetComponent<Collider2D>();
            Vector3 position = transform.position;
            //position.x = secondPlatform.transform.position.x + (secondPlatformCol.bounds.size.x) + Random.Range(5, 20); //WrapZoneRight +randomBumpX;
            position.y = linkedPlatform.transform.position.y + Random.Range(0, 3);
            transform.position = position;
        }
        

        
        
    }
    // Update is called once per frame
    void Update()
    {


        if (!FlipDirection)
        {
            secondPlatformCol = linkedPlatform.transform.GetChild(0).GetComponent<Collider2D>();
        }
        

        if(transform.position.y < linkedPlatform.transform.position.y)
        {
            VParallax.nextPanel = this.gameObject;
        }
        else if(transform.position.y == linkedPlatform.transform.position.y)
        {
            VParallax.nextPanel = this.gameObject;
        }
        Vector3 position;
        // Store current position
        if (!FlipDirection)
        {
            position = transform.position;
        }
        else
        {
            position = transform.localPosition;
        }
        
        

        // Left --> Right, Reset
        if(FlipDirection)
        {
            Debug.Log(transform.position.x - linkedPlatform.transform.position.x);
            if (transform.position.x <= WrapZoneLeft)
            {
                
                position.x = linkedPlatform.transform.localPosition.x+41;
            }
        }
        // Left <-- Right, Reset
        else
        {
            if (transform.position.x <= WrapZoneLeft)
            {
                //Destroy(transform.GetChild(0).gameObject);
                DestroyImmediate(transform.GetChild(0).gameObject);
                Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
                Task task = asyncBumper();
                //task.Start();

                transform.GetChild(0).localPosition = new Vector3(0, 0 - transform.GetChild(0).GetComponent<Collider2D>().bounds.size.y);
                position.x = linkedPlatform.transform.position.x+(secondPlatformCol.bounds.size.x)+ Random.Range(5, 20); //WrapZoneRight +randomBumpX;
                position.y = linkedPlatform.transform.position.y + Random.Range(-4, 10);
                

                randomBumpX = linkedPlatform.transform.position.x + Random.Range(3, 15);
                

                Random.InitState(System.DateTime.Now.Second);

            }
        }

        // Move
        if(FlipDirection)
        {
            position.x -= ActiveMoveSpeed * Time.deltaTime;
        }
        else
        {
            position.x -= ActiveMoveSpeed * Time.deltaTime;
        }
        /*if(position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
        {
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);
        }*/
        // Set new position
        if (!FlipDirection)
        {
            transform.position = position;
        }
        else
        {
            transform.localPosition = position;
        }
        
    }
    
    private void OnDrawGizmos()
    {
        Vector3 Size = new Vector3(1, 1);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Size);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector3(WrapZoneLeft, 0), Size);
    }
    async Task asyncBumper()
    {
        Debug.Log("testing async");
        await Task.Run(() =>
        {
            while(transform.GetChild(0).position.y >= Camera.main.transform.position.y - Camera.main.orthographicSize)
            {
                transform.Translate(new Vector3(0, -.1f));
            }
        });
    }
    private void FixedUpdate()
    {
        ActiveMoveSpeed += AccellerationAmount;
    }

}
