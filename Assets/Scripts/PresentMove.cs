using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentMove : MonoBehaviour
{
    [SerializeField] private GameObject leftPos;
    [SerializeField] private GameObject rightPos;
    [SerializeField] private GameObject goldPos;
    [SerializeField] private GameObject centerPos;
    [SerializeField] private GameObject targetPos = null;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private SpawnManager spawnManager = null;



    [SerializeField] private GameObject present = null;
    [SerializeField] private PresentInfo presentInfo = null;
    // [SerializeField] private Rigidbody presentRigidBody = null;

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float freezeTime = 2;

    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        playerInput.del_Action = MoveDirection;

        spawnManager = FindObjectOfType<SpawnManager>();
        spawnManager.del_FirstPresent = FirstPresent;
        targetPos = centerPos;
    }
    private void MoveDirection(int dir)
    {
        switch (dir)
        {
            case 1:
                targetPos = rightPos;
                break;
            case -1:
                targetPos = leftPos;
                break;
            case 0:
                targetPos = goldPos;
                break;
        }
        Move();
    }
    private void Move()
    {
        //상태가 빨강 경우 
        if (presentInfo.stateInfo == PresentInfo.PresentState.Red)
        {

            presentInfo.PresentMove(targetPos);
            //   StartCoroutine(Co_PresentMove());
            if (targetPos == rightPos)
            {
                GameMGR._instance.AddScore(10);
                GameMGR._instance.soundMGR.CorrectSound();
                //UI 점수 올려주는 함수 호출 
                //함수 어쩌록 입력하기 
            }
            else if (targetPos == leftPos)
            {
                //일시적으로 키입력 막는 로직
                GameMGR._instance.soundMGR.InCorrectSound();
                playerInput.FreezeClick();
                Invoke("PauseCancle", freezeTime);
                GameMGR._instance.InitFeverCount();
                GameMGR._instance.SendInitFeverCount();
            }
        }
        //상태가 파랑 경우
        else if (presentInfo.stateInfo == PresentInfo.PresentState.Blue)
        {
            presentInfo.PresentMove(targetPos);
            //  StartCoroutine(Co_PresentMove());
            //이동 시키기 
            if (targetPos == leftPos)
            {
                GameMGR._instance.AddScore(10);
                GameMGR._instance.soundMGR.CorrectSound();
                //UI 점수 올려주는 함수 호출 
                //함수 어쩌록 입력하기 
            }
            else if (targetPos == rightPos)
            {
                //일시적으로 키입력 막는 로직
                GameMGR._instance.soundMGR.InCorrectSound();
                playerInput.FreezeClick();
                Invoke("PauseCancle", freezeTime);
                GameMGR._instance.InitFeverCount();
                GameMGR._instance.SendInitFeverCount();
            }
        }
        //상태가 골드 경우
        else if (presentInfo.stateInfo == PresentInfo.PresentState.Gold)
        {
            presentInfo.PresentMove(targetPos);
            GameMGR._instance.AddScore(20);
            GameMGR._instance.soundMGR.CorrectSound();
        }
    }
    private void PauseCancle() => playerInput.UnFreezeClick();
    private void FirstPresent(GameObject firstPresent)
    {
        present = firstPresent;
        presentInfo = present.GetComponent<PresentInfo>();
    }
}
