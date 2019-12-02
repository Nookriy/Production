using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject goalPrefab;
    public static int tankSize = 5;
    static int numCubes = 10;
    public static GameObject[] allCubes = new GameObject[numCubes];
    public static Vector3 goalPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numCubes; i++)
        {
            //Where Cubes Spawn//
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize));
            allCubes[i] = (GameObject)Instantiate(cubePrefab, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 10000) < 50)
        {
            goalPos = new Vector3(Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize));
            goalPrefab.transform.position = goalPos;
        }
    }
}
