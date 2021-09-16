using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    public float rotationSpeed;
    private GameObject pivotObject;

    // Start is called before the first frame update
    void Start()
    {
        pivotObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
        pivotObject = GameObject.Find("Player");
    }
}
