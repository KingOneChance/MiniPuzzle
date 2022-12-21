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
    //선물 오브젝트 풀
    [SerializeField] private GameLogic gameLogic;

    public PresentState stateInfo { get { return state; } }

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
        if (gameObject.tag == "Present") state = PresentState.Present;
        else if (gameObject.tag == "Bomb") state = PresentState.Bomb;
        else Debug.Log("This Object don't have any tag.");
    }

    public void ActiveFalse()
    {
        gameLogic.ChangePosition();
    }
}
