using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrap : MonoBehaviour
{
    public float temp = 2f;

void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else
        {
            OnChock();
        }     
    }
    public void OnChock ()
    {
        Destroy(this.gameObject, temp);  
    }
}
