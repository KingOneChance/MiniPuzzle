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
            Debug.Log("?");
            presentInfo.PresentMove(targetPos);
            //   StartCoroutine(Co_PresentMove());
            if (targetPos == rightPos)
            {
                //UI ���� �÷��ִ� �Լ� ȣ�� 
                //�Լ� ��¼�� �Է��ϱ� 
            }
        }
        //���°� ��ź�� ���
        else if (presentInfo.stateInfo == PresentInfo.PresentState.Blue)
        {
            Debug.Log("?");
            presentInfo.PresentMove(targetPos);
            //  StartCoroutine(Co_PresentMove());
            //�̵� ��Ű�� 
            if (targetPos == rightPos)
            {
                //�Ͻ������� Ű�Է� ���� ����
                playerInput.canClick = false;
                Invoke("PauseCancle", 1f);
            }
        }
    }
    private void PauseCancle() => playerInput.canClick = true;
    private void FirstPresent(GameObject firstPresent)
    {
        present = firstPresent;
        presentInfo = present.GetComponent<PresentInfo>();
       // presentRigidBody = present.GetComponent<Rigidbody>();
    }
}
