using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    public string name;
    public Text locationText;

    private Animator anim;
    private void Start()
    {
        anim = locationText.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "player")
        {
            anim.SetTrigger("enter");
            locationText.text = name;
        }
    }   
}
