using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    [Header("timeControlParameter")]
    public float activeTime;
    public float activeStart;

    [Header("opasityControl")]
    private float alpha;
    public float alphaSet;
    public float alphaMultiplier;

    private void OnEnable()
    {
        player = GameObject.FindWithTag("player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        if (!PlayerController.facingRight)
        {
            thisSprite.flipX = true;
        }
        else
        {
            thisSprite.flipX = false;
        }
        
        alpha = alphaSet;
        

        thisSprite.sprite = playerSprite.sprite;

        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }
    void FixedUpdate()
    {
        alpha *= alphaMultiplier;
        color = new Color(0.5f, 0.5f, 1, alpha);
        

        thisSprite.color = color;

        if(Time.time >= activeStart + activeTime)
        {
            //return pool
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
