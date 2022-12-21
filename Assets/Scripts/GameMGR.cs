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

    public void SingleSceneAwak()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        spawnManager = FindObjectOfType<SpawnManager>();
        uiMGR = FindObjectOfType<UiMGR>();
    }

    public void AddScore(int num)
    {
        score += num;
        uiMGR.ShowScore(score);
        if (feverState == false) feverTimeCount++;
        if (feverTimeCount == 10)
        {
            feverState = true;
            FeverTime();
            InitFeverCount();
        }
    }
    public void InitFeverCount() => feverTimeCount = 0;
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
        spawnManager.ReSpawnCube();
        //Player Input State Change
    }
}
