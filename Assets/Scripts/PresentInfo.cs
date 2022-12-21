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
    [SerializeField] private PlayerInput playerInput;

    public PresentState stateInfo { get { return state; } }

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
        playerInput = FindObjectOfType<PlayerInput>();
        if (gameObject.tag == "Present") state = PresentState.Present;
        else if (gameObject.tag == "Bomb") state = PresentState.Bomb;
        else Debug.Log("This Object don't have any tag.");
    }

    public void ActiveFalse()
    {
        playerInput.canClick = true;
        gameLogic.ChangePosition();
    }

    public void PresentMove(GameObject target)
    {
        StartCoroutine(Co_PresentMove(target));
    }
    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Co_PresentMove(GameObject targetPos)
    {
        int i = 0;
        //  presentRigidBody.transform.LookAt(targetPos.transform.position);
        gameObject.transform.LookAt(targetPos.transform.position);
        while (i < 4)
        {
            gameObject.transform.Translate(Vector3.forward);
            i++;
            yield return time;
        }
        gameLogic.ReCycle(gameObject);
        gameObject.gameObject.SetActive(false);
        ActiveFalse();
        yield return time;
    }
}
