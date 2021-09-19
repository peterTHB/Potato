using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("MaxEnemies", 3);
        //PlayerPrefs.SetInt("MaxTargets", 3);
    }

    //private void Awake()
    //{
    //    PlayerPrefs.SetInt("MaxEnemies", 3);
    //    PlayerPrefs.SetInt("MaxTargets", 3);
    //}

    // Update is called once per frame
    void Update()
    {
        // Check if player has died or not
        //if (PlayerPrefs.GetInt("PlayerHealth") == 0)
        //{
        //    SceneManager.LoadScene("FinishedScene");
        //}
    }

    private void TestHealth()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    int currHealth = PlayerPrefs.GetInt("PlayerHealth") + 1;
        //    PlayerPrefs.SetInt("PlayerHealth", currHealth);
        //    PlayerPrefs.SetString("MakeHearts", "YES");
        //}
        //else if (Input.GetKeyDown(KeyCode.K))
        //{
        //    int currHealth = PlayerPrefs.GetInt("PlayerHealth") - 1;
        //    PlayerPrefs.SetInt("PlayerHealth", currHealth);
        //    PlayerPrefs.SetString("MakeHearts", "YES");
        //}
    }
}
