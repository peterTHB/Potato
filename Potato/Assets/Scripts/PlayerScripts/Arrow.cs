using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Range(0.0f, 500.0f)]
    public float arrowSpeed = 50.0f;

    public GameObject parent;

    private bool fired = false;
    private Rigidbody rb;
    private float startTime;

    void Start()
    {
        GetComponent<Collider>().enabled = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(fired)
        {
            GetComponent<Collider>().enabled = true;
            // Destroy arrow after 10 s of not hitting a target 
            if (Time.time - startTime > 10.0f)
            {
                Destroy(gameObject);
            }
        }

    }

    public void Fire()
    {
        startTime = Time.time;
        fired = true;
        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.AddForce(transform.right * arrowSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Destroy(collision.gameObject, 0.1f);
            Destroy(gameObject, 0.1f);
        }
        else
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
        }


    }
}
