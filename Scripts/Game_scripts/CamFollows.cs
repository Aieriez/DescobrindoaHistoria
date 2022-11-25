using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollows : MonoBehaviour
{
    public static CamFollows follows;

    public GameObject player;
    public float camVel = .25f;
    public bool followPlayer;
    public Vector3 velAtual, newPos;
    [Range(0,2)]
    public float fixCam = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        followPlayer  = true; 
    }

    private void Awake()
    {
        if(follows == null)
        {
            follows =  this;
        }
        
    }
    // Update is called once per frame
    private void FixedUpdate() {
        
        if (followPlayer)
        {
            if(player.transform.position.x >= transform.position.x)
            {
               newPos = Vector3.SmoothDamp(transform.position, player.transform.position, ref velAtual, camVel);
               transform.position = new Vector3 (newPos.x, newPos.y + fixCam, transform.position.z);
            }
            else if (transform.position.x >= GameObject.FindWithTag("Back").GetComponent<Transform>().position.x)
            {
                newPos = Vector3.SmoothDamp(transform.position, player.transform.position, ref velAtual, camVel);
                transform.position = new Vector3 (newPos.x, newPos.y + fixCam, transform.position.z);
            }
        }
    }
}
