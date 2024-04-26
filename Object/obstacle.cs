using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    public GameObject effect;
    public bool isBonus;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            if (isBonus)
                GameObject.Find("Counter").GetComponent<counter>().UpdateSubs(5000);
            else if (!PlayerController.is003) 
                GameObject.Find("Counter").GetComponent<counter>().UpdateSubs(Random.Range(800, 1200));
            Destroy(this.gameObject);
            GameObject temp = Instantiate(effect, transform.position, transform.rotation);
            Destroy(temp, 5f);
        }
        if (coll.gameObject.tag == "player")
        {
            if(isBonus) GameObject.Find("Counter").GetComponent<counter>().UpdateSubs(5000);
            Destroy(this.gameObject);
        }
        
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
