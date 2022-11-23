using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LEVELMANAGER : MonoBehaviour
{
    public static LEVELMANAGER instance;

    [SerializeField]
    public GameObject button;
    public Transform levelPanel;
    public List<Level> levelsList;
    public TextMeshProUGUI Loading;
    [SerializeField]
    private GameObject tutorial;
    public int firstPlay;
    [SerializeField]
    private TMP_InputField inputName;
    public string nick;
    [SerializeField]
    private string password;

    void Awake() 
    {   
        password = POINTMANAGER.instance.LoadPassword();
        ZPlayerPrefs.Initialize(password, "descobrindoahistoria");
        if (instance == null)
        {
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
        else 
        {
			Destroy (gameObject);
		}
        if (!PlayerPrefs.HasKey("FirstPlay"))
        {
            PlayerPrefs.SetInt("FirstPlay", 0);
            firstPlay = PlayerPrefs.GetInt("FirstPlay");
        }
        if (!ZPlayerPrefs.HasKey("Level_1"))
        {
            ZPlayerPrefs.SetInt("Level_1", 1);
        }   
    }  
    // Start is called before the first frame update
    void Start()
    {
        //ZPlayerPrefs.DeleteAll();
        //PlayerPrefs.DeleteAll();
        Loading = GameObject.Find("LoadingTxt").GetComponent<TextMeshProUGUI>();
        ListAdd();
        if(POINTMANAGER.playerName != null)
        {
            inputName.text = POINTMANAGER.playerName; 
        }
        firstPlay = PlayerPrefs.GetInt("FirstPlay");
        if (firstPlay == 0 || firstPlay == 1)
        {   
			if(firstPlay == 1 )
			{
				Destroy(tutorial);
			}    
		}
        
    }

    [System.Serializable]
    public class Level
    {
        public string levelTxt;
        public bool active;
        public int unblocked;
        public bool textActive;
        public bool stars;
    }

    
    void ListAdd()
    {
        foreach (Level level in levelsList)
        {
            GameObject newBtn = Instantiate (button) as GameObject;
            LevelButton levelBtn = newBtn.GetComponent<LevelButton>();
            levelBtn.buttonText.text = level.levelTxt;
            levelBtn.stars.GetComponent<Image>();

            if(ZPlayerPrefs.HasKey("Level_"+levelBtn.buttonText.text))
            {
                if(ZPlayerPrefs.GetInt("Level_"+levelBtn.buttonText.text) == 1)
                {
                    level.unblocked = 1;
                    level.active = true;
                    level.textActive = true;
                    level.stars = true;    
                }
                
                if(level.stars)
                {
                    levelBtn.stars.enabled = true;
                    if (ZPlayerPrefs.HasKey("StarsLevel_"+level.levelTxt))
                    {
                        switch (ZPlayerPrefs.GetInt("StarsLevel_"+level.levelTxt))
                        {
                            case 1:
                                levelBtn.starIco1.enabled = level.stars;
                                break;
                            case 2:
                                levelBtn.starIco1.enabled = level.stars;
                                levelBtn.starIco2.enabled = level.stars;
                                break;
                            case 3:
                                levelBtn.starIco1.enabled = level.stars;
                                levelBtn.starIco2.enabled = level.stars;
                                levelBtn.starIco3.enabled = level.stars;
                                break;
                        }
                    }
                }           
            }
            else
            {
                ZPlayerPrefs.SetInt("Level_"+levelBtn.buttonText.text, 0);
            }
            
            
            levelBtn.unblockedButton = level.unblocked;
            levelBtn.GetComponent<Button>().interactable = level.active;
            levelBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = level.textActive;
            levelBtn.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level_" + levelBtn.buttonText.text));
            newBtn.transform.SetParent(levelPanel, false);        
        }
    }

    void ClickLevel(string level)
    {
        if (nick != "")
        {
            if (/*level.Equals("Level_1") &&*/ firstPlay == 0)
            {   
                tutorial.SetActive(true);
            }
            else
            {
                StartCoroutine(LoadGame(level));
            }
        }
        else
        {
            inputName.Select();
        } 
        
    }

    IEnumerator LoadGame(string level)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(level);
        while (!async.isDone)
        {
            Loading.enabled = true;
            yield return null;
        }
    }
    public void CloseTutorial() 
    { 
        Destroy(tutorial);
        PlayerPrefs.SetInt("FirstPlay", 1);
        StartCoroutine(LoadGame("Level_1"));
    }

    public void InputEnd()
    {
        nick = inputName.text;
        POINTMANAGER.playerName = nick.ToUpper();
        inputName.DeactivateInputField();
    }

}
