using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drWang : MonoBehaviour
{
    public GameObject triangle;
    public GameObject keyboard;
    public GameObject blocker;
    public GameObject dialog;

    public static bool chk;
    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "player")
        {
            if (chk)
            {
                triangle.SetActive(false);
                keyboard.SetActive(false);
                return;
            }
            if (Input.GetKey(KeyCode.E))
            {
                blocker.SetActive(false);
                chk = true;
                dialog.SetActive(true);
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
