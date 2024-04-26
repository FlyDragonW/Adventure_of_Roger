using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
    public GameObject triangle;
    public GameObject keyboard;
    public GameObject dialog, portalUI;
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                //ENDING();
                portalUI.SetActive(true);
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

    void ENDING()
    {
        Dialog.isEND = true;
        dialog.SetActive(true);
    }
}
