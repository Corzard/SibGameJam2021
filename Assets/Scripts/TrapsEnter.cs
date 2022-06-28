using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsEnter : MonoBehaviour
{
    public GameObject saveThing;
    public GameObject trapThing;
    private TrapsBehav goodScr;
    private TrapsBehav badScript;
    //private TrapsBehav toTrap;
    private int luck;
    public bool firstStep = true;

    void Start()
    {
      //Debug.Log("Мои связанные теги "+ saveThing.tag + " " + trapThing.tag);
      goodScr = saveThing.GetComponent<TrapsBehav>();
      badScript = trapThing.GetComponent<TrapsBehav>();
    }

    private int GetRandomNumber(int maxRange)
    {
      int rnd = Random.Range(0, maxRange+1);
      return rnd;
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
      var luckScr = FindObjectOfType<Core>();
      luck = luckScr.GetLuck();
      //var toTrap = FindObjectOfType<TrapsBehav>();
      //Debug.Log(luck);
      if (trig.tag == "Player")
      {
        /*if(this.tag=="100Spikes")
        {
          badScript.DoTrap();
        }*/
        if(this.tag=="SaveButt" && firstStep)
        {
          firstStep = !firstStep;
          if(luck>=90)
          {
            goodScr.DoGood();
          }
          else
            return;
        }

        //firstStep = false;
        if(this.tag == "Listener")
        {
          if(luck<=30)
          {
            badScript.DoTrap();
          }
          else if(luck>30 && luck <=60)
          {
            int rnd = GetRandomNumber(3);
            if(rnd<=1)
            {
              badScript.DoTrap();
            }
          }
          else
            return;
        }
        if(this.tag == "Rock")
        {
          if(luck <=30)
            badScript.DoTrap();
          else
            return;
        }

        if(luck >= 90 && firstStep)
        {
          firstStep = !firstStep;
          Debug.Log(">90");
          goodScr.DoGood();
        }
        else if(luck >=70 && luck <90 && firstStep)
        {
          firstStep = !firstStep;
          Debug.Log("60<90");
          int rnd = GetRandomNumber(10);
          if(rnd > 6)
          {
            goodScr.DoGood();
          }
          else
          {
            badScript.DoTrap();
          }
        }
        else if(luck >= 30 && luck <10 && firstStep)
        {
          firstStep = !firstStep;
          Debug.Log("30<60");
          int rnd = GetRandomNumber(10);
          if(rnd < 2 )
          {
            goodScr.DoGood();
          }
          else
          {
            badScript.DoTrap();
          }
        }
        else if(firstStep)
        {
          firstStep = !firstStep;
          Debug.Log("<30");
          badScript.DoTrap();
        }
      }
    }

    void Update()
    {

    }
}
