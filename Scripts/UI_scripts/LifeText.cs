using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image lifebar;
    [SerializeField]
    private TextMeshProUGUI lifecount;
    [SerializeField]
    private int numLife;

    void Start()
    {
        lifebar = this.GetComponentInParent<Image>();
        lifecount = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        numLife = int.Parse(lifecount.text);

        if(numLife < 60)
        {
            lifecount.color = Color.yellow;
            lifebar.color = Color.yellow;
            if(numLife < 30)
            {
                lifecount.color = Color.red;
                lifebar.color = Color.red;
            }
        }
        
    }

}
