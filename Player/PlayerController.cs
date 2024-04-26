using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks;

public class PlayerController : MonoBehaviour
{
    static public bool isPausing;
    static public bool playerDead;
    static public bool facingRight;
    static public bool is003;
    static public int deathCount;
    
    static public float toneScore;

    public GameObject portal, pointer;

    [Header("Player")]
    static public int maxHp = 3;
    static public int hp;
    public float moveSpeed, jumpPower, hurtCD;
    public bool isHurt;
    private float currentHurtCD;
    public int maxJumpTimes;
    private int jumpTimes;
    static public int chair_index;
    public GameObject[] chair;
    public Cinemachine.CinemachineVirtualCamera c_Vcam;

    [Header("Ground")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Header("UI")]
    public GameObject dialog;
    public GameObject hpCounter;
    public GameObject dead_UI;
    public GameObject reload_UI;
    public GameObject heart_UI;
    public GameObject pause_UI;

    [Header("Effect")]
    public GameObject jumpEffect;
    public GameObject woodEffect;

    [Header("Tone")]
    public GameObject toneScoreBar;

    [Header("Dash Parameters")]
    public GameObject pool;
    public bool isAbleToDash;
    public bool isDashing;
    public float dashTime;
    private float dashTimeLeft;
    private float lastDash = -10f;
    public float dashCoolDown;
    public float dashSpeed;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator anim;
    
    static public bool isUI;
    [Header("Sound Effect")]
    public AudioSource AS;
    public AudioClip jumpSE, dashSE, dashSE2, dashSE3;
    
    void Start()
    {
        //turtle.isComplete003 = true;
        //isAbleToDash = true;
        //for test
        if (SteamManager.Initialized)
        {
            SteamUserStats.GetAchievement("i_am_power_man", out bool achievementCompleted);
            if (!achievementCompleted)
            {
                
                SteamUserStats.SetAchievement("i_am_power_man");
                SteamUserStats.StoreStats();
            }
        }

        hp = maxHp;
        toneScore = 20;
        facingRight = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = chair[chair_index].transform.position;
        UpdateHP();
    }

    private void Update()
    {
        if (turtle.isComplete003) maxJumpTimes = 1;
        if (BossFightTrigger.isDefeatGD) isAbleToDash = true;
        if (turtle.isComplete003 && BossFightTrigger.isDefeatGD)
        {
            portal.SetActive(true);
            pointer.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
        if (isGrounded) anim.SetBool("isJumping", false);
        if (!isGrounded) anim.SetBool("isJumping", true);
        if (Dialog.isDiaPausing) return;

        if (isHurt)
        {
            currentHurtCD += Time.deltaTime;
            if(currentHurtCD >= hurtCD)
            {
                isHurt = false;
                currentHurtCD = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isAbleToDash) return;
            if (isUI) return;
            if(Time.time >= lastDash + dashCoolDown)
            {
                ReadyToDash();
                int i = Random.Range(0, 20);
                if (i == 0) PlaySound(dashSE2);
                else if (i == 1) PlaySound(dashSE3);
                else PlaySound(dashSE);
            }
        }

        

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpTimes<maxJumpTimes))
        {
            jumpTimes++;
            if (onChair) return;
            if (isUI) return;
            Vector3 up = transform.TransformDirection(Vector3.up);
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            //if (Physics2D.Raycast(pos, up, 0.1f)) return;

            rb.velocity = Vector2.up * jumpPower;
            //anim.SetBool("isJumping", true);

            GameObject effect;
            effect = Instantiate(jumpEffect);
            effect.transform.position = new Vector3(transform.position.x, transform.position.y - 1.4f, transform.position.z);
            Destroy(effect, 2f);

            PlaySound(jumpSE);
        }
        
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded) jumpTimes = 0;

        Dash();
        if (isDashing)
            return;

        if (Dialog.isDiaPausing || isUI)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isShooting", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }
        //movement
        LimitSpeed();

        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(0.08f, 0, 0);
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            facingRight = true;
            GetComponent<SpriteRenderer>().flipX = false;
            if(isGrounded) anim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(-0.08f, 0, 0);
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            facingRight = false;
            GetComponent<SpriteRenderer>().flipX = true;
            if (isGrounded) anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "turtle")
        {
            pool.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "turtle")
        {
            pool.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (isGrounded) anim.SetBool("isJumping", false);

        UpdateHP();
        if (Dialog.isDiaPausing) return;

        if (coll.gameObject.tag == "worker")
        {
            if (Dialog.isDiaPausing) return;
            if (isDashing) return;
            hurt();
        }

        if(coll.gameObject.tag == "danger")
        {
            if (Dialog.isDiaPausing) return;
            hurt();
            rb.velocity = Vector2.up * jumpPower;
        }

        if(coll.gameObject.tag == "robot_bullet")
        {
            hurt();
        }

        if(coll.gameObject.tag == "obstacle")
        {
            hurt();
        }

    }
    

    public void UpdateHP()
    {
        if (hp <= 0) Dead();
        for(int i = 0; i < maxHp; i++)
        {
            if(hp > i)
            {
                hpCounter.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                hpCounter.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void Dead()
    {
        dead_UI.SetActive(true);
        playerDead = true;
        dialog.SetActive(true);
        //SceneManager.LoadScene(1);
    }
    void UpdateToneScore()
    {
        if(toneScore < 0)
        {
            toneScore = 0;
        }
        else if(toneScore > 20)
        {
            toneScore = 20;
        }
        toneScoreBar.transform.localScale = new Vector3(toneScore / 20, 1, 1);
    }
    void ReadyToDash()
    {
        isDashing = true;

        dashTimeLeft = dashTime;
        lastDash = Time.time;
    }


    bool isBlocked = false;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "toneBullet")
        {
            toneScore -= 1;
            UpdateToneScore();
        }
        if(coll.gameObject.tag == "toneBomb")
        {
            toneScore += 1;
            UpdateToneScore();
        }
        if(coll.gameObject.tag == "portal")
        {
            //SceneManager.LoadScene(1);
        }

        /**if(coll.gameObject.tag == "reload_item")
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
            BulletManager.reloadSpeed = 2.4f;
        }
        if(coll.gameObject.tag == "heart_item")
        {
            if (isBlocked) return;
            rb.velocity = new Vector2(0, rb.velocity.y);
            maxHp += 1;
            hp = maxHp;
            UpdateHP();
        }
        **/
        if(coll.gameObject.tag == "breakable")
        {
            isBlocked = true;
            if (isDashing)
            {
                Destroy(coll.gameObject);
                isDashing = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
                GameObject effect = Instantiate(woodEffect);
                effect.transform.position = transform.position;
                effect.transform.position = new Vector3(effect.transform.position.x, effect.transform.position.y - 1f, effect.transform.position.z);
                isBlocked = false;
                Destroy(effect, 5f);
            }
        }
        if(coll.gameObject.tag == "hammer")
        {
            hp -= 1;
            UpdateHP();
        }
        if(coll.gameObject.tag == "skyBottom" || coll.gameObject.tag == "teleport")
        {
            if (coll.gameObject.tag == "teleport") hp += 1;
            BackToLastChair();

        }
        if (coll.gameObject.tag == "check") is003 = true;
        UpdateHP();
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "breakable") isBlocked = false;
    }

    bool onChair = false;
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "chair" && Input.GetKeyDown(KeyCode.E) && !onChair)
        {
            this.gameObject.transform.position = coll.gameObject.transform.position;
            GetComponent<SpriteRenderer>().enabled = false;
            hp = maxHp;
            UpdateHP();
            onChair = true;
        }
        else if (coll.gameObject.tag == "chair" && gameObject.transform.position.x != coll.gameObject.transform.position.x)
        {
            onChair = false;
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void Dash()
    {
        if (!isAbleToDash) return;

        if (isDashing)
        {
            if(dashTimeLeft > 0 && facingRight)
            {
                
                
                rb.velocity = new Vector2(dashSpeed, rb.velocity.y);

                dashTimeLeft -= Time.deltaTime;

                ShadowPool.instance.GetFromPool();
            }
            else if (dashTimeLeft > 0 && !facingRight)
            {
                

                rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);

                dashTimeLeft -= Time.deltaTime;

                ShadowPool.instance.GetFromPool();
            }

            if(dashTimeLeft <= 0)
            {
                isDashing = false;
                rb.velocity = new Vector2(0,rb.velocity.y);
            }
        }
    }

    void LimitSpeed()
    {
        if (rb.velocity.y > 12f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (rb.velocity.x < 10f && rb.velocity.x > -10f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void BackToLastChair()
    {
        hp -= 1;
        UpdateHP();
        transform.position = chair[chair_index].transform.position;
        c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_XDamping = 1;
        c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneHeight = 0;
        c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneWidth = 0;
        anim.SetTrigger("hurt");
        c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_XDamping = 0.8f;
        c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneHeight = 0.32f;
        c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneWidth = 0;
    }

    //button
    public void Pause()
    {
        Time.timeScale = 0;
        isPausing = true;
        pause_UI.SetActive(true);
    }
    public void ContinueButton()
    {
        Time.timeScale = 1;
        isPausing = false;
        pause_UI.SetActive(false);
    }
    public void RetryButton()
    {
        Time.timeScale = 1;
        pause_UI.SetActive(false);
        dead_UI.SetActive(false);
        playerDead = false;
        Dialog.isDiaPausing = false;
        Dialog.isDiaWaiting = false;
        c_Vcam.m_Follow = transform;
        SceneManager.LoadScene("World");
    }

    void PlaySound(AudioClip AC)
    {
        AS.clip = AC;
        AS.Play();
    }

    void hurt()
    {
        if (isHurt) return;
        hp -= 1;
        anim.SetTrigger("hurt");
        isHurt = true;
        UpdateHP();
    }
}
