using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankManager : MonoBehaviour
{
    public static RankManager instance;
    public GameObject rankField;
    public Transform rankPanel;

    [SerializeField]
    private Rank[] ranksList;

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
        DataManager.data.RankingReader();
        SceneManager.sceneLoaded += RankLoad; 
    }
    
    void RankLoad(Scene scene, LoadSceneMode mode)
    {
       if(CURRENTSCENE.instance.levelName == "Ranking")
        { 
            rankPanel = GameObject.FindWithTag("RankPanel").GetComponent<Transform>();
            ShowRankList (); 
        } 
    }

    [System.Serializable]
    public class Rank
    {
        public long rankPos;
        public string name;
        public long score;
    }

    void ShowRankList ()
    {
        if (rankPanel.childCount < 10)
        {
            foreach (Rank rank in ranksList)
            {
                var newRank = Instantiate (rankField) as GameObject;
                RankField field = newRank.GetComponent<RankField>();
                field.pos.text = rank.rankPos.ToString();
                field.playerName.text = rank.name;
                field.playerScore.text = rank.score.ToString();
                newRank.transform.SetParent(rankPanel, false);
            }
        }
    }

    public void AddToRankList(int id, string name, long score)
    {       
        ranksList[id].rankPos = id+1;
        ranksList[id].name = name;
        ranksList[id].score = score;
    }


}
