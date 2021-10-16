using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillEnemyMovement : MonoBehaviour
{
    public enum ACTION { MOVEUP, MOVEDOWN, DRILL }

    //stats
    public float rotateAroundSpeed = 30;
    public float yAxisRange = 20f;
    private float moveSpeed = 4f;
    private float drillMoveSpeed = 3f;

    private GameObject player;
    private Animator animator;
    public ParticleSystem sparkParticals;

    //movement
    private ACTION currAction = ACTION.MOVEDOWN;
    private float lastChangeDirection;
    public float baseActionChangeDelay = 3f;
    public float drillDelay = 8f;
    private float drillTimeStart;
    private float drillMaxDuration = 5f;
    private float adjustedActionDelay;
    private int randomDirection;

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
            randomDirection = Random.Range(0, 2) == 0 ? -1 : 1;
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
        float step = 100 * Time.deltaTime; // calculate distance to move
        Quaternion dirToPlayer = Quaternion.LookRotation(player.transform.position - this.transform.position);

        switch (currAction)
        {
            case ACTION.MOVEDOWN:
                animator.SetBool("IsAttacking", false);
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
                transform.RotateAround(player.transform.position, new Vector3(0, randomDirection, 0), rotateAroundSpeed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, dirToPlayer, step);
                break;
            case ACTION.MOVEUP:
                animator.SetBool("IsAttacking", false);
                transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                transform.RotateAround(player.transform.position, new Vector3(0, randomDirection, 0), rotateAroundSpeed * Time.deltaTime);

                //face move direction
                transform.rotation = Quaternion.RotateTowards(transform.rotation, dirToPlayer, step);
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

            if (!animator.GetBool("IsAttacking"))
            {
                transform.LookAt(player.transform);
                drillTimeStart = Time.time;
            }

            animator.SetBool("IsAttacking", true);
            transform.Translate(0,0,Time.deltaTime * drillMoveSpeed);

            //check for drill max time
            if (drillTimeStart + drillMaxDuration < Time.time)
            {
                lastChangeDirection = Time.time - adjustedActionDelay;
            }else
            {
                lastChangeDirection = Time.time;
            }
        }
        else
        {
            //dont let attack cancel
            lastChangeDirection = Time.time;

            float step = 10 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player.transform.position.y, transform.position.z), step);

            float rotationStep = 100 * Time.deltaTime; // calculate distance to move
            Quaternion rotationTarget = Quaternion.LookRotation(player.transform.position - this.transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget, rotationStep);
        }
    }
}
