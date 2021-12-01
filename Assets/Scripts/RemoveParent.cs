using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveParent : MonoBehaviour
{
    private Vector2 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        transform.position = startPos;

        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5.5f, 5.5f), Random.Range(-5.5f, 5.5f));
        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(0, 360f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
