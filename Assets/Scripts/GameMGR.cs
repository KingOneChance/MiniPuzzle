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

    [SerializeField] private int score;
    [SerializeField] private UiMGR uiMGR;
    [SerializeField] private int feverTimeCount;
    [SerializeField] private SpawnManager spawnManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SingleSceneAwak()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        uiMGR = FindObjectOfType<UiMGR>();
    }

    public void AddScore()
    {
        score += 10;
        uiMGR.ShowScore(score);
        feverTimeCount++;
        if(feverTimeCount == 10) FeverTime();
    }
    public void InitFeverCount() => feverTimeCount = 0;
    public void FeverTime()
    {
        //spawnManager.FeverScore();
    }
    public void FeverTimeEnd()
    {
        feverTimeCount = 0;
        //Player Input State Change
    }
}
