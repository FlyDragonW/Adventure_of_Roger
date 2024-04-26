using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eye : MonoBehaviour
{
    public GameObject player;
    Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        if(playerPos.x > 121f)
        {
            transform.position = new Vector3(0.13f+121.1f, -30.7f, 14.4f);
        }
        else if(playerPos.x < 119)
        {
            transform.position = new Vector3(-0.07f+121.1f, -30.7f, 14.4f);
        }
        else
        {
            transform.position = new Vector3(121.1f, -30.7f, 14.4f);
        }
    }
}
