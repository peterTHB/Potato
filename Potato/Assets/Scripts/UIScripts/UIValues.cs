using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIValues : MonoBehaviour
{
    public TextMeshProUGUI currEnemies;
    public TextMeshProUGUI currTargets;
    public TextMeshProUGUI ammoCount;
    public TextMeshProUGUI levelCount;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("AmmoCount", 0);
        currEnemies.text = PlayerPrefs.GetInt("CurrentEnemies").ToString();
        currTargets.text = PlayerPrefs.GetInt("CurrentTargets").ToString();
        ammoCount.text = PlayerPrefs.GetInt("AmmoCount").ToString();
        levelCount.text = PlayerPrefs.GetInt("LevelCount").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currEnemies.text = PlayerPrefs.GetInt("CurrentEnemies").ToString();
        currTargets.text = PlayerPrefs.GetInt("CurrentTargets").ToString();
        ammoCount.text = PlayerPrefs.GetInt("AmmoCount").ToString();
        levelCount.text = PlayerPrefs.GetInt("LevelCount").ToString();
    }
}
