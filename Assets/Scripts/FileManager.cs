using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public GameObject fakeVersion;
    [Space]
    public string fileType;
    public Sprite icon;
    public GameObject fileName;
    TaskbarFile taskbar;
    public GameObject currentFile;

    public GameObject freeze;

    private bool isCompleted;
    private GameObject popup;

    // Start is called before the first frame update
    private void Start()
    {
        currentFile = GameManager.instance.currentFile;
        taskbar = currentFile.GetComponent<TaskbarFile>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.hadBossed)
        {
            if(fakeVersion != null)
            {
                GameObject obj = Instantiate(fakeVersion, transform.position, Quaternion.identity);
                fakeVersion = null;

                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-15.5f, 5.5f), Random.Range(-15.5f, 15.5f));
                obj.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(0, 360f);

                gameObject.SetActive(false);
            }
        }

        if (fileType == "GARBAGE BIN")
        {
            popup = GetComponent<TrashcanAttack>().popup;

            if (GetComponent<TrashcanAttack>().isAttacking)
            {
                isCompleted = true;
            }
        }

        if (fileType == "MOV.EXE")
        {
            popup = GetComponent<MovScript>().popup;

            if (GetComponent<MovScript>().isAttacking)
            {
                isCompleted = true;
            }
        }

        if (fileType == "BOOM.ASE")
        {
            popup = GetComponent<BoomScript>().popup;

            if (GetComponent<BoomScript>().isAttacking)
            {
                isCompleted = true;
            }
        }

        if (fileType == "LZR.MP4")
        {
            popup = GetComponent<LzrMP4>().popup;

            if (GetComponent<LzrMP4>().isAttacking)
            {
                isCompleted = true;
            }
        }


        if (fileType == "STCKMN.EXE")
        {
            popup = GetComponent<LzrMP4>().popup;

            if (GetComponent<LzrMP4>().isAttacking)
            {
                isCompleted = true;
            }
        }

        if(fileType == "FREEZE.CS" || fileType == "HEAL.CS" || fileType == "SHIELD.JS")
        {
            popup = GetComponent<CodeTyper>().popup;
            GetComponent<CodeTyper>().powerUp = fileType;
        }
    }

    public IEnumerator StopMakingAttack()
    {
        freeze.SetActive(true);

        if (fileType == "GARBAGE BIN")
        {
            GetComponent<TrashcanAttack>().isAttacking = false;
        }

        if (fileType == "MOV.EXE")
        {
            GetComponent<MovScript>().isAttacking = false;
        }

        if (fileType == "BOOM.ASE")
        {
            GetComponent<BoomScript>().isAttacking = false;
        }

        if (fileType == "LZR.MP4")
        {
            GetComponent<LzrMP4>().isAttacking = false;
        }

        popup.GetComponent<PopupBox>().isCompleted = false;

        yield return new WaitForSeconds(5f);

        freeze.SetActive(false);

        if (fileType == "GARBAGE BIN")
        {
            GetComponent<TrashcanAttack>().isAttacking = GetComponent<TrashcanAttack>().wasAttacking;
            popup.GetComponent<PopupBox>().isCompleted = GetComponent<TrashcanAttack>().wasAttacking;

        }

        if (fileType == "MOV.EXE")
        {
            GetComponent<MovScript>().isAttacking = GetComponent<MovScript>().wasAttacking;
            popup.GetComponent<PopupBox>().isCompleted = GetComponent<MovScript>().wasAttacking;

        }

        if (fileType == "BOOM.ASE")
        {
            GetComponent<BoomScript>().isAttacking = GetComponent<BoomScript>().wasAttacking;
            popup.GetComponent<PopupBox>().isCompleted = GetComponent<BoomScript>().wasAttacking;

        }

        if (fileType == "LZR.MP4")
        {
            GetComponent<LzrMP4>().isAttacking = GetComponent<LzrMP4>().wasAttacking;
            popup.GetComponent<PopupBox>().isCompleted = GetComponent<LzrMP4>().wasAttacking;

        }
    }

    public void SetNotAttacking()
    {
        if(gameObject.activeInHierarchy)
        {
            Instantiate(fakeVersion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
    public void ChangeNamePos()
    {
        //fileName.transform.parent = GameManager.instance.fileNameHolder.transform;
        //fileName.transform.position = transform.position - new Vector3(0, .2f, 0f);
    }

    public void StartOwnScript()
    {
        if(!GameManager.instance.openedPopup)
        {
            GameManager.instance.selectionOutline.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

            if (!isCompleted)
            {
                popup.GetComponent<PopupBox>().StartAnimation();
                popup.SetActive(true);
            }

            if(!isCompleted)
            {
                currentFile.SetActive(true);

                TaskbarFile f = currentFile.GetComponent<TaskbarFile>();

                f.OpenCurrentFile();

                f.fileName.text = fileType;
                f.icon.sprite = icon;
            }
        }
    }
}
