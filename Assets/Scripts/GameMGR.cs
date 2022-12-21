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

    private void Awake()
    {
        uiMGR = FindObjectOfType<UiMGR>();
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore()
    {
        score += 10;
        uiMGR.ShowScore(score);
    }
}
