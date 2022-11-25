using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Rigidbody2D playerBody;
    private Vector2 direction;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {   
        if(other.gameObject.CompareTag("Damage"))
        {
            if (!other.gameObject.GetComponent<BulletController>())
            {
                Damage(10);
            }
            else
            {
                Damage(other.gameObject.GetComponent<BulletController>().force);
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            if (!other.gameObject.GetComponent<EnemyController>().isDead)
            {
                direction = playerBody.transform.position - other.gameObject.transform.position;
                iTween.MoveBy(playerBody.gameObject, iTween.Hash("x", direction.normalized.x * 2.5f, "time", 0.3f));
                Damage( other.gameObject.GetComponent<EnemyController>().force);
            }            
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {   
        if(other.gameObject.CompareTag("Damage"))
        {
            Damage(10);
        }
    }

    public void Damage(int d)
    {
        if(this.GetComponent<PlayerController>().alive)
        {
            iTween.ColorTo(playerBody.gameObject, iTween.Hash("g",0,"b",0, "time", 0.03f, "looptype", iTween.LoopType.pingPong, "oncompletetarget", playerBody.gameObject, "oncomplete", "StopEffect"));
            TakeDamage(d);
        }
         
    }
    public void TakeDamage(int damage)
    {
        if (GAMEMANAGER.instance.life > 0)
        {
            GAMEMANAGER.instance.life -= damage;
            UIMANAGER.instance.lifebar.fillAmount = (float)GAMEMANAGER.instance.life/100;
        }
    }
}
