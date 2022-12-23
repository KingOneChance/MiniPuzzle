using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LobbyMGR : MonoBehaviour
{

    [Header("=====BackGoundFirst=====")]
    [SerializeField] private GameObject BackGroundFirst = null;
    [SerializeField] private Button startButton = null;
    [SerializeField] private Button explainButton = null;
    [SerializeField] private Button exitButton = null;

    [Header("=====BackGoundSecond=====")]
    [SerializeField] private GameObject BackGroundSecond = null;
    [SerializeField] private Button singleButton = null;
    [SerializeField] private Button multiButton = null;
    [SerializeField] private GameObject explain1P = null;
    [SerializeField] private GameObject explain2P = null;

    [SerializeField] private GameMGR gameMGR = null;
    [SerializeField] private SoundMGR soundMGR = null;

    private void Awake()
    {
        if (FindObjectOfType<GameMGR>() == null)
            gameMGR = Instantiate(gameMGR);
        if (FindObjectOfType<SoundMGR>() == null)
            soundMGR = Instantiate(soundMGR);
    }

    private void Start()
    {
        GameMGR._instance.FindSoundMGR();

        BackGroundFirst.SetActive(true);
        BackGroundSecondInit();
        BackGroundSecond.SetActive(false);
    }
    private void BackGroundSecondInit()
    {
        singleButton.gameObject.SetActive(false);
        multiButton.gameObject.SetActive(false);
        explain1P.SetActive(false);
        explain2P.SetActive(false);
    }


    //게임시작 버튼
    public void OnClick_GameStart()
    {
        BackGroundFirst.SetActive(false);
        BackGroundSecond.SetActive(true);
        singleButton.gameObject.SetActive(true);
        multiButton.gameObject.SetActive(true);

        GameMGR._instance.soundMGR.On_ClickBtnSound();
    }
    //홈 클릭버튼
    public void OnClick_Home()
    {
        BackGroundFirst.SetActive(true);
        BackGroundSecondInit();
        BackGroundSecond.SetActive(false);

        GameMGR._instance.soundMGR.On_ClickBtnSound();
    }
    //게임 설명 버튼 
    public void OnClick_Explain()
    {
        BackGroundFirst.SetActive(false);
        BackGroundSecond.SetActive(true);
        explain1P.SetActive(true);
        explain2P.SetActive(true);

        GameMGR._instance.soundMGR.On_ClickBtnSound();
    }
   
    public void OnClick_Exit()
    {
        GameMGR._instance.soundMGR.On_ClickBtnSound();
        Application.Quit();
    }
    public void OnClick_ToSingle()
    {
        GameMGR._instance.soundMGR.BGMSoundOn();
        SceneManager.LoadScene("SingleMode");
    }
    public void OnClick_ToMulti()
    {
        GameMGR._instance.soundMGR.BGMSoundOn();
        SceneManager.LoadScene("MultiMode");
    }
}
