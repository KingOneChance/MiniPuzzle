using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentInfo : MonoBehaviour
{
    public enum PresentState
    {
        Red,
        Blue,
        Gold,
    }
    [SerializeField] private PresentState state;
    //선물 오브젝트 풀
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private PlayerInput playerInput;

    public PresentState stateInfo { get { return state; } }

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        playerInput = FindObjectOfType<PlayerInput>();
        if (gameObject.tag == "Red") state = PresentState.Red;
        else if (gameObject.tag == "Blue") state = PresentState.Blue;
        else if (gameObject.tag == "Gold") state = PresentState.Gold;
        else Debug.Log("This Object don't have any tag.");
    }
    private void OnEnable()
    {
        gameObject.transform.rotation = Quaternion.identity;
    }
    public void ActiveFalse()
    {
        playerInput.RestartClick();
        spawnManager.ChangePosition();
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
        spawnManager.ReCycle(gameObject);
        gameObject.gameObject.SetActive(false);
        ActiveFalse();
        yield return time;
    }
}
