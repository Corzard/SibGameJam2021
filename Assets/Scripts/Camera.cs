using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
      public GameObject hero;
      private float dX = 2.88f;
      private float dY = 2.4f;

      void LateUpdate()
      {
          transform.position = new Vector3(hero.transform.position.x + dX, hero.transform.position.y + dY, -10);
      }

}
