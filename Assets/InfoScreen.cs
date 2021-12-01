using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoScreen : MonoBehaviour
{
    public GameObject fakeOk, realOk;

    private Vector3 lastPos;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = new Vector3(-8.5f, 13f);
        StartCoroutine(CreateOks());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CreateOks()
    {
        bool thing = true;

        while(thing)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 newPos = new Vector3(lastPos.x + .95f, lastPos.y - .5f, lastPos.z - .05f);

                GameObject obj = Instantiate(fakeOk);
                obj.transform.position = newPos;

                lastPos = newPos;

                yield return new WaitForSeconds(.025f);
            }

            lastPos = new Vector3(Random.Range(-10f, 8.5f), Random.Range(0f, 16f), 0);
        }
    }

    public void LoadLoading()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}
