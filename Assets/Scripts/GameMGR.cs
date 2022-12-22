using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMGR : MonoBehaviour
{
    #region GameMGR Singleton
    private static GameMGR instance;
    public static GameMGR _instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameMGR>();
                if (instance == null)
                    instance = new GameObject("GameMgr").AddComponent<GameMGR>();
            }
            return instance;
        }
    }
    #endregion
    public bool feverState;
    public bool feverState2;
    [SerializeField] private int score;
    [SerializeField] private int score2;
    [SerializeField] private int feverTimeCount;
    [SerializeField] private int feverTimeCount2;
    [SerializeField] private int targetFeverCount = 20;
    [SerializeField] private UiMGR uiMGR;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private SpawnManager2 spawnManager2;
    [SerializeField] public PlayerInput playerInput;
    [SerializeField] public PlayerInput2 playerInput2;
    [SerializeField] public bool isSingleMode;

    [SerializeField] private SoundMGR soundMGR;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SingleSceneAwake()
    {
        soundMGR = FindObjectOfType<SoundMGR>();
        playerInput = FindObjectOfType<PlayerInput>();
        spawnManager = FindObjectOfType<SpawnManager>();
        uiMGR = FindObjectOfType<UiMGR>();
        isSingleMode = true;
    }
    public void SingleSceneEnd()
    {
        playerInput = null;
        spawnManager = null;
        uiMGR = null;
        isSingleMode = false;    
    }
    public void MultiSceneAwake()
    {
        soundMGR = FindObjectOfType<SoundMGR>();
        playerInput = FindObjectOfType<PlayerInput>();
        spawnManager = FindObjectOfType<SpawnManager>();
        playerInput2 = FindObjectOfType<PlayerInput2>();
        spawnManager2 = FindObjectOfType<SpawnManager2>();
        uiMGR = FindObjectOfType<UiMGR>();
        isSingleMode = false;
    }
    public void MultiSceneEnd()
    {
        playerInput = null;
        spawnManager = null;
        playerInput2 = null;
        spawnManager2 = null;
        uiMGR = null;
        isSingleMode = false;
    }
    public void AddScore(int num)
    {
        score += num;
        uiMGR.ShowScore(score);
        if (feverState == false)
        {
            feverTimeCount++;
            //ui fever Count up
            SendFeverCount();
            soundMGR.CorrectSound();
        }
        if (feverTimeCount == targetFeverCount)
        {           
            feverState = true;
            FeverTime();
            InitFeverCount();
            //Ui fever Count Init
            SendInitFeverCount();
            soundMGR.FeverTimeSound();
        }
    }
    public void AddScore2(int num)
    {
        score2 += num;
        uiMGR.ShowScore2(score2);
        if (feverState2 == false)
        {
            soundMGR.CorrectSound();
            feverTimeCount2++;
            //ui fever Count up
            SendFeverCount2();
        }
        if (feverTimeCount2 == targetFeverCount)
        {
            feverState2 = true;
            FeverTime2();
            InitFeverCount2();
            //Ui fever Count Init
            SendInitFeverCount2();
        }
    }
    public void InitFeverCount() => feverTimeCount = 0;
    public void SendFeverCount() => uiMGR.GetFeverCount();
    public void SendInitFeverCount() => uiMGR.GetInitFeverCount();

    public void InitFeverCount2() => feverTimeCount2 = 0;
    public void SendFeverCount2() => uiMGR.GetFeverCount2();
    public void SendInitFeverCount2() => uiMGR.GetInitFeverCount2();

    public void FeverTime()
    {
        playerInput.RestartClick();
        spawnManager.FeverScore();
        Invoke("FeverTimeEnd",5);
    }
    public void FeverTime2()
    {
        playerInput2.RestartClick();
        spawnManager2.FeverScore();
        Invoke("FeverTimeEnd2", 5);
    }
    public void FeverTimeEnd()
    {
        playerInput.RestartClick();
        feverState = false;
        InitFeverCount();
        //Ui fever Count Init
        SendInitFeverCount();
        spawnManager.ReSpawnCube();
        //Player Input State Change
    }
    public void FeverTimeEnd2()
    {
        playerInput2.RestartClick();
        feverState2 = false;
        InitFeverCount2();
        //Ui fever Count Init
        SendInitFeverCount2();
        spawnManager2.ReSpawnCube();
        //Player Input State Change
    }

    public void GameOverScore()
    {
        soundMGR.GameOverSound();
        //ui매니저에 score값 전달해주기
        GameMGR._instance.uiMGR.FinalShow(score);
        Debug.Log(score);
        //점수를 띄우로 SCORE를 0으로 초기화
        score = 0;
    }
}
