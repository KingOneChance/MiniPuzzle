using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentInfo : MonoBehaviour
{

    public enum PresentState
    {
        Bomb,
        Present,
    }
    [SerializeField] private PresentState state;
    //���� ������Ʈ Ǯ
    [SerializeField] private SpawnManager gameLogic;

    public PresentState stateInfo { get { return state; } }

    private void Start()
    {
        gameLogic = FindObjectOfType<SpawnManager>();
        if (gameObject.tag == "Present") state = PresentState.Present;
        else if (gameObject.tag == "Bomb") state = PresentState.Bomb;
        else Debug.Log("This Object don't have any tag.");
    }

    public void ActiveFalse()
    {
        gameLogic.ChangePosition();
    }
}
