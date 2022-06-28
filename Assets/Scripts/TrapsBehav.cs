using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapsBehav : MonoBehaviour
{
  private bool moveV = false;
  private bool moveH = false;
  private bool spikeUP = true;
  private bool spikeDown = false;
  private bool moveSpikes = false;
  private bool flip = false;
  private bool movePlatform;
  private Vector3 startPos;
  private string selfTeg;
  private Renderer rend;
  private Collider2D coll;
  private SpriteRenderer sprite;

  void Start()
  {
    selfTeg = this.tag;
    if(this.tag == "Spikes" || this.tag == "Platforma" )
    {
      startPos = transform.position;
      //Debug.Log("Заинитил компоненты");
    }

  }
  public void DoGood()
  {
    Debug.Log("GoodThi");
    switch (selfTeg)
    {
      case "FakeGrnd":
        coll = GetComponent<Collider2D>();
        rend = GetComponent<Renderer>();
        Debug.Log("Отключаю компоненты");
        coll.enabled = !coll.enabled;
        rend.enabled = !rend.enabled;
        break;
      case "Platform":
        movePlatform = true;
        break;
      default:
        break;
      }
  }
  public void DoTrap()
    {
      switch (selfTeg)
      {
        case "ArrowsV":
          moveV = true;
          break;
        case "ArrowsH":
          moveH = true;
          sprite = GetComponentInChildren<SpriteRenderer>();
          break;
        case "Spikes":
          moveSpikes = true;
          break;
        default:
          break;
      }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      if(other.tag == "Player")
      {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);
      }
    }

    public bool GetFlip()
    {
      return flip;
    }
    public bool GetMoving()
    {
      return movePlatform;
    }

    void Update()
    {
      if(movePlatform)
      {
        if(!flip)
        {
          Vector3 moveR = new Vector3(1,0,0);
          transform.position = Vector3.MoveTowards(transform.position, transform.position + moveR, 1f * Time.deltaTime);
          if(transform.position.x - startPos.x >= 5)
                flip = true;
        }
        else
        {
          Vector3 moveL = new Vector3(-1,0,0);
          transform.position = Vector3.MoveTowards(transform.position, transform.position + moveL, 1f * Time.deltaTime);
          if(transform.position.x < startPos.x)
            flip = false;
        }
      }
      if(moveV)
      {
        transform.position = transform.position + Vector3.down*20*Time.deltaTime;
      }
      if(moveH)
      {
        Debug.Log(sprite.flipX);
        if(!sprite.flipX)
        transform.position = transform.position + Vector3.left*10*Time.deltaTime;
        else
        {
          transform.position = transform.position + Vector3.right*10*Time.deltaTime;
        }
      }

      if(moveSpikes)
      {
        if(transform.position.y - startPos.y < 0.4f && spikeUP)
        {
          //StartCoroutine("SpikesUp");
          transform.position = Vector3.MoveTowards(transform.position, transform.position+Vector3.up, 0.01f);
        }
        else
        {
          spikeUP = false;
          //StartCoroutine("SpikesDown");
          transform.position = Vector3.MoveTowards(transform.position, startPos, 0.01f);
          if(transform.position.y<=startPos.y)
            {
              moveSpikes = false;
              spikeUP=true;
            }
        }
      }
    }
}
