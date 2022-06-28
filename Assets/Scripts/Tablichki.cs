using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tablichki : MonoBehaviour
{
    public GameObject infoP;
    private Vector3 infoStartPos;
    private bool playerIn = false;
    [TextArea]
    public string inpTxt;
    private bool wasHere = false;
    // Start is called before the first frame update
    void Start()
    {
        infoStartPos = infoP.transform.position;
        Debug.Log(Screen.height);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
      if(other.tag == "Player")
      {
        infoP.GetComponentInChildren<Text>().text = inpTxt;
        playerIn = true;
        wasHere = true;
      }
    }

    void OnTriggerExit2D(Collider2D other)
    {
      if(other.tag == "Player")
      {
        playerIn = false;
      }
    }
    // Update is called once per frame
    void Update()
    {
      if(playerIn)
      {
        infoP.transform.position = Vector3.MoveTowards(infoP.transform.position, new Vector3(infoStartPos.x, (float)(Screen.height-Screen.height/5),infoStartPos.z), 1800f*Time.deltaTime);
      }
      else if(!playerIn && infoP.transform.position!= infoStartPos && wasHere)
      {
        infoP.transform.position = Vector3.MoveTowards(infoP.transform.position, infoStartPos, 1000f*Time.deltaTime);
        if(infoP.transform.position == infoStartPos)
        {
          wasHere = false;
        }
      }
    }
}
