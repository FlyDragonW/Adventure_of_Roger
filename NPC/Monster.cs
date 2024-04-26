using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int hp;
    public enum Status { idle,walkR,walkL,track,attack};
    public Status status;
    public float speed,jumpPower,SwitchCD;
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private float SwitchCount;
    public bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        hp = 2;
        SwitchCount = 0;
        player = GameObject.Find("Player");
        status = Status.idle;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SwitchCount += Time.deltaTime;
        if (transform.position.x > player.transform.position.x && (transform.position.x - player.transform.position.x < 8) && sprite.flipX == false && status != Status.attack)
        {
            status = Status.track;
        }
        else if(transform.position.x < player.transform.position.x && (transform.position.x - player.transform.position.x > -8) && sprite.flipX == true && status != Status.attack)
        {
            status = Status.track;
        }
        else
        {
            if (SwitchCount >= SwitchCD && status != Status.attack && status != Status.track)
            {
                int i = Random.Range(0, 6);
                if (i == 0)
                {
                    status = Status.walkR;
                }
                else if (i == 1)
                {
                    status = Status.walkL;
                }
                else
                {
                    status = Status.idle;
                }

                SwitchCount = 0;
            }

            
        }

        

        switch (status)
        {
            case Status.idle:
                anim.SetBool("isWalking", false);
                break;

            case Status.track:
                anim.SetBool("isWalking", true);
                
                int i = Random.Range(0, 100);
                if(SwitchCount >= (SwitchCD * 2) && i == 0)
                {
                    status = Status.attack;
                    SwitchCount = 0;
                }

                if (transform.position.x > player.transform.position.x) //if Mon on player right side
                {
                    if (isAttacking)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    }
                    sprite.flipX = false;
                    transform.Translate(-speed, 0, 0);
                }
                else
                {
                    if (isAttacking)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    }
                    sprite.flipX = true;
                    transform.Translate(speed, 0, 0);
                }
                break;

            case Status.walkR:
                
                sprite.flipX = true;
                transform.Translate(speed * 0.8f, 0, 0);
                anim.SetBool("isWalking", true);
                break;

            case Status.walkL:
                
                sprite.flipX = false;
                transform.Translate(-speed * 0.8f, 0, 0);
                anim.SetBool("isWalking", true);
                break;

            case Status.attack:

                isAttacking = true;
                anim.SetBool("isAttacking", true);
                if(sprite.flipX == false)
                {
                    
                    rb.AddForce(new Vector2(-jumpPower * rb.mass, 40), ForceMode2D.Impulse);
                }
                else
                {
                    
                    rb.AddForce(new Vector2(jumpPower * rb.mass, 40), ForceMode2D.Impulse);
                }
                
                status = Status.track;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "floor")
        {
            isAttacking = false;
            anim.SetBool("isAttacking", false);
        }
        else if (coll.gameObject.tag == "bullet")
        {
            hp -= 1;
            if(hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
