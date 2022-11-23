using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
   public float move;
    
   public WaitForSeconds tempo = new WaitForSeconds(3);

   public GameObject trap;

   void Start()
   {
      Throw();
   }


   public void Throw ()
   {
     GameObject arrow = Instantiate(trap, new Vector3(transform.position.x + 1, transform.position.y,0), Quaternion.Euler(0,0,180)) as GameObject;
     arrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(10 * (transform.localScale.x * -1), 0), ForceMode2D.Impulse);
     StartCoroutine(Spawner());
   }


   void RollBall()
   {
     move += -0.01f;
     this.transform.position = new Vector2(move, transform.position.y);
   }
   
   IEnumerator Spawner()
   {    
      yield return tempo;
      if (!GAMEMANAGER.isPaused)
      {
         Throw();
      }
      else
      {
         StartCoroutine(Spawner());
      }
      
   }
}
