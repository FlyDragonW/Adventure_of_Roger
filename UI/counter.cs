using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class counter : MonoBehaviour
{
    public static int subscriber;
    public static int completement;
    public static float timer;

    public Text MINText;
    public Text SECText;
    public Text subsText, deathText, completementText;

    public GameObject subsPrefab;
    public GameObject parent;
    void Start()
    {
        //Time.timeScale = 10;
        subsText.text = "璹綷计" + subscriber.ToString();
    }
    void Update()
    {
        timer += Time.deltaTime;
        int temp = (int)timer;
        UpdateText(temp);
        completement = (int)((double)CalcCompletement() / 10 * 100);
        deathText.text = "Ω计" + PlayerController.deathCount.ToString();
        completementText.text = "ЧΘ" + completement.ToString() + '%';

    }

    void UpdateText(int t)
    {
        int min = (int)(t / 60);
        int sec = t % 60;
        if (min < 10) MINText.text = '0' + min.ToString();
        else MINText.text = min.ToString();

        if (sec < 10) SECText.text = '0' + sec.ToString();
        else SECText.text = sec.ToString();
    }

    public void UpdateSubs(int x)
    {
        GameObject temp = Instantiate(subsPrefab);
        
        temp.transform.SetParent(parent.transform);
        temp.transform.localPosition = new Vector3(277, 141, 0);
        temp.transform.localScale = new Vector3(1, 1, 1);
        Text SubsText = temp.GetComponent<Text>();

        float opacity = x/255;
        if (opacity > 1) opacity = 1;
        else if (opacity < 0.2f) opacity = 0.3f;
        
        SubsText.color = new Color(SubsText.color.r, SubsText.color.g, SubsText.color.b, opacity);
        SubsText.text = "璹綷计+" + x.ToString();
        subscriber += x;

        if(subscriber >= 10000)
        {
            subsText.text = "璹綷计" + ((int)(subscriber / 10000)).ToString() + 'w';
        }
        else
        {
            subsText.text = "璹綷计" + subscriber.ToString();
        }
    }

    public int CalcCompletement()
    {
        int temp = 0;
        foreach (bool b in item.isGot) if (b) temp++;
        if (toneNEW.isDefeatedTone) temp++;
        if (turtle.isComplete003) temp++;
        if (BossFightTrigger.isDefeatGD) temp++;
        if (adSign.chk) temp++;
        if (gdStone.isPlayed) temp++;
        return temp;
    }
}
