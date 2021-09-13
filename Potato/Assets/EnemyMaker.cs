using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    public int maxEnemies;
    private int currEnemies = 0;
    public GameObject enemy;
    public GameObject rotateAround;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentEnemies", 0);
        rotateAround = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        currEnemies = PlayerPrefs.GetInt("CurrentEnemies");

        if (currEnemies == 0)
        {
            SpawnAllEnemies();
            PlayerPrefs.SetInt("CurrentEnemies", maxEnemies);
        }

        rotateAround = GameObject.Find("Player");
    }

    private void SpawnAllEnemies()
    {
        float[] acceptedPositions = new float[]{ -16f, -15f, -14f, -13f, -12f, -11f, -10f,
                                                  16f, 15f, 14f, 13f, 12f, 11f, 10f };

        for (int i = 0; i < maxEnemies; i++)
        {
            float randomX = acceptedPositions[Random.Range(0, acceptedPositions.Length)];
            float randomY = Random.Range(0, 5f);
            float randomZ = acceptedPositions[Random.Range(0, acceptedPositions.Length)];

            Vector3 position = new Vector3(randomX + rotateAround.transform.position.x, randomY,
                randomZ + rotateAround.transform.position.z);

            Instantiate(enemy, position, Quaternion.identity);
        }
    }
}
