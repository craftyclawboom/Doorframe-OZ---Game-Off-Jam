using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseScript : MonoBehaviour
{
    public static MouseScript instance;

    public Sprite regSprite, hardSprite;

    public int maxHp, hp;
    [HideInInspector] public float iFrames;
    [SerializeField] float clickSize = 1.5f;
    [SerializeField] Animator anim;
    Vector3 mouseStartSize;

    public GameObject hurtHit;
    private float laserHurt;

    public GameObject currentlySelected;
    AudioSource sounds;
    private bool firstVirusClick = true;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        mouseStartSize = transform.localScale;
        hp = maxHp;
        sounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp > maxHp)
        {
            hp = maxHp;
        }

        if(regSprite != null)
        {
            if (maxHp == 1)
            {
                GetComponent<SpriteRenderer>().sprite = hardSprite;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = regSprite;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
           if(SettingsManager.instance.sfx)
           {
               sounds.pitch = Random.Range(.8f, 1.25f);
               sounds.Play();
           }
        }

        //invincibility frames so you dont get insta-pwn'd
        iFrames -= Time.deltaTime;

        #region Keeping the Mouse Inside

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos -= new Vector3(-.3f, .4f, 0f);

        if(pos.x < 13.81 && pos.x > -14)
        {
            if(Input.GetMouseButtonDown(0))
            {
                gameObject.transform.localScale *= (1 / clickSize);
            }

            Cursor.visible = false;
            pos.z = -5;
            transform.position = pos;
        }
        else
        {
            Cursor.visible = true;
        }

        #endregion

        #region Mouse Click Detection

        if (currentlySelected != null)
        {
            GameManager.instance.selectionOutline.transform.position = currentlySelected.transform.position;
            GameManager.instance.selectionOutline.GetComponent<SpriteRenderer>().sortingOrder = 2;

            GameManager.instance.selectionOutline.transform.localScale = new Vector3(14.09772f, 14.09772f, 1f);

            currentlySelected.transform.SetParent(transform);

            currentlySelected.GetComponent<BoxCollider2D>().enabled = false;
        }


        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 29)
            {
                TakeDamage(5);
            }
        }

            //gets the icon under the mouse
            //starts it from the main filemanager script
        if (Input.GetMouseButtonDown(0))
        {
            if(GameManager.instance != null)
            {
                bool did = false;

                if (currentlySelected != null)
                {
                    currentlySelected.transform.SetParent(null);
                    currentlySelected.GetComponent<BoxCollider2D>().enabled = true;

                    currentlySelected.transform.localScale = new Vector3(14.09772f, 14.09772f, 1);

                    currentlySelected = null;
                    GameManager.instance.selectionOutline.transform.position = new Vector3(100, 100, 1);

                    did = true;

                }

                if (hit.collider != null)
                {
                    GameManager.instance.selectionOutline.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    GameManager.instance.selectionOutline.transform.localScale = new Vector3(10.3162f, 10.3162f, 1f);

                    if (hit.collider.GetComponent<FileManager>() != null)
                    {
                        if(hit.collider.GetComponent<FileManager>().fileType != "ViRuS.eXe")
                        {
                            StartCoroutine(GameManager.instance.ScreenShake(.095f, 1));

                            hit.collider.GetComponent<FileManager>().StartOwnScript();
                        }
                        else
                        {
                            GameManager.instance.SpawnBoss();
                        }
                    }

                    if(hit.collider.gameObject.layer == 14)
                    {
                        StartCoroutine(hit.collider.transform.parent.GetComponent<VirusScript>().OnClick());
                    }

                    if (hit.collider.gameObject.layer == 10)
                    {
                        GameManager.instance.selectionOutline.transform.localScale = new Vector3(18.05f, 18.8f, 1f);
                        GameManager.instance.selectionOutline.GetComponent<SpriteRenderer>().sortingOrder = 501;

                        GameManager.instance.SetTrashFile(hit.collider.gameObject);
                    }

                    if(hit.collider.gameObject.layer == 14)
                    {
                        if (firstVirusClick)
                        {
                            GameManager.instance.virusClicks = 0;
                        }
                        GameManager.instance.virusClicks += 1;
                        firstVirusClick = false;
                    }

                    if (!did)
                    {
                        if (hit.collider.gameObject.layer == 30)
                        {
                            currentlySelected = hit.collider.gameObject;
                        }
                    }
                }
            }
           
        }
        if (Input.GetMouseButtonUp(0)) { transform.localScale = mouseStartSize; }

        #endregion

        //Ending the game
        if (hp <= 0)
        {
            Time.timeScale = 1;
            StartCoroutine(glitchToLoseScreen());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 13)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int amount)
    {
        if(iFrames <= 0f)
        {
            StartCoroutine(GameManager.instance.ScreenShake(.1f, 1));

            iFrames = .075f;

            hp -= amount;
            StartCoroutine(FlashColor());
        }
    }

    public void StartLaser(float laserHurtRate)
    {
        anim.SetBool("Glitching", true);
        StartCoroutine(TakeLaserDamage(laserHurtRate));
        laserHurt = laserHurtRate;
    }

    IEnumerator TakeLaserDamage(float laserHurtRate)
    {
        while (LzrMP4.instance.KeepDamaging)
        {
            anim.SetBool("Glitching", true);
            hp -= 1;
            yield return new WaitForSeconds(laserHurtRate);
        }
        anim.SetBool("Glitching", false);
    }

    public IEnumerator FlashColor()
    {
        if (LzrMP4.instance.KeepDamaging)
        {
            StartLaser(laserHurt);
        }

        if(SettingsManager.instance.sfx)
        {
            Instantiate(hurtHit);
        }

        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.1f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(.1f);
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.1f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    IEnumerator glitchToLoseScreen()
    {
        Camera.main.GetComponent<Animator>().Play("GlitchToScene");
        yield return new WaitForSeconds(0.55f);
        SceneManager.LoadScene("LoseScreen");
    }
}
