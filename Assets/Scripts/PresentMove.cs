using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentMove : MonoBehaviour
{
    [SerializeField] private GameObject leftPos;
    [SerializeField] private GameObject rightPos;
    [SerializeField] private GameObject centerPos;
    [SerializeField] private GameObject targetPos = null;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private SpawnManager spawnManager = null;



    [SerializeField] private GameObject present = null;
    [SerializeField] private PresentInfo presentInfo = null;
    // [SerializeField] private Rigidbody presentRigidBody = null;

    [SerializeField] private float moveSpeed = 1;

    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        playerInput.del_PlayerClick = MoveDirection;

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
        }
        Move();
    }
    private void Move()
    {
        //상태가 선물일 경우 
        if (presentInfo.stateInfo == PresentInfo.PresentState.Red)
        {

            presentInfo.PresentMove(targetPos);
            //   StartCoroutine(Co_PresentMove());
            if (targetPos == rightPos)
            {
                Debug.Log("빨강 점수 증가");
                GameMGR._instance.AddScore();
                //UI 점수 올려주는 함수 호출 
                //함수 어쩌록 입력하기 
            }
            else if (targetPos == leftPos)
            {
                Debug.Log("빨강 얼음");
                //일시적으로 키입력 막는 로직
                playerInput.FreezeClick();
                Invoke("PauseCancle", 3f);
            }
        }
        //상태가 폭탄일 경우
        else if (presentInfo.stateInfo == PresentInfo.PresentState.Blue)
        {
            presentInfo.PresentMove(targetPos);
            //  StartCoroutine(Co_PresentMove());
            //이동 시키기 
            if (targetPos == leftPos)
            {
                Debug.Log("점수 증가");
                GameMGR._instance.AddScore();
                //UI 점수 올려주는 함수 호출 
                //함수 어쩌록 입력하기 
            }
            else if (targetPos == rightPos)
            {
                Debug.Log("얼음");
                //일시적으로 키입력 막는 로직
                playerInput.FreezeClick();
                Invoke("PauseCancle", 3f);
            }
        }
    }
    private void PauseCancle() => playerInput.UnFreezeClick();
    private void FirstPresent(GameObject firstPresent)
    {
        present = firstPresent;
        presentInfo = present.GetComponent<PresentInfo>();
        // presentRigidBody = present.GetComponent<Rigidbody>();
    }
}
