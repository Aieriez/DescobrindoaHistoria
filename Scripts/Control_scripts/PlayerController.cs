using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool face,
        alive;
    public Rigidbody2D playerBody;
    public Transform hero,
        check,
        launch;

    //Move Controllers
    public float speed = 15;
    public float move, updown;
    //Jump Controllers
    public float smallJump = 8f,
        bigJump = 2.2f;
    public bool onLand;
    public LayerMask isLand;
    public float radius = 0.02f;
    public float jumpForce = 19;
    //Climb Controllers
    public bool allowClimb = false;
    public bool isClimbing = false;
    public Collider2D ladder;
    public Animator playerAnimation;
    //Push Controllers
    public LayerMask mask;
    public bool push;
    public float distance = 0.19f;
    //Throw
    [SerializeField]
    private GameObject bomb,
        box;

    //Audio
    [SerializeField]
    AudioClip jumpClip;

    [SerializeField]
    AudioSource playerAudio;
    public Vector2 playerDirection;
    private WaitForSeconds tempo = new WaitForSeconds(1);
    
    void Start()
    {
        hero = GetComponent<Transform>();
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        check = GameObject.Find("check").GetComponent<Transform>();
        launch = GameObject.Find("launch").GetComponent<Transform>();
        alive = true;
        face = true;
        playerAudio = this.gameObject.GetComponent<AudioSource>();
        jumpClip = AUDIOMANAGER.instance.jump;
        playerDirection = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GAMEMANAGER.isPaused)
        {
            if (alive == true)
            {
                // JUMP
                if (onLand && Input.GetKeyDown(KeyCode.X))
                {
                    playerAudio.clip = jumpClip;
                    playerAudio.Play();
                    playerBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
                //MOVE
                move = Input.GetAxis("Horizontal");
                updown = Input.GetAxis("Vertical");

                if (onLand)
                {
                    playerAnimation.SetFloat("X", Mathf.Abs(move));
                }

                playerAnimation.SetFloat("Y", playerBody.velocity.y);
                playerAnimation.SetBool("Land", onLand);

                //CLIMB
                if (allowClimb && Mathf.Abs(updown) > 0f)
                {
                    isClimbing = true;
                }
                                
                //PUSH
                if (box != null)
                {
                    if (Input.GetKey(KeyCode.Z))
                    {
                        push = true;
                        if (move > 0)
                        {
                            playerAnimation.SetBool("IsPushingRight", true);
                            playerAnimation.SetBool("IsPushingLeft", false);
                        }
                        else if (move < 0)
                        {
                            playerAnimation.SetBool("IsPushingLeft", true);
                            playerAnimation.SetBool("IsPushingRight", false);
                        }
                        
                        box.GetComponent<Rigidbody2D>().isKinematic = false;
                        GetComponent<FixedJoint2D>().enabled = true;
                        GetComponent<FixedJoint2D>().connectedBody = box.GetComponent<Rigidbody2D>();
                    }
                    else
                    {
                        push = false;
                        playerAnimation.SetBool("IsPushingRight", false);
                        playerAnimation.SetBool("IsPushingLeft", false);
                        GetComponent<FixedJoint2D>().connectedBody = null;
                        GetComponent<FixedJoint2D>().enabled = false;
                        box.GetComponent<Rigidbody2D>().isKinematic = true;
                        box.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
                        box = null;
                    }
                }

                //THROW STONE
                if (Input.GetKeyDown(KeyCode.C) && Input.GetAxis("Horizontal") == 0)
                {
                    if (GAMEMANAGER.instance.bombsNum > 0)
                    {
                        playerAnimation.SetTrigger("Throw");
                        GAMEMANAGER.instance.bombsNum--;
                    }
                }

                if (transform.localScale.x > 0)
                {
                    playerDirection = Vector2.zero;
                    playerDirection += Vector2.right;
                }
                else
                {
                    playerDirection = Vector2.zero;
                    playerDirection += Vector2.left;
                }
            }

            // DEATH
            if (alive == false)
            {
                playerAnimation.SetTrigger("Death");
            }
        }
        else
        {
            move = 0;
            playerBody.velocity *= move;
            playerAnimation.SetFloat("X", 0);         
        }
        
    }

    private void FixedUpdate()
    {
        //IS ON LAND?
        onLand = Physics2D.OverlapCircle(check.position, radius, isLand);

        if(!GAMEMANAGER.isPaused)
        {
            if (playerBody.velocity.y < 0)
            {
                playerBody.gravityScale = smallJump;
            }
            else if (playerBody.velocity.y > 0)
            {
                playerBody.gravityScale = bigJump;
            }
            else if (!Input.GetKeyDown(KeyCode.UpArrow) && !allowClimb && !Input.GetKeyDown(KeyCode.X))
            {
                playerBody.gravityScale = 1;
            }

            playerBody.velocity = new Vector2(move * speed, playerBody.velocity.y);

            if (!push)
            {
                if (move > 0 && !face)
                {
                    Flip(hero);
                }
                else if (move < 0 && face)
                {
                    Flip(hero);
                }
            }
            if (isClimbing)
            {

                playerBody.gravityScale = 0f;
                ladder.GetComponentInChildren<BoxCollider2D>().enabled = false;
                if (updown > 0)
                {
                    playerAnimation.SetBool("IsClimbingUp", true);
                }
                else if (updown < 0)
                {
                    playerAnimation.SetBool("IsClimbingDown", true);
                }
                playerBody.velocity = new Vector2 (playerBody.velocity.x, (updown * (speed/2)));    
            }
            else
            {
                playerAnimation.SetBool("IsClimbingUp", false);
                playerAnimation.SetBool("IsClimbingDown", false);
            }
        }
    }

    private void Flip(Transform t)
    {
        face = !face;
        Vector3 scale = t.localScale;
        scale.x *= -1;
        t.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            allowClimb = true;
            ladder = other.GetComponentInChildren<BoxCollider2D>();
        }
        if (other.gameObject.CompareTag("Box"))
        {
            box = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            allowClimb = false;
            isClimbing = false;
            playerBody.gravityScale = 1f;
            ladder.GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
    }

    void PlayerDeath()
    {
        Destroy(this.gameObject, 2);
        GAMEMANAGER.instance.lose = true;
    }

    void ThrowBomb()
    {
        GameObject bombT = Instantiate(bomb, launch.position, Quaternion.identity) as GameObject;
        bombT.GetComponent<Rigidbody2D>().AddForce(new Vector2(5f * transform.localScale.x, 20f), ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawSphere(check.position, radius);
    }

    private void Jump()
    {
        onLand = Physics2D.OverlapCircle(check.position, radius, isLand);

        if (onLand)
        {
            playerBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            playerAnimation.SetTrigger("Jump");
        }
    }

    void ReturnColor()
    {
        iTween.ColorTo(gameObject, iTween.Hash("color", Color.white, "time", 0.01f));
    }

    IEnumerator StopEffect()
    {
        yield return tempo;
        iTween.Stop(gameObject, true);
        ReturnColor();
    }
}
