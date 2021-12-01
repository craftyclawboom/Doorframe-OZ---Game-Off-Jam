using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodeTyper : MonoBehaviour
{
    public GameObject popup;

    public bool isAttacking;
    public bool completed = false;

    public string powerUp;
    [SerializeField] string[] codeText;

    [SerializeField] TMP_InputField textInput;
    [SerializeField] TMP_Text codeHelperText;
    [SerializeField] TMP_Text placeholderText;
    [SerializeField] TMP_Text inputText;
    [SerializeField] Button submitButton;

    private float currentWait;
    private Animator anim;
    private bool startedAttacking;
    int lastCharType = 0;
    private int typeLength;
    string codeString;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //textInput.onValueChanged.AddListener(delegate { ValueChanged(); });
        /*if (powerUp == "FREEZE.CS")
        {
            codeString = "if(!freeze && unused)\n{\ncomputer.deltaTime = 0f;\n}";
        }
        else if(powerUp == "HEAL.JS")
        {
            codeString = "if(dying)\n{\nHP += 15;\n}";
        }
        else if (powerUp == "SHIELD.LUA")
        {
            codeString = "while(shielding)\n{\nHP = startHP;\n}";
        }
        codeHelperText.SetText(codeString);
        placeholderText.SetText(codeString);
        Debug.Log(powerUp);
        */
    }

    private void Update()
    {
        ValueChanged();
    }
    
    void ValueChanged()
    {
        Debug.Log("The value of the text box changed.");
        if(codeHelperText != null)
        {
            if (!(codeHelperText.text.Substring(0, inputText.text.Length - 1).Equals(inputText.text.Substring(0, inputText.text.Length-1))))
            {
                inputText.color = Color.red;
            }
            else
            {
                inputText.color = Color.white;
                if (codeHelperText.text.Length == inputText.text.Length - 1)
                {
                        inputText.color = Color.green;
                        submitButton.interactable = true;
                }
            }
        }  
    }
    public void DoPowerUp()
    {
        popup.GetComponent<PopupBox>().isCompleted = true;
        GameManager.instance.openedPopup = false;

        if (powerUp == "FREEZE.CS")
        {
            for(int i=0; i< GameManager.instance.fallApps.Length; i++)
            {
                GameManager.instance.fallApps[i].GetComponent<FileManager>().StartCoroutine(GameManager.instance.fallApps[i].GetComponent<FileManager>().StopMakingAttack());
            }
        }
        else if (powerUp == "HEAL.CS")
        {
            MouseScript.instance.hp += 15;
        }
        else if (powerUp == "SHIELD.JS")
        {
            Debug.Log("Activated Shield Powerup");
        }
        gameObject.SetActive(false);
    }
}
