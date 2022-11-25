using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
   public AudioSource starSource;
   public AudioClip clip;
   public string starName;

    public void PlayAudioStar()
    {
        starSource = GetComponent<AudioSource>();
        if(!starSource.isPlaying)
        {
            starSource.clip = clip;
            starSource.Play();
        }
    }
    public void CheckStar()
    {
        if(starName.Equals("Star1Full"))
        {
            GAMEMANAGER.instance.star1End = true;
        }
        else if(starName.Equals("Star2Full"))
        {
            GAMEMANAGER.instance.star2End = true;
        }
    }
}

