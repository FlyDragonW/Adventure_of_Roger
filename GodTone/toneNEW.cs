using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toneNEW : MonoBehaviour
{
    static public bool isDefeatedTone;

    public GameObject toneUI, HPUI, diabeteUI;

    public GameObject triangle;
    public GameObject keyboard;
    public GameObject player, playerShoot, follow, blocker;
    public Cinemachine.CinemachineVirtualCamera c_Vcam;

    public GameObject dialog;
    public GameObject[] bulletPrefab;
    public GameObject firePoint;
    public bool isAttacking, isDead, start;
    public float switchCD;
    private float switchCount;
    private Animator anim;

    private bool chk,check;

    float num1 = 2f;
    float num2 = 3f;
    // Start is called before the first frame update
    void Start()
    {
        if (isDefeatedTone)
        {
            HPUI.SetActive(true);
            diabeteUI.SetActive(true);
            playerShoot.SetActive(true);
            this.gameObject.SetActive(false);
        }
        isDead = false;
        PlayerController.toneScore = 20;
        anim = GetComponent<Animator>();
        //dialog = GameObject.FindGameObjectWithTag("dialog");
        player = GameObject.FindGameObjectWithTag("player");
        switchCD = Random.Range(1.5f, 2.5f);
    }

    private void Update()
    {
        if (!start) return;
        if (Dialog.isDiaPausing) return;

        switchCount += Time.deltaTime;
        if (switchCount >= switchCD)
        {
            isAttacking = true;
            anim.SetBool("attack", true);
            switchCD = Random.Range(num1, num2);
            switchCount = 0;
        }

        if (PlayerController.toneScore <= 0)
        {
            if (!check)
            {
                GameObject.Find("Counter").GetComponent<counter>().UpdateSubs(200000);
                check = true;
            }
            
            isDead = true;
            anim.SetBool("isDead", true);
            dialog.SetActive(true);
            Dialog.isDiaWaiting = false;
            playerShoot.SetActive(true);
            blocker.SetActive(false);
            c_Vcam.m_Follow = player.transform;
            HPUI.SetActive(true);
            diabeteUI.SetActive(true);
            toneUI.SetActive(false);
            Destroy(this.gameObject, 2f);
            isDefeatedTone = true;
        }
    }


    void FixedUpdate()
    {
        if (Dialog.isDiaPausing || isDead) return;

        if (num1 > 0.5f)
        {
            num1 -= Time.deltaTime / 10;
            num2 -= Time.deltaTime / 10;
        }
        if (!isPlaying(anim, "attack") && isAttacking)
        {
            int i = Random.Range(0, bulletPrefab.Length);
            GameObject bullet;
            bullet = Instantiate(bulletPrefab[i], transform);
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-4f, -10f), Random.Range(1.5f, 2.5f)), ForceMode2D.Impulse);


            isAttacking = false;
            anim.SetBool("attack", false);
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "player")
        {
            if (chk)
            {
                triangle.SetActive(false);
                keyboard.SetActive(false);
                return;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Dialog.isDiaWaiting = false;
                start = true;
                c_Vcam.m_Follow = follow.transform;
                toneUI.SetActive(true);
                blocker.SetActive(true);
                chk = true;
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
    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }
}
