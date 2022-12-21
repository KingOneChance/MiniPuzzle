using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    //움직일 선물상자를 전달해주는 델리게이트
    public delegate void Del_FirstPresent(GameObject present);
    public Del_FirstPresent del_FirstPresent;

    [SerializeField] private GameObject cubePrefabs = null;
    [SerializeField] private Transform[] spawnPoints = null;
    [SerializeField] private float moveSpeed = 1;

    public int ItemCount = 20;

    //아이템 풀을 담을 큐
    Queue<GameObject> itemQueue = new Queue<GameObject>();

    //활성화되어있는 큐브들
    private List<GameObject> cubes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ItemCount; i++)
        {
            //생성됨
            GameObject cube = Instantiate(cubePrefabs, Vector3.zero, Quaternion.identity);
            itemQueue.Enqueue(cube);
            cube.SetActive(false);
        }
        SpawnCube();
    }

    void SpawnCube()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            //큐에서 꺼내고
            GameObject cube = itemQueue.Dequeue();
            //활성화시키고
            cube.SetActive(true);
            cubes.Add(cube);
            // 아이템의 위치를 잡아준다
            cube.transform.position = spawnPoints[i].transform.position;

            //맨앞 상자 오브젝트 전달
            if (i == 0)
                del_FirstPresent(cube);
        }
    }

    //자리를 한칸씩 옮긴다
    public void ChangePosition()
    {
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            // cubes[i].transform.Translate(Vector3.back);
            StartCoroutine(Co_PreBoxMove(cubes[i]));
        }
        //맨앞 상자 오브젝트 전달
        del_FirstPresent(cubes[0]);
        
        GameObject cube = itemQueue.Dequeue();
        cube.SetActive(true);
        cubes.Add(cube);
    }

    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Co_PreBoxMove(GameObject cube)
    {
        int i = 0;
        while (i < 20)
        {
            cube.transform.Translate(Vector3.forward * -0.1f * moveSpeed);
            i++;
            yield return time;
        }
        yield return time;
    }
}