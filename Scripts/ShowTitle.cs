using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ShowTitle : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        title = GetComponent<TextMeshProUGUI>();
        title.DOFade(1, time);        
    }

}
