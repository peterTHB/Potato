using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    //private int maxEnemies = 0;
    //private int currEnemies = 0;
    public GameObject enemy;
    private GameObject rotateAround;

    float[] acceptedPositions = new float[]{ -16f, -15f, -14f, -13f, -12f, -11f, -10f,
                                                  16f, 15f, 14f, 13f, 12f, 11f, 10f };

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("InitialEnemyCount", 3);
        PlayerPrefs.SetInt("CurrentEnemies", 0);
        rotateAround = GameObject.Find("Player");
        SpawnAllEnemies();
        InvokeRepeating("SpawnEnemy", 5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        rotateAround = GameObject.Find("Player");
    }

    private void SpawnAllEnemies()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("InitialEnemyCount"); i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float randomX = acceptedPositions[Random.Range(0, acceptedPositions.Length)];
        float randomY = Random.Range(0, 5f);
        float randomZ = acceptedPositions[Random.Range(0, acceptedPositions.Length)];

        Vector3 position = new Vector3(randomX + rotateAround.transform.position.x, randomY,
            randomZ + rotateAround.transform.position.z);

        Instantiate(enemy, position, Quaternion.identity);
        int currEnemies = PlayerPrefs.GetInt("CurrentEnemies") + 1;
        PlayerPrefs.SetInt("CurrentEnemies", currEnemies);
    }
}
