using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UiMGR : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreText2;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI startTimerText;
    [SerializeField] private float time = 100;
    [SerializeField] private float startTime = 4;


    [Header("FeverCount")]
    [SerializeField] private RawImage[] feverCount;
    [SerializeField] private RawImage[] feverCount2;

    [Header("FeverLogo")]
    [SerializeField] private RawImage feverLogo;
    [SerializeField] private RawImage feverLogo2;

    [Header("EndBackGround")]
    //화면 덮을 배경
    [SerializeField] private Image backGroundMain = null;
    [SerializeField] private Image backGround = null;
    [SerializeField] private Button homeButton = null;

    [Header("Tier")]
    [SerializeField] private Image bronzeAward = null;
    [SerializeField] private Image silverAward = null;
    [SerializeField] private Image goldAward = null;
    [SerializeField] private Image platinumAward = null;
    [SerializeField] private Image diamondAward = null;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI finalScore = null;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < feverCount.Length; i++)
        {
            feverCount[i].gameObject.SetActive(false);
        }
        scoreText.text = "Score : 0";
        timerText.text = "Time : " + time;
        //multi mode Text and feverTimeCount
        if (scoreText2 != null)
        {
            for (int i = 0; i < feverCount2.Length; i++)
            {
                feverCount2[i].gameObject.SetActive(false);
            }
            scoreText2.text = "Score : 0";
        }

        backGroundMain.gameObject.SetActive(false);
        backGround.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        bronzeAward.gameObject.SetActive(false);
        silverAward.gameObject.SetActive(false);
        goldAward.gameObject.SetActive(false);
        platinumAward.gameObject.SetActive(false);
        diamondAward.gameObject.SetActive(false);
        feverLogo.gameObject.SetActive(false);
        if (feverLogo2 != null)
            feverLogo2.gameObject.SetActive(false);

        finalScore.gameObject.SetActive(false);
    }

    int c = 0;
    // Update is called once per frame
    void Update()
    {
        if (startTime >= 0)
        {
            startTime -= Time.deltaTime;
            startTimerText.text = "" + (int)startTime;
        }
     
        if (startTime > 0) return;

        if (c == 0)
        {
            c++;
            GameMGR._instance.isGameOver = false;
            startTimerText.gameObject.SetActive(false);
        }
        if (time >= 0)
        {
            time -= Time.deltaTime;
        }
        timerText.text = "Time : " + (int)time;

        if (time <= 0&&c==1 )
        {
            c++;
            Debug.Log("0초");
            GameMGR._instance.GameOverScore();
        }
    }
    public void ShowScore(int score)
    {
        scoreText.text = "Score : " + score;
    }
    public void ShowScore2(int score)
    {
        scoreText2.text = "Score : " + score;
    }
    public void ShowFeverLogo() => feverLogo.gameObject.SetActive(true);
    public void ShowFeverLogo2() => feverLogo2.gameObject.SetActive(true);
    public void ShowOffFeverLogo() => feverLogo.gameObject.SetActive(false);
    public void ShowOffFeverLogo2() => feverLogo2.gameObject.SetActive(false);

    public void FinalShow(int score)
    {
        backGroundMain.gameObject.SetActive(true);
        backGround.gameObject.SetActive(true);
        if (score >= 3000)
        {
            diamondAward.gameObject.SetActive(true);
        }
        else if (score >= 2000)
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
            bronzeAward.gameObject.SetActive(true);
        }
        finalScore.gameObject.SetActive(true);
        finalScore.text = score.ToString();

        homeButton.gameObject.SetActive(true);
    }

    public void On_ClickGameReset()
    {
        if (GameMGR._instance.isSingleMode == false)
            GameMGR._instance.MultiSceneEnd();
        GameMGR._instance.SingleSceneEnd();
        SceneManager.LoadScene("LobbyScene");
    }
    int count = 0;
    public void GetFeverCount()
    {
        feverCount[count].gameObject.SetActive(true);
        count++;
    }
    int count2 = 0;
    public void GetFeverCount2()
    {
        feverCount2[count2].gameObject.SetActive(true);
        count2++;
    }
    public void GetInitFeverCount()
    {
        count = 0;
        for (int i = 0; i < feverCount.Length; i++)
        {
            if (feverCount[i].gameObject.activeSelf == true)
                feverCount[i].gameObject.SetActive(false);
        }
    }
    public void GetInitFeverCount2()
    {
        count2 = 0;
        for (int i = 0; i < feverCount.Length; i++)
        {
            if (feverCount2[i].gameObject.activeSelf == true)
                feverCount2[i].gameObject.SetActive(false);
        }
    }
}
