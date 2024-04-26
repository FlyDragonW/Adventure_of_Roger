using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    static public bool isDefeatGD;

    public Cinemachine.CinemachineVirtualCamera c_Vcam;
    public GameObject followTarget,boss,boundary,bossUI,dialog,fenceL,fenceR,hpBar,diabeteBar,jumpPad;
    public TextAsset plotGD;
    public bool GD;
    bool check = false;
    public AudioSource AS;
    public AudioClip bgm;

    private void Start()
    {
        //dialog = GameObject.FindGameObjectWithTag("dialog");
        if (isDefeatGD && GD)
        {
            jumpPad.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "player" && !check)
        {
            c_Vcam.m_Follow = followTarget.transform;
            c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_XDamping = 1;
            c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneHeight = 0;
            c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneWidth = 0;
            c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY = 0.5f;
            c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenX = 0.5f;
            Dialog.isDiaWaiting = false;
            if (GD)
            {
                Dialog.isGD = true;
                Dialog.textAsset = plotGD;
                fenceL.SetActive(true);
                fenceR.SetActive(true);
                AS.clip = bgm;
                AS.Play();
            }
            boss.SetActive(true);
            bossUI.SetActive(true);
            boundary.SetActive(true);
            dialog.SetActive(true);
            check = true;

            hpBar.GetComponent<RectTransform>().localPosition = new Vector3(470, -394.5f, 0);
            diabeteBar.GetComponent<RectTransform>().localPosition = new Vector3(-10, -342, 0);
        }
        
    }
}
