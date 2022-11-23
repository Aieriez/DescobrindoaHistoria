using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundFX : MonoBehaviour
{
    private RawImage background;

    void Start()
    {
        background = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        background.uvRect = new Rect (0.01f *Time.time, 0,1,1);
    }
}
