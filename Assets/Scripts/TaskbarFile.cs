using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskbarFile : MonoBehaviour
{
    public TextMeshProUGUI fileName;
    public Image icon;
    Vector3 scale;

    private void Awake()
    {
        
    }
    private void Start()
    {
        scale = gameObject.transform.localScale;
        gameObject.transform.localScale = Vector3.zero;
        fileName = GetComponentInChildren<TextMeshProUGUI>();
        icon = GetComponentInChildren<Image>();
    }

    public void OpenCurrentFile()
    {
        gameObject.transform.localScale = scale;
        GetComponent<Animator>().Play("Open");
    }

    public void CloseCurrentFile()
    {
        gameObject.transform.localScale = scale;
        GetComponent<Animator>().Play("Close");
    }
}
