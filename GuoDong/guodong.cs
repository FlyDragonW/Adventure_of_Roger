using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guodong : MonoBehaviour
{
    Animator anim;
    public Text text;
    float viewer = 0;
    int DISPLAY;
    float timer = 0;
    bool check = false;
    public GameObject fenceL, fenceR, jumpPad;
    public AudioSource AS;
    public AudioClip bgm;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(viewer >= 10000)
        {
            viewer = 10000;
            Dialog.isDiaWaiting = false;
            if (!check)
            {
                GameObject.Find("Counter").GetComponent<counter>().UpdateSubs(230000);
                Destroy(this.gameObject, 8f);
                check = true;
                anim.SetTrigger("die");
                fenceL.GetComponent<Animator>().SetTrigger("open");
                fenceR.GetComponent<Animator>().SetTrigger("open");
                jumpPad.SetActive(true);
                AS.clip = bgm;
                AS.Play();
            }
            BossFightTrigger.isDefeatGD = true;
        }
        else
        {
            if (!Dialog.isDiaPausing)
            {
                viewer += 20 * Time.deltaTime;
            }
            else
            {
                if (timer > Random.Range(0.8f, 1.5f))
                {
                    viewer = Random.Range(120, 230);
                    timer = 0;
                }
                timer += Time.deltaTime;

            }
        }
        text.text = DISPLAY.ToString();
        DISPLAY = (int)viewer;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "bullet") viewer += Random.Range(150,250);
    }
}
