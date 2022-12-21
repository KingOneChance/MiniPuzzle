using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void Del_PlayerClick(int dir);
    public Del_PlayerClick del_PlayerClick;
    public bool canClick;
    private int dir;
    public void Start()
    {
        canClick = true;
    }
    void Update()
    {
        if (canClick==false) return;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dir = -1;
            del_PlayerClick(dir);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            dir = 1;
            del_PlayerClick(dir);
        }
    }
}
