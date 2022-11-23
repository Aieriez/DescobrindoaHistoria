using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUDIOMANAGER : MonoBehaviour
{
    public static AUDIOMANAGER instance;
    [SerializeField]
    public AudioSource source;
    public AudioClip winClip, loseClip, questPopUp, wrongAnswer, rightAnswer, jump;
    [SerializeField]
    private AudioClip[] backgroundClips;
    public bool audioPause = false;
    private void Awake()
    {
        if (instance == null)
        {
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
         
    }
    void Update()
    {
        if (!source.isPlaying && !audioPause)
        {
            source.clip = GetRandom();
            source.Play();
        }
        if(audioPause)
        {
            source.Pause();
        }
        else
        {
            source.UnPause();
        }
    }
    AudioClip GetRandom()
	{
		return backgroundClips [Random.Range (0, backgroundClips.Length)];
	}
}
