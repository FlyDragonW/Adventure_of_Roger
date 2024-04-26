using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public GameObject[] itemList, uiList;
    static public bool[] isGot;
    static public bool chk;
    public AudioSource AS;
    public AudioClip sfx;
    void Start()
    {
        if (!chk)
        {
            isGot = new bool[5];
            chk = true;
        }
        for (int i = 0; i < itemList.Length; i++)
        {
            if (isGot[i]) itemList[i].SetActive(false);
        }
    }

    
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space)) && PlayerController.isUI)
        {
            PlayerController.isUI = false;
            foreach(GameObject ui in uiList)
            {
                ui.SetActive(false);
            }
        }
    }

    public void GetItem(int index)
    {
        if(index == 0)
        {
            BulletManager.reloadSpeed = 2.4f;
        }
        if(index == 1)
        {
            PlayerController.maxHp += 1;
            PlayerController.hp = PlayerController.maxHp;
            GameObject.Find("Player").GetComponent<PlayerController>().UpdateHP();
        }
        AS.clip = sfx;
        AS.Play();
        PlayerController.isUI = true;
        itemList[index].SetActive(false);
        uiList[index].SetActive(true);
        isGot[index] = true;
        PlayerController.hp = PlayerController.maxHp;
        GameObject.Find("Counter").GetComponent<counter>().UpdateSubs(50000);
    }
}
