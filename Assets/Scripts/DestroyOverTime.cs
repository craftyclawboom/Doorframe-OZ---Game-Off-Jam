using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDestroying());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartDestroying()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
