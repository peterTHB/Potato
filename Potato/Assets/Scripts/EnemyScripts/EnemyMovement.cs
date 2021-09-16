using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum ACTION { MOVEUP, MOVEDOWN, IDLE, FIRE }

    public float rotateAroundSpeed = 30;
    public float yAxisRange = 20f;
    private float moveSpeed = 4f;
    private float rotationSpeed = 50;

    public GameObject model;

    private GameObject pivotObject;

    private ACTION currAction = ACTION.IDLE;
    private float lastChangeDirection;
    public float baseActionChangeDelay = 3f;
    private float adjustedActionDelay;

    // Start is called before the first frame update
    void Start()
    {
        pivotObject = GameObject.Find("Player");
        adjustedActionDelay = baseActionChangeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        model.transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

        if (lastChangeDirection + adjustedActionDelay <= Time.time)
        {
            //make sure new action is not same as old
            ACTION newAction;
            do
            {
                newAction = (ACTION)Random.Range(0, System.Enum.GetNames(typeof(ACTION)).Length);
            } while (newAction == currAction);
            adjustedActionDelay = baseActionChangeDelay + Random.Range(-1, 1);
            currAction = newAction;
            lastChangeDirection = Time.time;
        }

        if (transform.position.y >= yAxisRange)
        {
            currAction = ACTION.MOVEDOWN;
            lastChangeDirection = Time.time;
        }
        else if (transform.position.y <= 0)
        {
            currAction = ACTION.MOVEUP;
            lastChangeDirection = Time.time;
        }

        HandleAction();
    }

    private void HandleAction()
    {
        switch (currAction)
        {
            case ACTION.MOVEDOWN:
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
                transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotateAroundSpeed * Time.deltaTime);
                break;
            case ACTION.MOVEUP:
                transform.Translate(0,moveSpeed * Time.deltaTime,0);
                transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotateAroundSpeed * Time.deltaTime);
                break;
            case ACTION.FIRE:
                ShootAtPlayer();
                break;
            default:
                break;
        }
    }

    private void ShootAtPlayer()
    {
        //To be implemented
    }
}
