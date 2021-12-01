using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LzrMP4 : MonoBehaviour
{
    public static LzrMP4 instance;

    public GameObject popup;
    public bool isAttacking;

    public Slider decryptSlider;
    public TextMeshProUGUI percentAmt;
    private int percentDone;

    [SerializeField] GameObject bullet;
    GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] int waitSeconds = 10;
    [SerializeField] int laseringTime = 3;
    [SerializeField] bool laser;
    [SerializeField] float damageAmt = .7f;
    [SerializeField] BoxCollider2D laserHitbox;

    private Vector3 pos1, pos2;

    private float currentWait;
    private Animator anim;

    private bool startedAttacking;
    private bool doingUp;
    public bool KeepDamaging;
    private bool startedDestroyMouse;

    public bool wasAttacking;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            wasAttacking = true;
        }

        if (percentDone >= 100)
        {
            GameManager.instance.currentFile.GetComponent<TaskbarFile>().CloseCurrentFile();
            popup.GetComponent<PopupBox>().isCompleted = true;

            percentDone = -100;
        }

        if (popup.GetComponent<PopupBox>().isCompleted)
        {
            GameManager.instance.openedPopup = false;

            isAttacking = true;
            if (!startedAttacking)
            {
                //move up and down setup
                pos1 = new Vector3(transform.position.x, 15.5f, 0f);
                pos2 = new Vector3(transform.position.x, 0.5f, 0f);

                if(pos1.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90f);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, -90f);
                }

                startedAttacking = true;
                currentWait = waitSeconds;
            }
        }

        if (isAttacking)
        {
            Debug.Log("Started Attacking");
            if (KeepDamaging && !startedDestroyMouse)
            {
                startedDestroyMouse = true;
                MouseScript.instance.StartLaser(damageAmt);
            }
            if (doingUp)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos1, 2.5f * Time.deltaTime);

                if (Vector3.Distance(transform.position, pos1) < .1f)
                {
                    doingUp = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, pos2, 2.5f * Time.deltaTime);

                if (Vector3.Distance(transform.position, pos2) < .1f)
                {
                    doingUp = true;
                }
            }

            currentWait -= Time.deltaTime;

            if (currentWait > 0f && currentWait < 2f)
            {
            }

            if (currentWait <= 0)
            {

                currentWait = waitSeconds;
                StartCoroutine(ShootLazer());
            }
        }
    }

    public IEnumerator OpenBox()
    {
        yield return new WaitForSeconds(.75f);

        int ran = Random.Range(0, 100);
        float addAmt = -8.81f;
        if(ran > 50)
        {
            addAmt = 8.81f;
        }

        popup.transform.position = new Vector3(addAmt, popup.transform.position.y, 0f);
        popup.SetActive(true);
    }

    public void Decrypt()
    {
        if(percentDone < 90)
        {
            StartCoroutine(OpenBox());
        }

        percentDone += 10;
        percentAmt.text = percentDone.ToString() + "%";

        decryptSlider.value = percentDone;
    }

    IEnumerator ShootLazer()
    {
        Debug.Log("Started Lazering");
        
        anim.SetTrigger("StartLasering");
        anim.SetBool("StillLasering", true);
        yield return new WaitForSeconds(.4f);
        laserHitbox.enabled = true;
        if(SettingsManager.instance.sfx)
        {
            GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(laseringTime);
        if (SettingsManager.instance.sfx)
        {
            GetComponent<AudioSource>().Stop();
        }
        anim.SetBool("StillLasering", false);
        laserHitbox.enabled = false;
        startedDestroyMouse = false;
    }


}
