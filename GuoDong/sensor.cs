using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensor : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera c_Vcam;
    public GameObject player;
    public GameObject gateL, gateR, GDUI;
    public GameObject diabeteBar, hpBar;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "player")
        {
            c_Vcam.m_Follow = player.transform;
            c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_XDamping = 0.8f;
            c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneHeight = 0.32f;
            c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneWidth = 0;
            c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY = 0.35f;
            c_Vcam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenX = 0.67f;

            GDUI.SetActive(false);
            gateL.GetComponent<Animator>().SetTrigger("open");
            gateR.GetComponent<Animator>().SetTrigger("open");

            hpBar.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            diabeteBar.GetComponent<RectTransform>().localPosition = new Vector3(-141, 2, 0);
        }
    }
    private void Start()
    {
        if (BossFightTrigger.isDefeatGD)
        {
            gateL.GetComponent<Animator>().SetTrigger("open");
            gateR.GetComponent<Animator>().SetTrigger("open");
        }
    }
}
