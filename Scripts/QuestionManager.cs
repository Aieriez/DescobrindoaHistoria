using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI question;
    public AudioSource questAudio;
    [SerializeField]
    private AudioClip rightClip, wrongClip;
    [SerializeField]
    private string answer;
    [SerializeField]
    private Button aBtn, bBtn, cBtn, dBtn;
    [SerializeField]
    private Animator qstAnimation;
    [SerializeField]
    private GameObject qstPaper;
    private float questDifficult;
    [SerializeField]
    private int consecutive;
    private int pointsValue = 150;
    
    // Method for Load the Question Text
    public void LoadQuestion(string texto, string resp, float d) 
    {   
        answer = resp;
        question.text = texto;
        questDifficult =  d;
    }
    private void Start() {

        questAudio = this.GetComponent<AudioSource>();
        rightClip = AUDIOMANAGER.instance.rightAnswer;
        wrongClip = AUDIOMANAGER.instance.wrongAnswer;
        
        aBtn = GameObject.Find("aBtn").GetComponent<Button>();
        bBtn = GameObject.Find("bBtn").GetComponent<Button>();
        cBtn = GameObject.Find("cBtn").GetComponent<Button>();
        dBtn = GameObject.Find("dBtn").GetComponent<Button>();

        aBtn.onClick.AddListener(() => questAnswer("A"));
        bBtn.onClick.AddListener(() => questAnswer("B"));
        cBtn.onClick.AddListener(() => questAnswer("C"));
        dBtn.onClick.AddListener(() => questAnswer("D"));
    }
    public void questAnswer(string option)
    {
        if (option == answer)
        {
            questAudio.clip = rightClip;
            questAudio.Play();
            consecutive++;
            POINTMANAGER.instance.points += (int)(pointsValue * questDifficult)*consecutive;
            GAMEMANAGER.instance.rightAnswers++;
        } 
        else
        {
            consecutive = 0;
            questAudio.clip = wrongClip;
            questAudio.Play();
        }
        answer = "";
        GAMEMANAGER.instance.questCount--;
        qstAnimation.Play("QuestPainelAnimationR");
        GAMEMANAGER.isPaused = false;
        Destroy(qstPaper);
    }
    public void Paper (GameObject paper)
    {
        qstPaper = paper;
    }

}
