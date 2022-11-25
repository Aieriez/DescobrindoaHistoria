using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CURRENTSCENE : MonoBehaviour
{
    public static CURRENTSCENE instance;
    [SerializeField]
    public int level = -1;
    [SerializeField]
    public string levelName;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += CheckLevel;
    }

    void CheckLevel(Scene scene, LoadSceneMode mode)
    {
        level = SceneManager.GetActiveScene().buildIndex;
        levelName = SceneManager.GetActiveScene().name;
    }
}
