using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentMove2 : MonoBehaviour
{
    [SerializeField] private GameObject leftPos;
    [SerializeField] private GameObject rightPos;
    [SerializeField] private GameObject goldPos;
    [SerializeField] private GameObject centerPos;
    [SerializeField] private GameObject targetPos = null;
    [SerializeField] private PlayerInput2 playerInput2;
    [SerializeField] private SpawnManager2 spawnManager2 = null;

    [SerializeField] private GameObject present = null;
    [SerializeField] private PresentInfo2 presentInfo2 = null;
    // [SerializeField] private Rigidbody presentRigidBody = null;

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float freezeTime = 2;

    private void Awake()
    {
        playerInput2 = FindObjectOfType<PlayerInput2>();
        playerInput2.del_PlayerClick = MoveDirection;

        spawnManager2 = FindObjectOfType<SpawnManager2>();
        spawnManager2.del_FirstPresent = FirstPresent;
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
        if (presentInfo2.stateInfo == PresentInfo2.PresentState.Red)
        {

            presentInfo2.PresentMove(targetPos);
            //   StartCoroutine(Co_PresentMove());
            if (targetPos == rightPos)
            {
                GameMGR._instance.AddScore2(10);
                GameMGR._instance.soundMGR.CorrectSound();
                //UI ���� �÷��ִ� �Լ� ȣ�� 
                //�Լ� ��¼�� �Է��ϱ� 
            }
            else if (targetPos == leftPos)
            {
                //�Ͻ������� Ű�Է� ���� ����
                playerInput2.FreezeClick();
                Invoke("PauseCancle", freezeTime);
                GameMGR._instance.InitFeverCount2();
                GameMGR._instance.SendInitFeverCount2();
                GameMGR._instance.soundMGR.InCorrectSound();
            }
        }
        //���°� �Ķ� ���
        else if (presentInfo2.stateInfo == PresentInfo2.PresentState.Blue)
        {
            presentInfo2.PresentMove(targetPos);
            //  StartCoroutine(Co_PresentMove());
            //�̵� ��Ű�� 
            if (targetPos == leftPos)
            {
                GameMGR._instance.AddScore2(10);
                GameMGR._instance.soundMGR.CorrectSound();
                //UI ���� �÷��ִ� �Լ� ȣ�� 
                //�Լ� ��¼�� �Է��ϱ� 
            }
            else if (targetPos == rightPos)
            {
                //�Ͻ������� Ű�Է� ���� ����
                playerInput2.FreezeClick();
                Invoke("PauseCancle", freezeTime);
                GameMGR._instance.InitFeverCount2();
                GameMGR._instance.SendInitFeverCount2();
                GameMGR._instance.soundMGR.InCorrectSound();
            }
        }
        //���°� ��� ���
        else if (presentInfo2.stateInfo == PresentInfo2.PresentState.Gold)
        {
            presentInfo2.PresentMove(targetPos);
            GameMGR._instance.AddScore2(20);
            GameMGR._instance.soundMGR.CorrectSound();
        }
    }
    private void PauseCancle() => playerInput2.UnFreezeClick();
    private void FirstPresent(GameObject firstPresent)
    {
        present = firstPresent;
        presentInfo2 = present.GetComponent<PresentInfo2>();
    }
}
