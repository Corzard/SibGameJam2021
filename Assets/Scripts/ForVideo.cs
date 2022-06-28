using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ForVideo : MonoBehaviour
{
    public VideoPlayer vid;
    static bool firstPlay = true;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        GameObject camera = GameObject.Find("Main Camera");
        vid.loopPointReached += CheckOver;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
      Time.timeScale = 1;
      Debug.Log("Ended");
      firstPlay = false;
      Destroy(this.gameObject);
    }

    void Update()
    {
      if(!firstPlay)
      {
        Time.timeScale = 1;
        Destroy(this.gameObject);
      }
    }
    /*void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        Time.timeScale = 1;
        Debug.Log("Ended");
        Destroy(this);
    }
*/

}
