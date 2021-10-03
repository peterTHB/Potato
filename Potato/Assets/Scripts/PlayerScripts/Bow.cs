using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private bool allowed_to_shoot = true;
    private float cooldown = 2.0f;
    private const float COOLDOWN_TIMER = 2.0f;
    
    private Vector3 left_pos = new Vector3(-0.1f, 0.3f, 0.5f);
    private Vector3 right_pos = new Vector3(0.2f, 0.3f, 0.5f);

    private Vector3 left_rot = new Vector3(0f, -135f, 90f);
    private Vector3 right_rot = new Vector3(0f, -45f, 90f);

    private Vector3 right_focus_pos = new Vector3(0.15f, 0.3f, 0.15f);
    private Vector3 left_focus_pos = new Vector3(0.3f, 0.3f, 0.15f);
    private Vector3 focus_rot = new Vector3(0f, -90f, 90f);

    public GameObject arrow_obj;


    void Start()
    {

    }

    void Update()
    {
        //Fire an arrow 
        if (PlayerPrefs.GetString("Shooting").Equals("Yes") && allowed_to_shoot)
        {
            allowed_to_shoot = false;
            GameObject newArrow = Instantiate(arrow_obj, transform);
            newArrow.GetComponent<Arrow>().Fire();

            arrow_obj.SetActive(false);
            cooldown -= Time.deltaTime;

            PlayerPrefs.SetString("Shooting", "No");
        }

        // Update current cooldown when waiting to reload 
        if (cooldown != COOLDOWN_TIMER)
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0.0f)
        {
            allowed_to_shoot = true;
            // spawn a new arrow
            arrow_obj.SetActive(true);
            cooldown = 2.0f;
        }
    }
}
