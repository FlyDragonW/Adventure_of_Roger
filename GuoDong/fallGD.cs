using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallGD : MonoBehaviour
{
    Rigidbody2D rb;
    public float cd = 1.2f;
    public float force = -15;
    public float timer = 0;
    bool check;
    public GameObject MAINGD,effect,follow;
    // Start is called before the first frame update
    void Start()
    {
        follow = GameObject.FindGameObjectWithTag("camera");
        MAINGD = GameObject.FindGameObjectWithTag("GuoDong");
        rb = transform.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer >= cd && !check)
        {
            rb.AddForce(new Vector2(0,force),ForceMode2D.Impulse);
            check = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "floor" || coll.gameObject.tag == "player")
        {
            MAINGD.transform.GetComponent<Animator>().SetBool("Falling", false);
            follow.transform.GetComponent<Animator>().SetTrigger("shake");
            GameObject eff = Instantiate(effect,transform.position,transform.rotation);
            Destroy(eff, 5f);
        }
    }
}
