using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperLaunch : MonoBehaviour
{
    public string bulletType;

    public int dmg;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(GameManager.instance.ScreenShake(.1F, 2));

        if(bulletType == "paper")
        {
            if (collision.gameObject.layer == 31)
            {
                if (dmg > 0)
                {
                    MouseScript.instance.TakeDamage(dmg);
                    GetComponent<Animator>().SetBool("dothething", true);
                    StartCoroutine(DestroyOverTime(1f));

                    dmg = 0;
                }
            }

            if (collision.gameObject.layer == 8)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<Animator>().SetBool("dothething", true);

                StartCoroutine(DestroyOverTime(1f));
            }

            if (collision.gameObject.layer == 30)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<Animator>().SetBool("dothething", true);

                StartCoroutine(DestroyOverTime(1f));
            }
        }

        if(bulletType == "spike")
        {
            if (collision.gameObject.layer == 31)
            {
                if (dmg > 0)
                {
                    MouseScript.instance.TakeDamage(dmg);
                    GetComponent<Animator>().SetBool("dothething", true);
                    StartCoroutine(DestroyOverTime(.25f));

                    dmg = 0;
                }
            }

            if (collision.gameObject.layer == 8)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<Animator>().SetBool("dothething", true);

                StartCoroutine(DestroyOverTime(.25f));
            }

            if (collision.gameObject.layer == 11)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<Animator>().SetBool("dothething", true);

                StartCoroutine(DestroyOverTime(.25f));
            }

            if (collision.gameObject.layer == 12)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<Animator>().SetBool("dothething", true);

                StartCoroutine(DestroyOverTime(.25f));
            }

            if (collision.gameObject.layer == 30)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<Animator>().SetBool("dothething", true);

                StartCoroutine(DestroyOverTime(1f));
            }
        }

        if (bulletType == "virus-spike")
        {
            if (collision.gameObject.layer == 31)
            {
                if (dmg > 0)
                {
                    MouseScript.instance.TakeDamage(dmg);
                    GetComponent<Animator>().SetBool("dothething", true);
                    StartCoroutine(DestroyOverTime(.25f));

                    dmg = 0;
                }
            }

            if (collision.gameObject.layer == 8)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<Animator>().SetBool("dothething", true);

                StartCoroutine(DestroyOverTime(.25f));
            }

            if (collision.gameObject.layer == 11)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<Animator>().SetBool("dothething", true);

                StartCoroutine(DestroyOverTime(.25f));
            }

            if (collision.gameObject.layer == 12)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<Animator>().SetBool("dothething", true);

                StartCoroutine(DestroyOverTime(.25f));
            }

            if (collision.gameObject.layer == 30)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<Animator>().SetBool("dothething", true);

                StartCoroutine(DestroyOverTime(1f));
            }
        }
    }

    public IEnumerator DestroyOverTime(float time)
    {
        if (SettingsManager.instance.sfx)
        {
            GetComponent<AudioSource>().pitch = Random.Range(.5f, 1.5f);
            GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
