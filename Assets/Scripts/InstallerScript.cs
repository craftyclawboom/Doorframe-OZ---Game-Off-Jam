using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallerScript : MonoBehaviour
{
    public GameObject popup;

    public bool isAttacking;

    [SerializeField] GameObject bullet;
    GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] int waitSeconds = 4;

    private float currentWait;
    private Animator anim;
    private bool startedAttacking;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (popup.GetComponent<PopupBox>().isCompleted)
        {
            isAttacking = true;
            if (!startedAttacking)
            {
                startedAttacking = true;
                currentWait = waitSeconds;
            }
        }

        //Rotating and animating

        if (isAttacking)
        {
            currentWait -= Time.deltaTime;

            if (currentWait > 1f && currentWait < 2.5f)
            {
                anim.SetBool("Reloading", true);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 5f * Time.deltaTime);
            }

            if (currentWait < .75f)
            {
                Vector3 targ = GameManager.instance.mouse.transform.position;

                Vector3 objectPos = transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;

                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90f), 5f * Time.deltaTime);
            }

            if (currentWait <= 0)
            {
                anim.SetTrigger("Shoot");

                currentWait = waitSeconds;
                ShootBullet();
            }
        }
    }


    public void ShootBullet()
    {
        if (isAttacking)
        {
            float angleStep = 20f / 3f;
            float aimingAngle = transform.rotation.eulerAngles.z;
            float centeringOffset = (20 / 2) - (angleStep / 2);

            if (SettingsManager.instance.sfx)
            {
                GetComponent<AudioSource>().pitch = Random.Range(.7f, 1.3f);
                GetComponent<AudioSource>().Play();
            }

            for (int i = 0; i < 3; i++)
            {
                float currentBulAng = angleStep * i;

                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, aimingAngle + currentBulAng - centeringOffset));
                bulletPrefab = Instantiate(bullet, gameObject.transform.position, rotation);

                bulletPrefab.transform.Rotate(0, 0, Random.Range(-10, 10));

                #region Juicy Stuff

                //This is some spaghetti juice code

                int randNum = Random.Range(0, 100);

                if (randNum == 100)
                {
                    bulletPrefab.transform.GetChild(0).localScale += new Vector3(.05f, .05f, 0f);
                }

                if (randNum > 80)
                {
                    bulletPrefab.transform.GetChild(0).localScale += new Vector3(.05f, .05f, 0f);
                }

                if (randNum > 60)
                {
                    bulletPrefab.transform.GetChild(0).localScale += new Vector3(.05f, .05f, 0f);
                }

                if (randNum > 40)
                {
                    bulletPrefab.transform.GetChild(0).localScale += new Vector3(.05f, .05f, 0f);
                }

                if (randNum > 20)
                {
                    bulletPrefab.transform.GetChild(0).localScale += new Vector3(.05f, .05f, 0f);
                }

                float randomCol = Random.Range(.7f, 1f);

                bulletPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(randomCol, randomCol, randomCol);

                #endregion

                bulletPrefab.GetComponent<Rigidbody2D>().AddForce(bulletPrefab.transform.up * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }
}
