using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VirusScript : MonoBehaviour
{
    [SerializeField] TMP_Text ClickCountText;
    int virusHP = 100;
    GameObject virus;

    [Space]
    [Header("Attacking Stuff")]
    public GameObject bullet;
    bool startedAttack = false;
    int attackFrames;
    bool attacking = false;
    [SerializeField] int framesBAttack = 500;
    [SerializeField] int attackDelay = 5;
    [SerializeField] int bulletCount = 8;
    GameObject bulletPrefab;

    public GameObject ring1, ring2, ring3;

    public GameObject deathNoise, deathParticles, deathFlash;

    public Animator anim;
    public GameObject hurtNoise;

    // Start is called before the first frame update
    void Start()
    {
        virus = transform.GetChild(0).transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            GameManager.instance.virusHealth.SetActive(true);
        }

        ClickCountText.text = "VIRUS: " + virusHP + "%";


        if (virusHP <= 0)
        {
            StartCoroutine(GameManager.instance.ScreenShake(1.5f, 7));
            if(SettingsManager.instance.sfx)
            {
                Instantiate(deathNoise);
            }

            Instantiate(deathFlash);
            Instantiate(deathParticles, transform.position, Quaternion.identity);

            Time.timeScale = 1f;
            attacking = false;
            gameObject.SetActive(false);
            GameManager.instance.ExplodeAllApps();
        }

        if (virusHP > 50)
        {
            Time.timeScale = 1f;

            ring1.transform.Rotate(new Vector3(0f, 0f, 30f * Time.deltaTime));
            ring2.transform.Rotate(new Vector3(0f, 0f, 45f * Time.deltaTime));
            ring3.transform.Rotate(new Vector3(0f, 0f, 80f * Time.deltaTime));
        }

        if (virusHP <= 50 && virusHP > 25)
        {
            Time.timeScale = 1.25f;

            ring1.transform.Rotate(new Vector3(0f, 0f, 60f * Time.deltaTime));
            ring2.transform.Rotate(new Vector3(0f, 0f, 90f * Time.deltaTime));
            ring3.transform.Rotate(new Vector3(0f, 0f, 160f * Time.deltaTime));
        }

        if (virusHP <= 25 && virusHP > 0)
        {
            Time.timeScale = 1.5f;

            ring1.transform.Rotate(new Vector3(0f, 0f, 140f * Time.deltaTime));
            ring2.transform.Rotate(new Vector3(0f, 0f, 180f * Time.deltaTime));
            ring3.transform.Rotate(new Vector3(0f, 0f, 320f * Time.deltaTime));
        }

        if (!attacking)
        {
            StartCoroutine(Attack());
            attacking = true;
        }
    }
    private void FixedUpdate()
    {
        attackFrames += 1;
    }

    public IEnumerator OnClick()
    {
        virusHP -= 5;

        StartCoroutine(GameManager.instance.ScreenShake(.125f, 2));

        if (SettingsManager.instance.sfx)
        {
            GameObject obj = Instantiate(hurtNoise);
            obj.GetComponent<AudioSource>().pitch = Random.Range(.9f, 1.2f);
        }

        anim.SetTrigger("shrink");

        yield return new WaitForSeconds(.25f);

        virus.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.25f, 0.3f));
    }

    IEnumerator Attack()
    {
        Debug.Log("Started the Ienumerator");
        int rotAmt = 0;

        bool thing = true;

        while(thing)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                bulletPrefab = Instantiate(bullet, virus.transform.position, Quaternion.Euler(0, 0, rotAmt));
                rotAmt += 360 / bulletCount;
                bulletPrefab.GetComponent<Rigidbody2D>().AddForce(bulletPrefab.transform.up * 10f, ForceMode2D.Impulse);
                yield return new WaitForSeconds(.2f);
            }

            yield return new WaitForSeconds(attackDelay);
        }
    }

    //oid VirusAttack()
    //
    //   if(Random.Range(1,10) == 5)
    //   {
    //       startedAttack = true;
    //       Debug.Log("Started Attacking");
    //       StartCoroutine(VirusSpin());
    //   }
    //
    //
    //Enumerator VirusSpin()
    //
    //   Debug.Log("Spinning the virus.");
    //   for(int i = 0; i < spinTime; i++)
    //   {
    //       virus.transform.Rotate(new Vector3(0, spins * (360 / spinTime)));
    //       yield return new WaitForSeconds(spinSpeed);
    //   }
    //   startedAttack = false;
    //   attackFrames = 0;
    //

}
