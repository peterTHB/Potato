using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetSettingText : MonoBehaviour
{
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI viewingText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        difficultyText.text = PlayerPrefs.GetString("DifficultyText");
        viewingText.text = PlayerPrefs.GetString("ViewingMode");
    }
}
