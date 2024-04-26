using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turtle : MonoBehaviour
{
    static public bool isComplete003;

    public bool isWake,isBossFight;
    public int index = 0;
    public float speed;

    public Cinemachine.CinemachineVirtualCamera c_Vcam;

    public GameObject effect,follow;
    public GameObject player,BOSS;
    public GameObject hpBar, diabeteBar;
    public GameObject[] points;
    public TextAsset plot003;
    public GameObject dialog;
    public AudioSource AS;
    public AudioClip bgm, worldBgm;


    bool chk;
    private Animator anim;
    private Transform playerDefTransform;



    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("player");
        playerDefTransform = player.transform.parent;
    }
    void Update()
    {
        if (isWake) 
        {
            anim.SetBool("swim", true);
            effect.SetActive(false);
        } 
        else
        {
            anim.SetBool("swim", false);
            effect.SetActive(true);

            return;
        }

        if(index == points.Length && PlayerController.is003 && !isComplete003)
        {
            if (PlayerController.hp == 0) return;
            AS.clip = worldBgm;
            AS.Play();
            GameObject.Find("Counter").GetComponent<counter>().UpdateSubs(200000);
            isWake = false;
            isBossFight = false;
            Dialog.is003 = true;
            dialog.SetActive(true);
            Dialog.textAsset = plot003;
            isComplete003 = true;

            hpBar.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            diabeteBar.GetComponent<RectTransform>().localPosition = new Vector3(-141, 2, 0);

            BOSS.transform.GetComponent<Animator>().SetBool("isEnd", true);
        }
        else if(index == points.Length)
        {
            isWake = false;
            isBossFight = false;
        }
        else if(transform.position != points[index].transform.position)
        {
            MoveToNextPoint();
        }
        else 
        {
            index++;
        }
    }

    void MoveToNextPoint()
    {
        if(index == 3)
        {
            if (!chk && player.transform.parent == transform)
            {
                AS.clip = bgm;
                AS.Play();
                chk = true;
            }
            isBossFight = true;
            BOSS.transform.parent = transform;
            BOSS.GetComponent<Animator>().SetTrigger("start");
            if(player.transform.parent == transform) c_Vcam.m_Follow = follow.transform;
            speed = 3.5f;
            if (!isComplete003)
            {
                hpBar.GetComponent<RectTransform>().localPosition = new Vector3(470, -394.5f, 0);
                diabeteBar.GetComponent<RectTransform>().localPosition = new Vector3(-10, -342, 0);
                
            }
        }
        //if (index >= 3) speed = 3.5f;
        Vector2 temp = Vector2.MoveTowards(transform.position, points[index].transform.position, speed * Time.deltaTime);
        //GetComponent<Rigidbody2D>().MovePosition(temp);
        transform.position = temp;
    } 

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "player" && index < 4)
        {
            isWake = true;
            player.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "player")
        {
            if (isBossFight) return;
            player.transform.parent = playerDefTransform;
        }
    }
}
