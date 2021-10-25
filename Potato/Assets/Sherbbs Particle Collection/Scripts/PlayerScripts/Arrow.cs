using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float arrowSpeed;
    public Bow bow;
    
    public GameObject parent;

    private bool fired = false;
    private Rigidbody rb;
    private float startTime;
    private float timer = 0.0f;
    private float fullDrawTime = 5.0f;
    private bool drawing = false;
    private const float ARROW_SPEED_RAT = 3.0f;

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

        //rb.useGravity = true;
        float drawTime = Time.time - bow.GetTimer();
        print(drawTime);
        if (drawTime >= fullDrawTime)
        {
            drawTime = fullDrawTime;
        }
        arrowSpeed = drawTime * ARROW_SPEED_RAT;
        print(arrowSpeed);
        rb.AddForce(transform.right * arrowSpeed);
        transform.parent = null;
        arrowSpeed = 0.0f;
        drawing = false;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            collision.gameObject.GetComponent<TargetScript>().GotHit();
            Destroy(gameObject, 0.1f);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().GotHit();
            Destroy(gameObject, 0.1f);
        }
        else
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
        }


    }

  

    public void StartDraw()
    {
        drawing = true;
        print("Start draw");
       
        bow.SetTimer(Time.time);
        

    }

    public void FinishDraw()
    {
        
    }

    public Vector3 GetForce()
    {
        return transform.right * ( Time.time - timer );

    }
}
