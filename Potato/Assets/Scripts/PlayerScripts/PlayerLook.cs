using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float minVert = -75.0f;
    private float maxVert = 75.0f;
    private float rotationX = 0f;
    private static float sensitivity = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, minVert, maxVert);

        transform.localEulerAngles =
            new Vector3(rotationX, transform.localEulerAngles.y, 0f);
    }
}
