using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    
    [SerializeField]
    GameObject rangeAttack;
    [SerializeField]
    Transform launcher;
    SpriteRenderer sprite;
    int shoots = 5;
    private float launcherpos = 3.5f;
    WaitForSeconds timer = new WaitForSeconds (0.5f);

    // Start is called before the first frame update
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 scale = launcher.localScale; 
            
            if (sprite.flipX == false)
            {
                scale.x = 1;
                launcher.position = new Vector2 (transform.position.x + launcherpos, launcher.position.y);
                launcher.localScale = scale;
            }
            else
            {
                scale.x = - 1;
                launcher.position = new Vector2 (transform.position.x - launcherpos, launcher.position.y);
                launcher.localScale = scale;
            }
        
        StartCoroutine(Timer());
    }

    
    public void FireBullet ()
    {
        if (shoots > 0){ 
            var bullet = Instantiate(rangeAttack, launcher.position, Quaternion.Euler(0,0,90)) as GameObject;
            bullet.GetComponent<BulletController>().Velocity *= launcher.localScale.x;
            shoots--;
        }
    }

    public void FireBomb()
    {
        var bomb = Instantiate(rangeAttack, launcher.position, Quaternion.identity) as GameObject;
        bomb.GetComponent<BulletController>().Velocity *= launcher.localScale.x;
    }
    
    IEnumerator Timer ()
    {
        if(shoots == 0)
        {
            yield return timer;
            shoots = 5;
        }
    }

}
