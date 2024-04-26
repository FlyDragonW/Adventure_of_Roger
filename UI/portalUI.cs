using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalUI : MonoBehaviour
{
    public GameObject dialog;
    
    public void yes()
    {
        
        Dialog.isEND = true;
        dialog.SetActive(true);
        transform.gameObject.SetActive(false);
    }

    public void no()
    {
        transform.gameObject.SetActive(false);
    }
}
