using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tone : MonoBehaviour
{

    public GameObject dialog,portal;
    public GameObject[] bulletPrefab;
    public GameObject firePoint;
    public bool isAttacking,isDead;
    public float switchCD;
    private float switchCount;
    private Animator anim;

    float num1 = 2f;
    float num2 = 3f;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        PlayerController.toneScore = 20;
        anim = GetComponent<Animator>();
        //dialog = GameObject.FindGameObjectWithTag("dialog");
        switchCD = Random.Range(1.5f, 2.5f);
    }

    private void Update()
    {
        if (Dialog.isDiaPausing) return;

        switchCount += Time.deltaTime;
        if(switchCount >= switchCD)
        {
            isAttacking = true;
            anim.SetBool("attack",true);
            switchCD = Random.Range(num1, num2);
            switchCount = 0;
        }

        if(PlayerController.toneScore <= 0)
        {
            isDead = true;
            anim.SetBool("isDead", true);
            dialog.SetActive(true);
            portal.SetActive(true);
            Dialog.isDiaWaiting = false;
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
        if(!isPlaying(anim,"attack") && isAttacking)
        {
            int i = Random.Range(0, bulletPrefab.Length);
            GameObject bullet;
            bullet = Instantiate(bulletPrefab[i], transform);
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-4f,-10f), Random.Range(1.5f, 2.5f)), ForceMode2D.Impulse);


            isAttacking = false;
            anim.SetBool("attack", false);
        }
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
