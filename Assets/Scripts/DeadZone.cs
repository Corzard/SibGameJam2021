using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public GameObject spawnPoint;
    
    void OnTriggerEnter2D(Collider2D other)
    {
      if(other.tag == "Player")
      {
        other.transform.position = spawnPoint.transform.position;
      }
      else
      {
        Destroy(other.gameObject);
      }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
