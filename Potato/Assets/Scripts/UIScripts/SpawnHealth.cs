using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealth : MonoBehaviour
{
    public GameObject Heart;
    public GameObject EmptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("MaxHealth", 3);
        PlayerPrefs.SetInt("PlayerHealth", 3);
        PlayerPrefs.SetString("MakeHearts", "NO");
        SpawnHearts();
    }

    // Update is called once per frame
    void Update()
    {
        string checkMake = PlayerPrefs.GetString("MakeHearts");

        if (checkMake == "YES")
        {
            UpdateHearts();
        }
    }

    private void SpawnHearts()
    {
        int maxHealth = PlayerPrefs.GetInt("MaxHealth");
        int currHealth = PlayerPrefs.GetInt("PlayerHealth");
        int healthDifference = maxHealth - currHealth;

        float positionX = 0f;
        float positionY = Heart.transform.position.y;
        float positionZ = Heart.transform.position.z;

        for (int i = 0; i < currHealth; i++)
        {
            Vector3 newPosition = new Vector3(positionX, positionY, positionZ);

            if (i == 0)
            {
                Heart.SetActive(true);
            }

            GameObject newHeart = Instantiate(Heart, newPosition, Quaternion.identity, GameObject.FindGameObjectWithTag("HealthSpawn").GetComponent<RectTransform>().transform);
            Vector3 anchoredPos = new Vector3(positionX, 0f, 0f);
            newHeart.GetComponent<RectTransform>().anchoredPosition = anchoredPos;

            positionX += 50f;
        }

        for (int j = 0; j < healthDifference; j++)
        {
            Vector3 newPosition = new Vector3(positionX, positionY, positionZ);

            if (j == 0)
            {
                EmptyHeart.SetActive(true);
            }

            GameObject newHeart = Instantiate(EmptyHeart, newPosition, Quaternion.identity, GameObject.FindGameObjectWithTag("HealthSpawn").GetComponent<RectTransform>().transform);
            Vector3 anchoredPos = new Vector3(positionX, 0f, 0f);
            newHeart.GetComponent<RectTransform>().anchoredPosition = anchoredPos;

            positionX += 50f;
        }
    }

    private void UpdateHearts()
    {
        GameObject[] allHearts = GameObject.FindGameObjectsWithTag("Heart");

        foreach (GameObject heart in allHearts)
        {
            GameObject.Destroy(heart);
        }

        SpawnHearts();

        PlayerPrefs.SetString("MakeHearts", "NO");
    }
}
