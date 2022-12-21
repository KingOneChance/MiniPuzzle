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

    public PresentState stateInfo { get { return state; } }

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        if (gameObject.tag == "Red") state = PresentState.Red;
        else if (gameObject.tag == "Blue") state = PresentState.Blue;
        else if (gameObject.tag == "Gold") state = PresentState.Gold;
        else Debug.Log("This Object don't have any tag.");
    }

    public void ActiveFalse()
    {
        spawnManager.ChangePosition();
    }
}
