using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    public bool music, sfx, postPro, extreme;

    private void Awake()
    {
        if(instance != null)
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(music)
        {
            GetComponent<AudioSource>().volume = 1f;
        }
        else
        {
            GetComponent<AudioSource>().volume = 0f;
        }

        if (postPro)
        {
            PostProcessor.instance.gameObject.SetActive(true);
        }
        else
        {
            PostProcessor.instance.gameObject.SetActive(false);
        }

        if (extreme)
        {
            MouseScript.instance.maxHp = 1;
        }
        else
        {
            MouseScript.instance.maxHp = 100;
        }
    }

    public void ChangeMusic(bool t)
    {
        music = t;
    }

    public void ChangeSFX(bool t)
    {
        sfx = t;
    }

    public void ChangePostPro(bool t)
    {
        postPro = t;
    }
    public void ChangeExtreme(bool t)
    {
        extreme = t;
    }
}
