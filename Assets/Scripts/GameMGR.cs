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
    [SerializeField] private int score;
    [SerializeField] private int feverTimeCount;
    [SerializeField] private UiMGR uiMGR;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] public PlayerInput playerInput;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SingleSceneAwake()
    {
 
        playerInput = FindObjectOfType<PlayerInput>();
        spawnManager = FindObjectOfType<SpawnManager>();
        uiMGR = FindObjectOfType<UiMGR>();

    }
    public void SingleSceneEnd()
    {

        playerInput = FindObjectOfType<PlayerInput>();
        spawnManager = FindObjectOfType<SpawnManager>();
        uiMGR = FindObjectOfType<UiMGR>();

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

        }
        if (feverTimeCount == 10)
        {
            feverState = true;
            FeverTime();
            InitFeverCount();
            //Ui fever Count Init
            SendInitFeverCount();

        }
    }
    public void InitFeverCount() => feverTimeCount = 0;
    public void SendFeverCount() => uiMGR.GetFeverCount();
    public void SendInitFeverCount() => uiMGR.GetInitFeverCount();


    public void FeverTime()
    {
        playerInput.RestartClick();
        spawnManager.FeverScore();
        Invoke("FeverTimeEnd",5);
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

    public void GameOverScore()
    {
        //ui매니저에 score값 전달해주기
        GameMGR._instance.uiMGR.FinalShow(score);
        Debug.Log(score);
        //점수를 띄우로 SCORE를 0으로 초기화
        score = 0;
    }
}
