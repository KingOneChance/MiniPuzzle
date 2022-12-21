using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UiMGR : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float time = 5;


    //화면 덮을 배경
    [SerializeField] private Image backGroundMain = null;
    [SerializeField] private Image backGround = null;
    [SerializeField] private Button homeButton = null;
    //점수별 등급
    [SerializeField] private Image bronzeAward = null;
    [SerializeField] private Image silverAward = null;
    [SerializeField] private Image goldAward = null;
    [SerializeField] private Image platinumAward = null;
    [SerializeField] private Image diamondAward = null;
    //최종점수
    [SerializeField] private TextMeshProUGUI finalScore = null;

    private bool isStop = false;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : 0";
        timerText.text = "Time : 100";

        backGroundMain.gameObject.SetActive(false);
        backGround.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        bronzeAward.gameObject.SetActive(false);
        silverAward.gameObject.SetActive(false);
        goldAward.gameObject.SetActive(false);
        platinumAward.gameObject.SetActive(false);
        diamondAward.gameObject.SetActive(false);

        finalScore.gameObject.SetActive(false);
    }
   
    // Update is called once per frame
    void Update()
    {
        if(time >= 0)
        {
            time -= Time.deltaTime;
        }
        timerText.text = "Time : " + (int)time;
        
        if(time < 0 && isStop ==false)
        {
            GameMGR._instance.GameOverScore();

            isStop = true;
        }
        
    }
    public void ShowScore(int score)
    {
        scoreText.text = "Score : " + score;
    }

    public void FinalShow(int score)
    {
        backGroundMain.gameObject.SetActive(true);
        backGround.gameObject.SetActive(true);
        if (score >= 3000)
        {
            diamondAward.gameObject.SetActive(true);
        }
        else if(score >= 2000)
        {
            platinumAward.gameObject.SetActive(true);
        }
        else if (score >= 1500)
        {
            goldAward.gameObject.SetActive(true);
        }
        else if (score >= 1000)
        {
            silverAward.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("들어옴");
            bronzeAward.gameObject.SetActive(true);
        }
        finalScore.gameObject.SetActive(true);
        finalScore.text = score.ToString();
        
        homeButton.gameObject.SetActive(true);
    }

    public void On_ClickGameReset()
    {
        SceneManager.LoadScene("LobbyScene");
        
    }

}
