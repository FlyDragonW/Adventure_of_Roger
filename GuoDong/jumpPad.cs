using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{
    public GameObject floatArea;
    public GameObject GDroom;
    public float timer = 0;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "player" && timer > 5f)
        {
            coll.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            timer = 0;
            GameObject Area = Instantiate(floatArea,GDroom.transform);
            Destroy(Area, 4f);
        }
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
    }
}
