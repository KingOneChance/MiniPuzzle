using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    //������ �������ڸ� �������ִ� ��������Ʈ
    public delegate void Del_FirstPresent(GameObject present);
    public Del_FirstPresent del_FirstPresent;

    [SerializeField] private GameObject cubePrefabs = null;
    [SerializeField] private Transform[] spawnPoints = null;
    [SerializeField] private PlayerInput playerInput = null;
    [SerializeField] private float moveSpeed = 1;

    public int ItemCount = 20;

    //������ Ǯ�� ���� ť
    Queue<GameObject> itemQueue = new Queue<GameObject>();

    //Ȱ��ȭ�Ǿ��ִ� ť���
    private List<GameObject> cubes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ItemCount; i++)
        {
            //������
            GameObject cube = Instantiate(cubePrefabs, Vector3.zero, Quaternion.identity);
            itemQueue.Enqueue(cube);
            cube.SetActive(false);
        }
        playerInput = FindObjectOfType<PlayerInput>();
        SpawnCube();
    }

    void SpawnCube()
    {
        for (int i = 0; i < spawnPoints.Length - 1; i++)
        {
            //ť���� ������
            GameObject cube = itemQueue.Dequeue();
            //Ȱ��ȭ��Ű��
            cube.SetActive(true);
            cubes.Add(cube);
            // �������� ��ġ�� ����ش�
            cube.transform.position = spawnPoints[i].transform.position;

            //�Ǿ� ���� ������Ʈ ����
            if (i == 0)
                del_FirstPresent(cube);
        }
    }

    //�ڸ��� ��ĭ�� �ű��
    public void ChangePosition()
    {
        List<GameObject> newCubes = new List<GameObject>();

        for (int i = 1; i < spawnPoints.Length - 1; i++)
        {
            // cubes[i].transform.Translate(Vector3.back);
            StartCoroutine(Co_PreBoxMove(cubes[i], i));
            newCubes.Add(cubes[i]);
        }
        //�Ǿ� ���� ������Ʈ ����
        del_FirstPresent(newCubes[0]);

        GameObject cube = itemQueue.Dequeue();
        cube.SetActive(true);
        cube.transform.position = spawnPoints[6].transform.position;
        cube.transform.rotation = Quaternion.identity;
        cubes = newCubes;
        cubes.Add(cube);
        StartCoroutine(Co_PreBoxMove(cube, 6));
    }

    public void ReCycle(GameObject present)
    {
        itemQueue.Enqueue(present);
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