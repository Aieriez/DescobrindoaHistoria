using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAMEMANAGER : MonoBehaviour {

	public static GAMEMANAGER instance;
    public bool win, lose, playing = false, end;
    public static bool isPaused = false;
    public int quests, questCount, rightAnswers; 
    public int life;
    public WaitForSeconds time =  new WaitForSeconds(1);
    public GameObject player, fallLimit;
    public int starNums, dataSize;
    public Transform startPos;
    public bool star1End, star2End, winAudio = false, loseAudio = false, locked = false;
    [SerializeField]
    List<int> numArray = new List<int>();
    public int bombsNum;
    [SerializeField]
    private int levelPoints;
    [SerializeField]
    private string heroName;

    void Awake()
	{
        //ZPlayerPrefs.Initialize(POINTMANAGER.instance.LoadPassword(), "descobrindoahistoria");
		if (instance == null)
        {
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
        else 
        {
			Destroy (gameObject);
		}

	    SceneManager.sceneLoaded += LoadGame;
	}

    void LoadGame (Scene scene, LoadSceneMode mode)
    {
        if(!CURRENTSCENE.instance.levelName.Equals("Menu_Iniciar") && !CURRENTSCENE.instance.levelName.Equals("Menu_Levels") && !CURRENTSCENE.instance.levelName.Equals("Ranking"))
        {            
            startPos  = GameObject.FindWithTag("Respawn").GetComponent<Transform>();
            player = GameObject.FindWithTag("Player");
            life = 100;
            starNums = 0;
            rightAnswers = 0;
            levelPoints = POINTMANAGER.instance.points = 0;
            numArray.Clear();
            fallLimit = GameObject.FindWithTag("DownLimit");
            StartGame(); 
        }
    }

    void StartGame()
    {
        playing = true;
        win = false;
        lose = false;
        end = false;
        winAudio = false;
        loseAudio = false;
        locked =  false;
        star1End = false;
        star2End = false;
        isPaused = false;
        bombsNum = 3;
        heroName = POINTMANAGER.playerName;
        AUDIOMANAGER.instance.audioPause = false;
        UIMANAGER.instance.lifebar.fillAmount = (float)life/100; 
        questCount = GameObject.FindGameObjectsWithTag("PaperQuest").Length;
        quests = questCount;
        UIMANAGER.instance.lifeCount.text = life.ToString();
        UIMANAGER.instance.questCount.text = questCount.ToString();
    }
    void WinGame()
    {
        if(!CURRENTSCENE.instance.levelName.Equals("Menu_Iniciar") && !CURRENTSCENE.instance.levelName.Equals("Menu_Levels") && !CURRENTSCENE.instance.levelName.Equals("Ranking"))
        {
            
            if(playing != false)
            {
                AUDIOMANAGER.instance.audioPause = true;
                playing = false;
                ZPlayerPrefs.SetInt("Level_"+(CURRENTSCENE.instance.level), 1);
                UIMANAGER.instance.winPoints.text = POINTMANAGER.instance.points.ToString();
                levelPoints = POINTMANAGER.instance.points;
                UIMANAGER.instance.painelWin.Play("PainelYouWin");
                POINTMANAGER.instance.SaveBestLevelPoints(POINTMANAGER.playerName, CURRENTSCENE.instance.levelName, levelPoints);
                if(CURRENTSCENE.instance.levelName.Equals("Level_5"))
                {
                    POINTMANAGER.instance.SaveToData(POINTMANAGER.playerName);
                    ZPlayerPrefs.DeleteAll();
                }
                if (!UIMANAGER.instance.winClip.isPlaying && winAudio == false)
                {
                    UIMANAGER.instance.winClip.Play();
                    winAudio = true;
                }
            }
            if (winAudio && !UIMANAGER.instance.winClip.isPlaying && locked == false)
            {   
                if (rightAnswers == quests)
                {
                    UIMANAGER.instance.star1.Play("Star1");
                    if(star1End)
                    {
                        UIMANAGER.instance.star2.Play("Star2");
                        if(star2End)
                        {
                            UIMANAGER.instance.star3.Play("Star3");
                            WinMenu();
                        }
                    }  
                    starNums = 3;
                }
                else if(rightAnswers > (quests/2))
                {
                    UIMANAGER.instance.star1.Play("Star1");
                    if(star1End)
                    {
                        UIMANAGER.instance.star2.Play("Star2");
                        WinMenu(); 
                    }
                    starNums = 2;
                }            
                else
                {
                    UIMANAGER.instance.star1.Play("Star1");
                    starNums = 1;
                    WinMenu();
                }

                if (ZPlayerPrefs.HasKey("StarsLevel_" + (CURRENTSCENE.instance.level - 1)))
                {
                   int aux = ZPlayerPrefs.GetInt("StarsLevel_" + (CURRENTSCENE.instance.level - 1), starNums);
                   if (starNums > aux)
                   {
                        ZPlayerPrefs.SetInt("StarsLevel_" + (CURRENTSCENE.instance.level - 1), starNums);
                   }
                }
                else
                {
                    ZPlayerPrefs.SetInt("StarsLevel_" + (CURRENTSCENE.instance.level - 1), starNums);
                }
            }
            
        }
    }
    void GameOver()
    {
        if(!CURRENTSCENE.instance.levelName.Equals("Menu_Iniciar") && !CURRENTSCENE.instance.levelName.Equals("Menu_Levels") && !CURRENTSCENE.instance.levelName.Equals("Ranking"))
        {
            if(playing != false)
            {
                AUDIOMANAGER.instance.audioPause = true;
                playing = false;
                UIMANAGER.instance.painelGameOver.Play("PainelGameOver");
                UIMANAGER.instance.loseClip.Play();
                UIMANAGER.instance.loseBtnMenu.interactable = true;
                UIMANAGER.instance.loseBtnReplay.interactable = true;  
                isPaused = true;
            }
        }

    }

    void Start () {

        if(!CURRENTSCENE.instance.levelName.Equals("Menu_Iniciar") && !CURRENTSCENE.instance.levelName.Equals("Menu_Levels") && !CURRENTSCENE.instance.levelName.Equals("Ranking"))
        {
		    StartGame ();
        }
	}
	
	void Update () {

            UIMANAGER.instance.BombUI.text = bombsNum.ToString();
            UIMANAGER.instance.lifeCount.text = life.ToString();
            UIMANAGER.instance.questCount.text = questCount.ToString();
            UIMANAGER.instance.pointCount.text = POINTMANAGER.instance.points.ToString();

            if(questCount == 0 && end)
            {
                win = true;
            }
            else if(questCount == 0 && rightAnswers == 0)
            {
                lose = true;
            }
            else if (life <= 0)
            {
                life = 0;
                CamFollows.follows.followPlayer = false;
                if(playing)
                {
                    player.GetComponent<PlayerController>().alive = false;
                }   
            } 

            if(win)
            {
                WinGame ();
            }
            else if(lose)
            {
                GameOver ();
            }
        if (playing)
        { 
            if (player.transform.position.y <= fallLimit.transform.position.y)
            {
                player.transform.position = startPos.position;
                player.GetComponent<KnockBack>().Damage(10);
            }
        }
	}
    public int GetRandomNum()
    {
        int num = Random.Range(1, dataSize+1);
        if(numArray.Count == 0 || !numArray.Contains(num))
        {
            numArray.Add(num);
        }
        else
        {
            return GetRandomNum();
        }  
        return num;
                
    }
    private void WinMenu()
    {
        locked = true; 
        UIMANAGER.instance.winBtnMenu.interactable = true;
        UIMANAGER.instance.winBtnNext.interactable = true;
        UIMANAGER.instance.winBtnReplay.interactable = true;              
    }
    
}