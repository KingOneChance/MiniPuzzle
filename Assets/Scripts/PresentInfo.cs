using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentInfo : MonoBehaviour
{
    private enum PresentState
    {
        Bomb,
        Present,
    }
    [SerializeField] private PresentState state;
    [SerializeField] private bool canMove;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject leftPos;
    [SerializeField] private GameObject rightPos;
    [SerializeField] private GameObject targetPos;
    private void Start()
    {
        if (gameObject.tag == "Present") state = PresentState.Present;
        else if (gameObject.tag == "Bomb") state = PresentState.Bomb;
        else Debug.Log("This Object don't have any tag.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint")
        {
            canMove = true;
        }
    }
    //CheckPoint�� PlayerInput Script���� ȣ�� 
    public void PresentDir(string dir)
    {
        if (dir == "Right") targetPos = leftPos;
        else if (dir == "Left") targetPos = rightPos;
        else Debug.Log("It is not selected value ");
        PresentMove();
    }
    public void PresentMove()
    {
        if (canMove == true)
        {
            //���°� ������ ��� 
            if (state == PresentState.Present)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos.transform.position, 0.01f * moveSpeed);
                if (targetPos == rightPos)
                {
                    //UI ���� �÷��ִ� �Լ� ȣ�� 
                    //�Լ� ��¼�� �Է��ϱ� 
                }
            }
            //���°� ��ź�� ���
            else if (state == PresentState.Bomb)
            {
                //�̵� ��Ű�� 
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos.transform.position, 0.01f * moveSpeed);
                if (targetPos == rightPos)
                {
                    //�Ͻ������� Ű�Է� ���� ���� �ۼ�
                }
            }
        }
    }
}
