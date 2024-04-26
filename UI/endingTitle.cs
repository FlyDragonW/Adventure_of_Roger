using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class endingTitle : MonoBehaviour
{
    public Text MINText;
    public Text SECText;
    public Text subsText, deathText, completementText, title;
    bool test = false;

    [Range(0f, 1f)] public float lerpTime;
    public Color[] colors;
    int colorIndex;
    float t;
    int len;


    private int min,sec;
    void Start()
    {
        if (SteamManager.Initialized)
        {
            SteamUserStats.GetAchievement("100k_sub", out bool achievementCompleted);
            if (!achievementCompleted)
            {
                SteamUserStats.SetAchievement("100k_sub");
                SteamUserStats.StoreStats();
            }
        }

        if (counter.subscriber >= 1000000)
        {
            subsText.color = Color.yellow;
            if (SteamManager.Initialized)
            {
                SteamUserStats.GetAchievement("1m_sub", out bool achievementCompleted);
                if (!achievementCompleted)
                {
                    SteamUserStats.SetAchievement("1m_sub");
                    SteamUserStats.StoreStats();
                }
            }
        }
        if (PlayerController.deathCount == 0)
        {
            deathText.color = Color.red;
            if (SteamManager.Initialized)
            {
                SteamUserStats.GetAchievement("gamer", out bool achievementCompleted);
                if (!achievementCompleted)
                {
                    SteamUserStats.SetAchievement("gamer");
                    SteamUserStats.StoreStats();
                }
            }
        }
        if (counter.completement == 100)
        {
            completementText.color = Color.yellow;
            if (SteamManager.Initialized)
            {
                SteamUserStats.GetAchievement("collector", out bool achievementCompleted);
                if (!achievementCompleted)
                {
                    SteamUserStats.SetAchievement("collector");
                    SteamUserStats.StoreStats();
                }
            }
        }


        UpdateText((int)counter.timer);


        if (counter.subscriber > 10000)
        {
            subsText.text = "訂閱人數：" + ((int)(counter.subscriber / 10000)).ToString() + 'w';
        }
        else
        {
            subsText.text = "訂閱人數：" + counter.subscriber.ToString();
        }
        deathText.text = "死亡次數：" + PlayerController.deathCount.ToString();
        completementText.text = "完成度：" + counter.completement + '%';

        len = colors.Length;
    }
    private void Update()
    {
        if (counter.subscriber >= 1000000 && PlayerController.deathCount == 0 && counter.completement == 100 || test)
        {
            title.color = Color.Lerp(title.color, colors[colorIndex], lerpTime * Time.deltaTime);
            t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
            if (t > .9f)
            {
                t = 0f;
                colorIndex++;
                colorIndex = (colorIndex >= colors.Length) ? 0 : colorIndex;
            }
            title.text = "THE END";

            if (SteamManager.Initialized)
            {
                SteamUserStats.GetAchievement("power_man", out bool achievementCompleted);
                if (!achievementCompleted)
                {
                    SteamUserStats.SetAchievement("power_man");
                    SteamUserStats.StoreStats();
                }
            }
        }
    }
    void UpdateText(int t)
    {
        min = (int)(t / 60);
        sec = t % 60;
        if (min < 10) MINText.text = '0' + min.ToString();
        else MINText.text = min.ToString();

        if (sec < 10) SECText.text = '0' + sec.ToString();
        else SECText.text = sec.ToString();

        if (SteamManager.Initialized && min < 10)
        {
            SteamUserStats.GetAchievement("speedrun", out bool achievementCompleted);
            if (!achievementCompleted)
            {
                SteamUserStats.SetAchievement("speedrun");
                SteamUserStats.StoreStats();
            }
        }
    }
}
