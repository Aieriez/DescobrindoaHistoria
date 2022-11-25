using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private AudioSource spaceSource;
    private Animator spaceship;
    [SerializeField]
    private Button startBtn;
    [SerializeField]
    private AudioClip soundFX;

    // Start is called before the first frame update
    void Start()
    { 
        spaceship = GetComponent<Animator>();
        spaceSource = GetComponent<AudioSource>();
        startBtn.onClick.AddListener(() => StartLaunch());
    }

    public void StartLaunch()
    {
        spaceship.Play("Launch");
    }

    void LaunchSpaceship()
    {
        spaceSource.clip = soundFX;
        spaceSource.Play();
    }

    void GoLevels()
    {
        SceneManager.LoadScene("Menu_Levels");
    }

}
