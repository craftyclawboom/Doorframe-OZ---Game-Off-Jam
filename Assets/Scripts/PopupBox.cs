using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupBox : MonoBehaviour
{
    public bool isCompleted;

    public TextMeshProUGUI currentScore;
    public GameObject xButton;

    private Animator anim;

    private bool makeThing;

    private void Awake()
    {
        makeThing = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }

        if (GameManager.instance != null)
        {
            transform.parent = GameManager.instance.popupHolder.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCompleted)
        {
            xButton.GetComponent<Image>().color = new Color(1, 0, 0);
            xButton.GetComponent<Button>().enabled = true;
        }
        else
        {
            if (!makeThing)
            {
                if(GameManager.instance != null)
                {
                    GameManager.instance.openedPopup = false;
                }
            }
            else
            {
                if (GameManager.instance != null)
                {
                    GameManager.instance.openedPopup = true;
                }
            }
        }
    }

    public void ClosePowerupBox()
    {
        GameManager.instance.CloseThing(false);

        StartCoroutine(CloseBox2());
        GameManager.instance.currentFile.GetComponent<TaskbarFile>().CloseCurrentFile();

        makeThing = false;
    }

    public void CloseBox()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.CloseThing(false);
        }

        StartCoroutine(CloseBox2());
        if (GameManager.instance != null)
        {
            GameManager.instance.currentFile.GetComponent<TaskbarFile>().CloseCurrentFile();
        }
    }

    public void OpenAndRecloseBoxThing()
    {
        GameManager.instance.CloseThing(true);

        StartCoroutine(OpenAndRecloseBoxThing2());
    }

    public void CreateNewPopup()
    {

        if (MovScript.instance.boxesLeft > 0)
        {
            GameManager.instance.CloseThing(true);


            MovScript.instance.CreateNewError();
            StartCoroutine(CloseBox2());
        }
        else if (MovScript.instance.boxesLeft == 0)
        {
            GameManager.instance.CloseThing(false);

            MovScript.instance.popup.GetComponent<PopupBox>().isCompleted = true;

            StartCoroutine(CloseBox2());
            GameManager.instance.currentFile.GetComponent<TaskbarFile>().CloseCurrentFile();
        }
    }

    IEnumerator OpenAndRecloseBoxThing2()
    {
        if (anim != null)
        {
            anim.SetTrigger("Close");
        }

        yield return new WaitForSeconds(.2f);

        LzrMP4.instance.Decrypt();
        gameObject.SetActive(false);
    }

    IEnumerator CloseBox2()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.openedPopup = false;
        }

        if (anim != null)
        {
            anim.SetTrigger("Close");
        }

        yield return new WaitForSeconds(.2f);

        gameObject.SetActive(false);
    }

    public void StartAnimation()
    {
        if (anim != null)
        {
            anim.SetTrigger("Open");
        }
    }
}
