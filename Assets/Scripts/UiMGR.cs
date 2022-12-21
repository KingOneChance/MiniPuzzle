using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiMGR : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float time = 100;


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : 0";
        timerText.text = "Time : 100";
    }
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        timerText.text = "Time : " + (int)time;
    }
    public void ShowScore(int score)
    {
        scoreText.text = "Score : " + score;
    }

}
