using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovScript : MonoBehaviour
{
    public static MovScript instance;

    public GameObject popup;
    public bool isAttacking;

    [SerializeField] GameObject bullet;
    GameObject bulletPrefab;
    public float bulletSpeed = 2f;
    public int movIconSpeed = 1;
    [SerializeField] int waitSeconds = 4;

    public GameObject bounceSfx;

    public GameObject errorBox;
    public int boxesLeft;

    private float currentWait;
    private Animator anim;

    public bool wasAttacking;

    Vector2 oldVelocity;

    private bool startedAttacking, didTheAttacking;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        anim = GetComponent<Animator>();
        boxesLeft = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            wasAttacking = true;
        }

        if (boxesLeft == -1)
        {
            popup.GetComponent<PopupBox>().isCompleted = true;
        }

        if (popup.GetComponent<PopupBox>().isCompleted)
        {
            GameManager.instance.openedPopup = false;

            isAttacking = true;
            if (!startedAttacking)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

                GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 5f);

                startedAttacking = true;
                currentWait = waitSeconds;
            }
        }

        if (isAttacking)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            anim.enabled = true;

            if(didTheAttacking)
            {
                GetComponent<Rigidbody2D>().velocity = oldVelocity;
                didTheAttacking = false;
            }


            currentWait -= Time.deltaTime;

            if (currentWait > 0f && currentWait < 2f)
            {
                anim.SetBool("Reloading", true);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 5f * Time.deltaTime);
            }

            if (currentWait <= 0)
            {
                anim.SetTrigger("Shoot");

                currentWait = waitSeconds;
                ShootBullet();
            }
        }
        else
        {
            if(wasAttacking)
            {
                if(!didTheAttacking)
                {
                    oldVelocity = GetComponent<Rigidbody2D>().velocity;
                    didTheAttacking = true;
                }
            }

            anim.enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void CreateNewError()
    {
        if (boxesLeft > 0)
        {
            boxesLeft--;

            Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(0f, 14f), 0f);

            Instantiate(errorBox, spawnPosition, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAttacking)
        {
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 11 || collision.gameObject.layer == 12)
            {
                if (SettingsManager.instance.sfx)
                {
                    GameObject obj = Instantiate(bounceSfx);

                    obj.GetComponent<AudioSource>().pitch = Random.Range(.9f, 1.12f);
                    obj.GetComponent<AudioSource>().Play();
                }

                StartCoroutine(GameManager.instance.ScreenShake(.099f, 1));

                int xVal = 0;
                int yVal = 0;

                if (collision.gameObject.layer == 8)
                {
                    yVal = 5*movIconSpeed;

                    int rand2 = Random.Range(0, 100);

                    if (rand2 > 50)
                    {
                        xVal = 5 * movIconSpeed;
                    }
                    if (rand2 <= 50)
                    {
                        xVal = -5 * movIconSpeed;
                    }
                }

                if (collision.gameObject.layer == 11)
                {
                    int rand2 = Random.Range(0, 100);

                    if (rand2 > 50)
                    {
                        yVal = 5 * movIconSpeed;
                    }
                    if (rand2 <= 50)
                    {
                        yVal = -5 * movIconSpeed;
                    }

                    if (collision.transform.position.x > 0)
                    {
                        xVal = -5 * movIconSpeed;
                    }
                    else
                    {
                        xVal = 5 * movIconSpeed;
                    }
                }

                if (collision.gameObject.layer == 12)
                {
                    yVal = -5 * movIconSpeed;

                    int rand2 = Random.Range(0, 100);

                    if (rand2 > 50)
                    {
                        xVal = 5 * movIconSpeed;
                    }
                    if (rand2 <= 50)
                    {
                        xVal = -5 * movIconSpeed;
                    }
                }

                GetComponent<Rigidbody2D>().velocity = new Vector2(xVal, yVal);
            }
        }
    }

    public void ShootBullet()
    {
        if (isAttacking)
        {
            int rotAmt = 0;

            StartCoroutine(GameManager.instance.ScreenShake(.085f, 1));

            if (SettingsManager.instance.sfx)
            {
                GetComponent<AudioSource>().pitch = Random.Range(.7f, 1.3f);
                GetComponent<AudioSource>().Play();
            }

            for (int i = 0; i < 4; i++)
            {
                bulletPrefab = Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, 0, -rotAmt));
                rotAmt += 90;

                bulletPrefab.GetComponent<Rigidbody2D>().AddForce(-bulletPrefab.transform.up * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }
}
