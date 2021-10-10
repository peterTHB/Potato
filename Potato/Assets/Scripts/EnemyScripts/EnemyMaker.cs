using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    public GameObject[] enemys;
    private GameObject rotateAround;

    float[] acceptedPositions = new float[]{ -16f, -15f, -14f, -13f, -12f, -11f, -10f,
                                                  16f, 15f, 14f, 13f, 12f, 11f, 10f };

    private bool spawnEnemy = true;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("InitialEnemyCount", 3);
        PlayerPrefs.SetInt("CurrentEnemies", 0);
        rotateAround = GameObject.Find("Player");
        StartCoroutine(SpawnEnemyIEnum());
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("CurrentTargets") == 0)
        {
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] allParentEnemies = GameObject.FindGameObjectsWithTag("EnemyParent");

            for (int i = 0; i < allEnemies.Length; i++)
            {
                GameObject.Destroy(allEnemies[i]);
                GameObject.Destroy(allParentEnemies[i]);
            }

            int newLevelAmount = PlayerPrefs.GetInt("LevelCount") + 1;

            PlayerPrefs.SetInt("LevelCount", newLevelAmount);
            PlayerPrefs.SetInt("CurrentEnemies", 0);

            PlayerPrefs.SetInt("MaxEnemyCount", PlayerPrefs.GetInt("LevelCount") * PlayerPrefs.GetInt("MaxEnemyCount"));
        }

        if (PlayerPrefs.GetInt("CurrentEnemies") < PlayerPrefs.GetInt("MaxEnemyCount"))
        {
            spawnEnemy = true;
        } 
        else
        {
            spawnEnemy = false;
        }

        rotateAround = GameObject.Find("Player");
    }

    private IEnumerator SpawnEnemyIEnum()
    {
        while (spawnEnemy)
        {
            yield return new WaitForSeconds(5f);

            float randomX = acceptedPositions[Random.Range(0, acceptedPositions.Length)];
            float randomY = Random.Range(0, 5f);
            float randomZ = acceptedPositions[Random.Range(0, acceptedPositions.Length)];

            Vector3 position = new Vector3(randomX + rotateAround.transform.position.x, randomY,
                randomZ + rotateAround.transform.position.z);

            GameObject currEnemy = enemys[Random.Range(0, enemys.Length)];

            Instantiate(currEnemy, position, Quaternion.identity);
            int currEnemies = PlayerPrefs.GetInt("CurrentEnemies") + 1;
            PlayerPrefs.SetInt("CurrentEnemies", currEnemies);
        }
    }
}
