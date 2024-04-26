using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gdStone : MonoBehaviour
{
    static public bool isPlayed;

    public GameObject triangle;
    public GameObject keyboard;

    public GameObject stoneUI,failedUI,achievementUI;
    public Text title,upText,downText;
    public int index = 0;
    public string answer = "UUDUDUUUDDO";
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (isPlayed)
        {
            triangle.SetActive(false);
            keyboard.SetActive(false);
        }
        if(coll.gameObject.tag == "player" && !isPlayed)
        {
            triangle.SetActive(true);
            keyboard.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                isPlayed = true;
                UpdateText();
                stoneUI.SetActive(true);
                Dialog.isDiaPausing = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "player")
        {
            triangle.SetActive(false);
            keyboard.SetActive(false);
        }
    }
    public void BtnUP()
    {
        if (index == 10)
        {
            WIN();
        }
        else if (answer[index] == 'U')
        {
            index++;
            UpdateText();
        }
        else LOSE();
    }

    public void BtnDown()
    {
        if (index == 10)
        {
            WIN();
        }
        if (answer[index] == 'D')
        {
            index++;
            UpdateText();
        }
        else LOSE();
    }

    void UpdateText()
    {
        switch (index)
        {
            case 0:
                title.text = "這什麼到底什麼 __";
                upText.text = "-閃現-";
                downText.text = "-點燃-";
                break;
            case 1:
                title.text = "__ 一打五";
                upText.text = "-瑞斯-";
                downText.text = "-葉問-";
                break;
            case 2:
                title.text = "_____ 冰鳥";
                upText.text = "-NMSL-";
                downText.text = "-還敢下來R-";
                break;
            case 3:
                title.text = "收到耳機架該做什麼？";
                upText.text = "-戴在頭上-";
                downText.text = "-狠狠的 把它甩到旁邊去-";
                break;
            case 4:
                title.text = "海灘褲裡塞什麼？";
                upText.text = "-黃老師的便當-";
                downText.text = "-一百萬-";
                break;
            case 5:
                title.text = "瘋狗的外號 __就有了";
                upText.text = "-十歲-";
                downText.text = "-八歲-";
                break;
            case 6:
                title.text = "我沒投在";
                upText.text = "-*摔倒*-";
                downText.text = "-這個垃圾遊戲上-";
                break;
            case 7:
                title.text = "只要被我看到你的名字 一定";
                upText.text = "-到現場打你 一定打你-";
                downText.text = "-瘋 狂 送 頭-";
                break;
            case 8:
                title.text = "______ 你會愛我嗎";
                upText.text = "-瑞斯 開剁-";
                downText.text = "-如果我是DJ-";
                break;
            case 9:
                title.text = "閃現 ___ 拉進垃圾車！";
                upText.text = "-嘟嘟嘟-";
                downText.text = "-噠噠噠-";
                break;
            case 10:
                title.text = "要不是有____我現在可能很慘";
                upText.text = "-亞洲統神-";
                downText.text = "-這個弟弟-";
                break;
        }
    }

    public void WIN()
    {
        achievementUI.SetActive(false);
        achievementUI.SetActive(true);
        stoneUI.SetActive(false);
        GameObject.Find("Counter").GetComponent<counter>().UpdateSubs(100000);
    }

    public void LOSE()
    {
        failedUI.SetActive(false);
        failedUI.SetActive(true);
        stoneUI.SetActive(false);
        Dialog.isDiaPausing = false;
    }
}
