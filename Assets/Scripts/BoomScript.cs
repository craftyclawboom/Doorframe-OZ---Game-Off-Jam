using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoomScript : MonoBehaviour
{
    public GameObject popup;

    public bool drawing;
    public GameObject[] thingsThatShouldBeColored;

    public bool isAttacking;

    [SerializeField] int waitSeconds = 4;

    public GameObject explosionParticles;

    public bool blast = false;
    private float currentWait;
    private Animator anim;
    private bool startedAttacking;

    public Image pencil, eraser;

    private Vector3 randomPos;

    public bool wasAttacking;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking)
        {
            wasAttacking = true;
        }

        int coloredThings = 0;

        for(int i =0;i < thingsThatShouldBeColored.Length; i++)
        {
            if(thingsThatShouldBeColored[i].GetComponent<Image>().color.a == 1)
            {
                coloredThings++;
            }
        }

        if(coloredThings == thingsThatShouldBeColored.Length)
        {
            popup.GetComponent<PopupBox>().isCompleted = true;
        }

        if (popup.GetComponent<PopupBox>().isCompleted)
        {
            isAttacking = true;
            if (!startedAttacking)
            {
                randomPos = new Vector3(Random.Range(-10f, 10f), Random.Range(12.5f, 0f), 0);

                startedAttacking = true;
                currentWait = waitSeconds;
            }
        }

        //Rotating and animating

        if (isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, randomPos, 1.75f * Time.deltaTime);

            if (Vector3.Distance(transform.position, randomPos) < .1f)
            {

                currentWait -= Time.deltaTime;

                if (currentWait < .75f && currentWait > .7f)
                {
                    if (Vector3.Distance(transform.position, randomPos) < .1f)
                    {
                        anim.SetTrigger("Shoot");
                    }
                }

                if (currentWait <= 0f)
                {
                    if (Vector3.Distance(transform.position, randomPos) < .1f)
                    {
                        StartCoroutine(ShootBullet());

                    }
                }
            }
        }

        if(blast)
        {
            transform.GetChild(1).GetComponent<CircleCollider2D>().enabled = true;
        }
        else
        {
            transform.GetChild(1).GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public void ChangePixel(Image button)
    {
        if(drawing)
        {
            button.color = new Color(0, 0, 0, 1);
        }
        else
        {
            button.color = new Color(0, 0, 0, 0);
        }
    }

    public void ChangeTool(bool draw)
    {
        if (draw)
        {
            pencil.color = new Color(1, 1, 0);
            eraser.color = new Color(1,1,1);
            drawing = true;
        }
        
        if(!draw)
        {
            eraser.color = new Color(1, 1, 0);
            pencil.color = new Color(1,1,1);

            drawing = false;
        }
    }

    public IEnumerator ShootBullet()
    {
        if (isAttacking)
        {
            if (SettingsManager.instance.sfx)
            {
                GetComponent<AudioSource>().pitch = Random.Range(.85f, 1.15f);
                GetComponent<AudioSource>().Play();
            }

            randomPos = new Vector3(Random.Range(-10f, 10f), Random.Range(12.5f, 0f), 0);

            blast = true;
            Instantiate(explosionParticles, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(.1f);

            currentWait = waitSeconds;
            blast = false;
        }
    }
    
    
}
