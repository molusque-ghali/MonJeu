using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health = 100;
    public int decreaseAmount = 30;
    public bool isDead = false;
    Animator playerANimator;
    public Sprite deathSprite;
    public Image heathBar;
    AudioSource source;
    public AudioClip hurt;
    public AudioClip opossumHurt, batHurt, frogHurt, eagleHurt, bettleHurt;
    void Start()
    {
        playerANimator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
    }
    bool canTakeDamage = true;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyDegats" && canTakeDamage == true)
        {
            canTakeDamage = false;
            playerANimator.Play("hurt");
            health = health - decreaseAmount;
            source.PlayOneShot(hurt);
            if (health <= 0)
            {
                health = 0;
                isDead = true;
                StartCoroutine(playerDie());
                //Game Over son 


            }
            float healthEntre0et1 = health / 100;
            for (float amout=heathBar.fillAmount; amout >= healthEntre0et1;amout-=0.00001f)                      
                  heathBar.fillAmount = amout;

        }
    }
  


    IEnumerator playerDie()
    {
        playerANimator.enabled = false;
        GetComponent<SpriteRenderer>().sprite = deathSprite;
        Vector3 newPostion = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
        yield return new WaitForSeconds(1);
        foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
            collider.enabled = false;
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "EnemyDegats")
        {
            canTakeDamage = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="EnemyHead")
        {
            if(collision.gameObject.transform.parent.name=="frog")
            {
                source.PlayOneShot(frogHurt);
            }
            else if(collision.gameObject.transform.parent.name == "opossum")
            {
                source.PlayOneShot(opossumHurt);
            }
            else if (collision.gameObject.transform.parent.name == "bat")
            {
                source.PlayOneShot(batHurt);
            }
            else if (collision.gameObject.transform.parent.name == "eagle")
            {
                source.PlayOneShot(eagleHurt);
            }
            else if (collision.gameObject.transform.parent.name == "bettle")
            {
                source.PlayOneShot(bettleHurt);
            }
            foreach (Collider2D c in collision.gameObject.transform.parent.gameObject.GetComponentsInChildren<Collider2D>())
                c.enabled = false;
            
            StartCoroutine(frogDie(collision.gameObject.transform.parent.gameObject));
         

            Destroy(collision.gameObject.transform.parent.gameObject,5);
        }
    }
    IEnumerator frogDie(GameObject frog)
    {
        frog.GetComponent<Animator>().enabled = false;
        Vector3 actualScale = frog.transform.localScale;
        Vector3 newScale = new Vector3(actualScale.x, 0.4f, actualScale.z);

        float timeInSec = 0;
        float timeToDoAnimation = 1f;

        while(timeInSec<timeToDoAnimation)
        {
            frog.transform.localScale = Vector3.Lerp(actualScale, newScale, timeInSec / timeToDoAnimation);
            timeInSec += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        frog.transform.localPosition = new Vector3(frog.transform.localPosition.x, -4, frog.transform.localPosition.z);
    }
}
