using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject fadeScreen;

    public Toggle sfx, music, postProcess, extreme;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(startFading());
        sfx.isOn = SettingsManager.instance.sfx;
        music.isOn = SettingsManager.instance.music;
        postProcess.isOn = SettingsManager.instance.postPro;
        extreme.isOn = SettingsManager.instance.extreme;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeMusic(bool t)
    {
        SettingsManager.instance.music = t;
    }

    public void ChangeSFX(bool t)
    {
        SettingsManager.instance.sfx = t;
    }

    public void ChangePostPro(bool t)
    {
        SettingsManager.instance.postPro = t;
    }
    public void ChangeExtreme(bool t)
    {
        SettingsManager.instance.extreme = t;
    }

    public IEnumerator startFading()
    {
        fadeScreen.gameObject.SetActive(true);

        while (true)
        {
            fadeScreen.GetComponent<Image>().color -= new Color(0, 0, 0, .025f);
            yield return new WaitForSeconds(.05f);
        }
    }
}
