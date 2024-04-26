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
                title.text = "�o����쩳���� __";
                upText.text = "-�{�{-";
                downText.text = "-�I�U-";
                break;
            case 1:
                title.text = "__ �@����";
                upText.text = "-�紵-";
                downText.text = "-����-";
                break;
            case 2:
                title.text = "_____ �B��";
                upText.text = "-NMSL-";
                downText.text = "-�ٴ��U��R-";
                break;
            case 3:
                title.text = "����վ��[�Ӱ�����H";
                upText.text = "-���b�Y�W-";
                downText.text = "-������ �⥦�Ϩ����h-";
                break;
            case 4:
                title.text = "���y�Ǹ̶뤰��H";
                upText.text = "-���Ѯv���K��-";
                downText.text = "-�@�ʸU-";
                break;
            case 5:
                title.text = "�ƪ����~�� __�N���F";
                upText.text = "-�Q��-";
                downText.text = "-�K��-";
                break;
            case 6:
                title.text = "�ڨS��b";
                upText.text = "-*�L��*-";
                downText.text = "-�o�өU���C���W-";
                break;
            case 7:
                title.text = "�u�n�Q�ڬݨ�A���W�r �@�w";
                upText.text = "-��{�����A �@�w���A-";
                downText.text = "-�� �g �e �Y-";
                break;
            case 8:
                title.text = "______ �A�|�R�ڶ�";
                upText.text = "-�紵 �}��-";
                downText.text = "-�p�G�ڬODJ-";
                break;
            case 9:
                title.text = "�{�{ ___ �Զi�U�����I";
                upText.text = "-�ʹʹ�-";
                downText.text = "-������-";
                break;
            case 10:
                title.text = "�n���O��____�ڲ{�b�i��ܺG";
                upText.text = "-�Ȭw�ί�-";
                downText.text = "-�o�ӧ̧�-";
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
