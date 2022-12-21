using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //������ �������ڸ� �������ִ� ��������Ʈ
    public delegate void Del_FirstPresent(GameObject present);
    public Del_FirstPresent del_FirstPresent;

    //3���� ť��
    [SerializeField] private GameObject redCube = null;
    [SerializeField] private GameObject blueCube = null;
    [SerializeField] private GameObject goldCube = null;
    [SerializeField] private Transform[] spawnPoints = null;
    [SerializeField] private PlayerInput playerInput = null;
    [SerializeField] private float moveSpeed = 1;

    //�� ���� ����
    public int ItemCount = 20;

    //������ Ǯ�� ���� ť
    Queue<GameObject> mainQueue = new Queue<GameObject>();

    Queue<GameObject> redQueue = new Queue<GameObject>();
    Queue<GameObject> blueQueue = new Queue<GameObject>();
    Queue<GameObject> goldQueue = new Queue<GameObject>();


    //Ȱ��ȭ�Ǿ��ִ� ť���
    private List<GameObject> activeCubes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ItemCount; i++)
        {
            //3���� ť�� ������
            GameObject red = Instantiate(redCube, Vector3.zero, Quaternion.identity);
            GameObject blue = Instantiate(blueCube, Vector3.zero, Quaternion.identity);
            GameObject gold = Instantiate(goldCube, Vector3.zero, Quaternion.identity);

            //red,blue gold�� ���� ����
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
    //ť�� �ʱ� ����
    void SpawnCube()
    {
        for (int i = 0; i < spawnPoints.Length-1; i++)
        {
            int rand = Random.Range(0, 2);

            if(rand == 0)
            {
                //ť���� ������
                GameObject redcube = redQueue.Dequeue();
                //Ȱ��ȭ��Ű��
                redCube.SetActive(true);
                //Ȱ��ȭ�� ť����� ����Ʈ�� �־��ش�
                activeCubes.Add(redCube);
            }
            else
            {
                //ť���� ������
                GameObject bluecube = blueQueue.Dequeue();
                //Ȱ��ȭ��Ű��
                blueCube.SetActive(true);
                //Ȱ��ȭ�� ť����� ����Ʈ�� �־��ش�
                activeCubes.Add(bluecube);
            }
            // �������� ��ġ�� ����ش�
            activeCubes[i].transform.position = spawnPoints[i].transform.position;

            //�Ǿ� ���� ������Ʈ ����
            if (i == 0)
                del_FirstPresent(activeCubes[0]);
        }
    }

    //�ڸ��� ��ĭ�� �ű��
    public void ChangePosition()
    {
        List<GameObject> newCubes = new List<GameObject>();

        for (int i = 1; i < spawnPoints.Length-1; i++)
        {
            // cubes[i].transform.Translate(Vector3.back);
            StartCoroutine(Co_PreBoxMove(activeCubes[i],i));
            newCubes.Add(activeCubes[i]);
        }
        activeCubes.Clear();
        //�Ǿ� ���� ������Ʈ ����
        del_FirstPresent(newCubes[0]);
        activeCubes = newCubes;
        //���� �����ϴ°��� �����̾����
        int rand = Random.Range(0, 2);
        if( rand == 0)
        {
            //Cube ť���� ����
            GameObject redCube = redQueue.Dequeue();
            //Ȱ��ȭ
            redCube.SetActive(true);
            //������ �� ȸ���� ����
            redCube.transform.position = spawnPoints[6].transform.position;
            redCube.transform.rotation = Quaternion.identity;
            activeCubes.Add(redCube);
            StartCoroutine(Co_PreBoxMove(redCube, -1));
        }
        else
        {
            //Cube ť���� ����
            GameObject blueCube = blueQueue.Dequeue();
            //Ȱ��ȭ
            blueCube.SetActive(true);
            //������ �� ȸ���� ����
            blueCube.transform.position = spawnPoints[6].transform.position;
            blueCube.transform.rotation = Quaternion.identity;
            activeCubes.Add(blueCube);
            StartCoroutine(Co_PreBoxMove(blueCube, -1));
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