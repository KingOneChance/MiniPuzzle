using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiMGR : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : 0";
        timerText.text = "Time : 100";   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
