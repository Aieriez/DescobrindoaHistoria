using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    private SpriteRenderer sp;
    [SerializeField]
    private SpriteRenderer switchOn;
    [SerializeField]
    private GameObject fence;

    void Start() 
    {
        sp = this.gameObject.GetComponent<SpriteRenderer>();   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            sp.sprite = switchOn.sprite;
            Destroy(fence);
        }
        
    }
}
