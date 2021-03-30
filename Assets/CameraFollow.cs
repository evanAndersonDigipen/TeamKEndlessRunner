using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject Player;
    public float lerpSpeed = .5f;
    public GameObject follow;
    public bool on;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!on)
        {
            Vector3 position = transform.position;
            position = Vector3.Lerp(position, new Vector3(position.x, follow.transform.position.y, position.z), lerpSpeed);
            transform.position = position;
        }
        else
        {
            Vector3 position = transform.position;
            position = Vector3.Lerp(position, new Vector3(position.x, follow.transform.position.y-Camera.main.orthographicSize, position.z), lerpSpeed);
            transform.position = position;
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y - Camera.main.orthographicSize), new Vector3(1, 1));
    }
}
