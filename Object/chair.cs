using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chair : MonoBehaviour
{
    public int index;
    public GameObject player;
    public GameObject triangle;
    public GameObject keyboard;
    public Sprite chenOnChair_sprite;
    public Sprite original_sprite;
    public bool isOnChair;
    public AudioSource AS;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E))
        {
            isOnChair = true;
        }
    }

    
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (gameObject.transform.position.x != player.transform.position.x) isOnChair = false;
        if (coll.gameObject.tag == "player")
        {
            triangle.SetActive(true);
            keyboard.SetActive(true);
            if (isOnChair)
            {
                AS.Play();
                PlayerController.chair_index = index;
                GetComponent<SpriteRenderer>().sprite = chenOnChair_sprite;
                triangle.SetActive(false);
                keyboard.SetActive(false);
            }
            if (isOnChair == false) GetComponent<SpriteRenderer>().sprite = original_sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "player")
        {
            isOnChair = false;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "player")
        {
            triangle.SetActive(false);
            keyboard.SetActive(false);
        }
    }
}
