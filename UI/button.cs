using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public static bool check;
    public GameObject triangle;
    public GameObject keyboard;
    public GameObject ladder;
    public GameObject transition;
    float timer = 0;

    private void FixedUpdate()
    {
        
        if (check)
        {
            timer += Time.deltaTime;
            if(timer >= 1.5f) ladder.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (check)
        {
            triangle.SetActive(false);
            keyboard.SetActive(false);
            return;
        }
        if(coll.gameObject.tag == "player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                transition.GetComponent<Animator>().SetTrigger("trig");
                check = true;
                
            }
            triangle.SetActive(true);
            keyboard.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        triangle.SetActive(false);
        keyboard.SetActive(false);
    }
}
