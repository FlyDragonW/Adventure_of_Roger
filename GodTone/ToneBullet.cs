using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToneBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "player" || coll.gameObject.tag == "floor") Destroy(this.gameObject);
    }
}
