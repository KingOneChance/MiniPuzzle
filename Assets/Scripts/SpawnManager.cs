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
    [SerializeField] private PlayerInput playerInput = null;
    [SerializeField] private bool Fever = false;
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
        playerInput = FindObjectOfType<PlayerInput>();
        SpawnCube();
    }
    public void ReSpawnCube()
    {
        Invoke("SpawnCube", 0.5f);
    }

    //큐브 초기 생성
    public void SpawnCube()
    {
        Fever = false;
        if (activeCubes.Count > 0)
        {
            for (int i = 0; i < spawnPoints.Length - 1; i++)
            {
                //엑티브큐브 리스틀르 비활성화시키고
                activeCubes[i].SetActive(false);
                Debug.Log(i);
            }
            //리스트를 비우고
            activeCubes.Clear();
        }


        for (int i = 0; i < spawnPoints.Length - 1; i++)
        {
            int rand = Random.Range(0, 2);

            if (rand == 0)
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

            //맨앞 상자 오브젝트 전달
            if (i == 0)
                del_FirstPresent(activeCubes[0]);
        }
    }
    //골드상자 위치 초기화
    public void FeverScore()
    {
        StartCoroutine(Co_FeverStart());
    }
    IEnumerator Co_FeverStart()
    {
        yield return new WaitForSeconds(0.5f);
        Fever = true;

        if (activeCubes.Count > 0)
        {
            GameMGR._instance.playerInput.RestartClick();
            for (int i = 0; i < spawnPoints.Length - 1; i++)
            {
                //엑티브큐브 리스틀르 비활성화시키고
                activeCubes[i].SetActive(false);
            }
            //리스트를 비우고
            activeCubes.Clear();
        }
        for (int i = 0; i < spawnPoints.Length - 1; i++)
        {
            //큐에서 꺼내고
            GameObject goldCube = goldQueue.Dequeue();
            //활성화시키고
            goldCube.SetActive(true);
            //활성화된 큐브들을 리스트에 넣어준다
            activeCubes.Add(goldCube);
            // 아이템의 위치를 잡아준다
            activeCubes[i].transform.position = spawnPoints[i].transform.position;
            //맨앞 상자 오브젝트 전달
            if (i == 0)
                del_FirstPresent(activeCubes[0]);
        }
        yield return null;
    }

    //자리를 한칸씩 옮긴다
    public void ChangePosition()
    {
        List<GameObject> newCubes = new List<GameObject>();

        for (int i = 1; i < spawnPoints.Length - 1; i++)
        {
            // cubes[i].transform.Translate(Vector3.back);
            StartCoroutine(Co_PreBoxMove(activeCubes[i], i));
            newCubes.Add(activeCubes[i]);
        }
        activeCubes.Clear();
        //맨앞 상자 오브젝트 전달
        del_FirstPresent(newCubes[0]);
        activeCubes = newCubes;
        //새로 생성하는것은 랜덤이어야함
        if (Fever == false)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                //Cube 큐에서 빼고
                GameObject redCube = redQueue.Dequeue();
                //활성화
                redCube.SetActive(true);
                //포지션 및 회전값 설정
                redCube.transform.position = spawnPoints[6].transform.position;
                redCube.transform.rotation = Quaternion.identity;
                activeCubes.Add(redCube);
                StartCoroutine(Co_PreBoxMove(redCube, -1));
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
                activeCubes.Add(blueCube);
                StartCoroutine(Co_PreBoxMove(blueCube, -1));
            }
        }
        else
        {
            //Cube 큐에서 빼고
            GameObject goldCube = goldQueue.Dequeue();
            //활성화
            goldCube.SetActive(true);
            //포지션 및 회전값 설정
            goldCube.transform.position = spawnPoints[6].transform.position;
            goldCube.transform.rotation = Quaternion.identity;
            activeCubes.Add(goldCube);
            StartCoroutine(Co_PreBoxMove(goldCube, -1));
        }
    }

    public void ReCycle(GameObject present)
    {
        if (present.tag == "Red")
            redQueue.Enqueue(present);
        else if (present.tag == "Blue")
            blueQueue.Enqueue(present);
        else if (present.tag == "Gold")
            goldQueue.Enqueue(present);
        else
            Debug.Log("It is empty tag");
        Debug.Log("재활용");

    }

    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Co_PreBoxMove(GameObject cube, int num)
    {
        if (num == 1)
            playerInput.arriveFirstPos = false;
        int i = 0;
        while (i < 4)
        {
            cube.transform.Translate(Vector3.forward * -0.5f * moveSpeed);
            i++;
            yield return time;
        }
        if (num == 1)
            playerInput.arriveFirstPos = true;
        yield return time;
    }
}