using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    public float speed = 4f;
    public int lives = 5;
    public int jumpForce = 1;
    private bool isGrounded = false;
    private bool naLestn;
    public Transform groundCheck;
    private float groundRadius = 0.15f;
    public LayerMask whatIsGround;
    public LayerMask whatIsLest;
    //public Core scrCore;
    public GameObject pressE;
    //public Transform attacPoint;
    //private Vector3 flipPoint = new Vector3 (-1f, 0, 0);

    private Animator anim;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    //private float attacTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Running(){
      Vector3 dir = transform.right * Input.GetAxis("Horizontal");
      transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
      sprite.flipX = dir.x < 0.0f;
      if (isGrounded)
        anim.SetInteger("state", 1);
      //if (sprite.flipX && attacPoint.localPosition.x>0)
        //attacPoint.localPosition = (new Vector3(attacPoint.localPosition.x * -1f, attacPoint.localPosition.y, attacPoint.localPosition.z));
      //else if(!sprite.flipX && attacPoint.localPosition.x<0)
        //attacPoint.localPosition = (new Vector3(attacPoint.localPosition.x * -1f, attacPoint.localPosition.y, attacPoint.localPosition.z));
    }

    private void Jump()
    {
      rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Finish")
        {
          FindObjectOfType<Core>().Finished();
        }
        if(other.tag == "Door")
        {
          pressE.GetComponent<SpriteRenderer>().enabled = true;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
      if(other.tag == "Moving")
      {
        var movePlatform = other.gameObject.GetComponentInParent<TrapsBehav>().GetMoving();
        var flip = other.gameObject.GetComponentInParent<TrapsBehav>().GetFlip();
        if(movePlatform)
        {
          if(!flip)
          {
            Vector3 moveR = new Vector3(1,0,0);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveR, 1f * Time.deltaTime);
          }
          else
          {
            Vector3 moveL = new Vector3(-1,0,0);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveL, 1f * Time.deltaTime);
          }
        }
      }
      if(other.tag == "Door" && Input.GetKey(KeyCode.E))
      {
        Debug.Log("Load LVL");
        Core.SetMAXLuck();
        SceneManager.LoadScene(1);
      }
    }

    void OnTriggerExit2D(Collider2D other)
    {
      if(other.tag == "Door")
      {
        pressE.GetComponent<SpriteRenderer>().enabled = false;
      }
    }

    private void Climb()
    {
      rb.bodyType =  RigidbodyType2D.Static;
      Vector3 dir = Vector3.up * Input.GetAxis("Vertical");
      transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, 5 * Time.deltaTime);
      anim.SetInteger("state",4);
    }

    /*private void Attack_anim()
    {
        anim.SetBool("attac", true);
        attacTime = Time.time;

    }
    */
    void FixedUpdate(){
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (!isGrounded)
        {
          if(!naLestn)
            rb.bodyType =  RigidbodyType2D.Dynamic;
          return;
        }
        if(!naLestn)
          rb.bodyType =  RigidbodyType2D.Dynamic;


    }

    void Update()
    {
      naLestn = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsLest);
      if(isGrounded) anim.SetInteger("state",0);
      //if (Input.GetKey(KeyCode.R) && Time.time-attacTime > .21f)
        //Attack_anim();
      //else if (Time.time-attacTime > .21f)
      //{
        //anim.SetBool("attac", false);
      //}

      if (Input.GetButton("Horizontal"))
        Running();
      if(!isGrounded && !naLestn)
      {
        anim.SetInteger("state",2);
      }
      if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        Jump();
      if(Input.GetButton("Vertical") && naLestn)
        {
          Climb();
        }

    }
}
