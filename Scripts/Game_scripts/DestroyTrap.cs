using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrap : MonoBehaviour
{
    public float temp = 1f;
    private Rigidbody2D arrow;

    void Start() 
    {
        arrow = GetComponent<Rigidbody2D>();
    }

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
