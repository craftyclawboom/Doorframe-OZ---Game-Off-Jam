using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public Slider slide;
    public GameObject fadeScreen;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLoading());
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeScreen.GetComponent<Image>().color.a >= 1)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (slide.value >= 1)
        {
            StartCoroutine(startFading());
        }
    }
    public IEnumerator startFading()
    {
        while(true)
        {
            fadeScreen.GetComponent<Image>().color += new Color(0, 0, 0, .025f);
            yield return new WaitForSeconds(2.5f);
        }
    }

    public IEnumerator StartLoading()
    {
        while(true)
        {
            slide.value += Random.Range(0.025f, .075f);
            yield return new WaitForSeconds(.25f);

            text.text = "BOOTING THE OPERATING (Z)YSTEM..";

            slide.value += Random.Range(0.025f, .075f);
            yield return new WaitForSeconds(.25f);

            text.text = "BOOTING THE OPERATING (Z)YSTEM..";

            slide.value += Random.Range(0.025f, .075f);
            yield return new WaitForSeconds(.25f);

            text.text = "BOOTING THE OPERATING (Z)YSTEM...";

            slide.value += Random.Range(0.025f, .075f);
            yield return new WaitForSeconds(.25f);

            text.text = "BOOTING THE OPERATING (Z)YSTEM...";

            slide.value += Random.Range(0.025f, .075f);
            yield return new WaitForSeconds(.25f);

            text.text = "BOOTING THE OPERATING (Z)YSTEM.";

            slide.value += Random.Range(0.025f, .075f);
            yield return new WaitForSeconds(.25f);

            text.text = "BOOTING THE OPERATING (Z)YSTEM.";
        }

    }
}
