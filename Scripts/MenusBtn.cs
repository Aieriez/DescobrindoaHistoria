using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenusBtn : MonoBehaviour
{
    [SerializeField]
    private Button btn, infoBtn;
    [SerializeField]
    private AudioSource menuAudio;
    [SerializeField]
    private AudioClip menuClip;
    [SerializeField]
    private string levelName;

    // Start is called before the first frame update
    void Start()
    {
        menuAudio = this.GetComponent<AudioSource>();
        menuAudio.clip = menuClip;
        menuAudio.Play();
        btn.onClick.AddListener(() => GoTo());
    }

    void GoTo()
    {
        menuAudio.Stop();
        SceneManager.LoadScene(levelName);
    }

}
