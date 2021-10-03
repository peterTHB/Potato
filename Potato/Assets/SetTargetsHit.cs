using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTargetsHit : MonoBehaviour
{

    public TextMeshProUGUI targetsHit;
        
    // Start is called before the first frame update
    void Start()
    {
        targetsHit.text = PlayerPrefs.GetInt("PlayerTargetsHit").ToString();
    }
}
