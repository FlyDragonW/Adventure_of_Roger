using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject[] bulletPrefab;
    public GameObject bullet, player,diabeteBar;
    public float aimOffset;

    public float shootCD,diabeteValue,MAXdia;
    private float shootCount;
    static public float reloadSpeed = 1.2f;
    private Animator anim;

    [SerializeField] private AudioSource shootSE;


    private void Start()
    {
        aimOffset = 4;
        anim = player.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Dialog.isDiaPausing) return;

        if (diabeteValue > MAXdia)
        {
            diabeteValue -= diabeteValue - MAXdia;
        }
        else if(diabeteValue < MAXdia)
        {
            diabeteValue += Time.deltaTime * reloadSpeed;
        }

        diabeteBar.transform.localScale = new Vector3(diabeteValue / MAXdia, 1, 1);

        if (!Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J))
        {
            anim.SetBool("isShooting", false);
        }
    }
    
    void FixedUpdate()
    {
        if (Dialog.isDiaPausing) return;

        shootCount -= Time.deltaTime;
        if ((Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.J) )&& shootCount <= 0 && diabeteValue >= 1)
        {
            int i = Random.Range(0, bulletPrefab.Length);
            anim.SetBool("isShooting", true);
            bullet = Instantiate(bulletPrefab[i], transform.position, bulletPrefab[i].transform.rotation);
            bullet.GetComponent<Rigidbody2D>().gravityScale = 0;

            shootSE.Play();

            if (PlayerController.facingRight)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(15, aimOffset), ForceMode2D.Impulse);
                    bullet.transform.position += new Vector3(1.2f, 0, 0);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(15,-aimOffset), ForceMode2D.Impulse);
                    bullet.transform.position += new Vector3(1.2f, 0, 0);
                }
                else
                {
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(15, 0), ForceMode2D.Impulse);
                    bullet.transform.position += new Vector3(1.2f, 0, 0);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15, aimOffset), ForceMode2D.Impulse);
                    bullet.transform.position += new Vector3(-1.2f, 0, 0);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15, -aimOffset), ForceMode2D.Impulse);
                    bullet.transform.position += new Vector3(-1.2f, 0, 0);
                }
                else
                {
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15, -0.4f), ForceMode2D.Impulse);
                    bullet.transform.position += new Vector3(-1.2f, 0, 0);
                }
            }
            diabeteValue -= 1;
            shootCount = shootCD;
            
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.K) || Input.GetKeyUp(KeyCode.J))
            {
                anim.SetBool("isShooting", false);
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J))
        {
            anim.SetBool("isShooting", false);
        }
    }

}
