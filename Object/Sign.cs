using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public string text;
    public Text signText;
    public GameObject signUI;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "player")
        {
            signUI.SetActive(true);
            signText.text = text;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "player")
        {
            signUI.SetActive(false);
        }
    }
}
