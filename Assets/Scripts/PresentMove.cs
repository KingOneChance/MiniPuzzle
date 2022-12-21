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
        //���°� ������ ��� 
        if (presentInfo.stateInfo == PresentInfo.PresentState.Red)
        {

            presentInfo.PresentMove(targetPos);
            //   StartCoroutine(Co_PresentMove());
            if (targetPos == rightPos)
            {
                Debug.Log("���� ���� ����");
                GameMGR._instance.AddScore();
                //UI ���� �÷��ִ� �Լ� ȣ�� 
                //�Լ� ��¼�� �Է��ϱ� 
            }
            else if (targetPos == leftPos)
            {
                Debug.Log("���� ����");
                //�Ͻ������� Ű�Է� ���� ����
                playerInput.FreezeClick();
                Invoke("PauseCancle", 3f);
            }
        }
        //���°� ��ź�� ���
        else if (presentInfo.stateInfo == PresentInfo.PresentState.Blue)
        {
            presentInfo.PresentMove(targetPos);
            //  StartCoroutine(Co_PresentMove());
            //�̵� ��Ű�� 
            if (targetPos == leftPos)
            {
                Debug.Log("���� ����");
                GameMGR._instance.AddScore();
                //UI ���� �÷��ִ� �Լ� ȣ�� 
                //�Լ� ��¼�� �Է��ϱ� 
            }
            else if (targetPos == rightPos)
            {
                Debug.Log("����");
                //�Ͻ������� Ű�Է� ���� ����
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
