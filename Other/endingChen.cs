using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endingChen : MonoBehaviour
{
    Rigidbody2D rb;
    public float t;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Time.timeScale = t;
        rb.velocity = new Vector2(3.5f, rb.velocity.y);
    }
}
