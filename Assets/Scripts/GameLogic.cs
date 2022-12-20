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
        SpawnCube();
    }

    void SpawnCube()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
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
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            // cubes[i].transform.Translate(Vector3.back);
            StartCoroutine(Co_PreBoxMove(cubes[i]));
        }
        //�Ǿ� ���� ������Ʈ ����
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