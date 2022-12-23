using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public Action<int> del_Action;
    
    [SerializeField] private bool canClick;
    [SerializeField] private bool freeze;

    public bool arriveFirstPos;
    private int dir;

    public void Awake()
    {
        GameMGR._instance.SingleSceneAwake();
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
        if (Input.GetKeyDown(KeyCode.Z) && GameMGR._instance.feverState == false)
        {
            dir = -1;
            del_Action(dir);
        }
        if (Input.GetKeyDown(KeyCode.C) && GameMGR._instance.feverState == false)
        {
            dir = 1;
            del_Action(dir);
        }
        if (Input.GetKeyDown(KeyCode.X) && GameMGR._instance.feverState == true)
        {
            dir = 0;
            del_Action(dir);
        }
    }

    public void FreezeClick()
    {
        freeze = true;
    }
    public void UnFreezeClick()
    {
        freeze = false;
    }
    public void RestartClick()
    {
        canClick = true;
    }
}
