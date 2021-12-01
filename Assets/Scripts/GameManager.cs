using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int timeToBeat;
    private bool ticking;

    public List<GameObject> possibleApps = new List<GameObject>();
    public List<Transform> appSpots = new List<Transform>();

    public PopupBox[] popups;
    public GameObject[] fallApps;

    public bool isBossing, hadBossed;

    public GameObject particlesBossSwirl;
    public GameObject virusApp;

    public List<GameObject> activeApps = new List<GameObject>();

    public GameObject fileNameHolder, popupHolder;

    public GameObject explainPopup;

    public bool music, sfx;

    public GameObject shutDownPopup;

    public TextMeshProUGUI hpText;

    public GameObject selectionOutline;
    public GameObject mouse;
    public GameObject currentFile;

    public GameObject grayDelButton;

    public int virusClicks;

    public TextMeshProUGUI timeText;

    private int curHour, curMin;

    public GameObject closeBoxSound;

    [HideInInspector] public GameObject currentTrashFile;
    public bool openedPopup;

    public GameObject virusHealth;
    public GameObject enableBossObject;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ticking = true;

        RandomizeAppSpots();
        StartCoroutine(AddTime());
    }

    public void Update()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(0f, 6.55f, -10f), 5f* Time.deltaTime);

        int num = 0;

        for (int i = 0; i < popups.Length; i++)
        {
            if (popups[i].isCompleted)
            {
                num++;
            }
        }

        if (num == 4)
        {
            if(!isBossing)
            {
                StartCoroutine(BossBegin());
            }
        }

        #region Time
        int hours = System.DateTime.Now.Hour;
        int minutes = System.DateTime.Now.Minute;

        string actualThing = "";

        if (hours < 12)
        {
            actualThing = " AM";
        }

        if(hours == 12)
        {
            actualThing = " PM";
        }

        if (hours > 12)
        {
            actualThing = " PM";
            hours = hours - 12;
        }

        string niceTime = string.Format("{0:0}:{1:00}", hours, minutes);

        timeText.text = niceTime + actualThing;

        hpText.text = "HEALTH: " + MouseScript.instance.hp + "%";
        #endregion
    }

    public void SpawnBoss()
    {
        enableBossObject.gameObject.SetActive(true);
        StartCoroutine(ScreenShake(.3f, 10));

        virusApp.SetActive(false);
    }

    public IEnumerator ScreenShake(float val, int times)
    {
        for (int i = 0; i < times; i++)
        {
            Camera.main.transform.position += new Vector3(Random.Range(-val, val), Random.Range(-val, val));
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator AddTime()
    {
        while(ticking)
        {
            timeToBeat++;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator BossBegin()
    {
        isBossing = true;
        Debug.Log("PARTICLES!!!!");

        Instantiate(particlesBossSwirl);
        yield return new WaitForSeconds(1.95f);
        virusApp.SetActive(true);
    }

    public void ExplodeAllApps()
    {
        hadBossed = true;
        ticking = false;

        explainPopup.SetActive(true);
    }

    public void CloseThing(bool rand)
    {
        if (!rand)
        {
            if (SettingsManager.instance.sfx)
            {
                Instantiate(closeBoxSound);
            }
        }
        else
        {
            if (SettingsManager.instance.sfx)
            {
                GameObject ran = Instantiate(closeBoxSound);
                ran.GetComponent<AudioSource>().pitch = Random.Range(.9f, 1.25f);
            }
        }
    }

    public void RandomizeAppSpots()
    {
        //set to 4 later
        for (int i = 0; i < 5; i++)
        {
            int num = Random.Range(0, possibleApps.Count);
            int num2 = Random.Range(0, appSpots.Count);

            possibleApps[num].transform.position = appSpots[num2].transform.position;

            possibleApps[num].GetComponent<FileManager>().ChangeNamePos();
            activeApps.Add(possibleApps[num]);

            possibleApps.RemoveAt(num);
            appSpots.RemoveAt(num2);
        }
    }

    public void SetTrashFile(GameObject obj)
    {
        currentTrashFile = obj;
        selectionOutline.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0f);
        grayDelButton.SetActive(false);
    }

    public void DelFile1()
    {
        CloseThing(true);

        selectionOutline.transform.position = new Vector3(100, 100, 0);

        currentTrashFile.GetComponent<Animator>().SetTrigger("thing");
        currentTrashFile = null;
        grayDelButton.SetActive(true);
    }

    public void ShutDownGameOption()
    {
        CloseThing(true);
        shutDownPopup.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitToMain()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}
