using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secret : MonoBehaviour
{
    public GameObject fence;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(fence);
        }
    }
}
