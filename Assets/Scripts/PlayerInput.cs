using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void Del_PlayerClick(int dir);
    public Del_PlayerClick del_PlayerClick;
    [SerializeField] private bool canClick;
    [SerializeField] private bool freeze;

    public bool arriveFirstPos;
    private int dir;
    public void Awake()
    {
        GameMGR._instance.SingleSceneAwak();
    }

    public void Start()
    {
        canClick = true;
        arriveFirstPos = true;
    }
    void Update()
    {
        if (canClick==false||arriveFirstPos==false||freeze==true) return;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dir = -1;
            del_PlayerClick(dir);
            canClick = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            dir = 1;
            del_PlayerClick(dir);
            canClick = false;
        }
    }
    
    public void FreezeClick()
    {
        freeze = true;
    }
    public void RestartClick()
    {
        Debug.Log("«ÿ¡¶");
        canClick = true;
    }
    public void UnFreezeClick()
    {
        Debug.Log("æÛ¿Ω∂Ø");
        freeze = false;
    }
}
