using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    bool inCollider = false;
    int blastDamage = 10;
    private void Update()
    {
        if (GetComponentInParent<BoomScript>().blast && inCollider)
        {
            GameManager.instance.mouse.GetComponent<MouseScript>().TakeDamage(blastDamage);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider == GameManager.instance.mouse.GetComponent<Collider2D>())
        {
            inCollider = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == GameManager.instance.mouse.GetComponent<Collider2D>())
        {
            inCollider = false;
        }
    }
}
