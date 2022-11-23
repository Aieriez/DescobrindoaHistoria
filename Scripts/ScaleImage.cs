using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleImage : MonoBehaviour
{
    private RectTransform img;
    [SerializeField]
    private int time;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<RectTransform>();
        ImageScale();
    }

    void ImageScale()
    {
        iTween.ScaleTo(img.gameObject, new Vector3(1,1,0), time);
        //img.DOScale (new Vector3(1,1,0), time);
    }
}
