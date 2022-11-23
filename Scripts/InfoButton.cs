using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    [SerializeField]
    GameObject infoPanel;
    [SerializeField]
    Button infoButton, playButton, rankButton, tutorialOn, tutorialOff;
    bool isactive = false;

    // Start is called before the first frame update
    void Start()
    {
        infoPanel.SetActive(isactive);
        infoButton = this.gameObject.GetComponent<Button>();
        infoButton.onClick.AddListener(() => ShowInfoPanel());
        tutorialOn.onClick.AddListener(() => TutorialOnOff(0));
        tutorialOff.onClick.AddListener(() => TutorialOnOff(1));
        if(PlayerPrefs.HasKey("FirstPlay"))
        {
            int on = PlayerPrefs.GetInt("FirstPlay");
            if (on == 0)
            {   
                tutorialOn.interactable = false;
                tutorialOff.interactable = true;
            }
            else
            {
                tutorialOn.interactable = true;
                tutorialOff.interactable = false;
            }
        }
    }


    void ShowInfoPanel()
    {
        isactive = !isactive;
        infoPanel.SetActive(isactive);
        playButton.interactable = !isactive;
        rankButton.interactable = !isactive;
    }

    public void TutorialOnOff (int on)
    {
        PlayerPrefs.SetInt("FirstPlay", on );
        if(tutorialOn.interactable)
        {
            tutorialOn.interactable = false;
            tutorialOff.interactable = true;
        }
        else
        {
            tutorialOn.interactable = true;
            tutorialOff.interactable = false;
        }
        
    }
}
