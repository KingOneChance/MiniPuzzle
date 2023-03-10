using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput2 : MonoBehaviour
{
    public delegate void Del_PlayerClick(int dir);
    public Del_PlayerClick del_PlayerClick;
    [SerializeField] private bool canClick;
    [SerializeField] private bool freeze;
    public bool arriveFirstPos;
    private int dir;
    public void Awake()
    {
        GameMGR._instance.MultiSceneAwake();
    }

    public void Start()
    {
        canClick = true;
        arriveFirstPos = true;
    }
    void Update()
    {
        if (GameMGR._instance.isGameOver == true) return;
        if (canClick == false || arriveFirstPos == false || freeze == true) return;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && GameMGR._instance.feverState2 == false)
        {
            dir = -1;
            del_PlayerClick(dir);
         //   canClick = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && GameMGR._instance.feverState2 == false)
        {
            dir = 1;
            del_PlayerClick(dir);
        //    canClick = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && GameMGR._instance.feverState2 == true)
        {
            dir = 0;
            del_PlayerClick(dir);
        //    canClick = false;
        }
    }

    public void FreezeClick()
    {
        freeze = true;
    }
    public void RestartClick()
    {
        canClick = true;
    }
    public void UnFreezeClick()
    {
        freeze = false;
    }
}
