using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysDO : MonoBehaviour
{
    public GameObject saveThing;
    public GameObject trapThing;
    private TrapsBehav goodScr;
    private TrapsBehav badScript;
    public bool firstStep = true;

    void Start()
    {
      goodScr = saveThing.GetComponent<TrapsBehav>();
      badScript = trapThing.GetComponent<TrapsBehav>();
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
      if (trig.tag == "Player" && firstStep)
      {
        firstStep = !firstStep;
        badScript.DoTrap();
        goodScr.DoGood();
      }
    }

}
