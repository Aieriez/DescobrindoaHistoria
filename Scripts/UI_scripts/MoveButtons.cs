using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoveButtons : MonoBehaviour
{
    [SerializeField]
    private Button btn;
    [SerializeField]
    private int time;
    [SerializeField]
    private float position;
    [SerializeField]
    private float seconds = 4f;
    private WaitForSeconds wfs;
    void Start()
    {
        btn = GetComponent<Button>();
        wfs = new WaitForSeconds(seconds);
        MoveUp();
    }

    // Update is called once per frame
    void MoveUp()
    {
        btn.GetComponent<RectTransform>().DOAnchorPosY(position, time, true);
        StartCoroutine(EnableButton());
    }

    IEnumerator EnableButton()
    {
        yield return wfs;
        btn.interactable = true;
    }

}
