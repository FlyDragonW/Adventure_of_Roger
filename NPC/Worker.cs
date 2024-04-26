using UnityEngine;

public class Worker : MonoBehaviour
{
    [Header("Worker Value")]
    public int hp;
    public float RangeX,RangeY,speed, jumpPower, SwitchCD;
    public enum Status { idle, walkR, walkL, track, attackL, attackR, wait};
    public Status status;
    private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;
    private float SwitchCount;
    public bool isAttacking, isFacingLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        SwitchCount = 0;
        player = GameObject.Find("Player");
        status = Status.wait;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LimitSpeed();
        if (Dialog.isDiaPausing) return;
        if (isFacingLeft)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }




        Vector3 workerPos = transform.position;
        Vector3 playerPos = player.transform.position;
        switch (status)
        {
            case Status.wait:
                anim.SetBool("isWalking", false);
                if (Mathf.Abs(workerPos.x - playerPos.x) < RangeX && Mathf.Abs(workerPos.y - playerPos.y) < RangeY) status = Status.idle;
                break;

            case Status.idle:
                anim.SetBool("isWalking", false);
                break;

            case Status.track:
                anim.SetBool("isWalking", true);

                int i = Random.Range(0, 100);
                if (SwitchCount >= (SwitchCD * 2) && i == 0)
                {
                    if (isFacingLeft)
                    {
                        status = Status.attackL;
                    }
                    else
                    {
                        status = Status.attackR;
                    }
                    
                    SwitchCount = 0;
                }

                if(Mathf.Abs(transform.position.x - player.transform.position.x) > 0.5f)
                {
                    if (transform.position.x > player.transform.position.x) //if Mon on player right side
                    {
                        isFacingLeft = true;
                        transform.Translate(-speed, 0, 0);
                    }
                    else
                    {
                        isFacingLeft = false;
                        transform.Translate(speed, 0, 0);
                    }
                }
                break;

            case Status.walkR:
                isFacingLeft = false;
                this.transform.Translate(speed * 0.8f, 0, 0);
                anim.SetBool("isWalking", true);
                break;

            case Status.walkL:
                isFacingLeft = true;
                transform.Translate(-speed * 0.8f, 0, 0);
                anim.SetBool("isWalking", true);
                break;

            case Status.attackL:

                isAttacking = true;
                anim.SetBool("isAttackingL", true);
                rb.AddForce(new Vector2(-jumpPower * rb.mass, 40), ForceMode2D.Impulse);
                status = Status.track;
                break;

            case Status.attackR:
                isAttacking = true;
                anim.SetBool("isAttackingR", true);
                rb.AddForce(new Vector2(jumpPower * rb.mass, 40), ForceMode2D.Impulse);
                status = Status.track;
                break;

        }


        if (status == Status.wait) return;
        SwitchCount += Time.deltaTime;
        if (transform.position.x > player.transform.position.x && (transform.position.x - player.transform.position.x < 8) && isFacingLeft && status != Status.attackR && status != Status.attackL)
        {
            if (transform.position.x - player.transform.position.x > 10 && status == Status.track)
            {
                status = Status.idle;
            }
            else
            {
                status = Status.track;
            }
        }
        else if (transform.position.x < player.transform.position.x && (transform.position.x - player.transform.position.x > -8) && !isFacingLeft && status != Status.attackR && status != Status.attackL)
        {
            if (player.transform.position.x - transform.position.x > 10 && status == Status.track)
            {
                status = Status.idle;
            }
            else
            {
                status = Status.track;
            }
        }
        else
        {
            if (SwitchCount >= SwitchCD && status != Status.attackL && status != Status.attackR && status != Status.track)
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
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (status == Status.wait) return;
        if (coll.gameObject.tag == "floor")
        {
            isAttacking = false;
            anim.SetBool("isAttackingR", false);
            anim.SetBool("isAttackingL", false);
        }
        else if (coll.gameObject.tag == "bullet")
        {
            hp -= 1;
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        if (coll.gameObject.tag == "danger")
        {
            status = Status.idle;
            rb.velocity = Vector2.up * jumpPower * 1.5f;
        }
    }

    void LimitSpeed()
    {
        if (rb.velocity.y > 9.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (rb.velocity.x > 8f)
        {
            rb.velocity -= new Vector2(2.5f, rb.velocity.y);
        }
        else if(rb.velocity.x < -8f)
        {
            rb.velocity += new Vector2(2.5f, rb.velocity.y);
        }
    }
}
