using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
   
    public float speed = 12;
    public float jumpForce = 100;
    Rigidbody2D rigidbody;
    public TextMeshProUGUI coinText;

   public Collider2D colliderCheckGround;
    public LayerMask groundLayer;
    public Transform coinReference;
    public GameObject drapeau;
    public Transform drapeauReference;

    bool isGrounded = true;
   public float smooth = 3;
    int numberOfCoins = 0;
    AudioSource source;
    public AudioClip  coinCollect;

    Animator animatorPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    float x;
    bool isFcingRight = true;
    // Update is called once per frame
    void Update()
    {
       
       isGrounded=colliderCheckGround.IsTouchingLayers(groundLayer);


        if(Input.GetKeyDown(KeyCode.Space) && isGrounded==true)
        {
            rigidbody.AddForce(Vector2.up * jumpForce);
            rigidbody.AddForce(Vector2.up * jumpForce);
            animatorPlayer.SetBool("isJumping", true);
        }else
            {
            animatorPlayer.SetBool("isJumping", false);
        }
      
             x = Input.GetAxisRaw("Horizontal");  //1 -1 0
            if(x==0)
            {
            animatorPlayer.SetBool("IsRunning", false);
        }
        else
        {
            animatorPlayer.SetBool("IsRunning", true);
        }
            
            Vector2 direction = new Vector2(x*speed, rigidbody.velocity.y);
           rigidbody.velocity = Vector2.SmoothDamp(rigidbody.velocity, direction, ref direction, Time.deltaTime * smooth);
        flip();
    }


    void flip()
    {
        if(x>0 && isFcingRight==false)
        {
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            isFcingRight = true;

        }
        if (x < 0 && isFcingRight == true)
        {
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            isFcingRight = false;
        }
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            transform.rotation = Quaternion.Euler(0, 0, 50);
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("finMontee"))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(collision.gameObject.CompareTag("coin"))
        {
            source.PlayOneShot(coinCollect);
            collision.enabled = false;
           
            StartCoroutine(coinFly(collision.gameObject));
        }
        if (collision.gameObject.CompareTag("drapeau"))
        {
            Vector3 initailPosition = drapeau.transform.position;
            Vector3 finalPosition = drapeauReference.position;
        }
    }

    IEnumerator coinFly(GameObject coin)
    {
        Vector3 initailPosition = coin.transform.position;
        Vector3 finalPosition = coinReference.position;

        float timeInSec = 0;
        float timeToDoAnimation = 1f;

        while(timeInSec<timeToDoAnimation)
        {
            coin.transform.position = Vector3.Lerp(initailPosition, finalPosition, timeInSec / timeToDoAnimation);
            timeInSec += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        numberOfCoins = numberOfCoins + 1;
        coinText.text = numberOfCoins.ToString();
        Destroy(coin, 0.5f);
    }
}
