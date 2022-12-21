using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //움직일 선물상자를 전달해주는 델리게이트
    public delegate void Del_FirstPresent(GameObject present);
    public Del_FirstPresent del_FirstPresent;

    //3가지 큐브
    [SerializeField] private GameObject redCubePrefab = null;
    [SerializeField] private GameObject blueCubePrefab = null;
    [SerializeField] private GameObject goldCubePrefab = null;
     
    [SerializeField] private Transform[] spawnPoints = null;
    [SerializeField] private float moveSpeed = 1;

    //각 상자 개수
    public int ItemCount = 20;

    //아이템 풀을 담을 큐
    Queue<GameObject> mainQueue = new Queue<GameObject>();

    Queue<GameObject> redQueue = new Queue<GameObject>();
    Queue<GameObject> blueQueue = new Queue<GameObject>();
    Queue<GameObject> goldQueue = new Queue<GameObject>();


    //활성화되어있는 큐브들
    private List<GameObject> activeCubes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ItemCount; i++)
        {
            //3가지 큐브 생성됨
            GameObject red = Instantiate(redCubePrefab, Vector3.zero, Quaternion.identity);
            GameObject blue = Instantiate(blueCubePrefab, Vector3.zero, Quaternion.identity);
            GameObject gold = Instantiate(goldCubePrefab, Vector3.zero, Quaternion.identity);

            //red,blue gold는 따로 관리
            redQueue.Enqueue(red);
            blueQueue.Enqueue(blue);
            goldQueue.Enqueue(gold);

            red.SetActive(false);
            blue.SetActive(false);
            gold.SetActive(false);
        }
        SpawnCube();
    }
    //큐브 초기 생성
    void SpawnCube()
    {
        activeCubes.Clear();
        for (int i = 0; i < spawnPoints.Length-1; i++)
        {
            int rand = Random.Range(0, 2);

            if(rand == 0)
            {
                //큐에서 꺼내고
                GameObject redCube = redQueue.Dequeue();
                //활성화시키고
                redCube.SetActive(true);
                //활성화된 큐브들을 리스트에 넣어준다
                activeCubes.Add(redCube);
            }
            else
            {
                //큐에서 꺼내고
                GameObject blueCube = blueQueue.Dequeue();
                //활성화시키고
                blueCube.SetActive(true);
                //활성화된 큐브들을 리스트에 넣어준다
                activeCubes.Add(blueCube);
            }
            // 아이템의 위치를 잡아준다
            activeCubes[i].transform.position = spawnPoints[i].transform.position;
            Debug.Log(activeCubes[i].transform.position);
            //맨앞 상자 오브젝트 전달
            if (i == 0)
                del_FirstPresent(activeCubes[0]);
        }
    }

    //자리를 한칸씩 옮긴다
    public void ChangePosition()
    {
        List<GameObject> newCubes = new List<GameObject>();

        for (int i = 1; i < spawnPoints.Length-1; i++)
        {
            // cubes[i].transform.Translate(Vector3.back);
            StartCoroutine(Co_PreBoxMove(activeCubes[i]));
            newCubes.Add(activeCubes[i]);
        }
        
        //맨앞 상자 오브젝트 전달
        del_FirstPresent(newCubes[0]);

        //새로 생성하는것은 랜덤이어야함
        int rand = Random.Range(0, 2);
        if( rand == 0)
        {
            //Cube 큐에서 빼고
            GameObject redCube = redQueue.Dequeue();
            //활성화
            redCube.SetActive(true);
            //포지션 및 회전값 설정
            redCube.transform.position = spawnPoints[6].transform.position;
            redCube.transform.rotation = Quaternion.identity;
            activeCubes = newCubes;
            activeCubes.Add(redCube);
        }
        else
        {
            //Cube 큐에서 빼고
            GameObject blueCube = blueQueue.Dequeue();
            //활성화
            blueCube.SetActive(true);
            //포지션 및 회전값 설정
            blueCube.transform.position = spawnPoints[6].transform.position;
            blueCube.transform.rotation = Quaternion.identity;
            activeCubes = newCubes;
            activeCubes.Add(blueCube);
        }
       
    }

    public void ReCycle(GameObject present)
    {
        if(present.tag == "Red")
        redQueue.Enqueue(present);
        else if(present.tag == "Blue")
            blueQueue.Enqueue(present);
        else if(present.tag == "Gold")
        {
            goldQueue.Enqueue(present);
        }
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