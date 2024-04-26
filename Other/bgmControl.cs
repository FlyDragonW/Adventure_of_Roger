using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmControl : MonoBehaviour
{
    public AudioClip world, bossFight;
    public AudioSource AS;
    static public float volume = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Dialog.isDiaPausing)
        {
            AS.volume = volume/2;
        }
        else
        {
            AS.volume = volume;
        }
    }
}
