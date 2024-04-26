using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class adSign : MonoBehaviour
{
    public GameObject triangle;
    public GameObject keyboard;
    public GameObject adUI, worker, fence, effect;

    public static bool chk;
    void Start()
    {
        if(chk) worker.SetActive(false);
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
            if (Input.GetKey(KeyCode.E))
            {
                chk = true;
                adUI.SetActive(true);
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

    public void SaveBtn()
    {
        adUI.SetActive(false);
        //Application.OpenURL("https://www.youtube.com/channel/UCo23WwgXJjsgToHBXSewzBw");
        fence.SetActive(false);
        GameObject.Find("Counter").GetComponent<counter>().UpdateSubs(50000);
        if (SteamManager.Initialized)
        {
            SteamUserStats.GetAchievement("charity_ambassador", out bool achievementCompleted);
            if (!achievementCompleted)
            {
                SteamUserStats.SetAchievement("charity_ambassador");
                SteamUserStats.StoreStats();
            }
        }
    }

    public void NotSaveBtn()
    {
        adUI.SetActive(false);
        GameObject fx = Instantiate(effect);
        worker.SetActive(false);
        fx.transform.position = worker.transform.position;
        GameObject.Find("Counter").GetComponent<counter>().UpdateSubs(50000);
        if (SteamManager.Initialized)
        {
            SteamUserStats.GetAchievement("ruthless", out bool achievementCompleted);
            if (!achievementCompleted)
            {
                SteamUserStats.SetAchievement("ruthless");
                SteamUserStats.StoreStats();
            }
        }
    }
}
