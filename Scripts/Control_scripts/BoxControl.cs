using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControl : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D boxBody;
    public bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
        boxBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isMoving = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Land"))
        {
            if(!isMoving)
            {
                boxBody.isKinematic = true;
            }
        }   
    }
}
