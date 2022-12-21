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
    Queue<GameObject> itemQueue1 = new Queue<GameObject>();
    Queue<GameObject> itemQueue2 = new Queue<GameObject>();


    //활성화되어있는 큐브들
    private List<GameObject> cubes = new List<GameObject>();

    private List<GameObject> activeCubes = new List<GameObject>();
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
    //큐브 초기 생성
    void SpawnCube()
    {
        for (int i = 0; i < spawnPoints.Length-1; i++)
        {
            //큐에서 꺼내고
            GameObject cube = itemQueue.Dequeue();
            //활성화시키고
            cube.SetActive(true);
            //활성화된 큐브들을 리스트에 넣어준다
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
        List<GameObject> newCubes = new List<GameObject>();

        for (int i = 1; i < spawnPoints.Length-1; i++)
        {
            // cubes[i].transform.Translate(Vector3.back);
            StartCoroutine(Co_PreBoxMove(cubes[i]));
            newCubes.Add(cubes[i]);
        }
        
        //맨앞 상자 오브젝트 전달
        del_FirstPresent(newCubes[0]);

        GameObject cube = itemQueue.Dequeue();
        cube.SetActive(true);
        cube.transform.position = spawnPoints[6].transform.position;
        cube.transform.rotation = Quaternion.identity;  
        cubes = newCubes;
        cubes.Add(cube);
    }

    public void ReCycle(GameObject present)
    {
        itemQueue.Enqueue(present);
    }

    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Co_PreBoxMove(GameObject cube)
    {
        int i = 0;
        while (i < 8)
        {
            cube.transform.Translate(Vector3.forward * -0.25f * moveSpeed);
            i++;
            yield return time;
        }
        yield return time;
    }
}