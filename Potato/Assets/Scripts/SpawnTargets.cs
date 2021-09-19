using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
    //private int maxTargets = 0;
    //private int currTargets = 0;
    public GameObject target;
    public GameObject rotateAround;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentTargets", 0);
        rotateAround = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("CurrentTargets") == 0)
        {
            SpawnAllTargets();
            PlayerPrefs.SetInt("CurrentTargets", PlayerPrefs.GetInt("MaxTargets"));
        }

        rotateAround = GameObject.Find("Player");
    }

    private void SpawnAllTargets()
    {
        float[] acceptedPositions = new float[]{ -21f, -20f, -19f, -18f, -17f,
                                                  21f, 20f, 19f, 18f, 17f };
        for (int i = 0; i < PlayerPrefs.GetInt("MaxTargets"); i++)
        {
            float randomX = acceptedPositions[Random.Range(0, acceptedPositions.Length)];
            float randomY = Random.Range(0, 5f);
            float randomZ = acceptedPositions[Random.Range(0, acceptedPositions.Length)];

            Vector3 position = new Vector3(randomX + rotateAround.transform.position.x, randomY,
                randomZ + rotateAround.transform.position.z);

            Instantiate(target, position, Quaternion.identity);
        }
    }
}
