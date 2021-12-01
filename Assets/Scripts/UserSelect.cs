using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSelect : MonoBehaviour
{
    GameObject highlight;
    public string Name;

    // Start is called before the first frame update
    void Start()
    {
        highlight = gameObject.transform.Find("Activated").gameObject;

    }

    private void OnMouseDown()
    {
        Debug.Log("testing");
        highlight.SetActive(true);
    }
}
