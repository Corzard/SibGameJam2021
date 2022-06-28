using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Core : MonoBehaviour
{
    private float barMaxX = 0f;
    private float batMinX = -(Screen.width/2);
    private float modX;
    private float deltaX;
    public int luck;
    private static int maxLuck;
    private Vector3 startPos;
    int rnd;
    private float timeRemaining = 10;
    public GameObject luckBar;
    public GameObject countBar;
    public GameObject countDown;
    public GameObject flag;
    public GameObject infoPanel;
    public GameObject endButton;
    public GameObject startButton;
    public static bool firstLoad = true;

    public int GetLuck()
    {
      return luck;
    }

    // Start is called before the first frame update
    void Start()
    {
      startPos = transform.position;
      modX = barMaxX - batMinX;
      deltaX =  modX/10;
      //SetMAXLuck();
      SetFlag(maxLuck);
      SetLuck();
      Time.timeScale = 0;
      infoPanel.SetActive(true);
      startButton.SetActive(true);
      infoPanel.GetComponentInChildren<Text>().text = "Каждые 10 секунд твоя удача будет меняться, она влияет на ловушки\nМаксимальная удача не меняется";

    }
    private void SetFlag(int maxLuck)
    {
      switch (maxLuck)
      {
        case 70:
          flag.SetActive(true);
          flag.transform.position = startPos - new Vector3(deltaX*3, 0, 0);
          break;
        case 50:
          flag.SetActive(true);
          flag.transform.position = startPos - new Vector3(deltaX*5, 0, 0);
          break;
        case 20:
          flag.SetActive(true);
          flag.transform.position = startPos - new Vector3(deltaX*8, 0, 0);
          break;
        default:
          break;
      }
    }

    public static void SetMAXLuck()
    {
      int rnd = Random.Range(0, 101);
      if(rnd <=100 && rnd >= 80)
          maxLuck = 100;
      else if(rnd < 80 && rnd >= 40)
          {
            maxLuck = 70;
            //transform.position = startPos - new Vector3(deltaX*3, 0, 0);
            //flag.SetActive(true);
            //flag.transform.position = startPos - new Vector3(deltaX*3, 0, 0);
          }
      else if(rnd < 40 && rnd >= 15)
          {
            maxLuck = 50;
            //transform.position = startPos - new Vector3(deltaX*5, 0, 0);
            //flag.SetActive(true);
            //flag.transform.position = startPos - new Vector3(deltaX*5, 0, 0);
          }
      else
          {
            maxLuck = 20;
            //transform.position = startPos - new Vector3(deltaX*8, 0, 0);
            //flag.SetActive(true);
            //flag.transform.position = startPos - new Vector3(deltaX*8, 0, 0);
          }
      //countBar.GetComponent<Text>().text = maxLuck + "/" + maxLuck;
      //countDown.GetComponent<Text>().text = timeRemaining.ToString("F1");
    }

    private void SetLuck()
    {
      rnd = Random.Range(10, maxLuck+5);
      luck = rnd/10 * 10;
      if((rnd%10)>5)
      {
        luck+=10;
      }
      transform.position = startPos - new Vector3(deltaX*((100-luck)/10), 0, 0);
      countBar.GetComponent<Text>().text = luck + "/" + maxLuck;
      countDown.GetComponent<Text>().text = timeRemaining.ToString("F1") + " сек.";
    }
    // Update is called once per frame
    void Update()
    {
      if(timeRemaining>0)
      {
        timeRemaining -= Time.deltaTime;
        countDown.GetComponent<Text>().text = timeRemaining.ToString("F1")+ " сек.";
      }
      else
      {
        SetLuck();
        timeRemaining = 10;
      }
    }

    public void Finished()
    {
      Time.timeScale = 0;
      infoPanel.SetActive(true);
      endButton.SetActive(true);
      infoPanel.GetComponentInChildren<Text>().text = "Уровень пройден";
    }
    public void ForButton()
    {
      Time.timeScale = 1;
      SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
      Time.timeScale = 1;
      infoPanel.SetActive(false);
      startButton.SetActive(false);
    }
}
