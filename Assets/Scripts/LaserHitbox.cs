using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitbox : MonoBehaviour
{
    LzrMP4 LZR;
    private void Start()
    {
        LZR = GetComponentInParent<LzrMP4>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Everything is working as planned with laser collisions");
        if(collision.gameObject.layer == 31)
        {
            Debug.Log("Damaged the mouse");
            LZR.KeepDamaging = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 31)
        {
            LZR.KeepDamaging = false;
        }
    }
}
