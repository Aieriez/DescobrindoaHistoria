using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float vel = 2f;
    private Rigidbody2D ebody;
    [SerializeField]
    public float radius = 20f, distance = 0.4f, rad = .12f;
    [SerializeField]
    private bool moveLeft;
    [SerializeField]
    public bool isDead = false;
    [SerializeField]
    private Transform[] limits;
    public bool call = true;
    public LayerMask layer;
    [SerializeField]
    private LayerMask layerColl;
    [SerializeField]
    private SpriteRenderer spRenderer;
    [SerializeField]
    private string[] animationName;
    private Animator eAnimation;
    private WaitForSeconds tempo = new WaitForSeconds(1);
    [SerializeField]
    private bool visonField;
    [SerializeField]
    private string direction = "left";
    [SerializeField]
    private bool attack;
    public int force;
    private AudioSource eAudio;
    [SerializeField]
    private AudioClip eClips;


    // Start is called before the first frame update
    void Start()
    {
        //Ignore Collision
        Physics2D.IgnoreLayerCollision(10,10);
        Physics2D.IgnoreLayerCollision(10, 8);
        
        ebody =  GetComponent<Rigidbody2D>();
        moveLeft = true;
        spRenderer = GetComponent<SpriteRenderer>();
        eAnimation = GetComponent<Animator>();
        eAudio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        visonField =  Physics2D.OverlapCircle(transform.position, radius, layer);
        if (!GAMEMANAGER.isPaused)
        {
            if(!isDead)
            {
                if (visonField)
                {   
                    eAudio.clip = eClips;
                    if (!eAudio.isPlaying)
                    {
                        eAudio.Play();
                    }
                
                    var relativePosition = transform.InverseTransformPoint(Physics2D.OverlapCircle(transform.position, radius, layer).gameObject.transform.position);

                    if (relativePosition.x < 0.0f)
                    {
                        if (direction == "left")
                        {
                            StartCoroutine(FollowPlayer(true, true));
                            //print("CourotineEsquerda");
                            direction = "right";
                        }
                    
                    }
                    else if (relativePosition.x > 0.0f)
                    {
                        if (direction == "right")
                        {
                            StartCoroutine(FollowPlayer(false, false));
                            //print("CourotineDireita");
                            direction = "left";
                        }
                    }
                }

                attack = Physics2D.OverlapCircle(transform.position, radius*rad, layer);
                
                if (!attack)
                {
                    if(moveLeft)
                    {
                        ebody.velocity = new Vector2(-vel, ebody.velocity.y);
                    }
                    else
                    {
                        ebody.velocity =  new Vector2(vel, ebody.velocity.y);
                    }
                }
                else
                {   
                    eAudio.loop = false;
                    eAudio.Stop();
                    eAnimation.Play(animationName[0]);
                }
                

                VerifyCollision();
            }
            if (isDead)
            {
                Physics2D.IgnoreCollision(GAMEMANAGER.instance.player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
            }
        }
        if (transform.position.y <= GAMEMANAGER.instance.fallLimit.transform.position.y)
        {
            Destroy(this.gameObject);
        }
    }

    void VerifyCollision()
    {
        if(!Physics2D.Raycast(limits[0].position, Vector2.down, distance) && call || !Physics2D.Raycast(limits[1].position, Vector2.down, distance) && call) 
        {
            StartCoroutine("CallFlip");
        }
          else if (Physics2D.Raycast(limits[0].position, Vector2.left, distance, layerColl) && call && visonField == false || Physics2D.Raycast(limits[1].position, Vector2.right, distance, layerColl) && call && visonField == false)
        {
            StartCoroutine("CallFlip");
        }

    }

    IEnumerator CallFlip()
    {
        Flip();
        call = false;
        yield return tempo;
        call = true;
    }

    void Flip()
    {
        moveLeft = !moveLeft;

        if (moveLeft)
        {
            spRenderer.flipX = true;
        }
        else
        {
            spRenderer.flipX = false;
        }

    }

    IEnumerator FollowPlayer(bool flip, bool move)
    {
        yield return tempo;
        spRenderer.flipX = flip;
        moveLeft = move;
    }
    



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius*rad);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(limits[1].position, (Vector2)limits[1].position + Vector2.right * distance);
        Gizmos.DrawLine(limits[0].position, (Vector2)limits[0].position + Vector2.left * distance);
        Gizmos.DrawLine(limits[1].position, (Vector2)limits[1].position + Vector2.down * distance);
        Gizmos.DrawLine(limits[0].position, (Vector2)limits[0].position + Vector2.down * distance);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Explosion"))
        {
            eAnimation.Play(animationName[1]);
            isDead = true;
            eAudio.Stop();   
        }
        
    }

    private void Death ()
    {
        iTween.ColorTo(this.gameObject, iTween.Hash("g",0,"b",0, "r",0, "a",0, "time", 0.1f, "looptype", iTween.LoopType.pingPong));
        Destroy(this.gameObject, 2.2f);
    }
}
