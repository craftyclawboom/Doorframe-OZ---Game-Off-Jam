using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomizeName : MonoBehaviour
{
    public string[] names;
    public TextMeshProUGUI t;
    public bool randomizeAll;

    // Start is called before the first frame update
    void Start()
    {
        t.text = names[Random.Range(0, names.Length)];

        if (randomizeAll)
        {
            StartCoroutine(Randomizify());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(randomizeAll)
        {
        }

        if (GetComponent<SpriteRenderer>() != null)
        { 
            if(!GetComponent<SpriteRenderer>().enabled)
            {
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Randomizify()
    {
        bool thing = true;

        while(true)
        {
            if (randomizeAll)
            {
                t.text = names[Random.Range(0, names.Length)];
                yield return new WaitForSeconds(.025f);
            }
        }
    }
}
