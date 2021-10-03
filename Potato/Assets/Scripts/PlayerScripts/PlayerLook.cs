using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float minVert = -75.0f;
    private float maxVert = 75.0f;
    private float rotationX = 0f;
    private float rotationY = 0f;
    private float rotationZ = 0f;
    private static float sensitivity = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float xMovement = -Input.gyro.rotationRateUnbiased.x;
        //float yMovement = -Input.gyro.rotationRateUnbiased.y;
        //float zMovement = -Input.gyro.rotationRateUnbiased.z;

        //transform.Rotate(xMovement, yMovement, zMovement);

        ////rotationX -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        //rotationX -= xMovement * sensitivity * Time.deltaTime;
        //rotationY -= yMovement * sensitivity * Time.deltaTime;
        //rotationZ -= zMovement * sensitivity * Time.deltaTime;

        ////rotationX = Mathf.Clamp(rotationX, minVert, maxVert);

        ////transform.localEulerAngles =
        ////    new Vector3(rotationX, transform.localEulerAngles.y, 0f);

        //transform.localEulerAngles =
        //    new Vector3(rotationX, rotationY, rotationZ);
    }
}
