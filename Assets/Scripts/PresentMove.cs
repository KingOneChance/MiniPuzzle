using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentMove : MonoBehaviour
{
    [SerializeField] private GameObject leftPos;
    [SerializeField] private GameObject rightPos;
    [SerializeField] private GameObject centerPos;
    [SerializeField] private GameObject targetPos = null;
    [SerializeField] private PlayerInput PlayerInput;
    [SerializeField] private GameLogic gameLogic = null;

    [SerializeField] private GameObject present = null;
    [SerializeField] private PresentInfo presentInfo = null;
    [SerializeField] private Rigidbody presentRigidBody = null;

    [SerializeField] private float moveSpeed = 1;

    private void Awake()
    {
        PlayerInput = FindObjectOfType<PlayerInput>();
        PlayerInput.del_PlayerClick = MoveDirection;
        gameLogic = FindObjectOfType<GameLogic>();
        gameLogic.del_FirstPresent = FirstPresent;
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
        if (presentInfo.stateInfo == PresentInfo.PresentState.Present)
        {
            Debug.Log("?");
            gameObject.transform.LookAt(targetPos.transform);
            StartCoroutine(Co_PresentMove());
            if (targetPos == rightPos)
            {
                //UI ���� �÷��ִ� �Լ� ȣ�� 
                //�Լ� ��¼�� �Է��ϱ� 
            }
        }
        //���°� ��ź�� ���
        else if (presentInfo.stateInfo == PresentInfo.PresentState.Bomb)
        {
            Debug.Log("?");
            gameObject.transform.LookAt(targetPos.transform);
            StartCoroutine(Co_PresentMove());
            //�̵� ��Ű�� 
            if (targetPos == rightPos)
            {
                //�Ͻ������� Ű�Է� ���� ����
                PlayerInput.canClick = false;
                Invoke("PauseCancle", 1f);
            }
        }
    }
    private void PauseCancle() => PlayerInput.canClick = true;
    private void FirstPresent(GameObject firstPresent)
    {
        present = firstPresent;
        presentInfo = present.GetComponent<PresentInfo>();
        presentRigidBody = present.GetComponent<Rigidbody>();
    }
    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Co_PresentMove()
    {
        int i = 0;
        presentRigidBody.transform.LookAt(targetPos.transform.position);
        while (i < 10)
        {
            presentRigidBody.transform.Translate(Vector3.forward);
            i++;
            yield return time;
        }
        gameLogic.ReCycle(present);
        present.gameObject.SetActive(false);
        presentInfo.ActiveFalse();
        yield return time;
    }
}
