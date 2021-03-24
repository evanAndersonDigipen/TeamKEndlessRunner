using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject Player;
    public float lerpSpeed = .5f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 position = transform.position;
        position = Vector3.Lerp(position, new Vector3(position.x, Player.transform.position.y + 5 , -10), lerpSpeed);
        transform.position = position;
    }
}
