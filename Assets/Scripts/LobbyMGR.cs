using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LobbyMGR : MonoBehaviour
{
    [SerializeField] private Button startButton = null;
    [SerializeField] private Button explainButton = null;
    [SerializeField] private Button exitButton = null;
    [SerializeField] private Button singleButton = null;
    [SerializeField] private Button multiButton = null;

    [SerializeField] private Image explain1P = null;
    [SerializeField] private Image explain2P = null;

    [SerializeField] private GameMGR gameMGR = null;
    [SerializeField] private SoundMGR sounMGR = null;

    private void Awake()
    {
        if (FindObjectOfType<GameMGR>() == null)
            gameMGR = Instantiate(gameMGR);
        if (FindObjectOfType<SoundMGR>() == null)
            sounMGR = Instantiate(sounMGR);
    }

    private void Start()
    {
        GameMGR._instance.FindSoundMGR();
        singleButton.gameObject.SetActive(false);
        multiButton.gameObject.SetActive(false);
        explain1P.gameObject.SetActive(false);
        explain2P.gameObject.SetActive(false);
    }

    //게임시작 버튼
    public void OnClick_GameStart()
    {
        startButton.gameObject.SetActive(false);
        explainButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        singleButton.gameObject.SetActive(true);
        multiButton.gameObject.SetActive(true);
        explain1P.gameObject.SetActive(false);
        explain2P.gameObject.SetActive(false);

        GameMGR._instance.soundMGR.On_ClickBtnSound();
    }

    public void OnClick_Home()
    {
        startButton.gameObject.SetActive(true);
        explainButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        singleButton.gameObject.SetActive(false);
        multiButton.gameObject.SetActive(false);
        explain1P.gameObject.SetActive(false);
        explain2P.gameObject.SetActive(false);

        GameMGR._instance.soundMGR.On_ClickBtnSound();
    }

    public void OnClick_Explain()
    {
        explain1P.gameObject.SetActive(true);
        explain2P.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
        explainButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        singleButton.gameObject.SetActive(false);
        multiButton.gameObject.SetActive(false);
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
