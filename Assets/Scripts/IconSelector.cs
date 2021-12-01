using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSelector : MonoBehaviour
{
    public Sprite[] icons; 
    // Start is called before the first frame update
    void Start()
    {
        Image profPic = GetComponent<Image>();
        profPic.sprite = icons[Random.Range(0, icons.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
