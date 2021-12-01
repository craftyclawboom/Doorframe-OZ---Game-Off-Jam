using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    Vector3 pos1, pos2;

    bool doingUp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pos1 = new Vector3(transform.position.x, 13.5f, 0f);
        pos2 = new Vector3(transform.position.x, 1.75f, 0f);

        if (doingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos1, 8.5f * Time.deltaTime);

            if (Vector3.Distance(transform.position, pos1) < .1f)
            {
                doingUp = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pos2, 8.5f * Time.deltaTime);

            if (Vector3.Distance(transform.position, pos2) < .1f)
            {
                doingUp = true;
            }
        }
    }
}
