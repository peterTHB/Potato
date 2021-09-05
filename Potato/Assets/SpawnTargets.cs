using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
    public int maxTargets;
    private int currTargets = 0;
    public GameObject target;
    public GameObject platform;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentTargets", 0);
    }

    // Update is called once per frame
    void Update()
    {
        currTargets = PlayerPrefs.GetInt("CurrentTargets");

        if (currTargets == 0)
        {
            SpawnAllTargets();
            PlayerPrefs.SetInt("CurrentTargets", maxTargets);
        }
    }

    private void SpawnAllTargets()
    {
        float[] acceptedPositions = new float[]{ -16f, -15f, -14f, -13f, -12f, -11f, -10f, 
                                                  16f, 15f, 14f, 13f, 12f, 11f, 10f };

        for (int i = 0; i < maxTargets; i++)
        {
            float randomX = acceptedPositions[Random.Range(0, acceptedPositions.Length)];
            float randomY = Random.Range(0, 5f);
            float randomZ = acceptedPositions[Random.Range(0, acceptedPositions.Length)];

            Vector3 position = new Vector3(randomX + platform.transform.position.x, randomY, 
                randomZ + platform.transform.position.z);

            Instantiate(target, position, Quaternion.identity);
        }
    }
}
