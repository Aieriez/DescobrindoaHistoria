using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float vel = 40;
    public int force = 15;

    public float Velocity
    {
        get{return vel;}
        set{vel = value;}
    }
    
    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }
    
    void MoveBullet()
    {
        Vector3 aux = transform.position;
		aux.x += vel * Time.deltaTime;
		transform.position = aux;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {           
        Destroy(gameObject);
    }
}
