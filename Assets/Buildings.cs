using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    // Start is called before the first frame update
    ScrollHorizontal ScrollHorizontal;
    void Start()
    {
        ScrollHorizontal = transform.parent.GetComponent<ScrollHorizontal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Side Collider")
        {
            Debug.Log("yeet");
            ScrollHorizontal.BasicMoveSpeed = ScrollHorizontal.ActiveMoveSpeed;
            ScrollHorizontal.ActiveMoveSpeed = 0;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Side Collider")
        {
            ScrollHorizontal.ActiveMoveSpeed = ScrollHorizontal.BasicMoveSpeed;
        }
    }
}
