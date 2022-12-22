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
    //���� ������Ʈ Ǯ
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
        spawnManager.ReCycle(gameObject);
        gameObject.SetActive(false);
        yield return time;
    }
}
