using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefabs = null;
    [SerializeField] private Transform[] spawnPoints = null;

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
        }
    }

    //자리를 한칸씩 옮긴다
    void ChangePosition()
    {
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            cubes[i].transform.Translate(Vector3.back, Space.Self);
        }
        GameObject cube = itemQueue.Dequeue();
        cube.SetActive(true);
        cubes.Add(cube);

    }

    // Update is called once per frame
    void Update()
    {

    }

}