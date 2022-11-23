using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float temp = 1f;
    [SerializeField]
    GameObject explosion;
    private bool bombCollision;
    [SerializeField]
    private float rad;
    [SerializeField]
	private LayerMask layer;

    public GameObject player;

    public void DestroyBomb()
    {
        Destroy(this.gameObject, 0.1f);
        GAMEMANAGER.instance.bombsNum++;
    }

    void Awake() 
    {
        player = GameObject.FindWithTag("Player");    
    }

    void Update() 
    {
        if(this.gameObject.name == "Bomb" || this.gameObject.name == "Bomb(Clone)")
        {   
            if (bombCollision)
                {
                    GameObject bombE = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
                    Destroy(gameObject); 
                }
        }   
    }


    void FixedUpdate () 
    {
		bombCollision = Physics2D.OverlapCircle (transform.position, rad, layer);   
	}

    private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (transform.position, rad);
	}

        
}
