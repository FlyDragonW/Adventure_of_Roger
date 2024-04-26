using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera c_Vcam;
    public GameObject point,player;
    public bool active;
    public float speed;
    private Transform playerDefTransform;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerDefTransform = player.transform.parent;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "player")
        {
            player.transform.parent = transform;
            active = true;
            c_Vcam.m_Follow = player.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "player") player.transform.parent = playerDefTransform;
    }

    private void Update()
    {
        if (active && transform.position != point.transform.position)
        {
            Vector2 temp = Vector2.MoveTowards(transform.position, point.transform.position, speed*Time.deltaTime);
            transform.position = temp;
        }
    }
}
