using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillEnemyMovement : MonoBehaviour
{
    public enum ACTION { MOVEUP, MOVEDOWN, IDLE, DRILL }

    public float rotateAroundSpeed = 30;
    public float yAxisRange = 20f;
    private float moveSpeed = 4f;
    private float drillMoveSpeed = 3f;

    private GameObject player;
    private Animator animator;
    public ParticleSystem sparkParticals;

    private ACTION currAction = ACTION.IDLE;
    private float lastChangeDirection;
    public float baseActionChangeDelay = 3f;
    public float drillDelay = 8f;
    private float lastTimeDrill;
    private float adjustedActionDelay;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
                animator.SetBool("IsAttacking", false);
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
                transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), rotateAroundSpeed * Time.deltaTime);
                break;
            case ACTION.MOVEUP:
                animator.SetBool("IsAttacking", false);
                transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), rotateAroundSpeed * Time.deltaTime);
                break;
            case ACTION.DRILL:
                DrillAtPlayer();
                break;
            default:
                break;
        }
    }

    private void DrillAtPlayer()
    {
        float yAxisDiff = Mathf.Abs(transform.position.y - player.transform.position.y);
        //if y axis is similar
        if (yAxisDiff < 0.3f)
        {
            animator.SetBool("IsAttacking", true);
            transform.LookAt(player.transform);
            transform.Translate(0,0,Time.deltaTime * drillMoveSpeed);
        }
        else
        {
            //dont let attack cancel
            lastChangeDirection = Time.time;

            float step = 10 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player.transform.position.y, transform.position.z), step);
            Vector3.RotateTowards(transform.position, player.transform.position, step, 0);
        }
    }
}
