using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VParallax : MonoBehaviour
{
    public GameObject Skylines;
    public GameObject Stars;
    public GameObject player;
    public float magnitude;
    public float speed;


    public static GameObject nextPanel;
    float playerPosOld = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 skylinesPos = Skylines.transform.position;
        skylinesPos.y = Mathf.Clamp(Vector3.Lerp(skylinesPos, new Vector3(skylinesPos.x, skylinesPos.y + (magnitude * (playerPosOld-player.transform.position.y))), speed).y, float.MinValue, Camera.main.transform.position.y - Camera.main.orthographicSize);
        Skylines.transform.position = skylinesPos;
    }

    IEnumerator playerPosUpdater()
    {
        while (true)
        {
            playerPosOld = player.transform.position.y;
            yield return new WaitForSeconds(1);
        }
    }
}
