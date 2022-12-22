using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentInfo2 : MonoBehaviour
{
    public enum PresentState
    {
        Red,
        Blue,
        Gold,
    }
    [SerializeField] private PresentState state;
    //선물 오브젝트 풀
    [SerializeField] private SpawnManager2 spawnManager2;
    [SerializeField] private PlayerInput2 playerInput2;

    public PresentState stateInfo { get { return state; } }

    private void Start()
    {
        spawnManager2 = FindObjectOfType<SpawnManager2>();
        playerInput2 = FindObjectOfType<PlayerInput2>();
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
        playerInput2.RestartClick();
        spawnManager2.ChangePosition();
    }

    public void PresentMove(GameObject target)
    {
        if (target.activeInHierarchy != false)
        StartCoroutine(Co_PresentMove(target));
    }
    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Co_PresentMove(GameObject targetPos)
    {
        ActiveFalse();
        int i = 0;
        gameObject.transform.LookAt(targetPos.transform.position);
        while (i < 4)
        {
            gameObject.transform.Translate(Vector3.forward);
            i++;
            yield return time;
        }
        spawnManager2.ReCycle(gameObject);
        gameObject.SetActive(false);
        yield return time;
    }
}
