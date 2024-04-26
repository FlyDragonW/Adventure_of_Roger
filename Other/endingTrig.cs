using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endingTrig : MonoBehaviour
{
    public GameObject UI;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "player")
        {
            UI.SetActive(true);
        }
    }
}
