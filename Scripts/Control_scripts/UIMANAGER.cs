using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMANAGER : MonoBehaviour
{
    public static UIMANAGER instance;
    public Animator painelGameOver, painelWin, painelPause, star1, star2, star3;
    public Button winBtnMenu, winBtnReplay, winBtnNext;
    public AudioSource winClip, loseClip;
    public Button loseBtnMenu, loseBtnReplay;
    public Button pauseBtn, pauseBtnMenu, pauseBtnReplay, pauseBtnContinue;
    public TextMeshProUGUI lifeCount, pointCount, questCount, winPoints;
    public Image pauseBackground, lifebar;
    public TextMeshProUGUI BombUI;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Load;
    }

    void Load(Scene scene, LoadSceneMode mode)
    {
        DataLoanding();
    }

    void DataLoanding()
    {
        if (CURRENTSCENE.instance.level >= 2 && CURRENTSCENE.instance.level < 7)
        {
            //Painel
            painelGameOver = GameObject.Find("LosePainel").GetComponent<Animator>();
            painelWin = GameObject.Find("WinPainel").GetComponent<Animator>();
            painelPause = GameObject.Find("PausePainel").GetComponent<Animator>();
            //Win Buttons
            winBtnMenu = GameObject.Find("WinMenu").GetComponent<Button>();
            winBtnReplay = GameObject.Find("WinReplay").GetComponent<Button>();
            winBtnNext = GameObject.Find("WinNext").GetComponent<Button>();
            //Lose Buttons
            loseBtnMenu = GameObject.Find("LoseMenu").GetComponent<Button>();
            loseBtnReplay = GameObject.Find("LoseReplay").GetComponent<Button>();
            //Pause Buttons
            pauseBtn = GameObject.Find("PauseButton").GetComponent<Button>();
            pauseBtnMenu = GameObject.Find("PauseMenu").GetComponent<Button>();
            pauseBtnReplay = GameObject.Find("PauseReplay").GetComponent<Button>();
            pauseBtnContinue = GameObject.Find("PauseContinue").GetComponent<Button>();
            //Painel Win Star Animation
            star1 = GameObject.FindWithTag("Star1").GetComponent<Animator>();
            star2 = GameObject.FindWithTag("Star2").GetComponent<Animator>();
            star3 = GameObject.FindWithTag("Star3").GetComponent<Animator>();
            //Pause Image Background
            pauseBackground = GameObject.FindWithTag("BgPause").GetComponent<Image>();
            //Audio
            winClip = painelWin.GetComponent<AudioSource>();
            loseClip = painelGameOver.GetComponent<AudioSource>();
            //Text Counts
            BombUI = GameObject.FindWithTag("GameController").GetComponentInChildren<TextMeshProUGUI>();
            lifebar = GameObject.FindWithTag("Lifebar").GetComponent<Image>(); 
            lifeCount = lifebar.GetComponentInChildren<TextMeshProUGUI>();
            pointCount = GameObject.FindWithTag("PointsNum").GetComponent<TextMeshProUGUI>();
            questCount = GameObject.FindWithTag("QuestNum").GetComponent<TextMeshProUGUI>();
            winPoints = GameObject.FindWithTag("WinPoints").GetComponent<TextMeshProUGUI>();
            //Pause Buttons Events
            pauseBtn.onClick.AddListener(Pause);
            pauseBtnContinue.onClick.AddListener(Continue);
            pauseBtnMenu.onClick.AddListener(GoMenu);
            pauseBtnReplay.onClick.AddListener(Replay);
            //Win Buttons Events
            winBtnMenu.onClick.AddListener(GoMenu);
            winBtnReplay.onClick.AddListener(Replay);
            winBtnNext.onClick.AddListener(NextLevel);
            //Lose Buttons Events
            loseBtnMenu.onClick.AddListener(GoMenu);
            loseBtnReplay.onClick.AddListener(Replay);
            //Win and Lose clips
            winClip.clip = AUDIOMANAGER.instance.winClip;
            loseClip.clip = AUDIOMANAGER.instance.loseClip;
        }
    }

    //Pause Methods
    void Pause()
    {
        AudioListener.pause = true;
        GAMEMANAGER.isPaused = true;
        pauseBtn.interactable = false;
        pauseBackground.enabled = true;
        painelPause.Play("PauseAnimation");
        TimerController.TimerStop(); 
    }

    void Continue()
    {
        pauseBtn.interactable = true;
        AudioListener.pause = false;
        GAMEMANAGER.isPaused = false;
        pauseBackground.enabled = false;
        painelPause.Play("ContinueAnimation");
        TimerController.TimerStop();
    }

    //Global Methods
    void Replay()
    {
        SceneManager.LoadScene(CURRENTSCENE.instance.level);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    void GoMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        GAMEMANAGER.instance.playing = false;
        AUDIOMANAGER.instance.source.Stop();
        AUDIOMANAGER.instance.audioPause = true;
        AudioListener.pause = false;
    }

    //Win Methods
    void NextLevel()
    {
        SceneManager.LoadScene(CURRENTSCENE.instance.level + 1);
    }
}
