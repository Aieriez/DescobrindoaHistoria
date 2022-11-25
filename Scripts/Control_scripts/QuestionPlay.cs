using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPlay : MonoBehaviour
{
    [SerializeField]
    private Animator qAnimation;

    [SerializeField]
    QuestionManager qManager;
    string body, answer;
    public int identity;
    public float difficult;
    [SerializeField]
    private float paperPosY;
    [SerializeField]
    DataManager data;

    void Start()
    {
        paperPosY  = transform.position.y + 1.4f;
        qAnimation = GameObject.FindWithTag("Question").GetComponent<Animator>();
        qManager = GameObject.FindWithTag("Question").GetComponent<QuestionManager>();
        data = GameObject.FindWithTag("DataSql").GetComponent<DataManager>();
        questPaperAnim();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            identity = GAMEMANAGER.instance.GetRandomNum();
            this.GetComponent<CircleCollider2D>().enabled = false;  
            qAnimation.Play("QuestPainelAnimation");
            LoadPaperId(identity);
            qManager.LoadQuestion(body, answer, difficult);
            body = "";
            answer = "";
            difficult = 0;
            qManager.Paper(this.gameObject);
            qManager.questAudio.clip  = AUDIOMANAGER.instance.questPopUp;
            qManager.questAudio.Play();
            GAMEMANAGER.isPaused = true;
        }
    }

    void LoadPaperId(int id)
    {   
        data.DataReader(id);
        body = data.text;
        answer = data.answer;
        difficult = data.difficulty;
    
    }
    void questPaperAnim()
    {
        iTween.MoveTo(this.gameObject,iTween.Hash("y", paperPosY, "time", 2,"looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.easeInBack));   
    }
}
