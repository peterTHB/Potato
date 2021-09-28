using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillEnemyHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currHealth = PlayerPrefs.GetInt("PlayerHealth") - 1;
            PlayerPrefs.SetInt("PlayerHealth", currHealth);
            PlayerPrefs.SetString("MakeHearts", "YES");

        }
    }
}
