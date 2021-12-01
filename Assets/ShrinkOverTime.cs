using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkOverTime : MonoBehaviour
{
    public bool addExtra;
    private float spawnZ;

    // Start is called before the first frame update
    void Start()
    {
        spawnZ = transform.position.z;
        StartCoroutine(WaitTillShrink());
    }

    // Update is called once per frame
    void Update()
    {
        if(addExtra)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, spawnZ + transform.position.y / 10f);

           //if (transform.position.y > 0)
           //{
           //    transform.position = new Vector3(transform.position.x, transform.position.y, spawnZ + transform.position.y / 10f);
           //}
           //else
           //{
           //    transform.position = new Vector3(transform.position.x, transform.position.y, spawnZ - transform.position.y / 10f);
           //}
        }
    }

    IEnumerator WaitTillShrink()
    {
        yield return new WaitForSeconds(2.5f);
        GetComponent<PopupBox>().CloseBox();
    }
}
