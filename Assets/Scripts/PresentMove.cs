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
        //���°� ���� ��� 
        if (presentInfo.stateInfo == PresentInfo.PresentState.Red)
        {

            presentInfo.PresentMove(targetPos);
            //   StartCoroutine(Co_PresentMove());
            if (targetPos == rightPos)
            {
                GameMGR._instance.AddScore(10);
                GameMGR._instance.soundMGR.CorrectSound();
                //UI ���� �÷��ִ� �Լ� ȣ�� 
                //�Լ� ��¼�� �Է��ϱ� 
            }
            else if (targetPos == leftPos)
            {
                //�Ͻ������� Ű�Է� ���� ����
                GameMGR._instance.soundMGR.InCorrectSound();
                playerInput.FreezeClick();
                Invoke("PauseCancle", freezeTime);
                GameMGR._instance.InitFeverCount();
                GameMGR._instance.SendInitFeverCount();
            }
        }
        //���°� �Ķ� ���
        else if (presentInfo.stateInfo == PresentInfo.PresentState.Blue)
        {
            presentInfo.PresentMove(targetPos);
            //  StartCoroutine(Co_PresentMove());
            //�̵� ��Ű�� 
            if (targetPos == leftPos)
            {
                GameMGR._instance.AddScore(10);
                GameMGR._instance.soundMGR.CorrectSound();
                //UI ���� �÷��ִ� �Լ� ȣ�� 
                //�Լ� ��¼�� �Է��ϱ� 
            }
            else if (targetPos == rightPos)
            {
                //�Ͻ������� Ű�Է� ���� ����
                GameMGR._instance.soundMGR.InCorrectSound();
                playerInput.FreezeClick();
                Invoke("PauseCancle", freezeTime);
                GameMGR._instance.InitFeverCount();
                GameMGR._instance.SendInitFeverCount();
            }
        }
        //���°� ��� ���
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
